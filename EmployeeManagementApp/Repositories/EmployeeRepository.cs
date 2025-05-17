using EmployeeManagementApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace EmployeeManagementApp.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            var employees = new List<Employee>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("GetEmployee", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var employee = new Employee
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Position = reader["Position"].ToString(),
                            Department = reader["Department"].ToString(),
                            JoinDate = Convert.ToDateTime(reader["JoinDate"]),
                            Salary = Convert.ToDecimal(reader["Salary"])
                        };

                        employees.Add(employee);
                    }
                }
            }

            return await Task.FromResult(employees);
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            Employee empl = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                var query = "SELECT Id, Name, Position, Department, JoinDate, Salary FROM Employee WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        empl = new Employee
                        {
                            Id = (int)reader["Id"],
                            Name = reader["Name"].ToString(),
                            Position = reader["Position"].ToString(),
                            Department = reader["Department"].ToString(),
                            JoinDate = Convert.ToDateTime(reader["JoinDate"]),
                            Salary = Convert.ToDecimal(reader["Salary"])
                        };
                    }
                }
            }
            return await Task.FromResult(empl);
        }
    }
}
