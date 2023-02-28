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
                    Online = true
                },
                new Item()
                {
                    Text = "AR rebuild"
                }
            };
        }

        Task<Item?> IManagementCentralClient.DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Item>?> IManagementCentralClient.GetAsync()
        {
            throw new NotImplementedException();
        }

        Task<Item?> IManagementCentralClient.PostAsync(CreateItem createItem)
        {
            throw new NotImplementedException();
        }

        Task<bool> IManagementCentralClient.PutAsync(Item item)
        {
            throw new NotImplementedException();
        }

        Task<bool> IManagementCentralClient.RemoveAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
