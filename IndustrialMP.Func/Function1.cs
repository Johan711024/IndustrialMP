using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Extensions.Logging;

namespace IndustrialMP.Func
{
    public class Function1
    {
        private readonly ILogger _logger;

        public Function1(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("negotiate");
        }

        //[Function("negotiate")]
        //public HttpResponseData Negotiate(
        //    [Microsoft.Azure.Functions.Worker.HttpTrigger(Microsoft.Azure.Functions.Worker.AuthorizationLevel.Function, "post")] HttpRequestData req,
        //    [SignalRConnectionInfoInput(HubName = "HubValue")] MyConnectionInfo connectionInfo)
        //{
        //    _logger.LogInformation($"SignalR Connection URL = '{connectionInfo.Url}'");

        //    var response = req.CreateResponse(HttpStatusCode.OK);
        //    response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
        //    response.WriteString($"Connection URL = '{connectionInfo.Url}'");

        //    return response;
        //}

        [FunctionName("negotiate")]
        public static SignalRConnectionInfo GetSignalRInfo(
    [Microsoft.Azure.WebJobs.HttpTrigger(Microsoft.Azure.WebJobs.Extensions.Http.AuthorizationLevel.Anonymous)] HttpRequest req,
    [SignalRConnectionInfo(HubName = "ChatHub")] SignalRConnectionInfo connectionInfo)
        {
            return connectionInfo;
        }


    }

    public class MyConnectionInfo
    {
        public string Url { get; set; }

        public string AccessToken { get; set; }
    }
}