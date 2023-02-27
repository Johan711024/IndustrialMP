using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace IndustrialMP.Func
{
    public class MySignalRFunction : SignalRFunction
    {
        public MySignalRFunction(
            ILoggerFactory loggerFactory,
            IAzureSignalRSender azureSignalRSender)
            : base(loggerFactory, azureSignalRSender)
        {
        }

        [Function("negotiate")]
        public SignalRConnectionInfo GetSignalRInfo(
            [SignalRTrigger] SignalRConnectionInfo connectionInfo)
        {
            return connectionInfo;
        }
        protected override async Task OnConnectedAsync(SignalRConnectionInfo connectionInfo, ClaimsPrincipal user)
        {
            await base.OnConnectedAsync(connectionInfo, user);

            // Handle client connection here
        }

        protected override async Task OnDisconnectedAsync(SignalRConnectionInfo connectionInfo, Exception exception, ClaimsPrincipal user)
        {
            await base.OnDisconnectedAsync(connectionInfo, exception, user);

            // Handle client disconnection here
        }

    }
}
