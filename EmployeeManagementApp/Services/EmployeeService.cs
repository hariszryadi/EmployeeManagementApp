using EmployeeManagementApp.Models;
using EmployeeManagementApp.Repositories;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System;

namespace EmployeeManagementApp.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _employeeRepository.GetAllEmployeesAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _employeeRepository.GetEmployeeByIdAsync(id);
        }

        public byte[] ExportToExcel()
        {
            var employees = _employeeRepository.GetAllEmployeesAsync().Result;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Daftar Pegawai");

                // Header
                worksheet.Cells[1, 1].Value = "ID";
                worksheet.Cells[1, 2].Value = "Nama Pegawai";
                worksheet.Cells[1, 3].Value = "Jabatan";
                worksheet.Cells[1, 4].Value = "Departemen";
                worksheet.Cells[1, 5].Value = "Tanggal Bergabung";
                worksheet.Cells[1, 6].Value = "Gaji";

                // Style header
                using (var range = worksheet.Cells[1, 1, 1, 6])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                    range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                // Data
                int row = 2;
                foreach (var employee in employees)
                {
                    worksheet.Cells[row, 1].Value = employee.Id;
                    worksheet.Cells[row, 2].Value = employee.Name;
                    worksheet.Cells[row, 3].Value = employee.Position;
                    worksheet.Cells[row, 4].Value = employee.Department;
                    worksheet.Cells[row, 5].Value = employee.JoinDate.ToShortDateString();
                    worksheet.Cells[row, 6].Value = employee.Salary;

                    // Format salary as currency
                    worksheet.Cells[row, 6].Style.Numberformat.Format = "#,##0";
                    row++;
                }

                // Adjust column width
                worksheet.Cells.AutoFitColumns();

                // Add border to all cells with data
                using (var range = worksheet.Cells[1, 1, row - 1, 6])
                {
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                }

                return package.GetAsByteArray();
            }
        }

        public byte[] ExportToPdf()
        {
            throw new NotImplementedException("PDF export dilakukan di controller menggunakan Rotativa");
        }
    }
}
