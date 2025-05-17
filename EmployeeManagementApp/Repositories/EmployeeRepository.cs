using EmployeeManagementApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace EmployeeManagementApp.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private static List<Employee> _employees = new List<Employee>
        {
            new Employee
            {
                Id = 1,
                Name = "Budi Santoso",
                Position = "Software Developer",
                Department = "IT",
                JoinDate = new DateTime(2020, 5, 15),
                Salary = 8000000
            },
            new Employee
            {
                Id = 2,
                Name = "Siti Rahayu",
                Position = "UI/UX Designer",
                Department = "Design",
                JoinDate = new DateTime(2021, 3, 10),
                Salary = 7500000
            },
            new Employee
            {
                Id = 3,
                Name = "Ahmad Hidayat",
                Position = "Project Manager",
                Department = "Management",
                JoinDate = new DateTime(2019, 11, 22),
                Salary = 12000000
            },
            new Employee
            {
                Id = 4,
                Name = "Dewi Lestari",
                Position = "Business Analyst",
                Department = "Business",
                JoinDate = new DateTime(2022, 1, 5),
                Salary = 9000000
            },
            new Employee
            {
                Id = 5,
                Name = "Eko Prasetyo",
                Position = "Database Administrator",
                Department = "IT",
                JoinDate = new DateTime(2020, 8, 17),
                Salary = 8500000
            }
        };

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await Task.FromResult(_employees);
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            var employee = _employees.FirstOrDefault(e => e.Id == id);
            return await Task.FromResult(employee);
        }
    }
}
