using EmployeeManagementApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagementApp.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(int id);
        byte[] ExportToExcel();
        byte[] ExportToPdf();
    }
}
