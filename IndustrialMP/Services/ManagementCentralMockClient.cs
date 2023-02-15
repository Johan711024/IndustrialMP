using IndustrialMP.Shared;

namespace IndustrialMP.Services
{

    public class ManagementCentralMockClient : IManagementCentralClient
    {
        private readonly HttpClient httpClient;

        public ManagementCentralMockClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<IEnumerable<Item>> GetAsync()
        {
            return new List<Item>()
            {
                new Item()
                {
                    Text = "Banan"
                },
                new Item()
                {
                    Text = "Apelsin"
                },
                new Item()
                {
                    Text = "Mjölk",
                    Completed = true
                },
                new Item()
                {
                    Text = "Bröd"
                }
            };
        }
    }
}
