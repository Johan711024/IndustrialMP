using IndustrialMP.Shared;

namespace IndustrialMP.Services
{
    public interface IManagementCentralClient
    {
        Task<IEnumerable<Item>> GetAsync();
    }
}