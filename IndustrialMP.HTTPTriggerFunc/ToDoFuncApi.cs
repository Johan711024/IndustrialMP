using System.Net;
using System.Text.Json;
using Azure.Data.Tables;
using Grpc.Core;
using IndustrialMP.HTTPTriggerFunc.Entities;
using IndustrialMP.HTTPTriggerFunc.Extensions;
using IndustrialMP.HTTPTriggerFunc.Helpers;
using IndustrialMP.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace IndustrialMP.HTTPTriggerFunc
{
    public class ToDoFuncApi
    {
        private readonly ILogger _logger;

        public List<Item> DeletedItemsQueu { get; set; } = new List<Item>();

        public ToDoFuncApi(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ToDoFuncApi>();
        }



        [Function("Add Item")]
        public async Task<HttpResponseData> Create([HttpTrigger(AuthorizationLevel.Function, "post", Route = "devices")] HttpRequestData req)
        {
            _logger.LogInformation("Create new todo item");

            var tableClient = GetTableClient();
            var response = req.CreateResponse();

            //var stream = await new StreamReader(req.Body).ReadToEndAsync();
            var createdItem = JsonSerializer.Deserialize<CreateItem>(req.Body, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            if (createdItem is null || string.IsNullOrWhiteSpace(createdItem.Text))
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            var item = new Item
            {
                Text = createdItem.Text,
            };

            await tableClient.CreateIfNotExistsAsync();
            await tableClient.AddEntityAsync(item.ToTableEntity());

            await response.WriteAsJsonAsync(item);
            response.StatusCode=HttpStatusCode.Created;

            //response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            //response.WriteString("Welcome to Azure Functions!");

            return response;
        }




        [Function("Edit Item")]
        public async Task<HttpResponseData> Edit(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "devices/{id}")] HttpRequestData req,
            [FromRoute] string id)
        {
            _logger.LogInformation("Edit item");

            var tableClient = GetTableClient();
            var response = req.CreateResponse();

            var editItem = await JsonSerializer.DeserializeAsync<Item>(req.Body, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            if (editItem is null || string.IsNullOrWhiteSpace(editItem.Text) || editItem.Id != id)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            var found = await tableClient.GetEntityIfExistsAsync<ItemTableEntity>(TableNames.PartionKey, id);

            if (!found.HasValue) 
            { 
                response.StatusCode = HttpStatusCode.NotFound;
                return response;    
            }

            await tableClient.UpdateEntityAsync((ItemTableEntity?)editItem.ToTableEntity(), Azure.ETag.All);

            response.StatusCode = HttpStatusCode.NoContent;

            return response;
        }



        [Function("Delete Item")]
        
        public async Task<HttpResponseData> Delete(
            [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "devices/{id}")] HttpRequestData req,
            [FromRoute] string id)
        {
            _logger.LogInformation("Delete item");

            var tableClient = GetTableClient();
            var response = req.CreateResponse();

            var itemEntity = tableClient.QueryAsync<ItemTableEntity>(i => i.RowKey == id && i.PartitionKey == TableNames.PartionKey);

            //if(itemEntity == null)
            //{
            //    response.StatusCode = HttpStatusCode.NotFound;
            //}

            //var item = Mapper.ToItem(itemEntity);


            var isOk = await tableClient.DeleteEntityAsync(TableNames.PartionKey, id);

            if (isOk.Status == StatusCodes.Status404NotFound)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }

            //DeletedItemsQueu.Add(item);



            response.StatusCode = HttpStatusCode.NoContent;

            //response = item;

            return response;
        }


        [Function("Get Items")]
        public async Task<HttpResponseData> Get(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "devices")] HttpRequestData req,
            [TableInput(TableNames.TableName, TableNames.PartionKey, Connection = "AzureWebJobsStorage")] IEnumerable<ItemTableEntity> tableEntities)
        {
            _logger.LogInformation("Get item");

            var response = req.CreateResponse();
            var items = tableEntities.Select(Mapper.ToItem);
            await response.WriteAsJsonAsync(items);

            return response;
        }


        [Function("Timer")]
        [QueueOutput("RemovedDevices", Connection = "AzureWebJobsStorage")]
        public async Task<IEnumerable<ItemTableEntity>> Timer(
          [TimerTrigger("0 */1 * * * *")] TimerInfo timerInfo,
          FunctionContext context)
        {

            _logger.LogInformation($"Timer excecuted. Next timer schedule = {timerInfo.ScheduleStatus?.Next}");

            var tableClient = GetTableClient();

            var res = tableClient.QueryAsync<ItemTableEntity>(i => i.Online == true);

            var result = new List<ItemTableEntity>();

            await foreach(var item in res )
            {
                await tableClient.DeleteEntityAsync(item.PartitionKey, item.RowKey);
                result.Add(item);
                _logger.LogInformation($"The {item.Text} is removed and added to the queue");
            }

            //result är kön RemovedDevices
            return result;

        }

        [Function("FromQueue")]
        [BlobOutput("removed/{rand-guid}")]
        public string FromQueue(
           [QueueTrigger("RemovedDevices", Connection = "AzureWebJobsStorage")] ItemTableEntity itemTable)
        {
            _logger.LogInformation($"C# Queue trigger function processed");

            //det borttagna elementet i kön sparas i blob
            return $"Item: {itemTable.Text} was processed at: {DateTime.Now}.txt";

        }


        private TableClient GetTableClient()
        {
            var connectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            return new TableClient(connectionString, TableNames.TableName);
        }

    }
}
