using CoffeeShopAPI.Data;

namespace CoffeeShopAPI.IRepository
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<Employee?> GetEmployee(string id);
        Task<bool> Exists(string id);
        Task DeleteAsync(string id); 
    }
}
