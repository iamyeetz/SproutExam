using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sprout.Exam.Business.DataTransferObjects
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Full Name is required.")]
        public string FullName { get; set; }
        public string Birthdate { get; set; }
        [Required(ErrorMessage = "Tin is required.")]
        public string Tin { get; set; }
        [Required(ErrorMessage = "EmployeeType is required.")]
        public int TypeId { get; set; }
    }
}
