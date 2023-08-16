using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.Core.Interface;

namespace Sprout.Exam.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        [Authorize]
        [Route("api/[controller]")]
        [ApiController]
        public class EmployeeController : ControllerBase
        {

            private IEmployeeService _employeeService;
            public EmployeeController(IEmployeeService employeeService)
            {
                _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
            }

            /// <summary>
            /// Refactor this method to go through proper layers and fetch from the DB.
            /// </summary>
            /// <returns></returns>
            [HttpGet]
            public async Task<IActionResult> Get()
            {
                var result = await _employeeService.GetAll().ConfigureAwait(false);
                return Ok(result);
            }

            /// <summary>
            /// Refactor this method to go through proper layers and fetch from the DB.
            /// </summary>
            /// <returns></returns>
            [HttpGet("{id}")]
            [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeDto))]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public async Task<IActionResult> GetById(int id)
            {
                var result = await _employeeService.GetById(id).ConfigureAwait(false);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }

            /// <summary>
            /// Refactor this method to go through proper layers and update changes to the DB.
            /// </summary>
            /// <returns></returns>
            [HttpPut("{id}")]
            public async Task<IActionResult> Put([FromBody] EditEmployeeDto input)
            {
                var item = await _employeeService.Update(input).ConfigureAwait(false);
                return Ok(item);
            }

            /// <summary>
            /// Refactor this method to go through proper layers and insert employees to the DB.
            /// </summary>
            /// <returns></returns>
            [HttpPost]
            public async Task<IActionResult> Post([FromBody] CreateEmployeeDto input)
            {
                var id = await _employeeService.Create(input).ConfigureAwait(false);
                return Created($"/api/employee/{id}", id);
            }


            /// <summary>
            /// Refactor this method to go through proper layers and perform soft deletion of an employee to the DB.
            /// </summary>
            /// <returns></returns>
            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                var deletedRecordId = await _employeeService.Delete(id).ConfigureAwait(false);
                return Ok(deletedRecordId);
            }

        }
    }
}
