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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EmployeeDto))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPut("{id}")]
            public async Task<IActionResult> Put([FromBody] EditEmployeeDto input)
            {
            if(!ModelState.IsValid)
            {
                return BadRequest("Please Complete Required Fields.");
            }
                var item = await _employeeService.Update(input).ConfigureAwait(false);
                return Ok(item);
            }

            /// <summary>
            /// Refactor this method to go through proper layers and insert employees to the DB.
            /// </summary>
            /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Post([FromBody] CreateEmployeeDto input)
            {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please Complete Required Fields.");
            }
            var id = await _employeeService.Create(input).ConfigureAwait(false);
                return Created($"/api/employee/{id}", id);
            }


            /// <summary>
            /// Refactor this method to go through proper layers and perform soft deletion of an employee to the DB.
            /// </summary>
            /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(int id)
            {
                var deletedRecordId = await _employeeService.Delete(id).ConfigureAwait(false);
                return Ok(deletedRecordId);
            }

        }
    }
