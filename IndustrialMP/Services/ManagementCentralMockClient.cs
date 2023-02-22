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
                    Text = "Moist Sensor"
                },
                new Item()
                {
                    Text = "Excavator Top-sensor"
                },
                new Item()
                {
                    Text = "3D printer",
                    Completed = true
                },
                new Item()
                {
                    Text = "AR rebuild"
                }
            };
        }
        
    }
}
