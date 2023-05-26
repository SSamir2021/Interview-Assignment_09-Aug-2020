using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeesWebApi.Models
{
    public class Employee
    {
        [Key]
        public int EmpId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string EmailID { get; set; }

        [Required]
        public string Phone { get; set; }
    }
}



