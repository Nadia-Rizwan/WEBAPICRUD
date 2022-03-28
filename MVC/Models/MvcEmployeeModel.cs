using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class MvcEmployeeModel
    {
        public int EmployeeID { get; set; }

        [Required(ErrorMessage ="This field is required")]
        public string EmployeeName { get; set; }
        public string EmployeePosition { get; set; }
        public Nullable<int> EmployeeAge { get; set; }
        public Nullable<int> EmployeeSalary { get; set; }
    }
}