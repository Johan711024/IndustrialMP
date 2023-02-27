using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR.Client;

namespace IndustrialMP.Func
{
    

    public class MySignalRClient 
    {
        public MySignalRClient(HubConnection hubConnection) : base(hubConnection)
        {
        }

        public async Task SendMessageAsync(string message)
        {
            await HubConnection.SendAsync("SendMessage", message);

            
        }
    }

}
