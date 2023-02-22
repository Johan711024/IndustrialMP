using IndustrialMP.Shared;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace IndustrialMP.Services
{
    public class ManagementCentralClient : IManagementCentralClient
    {
        private readonly HttpClient httpClient;

        public ManagementCentralClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
           // this.httpClient.BaseAddress = new Uri("https://localhost:7297");
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<Item>?> GetAsync()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<Item>>("api/devices");
        }



    }
}
