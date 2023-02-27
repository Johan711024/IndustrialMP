using IndustrialMP.Shared;

namespace IndustrialMP.Services
{
    public interface IManagementCentralClient
    {
        Task<Item?> DeleteAsync(string id);
        Task<IEnumerable<Item>?> GetAsync();
        Task<Item?> PostAsync(CreateItem createItem);
        Task<bool> PutAsync(Item item);
        Task<bool> RemoveAsync(string id);
    }
}