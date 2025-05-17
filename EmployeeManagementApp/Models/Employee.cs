using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System;

namespace EmployeeManagementApp.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nama Pegawai")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Jabatan")]
        public string Position { get; set; }

        [Required]
        [Display(Name = "Departemen")]
        public string Department { get; set; }

        [Required]
        [Display(Name = "Tanggal Bergabung")]
        [DataType(DataType.Date)]
        public DateTime JoinDate { get; set; }

        [Required]
        [Display(Name = "Gaji")]
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
    }
}
