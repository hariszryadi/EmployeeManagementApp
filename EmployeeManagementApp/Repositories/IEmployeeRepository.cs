using EmployeeManagementApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagementApp.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(int id);
    }
}
