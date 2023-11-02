using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Shard.DataTransferObjects;
using Shard.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmployeeManagement.Presentations.Controllers
{

    [Route("api/companies/{companyId}/employees")]
    [ApiController]
    public class EmployeesController :ControllerBase
    {
        private readonly IServiceManager _service;

        public EmployeesController(IServiceManager service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeesForCompany(Guid companyId,
            [FromQuery] EmployeeParameters employeeParameters)
        {
            var PageList = await _service.EmployeeService.
                GetEmployeesAsync(companyId, employeeParameters,traceChanges: false);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(PageList.metaData));
            return Ok(PageList.employees);
        }

        [HttpGet("{employeeId:Guid}",Name ="EmployeeGetById")]
        public async Task <IActionResult> GetEmployee(Guid companyId, Guid employeeId)
        {
            var employee = await _service.EmployeeService.GetEmployeeAsync(companyId, employeeId, false);
            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployeeForCompany(Guid companyId, 
                                     [FromBody] EmployeeForCreationDto employee)
        {
            if (employee is null)
                return BadRequest("EmployeeForCreationDto object is null");

           if(!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var employeeToReturn  = 
           await  _service.EmployeeService.CreateEmployeeForCompanyAsync(companyId, employee, trackChanges:false);
            return CreatedAtRoute("EmployeeGetById", new { companyId, employeeId = employeeToReturn.Id }, employeeToReturn);
        }

        [HttpDelete("{id:Guid}")]
        public async Task <IActionResult> DeleteEmployeeForCompany(Guid companyId, Guid id)
        {
           await _service.EmployeeService.DeleteEmployeeForCompanyAsync(companyId, id, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateEmployeeForCompany(Guid companyId, Guid id,
                                  [FromBody] EmployeeForUpdateDto employee)
        {
            if (employee is null)
                return BadRequest("EmployeeForUpdateDto object is null");
           await _service.EmployeeService.UpdateEmployeeForCompanyAsync(companyId, id, employee,
                                                               comTrackChaanges: false,
                                                               empTrackChanges: true);
            return NoContent();

        }
    }
}
