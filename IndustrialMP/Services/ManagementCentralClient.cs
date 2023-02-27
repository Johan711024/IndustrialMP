using IndustrialMP.Shared;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace IndustrialMP.Services
{
    public class ManagementCentralClient : IManagementCentralClient
    {
        private readonly HttpClient httpClient;

        public ManagementCentralClient(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;

            var baseAddress = configuration.GetSection("BaseAddress").Value;

            ArgumentNullException.ThrowIfNull(baseAddress, nameof(configuration));

            this.httpClient.BaseAddress = new Uri(baseAddress);
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<Item>?> GetAsync()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<Item>>("api/devices");
        }

        //funkar ej ännu
        //public async Task<Item?> GetAsync(string id)
        //{
        //    return await httpClient.GetFromJsonAsync<Item>("api/devices/" + id);
        //}

        public async Task<Item?> PostAsync(CreateItem createItem)
        {
            var response = await httpClient.PostAsJsonAsync("api/devices", createItem);
            return response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<Item>() : null;
        }


        public async Task<Item?> DeleteAsync(string id)
        {
            return await httpClient.DeleteFromJsonAsync<Item?>("api/devices/" + id);

            //returnerar bool istället
            //return (await httpClient.DeleteAsync($"api/devices/{id}")).IsSuccessStatusCode;

        }

        public async Task<bool> RemoveAsync(string id)
        {
            return (await httpClient.DeleteAsync($"api/devices/{id}")).IsSuccessStatusCode;
        }


        public async Task<bool> PutAsync(Item item)
        {
            return (await httpClient.PutAsJsonAsync($"api/devices/{item.Id}", item)).IsSuccessStatusCode;
        }


    }
}
