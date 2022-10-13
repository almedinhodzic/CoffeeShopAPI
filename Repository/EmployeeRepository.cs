using CoffeeShopAPI.Data;
using CoffeeShopAPI.IRepository;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShopAPI.Repository
{
    public class EmployeeRepository : Repository<Employee>,IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;
        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task DeleteAsync(string id)
        {
            var employee = await GetEmployee(id);
            if (employee != null)
            {
                _context.Set<Employee>().Remove(employee);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> Exists(string id)
        {
            var employee = await _context.Set<Employee>().FindAsync(id);
            if (employee != null)
            {
                _context.Entry(employee).State = EntityState.Detached;
            }
            return employee != null;
        }

        public async Task<Employee?> GetEmployee(string id)
        {
            var employee = await _context.Set<Employee>().FindAsync(id);
            if (employee == null)
            {
                return null;
            }

            return employee;
        }
    }
}
