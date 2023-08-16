using Microsoft.AspNetCore.Mvc;
using Sprout.Exam.Core.Interface;
using System.Threading.Tasks;
using System;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.WebApp.Models;

namespace Sprout.Exam.WebApp.Controllers
{
    public class PayrollController : Controller
    {
        private IEmployeeService _employeeService;
        public PayrollController(IEmployeeService employeeService)
        {
            _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
        }

        [HttpPost("{id}/calculate")]
        public async Task<IActionResult> Calculate([FromBody] ComputeSalaryDto input)
        {
            var result = await _employeeService.GetById(input.Id).ConfigureAwait(false);
            if (result == null) return NotFound();
            var type = (EmployeeType)result.TypeId;

            switch (type)
            {
                case EmployeeType.Regular:
                    RegularEmployee regEmp = new RegularEmployee();
                    regEmp.MonthlySalary = 20000;
                    regEmp.TaxRate = 12;
                    regEmp.AbsentCount = input.NoOfDays;
                    return Ok(regEmp.ComputeSalary());
                case EmployeeType.Contractual:
                    ContractualEmployee contEmp = new ContractualEmployee();
                    contEmp.WorkDayCount = input.NoOfDays;
                    contEmp.DailyRate = 500;
                    return Ok(contEmp.ComputeSalary());
                default:
                    return NotFound("Employee Type not found");

            }
        }
    }
}
