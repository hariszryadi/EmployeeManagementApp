using EmployeeManagementApp.Services;
using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using System.Threading.Tasks;
using System;
using EmployeeManagementApp.Models;

namespace EmployeeManagementApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return View(employees);
        }

        public async Task<IActionResult> Details(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }


        public IActionResult ExportToExcel()
        {
            var excelData = _employeeService.ExportToExcel();
            string fileName = $"Daftar_Pegawai_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
            return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        public async Task<IActionResult> ExportToPdf()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();

            return new ViewAsPdf("ExportToPdf", employees)
            {
                FileName = $"Daftar_Pegawai_{DateTime.Now:yyyyMMdd_HHmmss}.pdf",
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageMargins = { Left = 20, Bottom = 20, Right = 20, Top = 20 },
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
            };
        }
    }
}
