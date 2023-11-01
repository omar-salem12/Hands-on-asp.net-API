using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Shard.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
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
        public IActionResult GetEmployeesForCompany(Guid companyId)
        {
            var employees = _service.EmployeeService.GetEmployees(companyId, traceChanges: false);
            return Ok(employees);
        }

        [HttpGet("{employeeId:Guid}",Name ="EmployeeGetById")]
        public IActionResult GetEmployee(Guid companyId, Guid employeeId)
        {
            var employee = _service.EmployeeService.GetEmployee(companyId, employeeId, false);
            return Ok(employee);
        }

        [HttpPost]
        public IActionResult CreateEmployeeForCompany(Guid companyId, 
                                     [FromBody] EmployeeForCreationDto employee)
        {
            if (employee is null)
                return BadRequest("EmployeeForCreationDto object is null");

           if(!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            var employeeToReturn  = 
             _service.EmployeeService.CreateEmployeeForCompany(companyId, employee, trackChanges:false);
            return CreatedAtRoute("EmployeeGetById", new { companyId, employeeId = employeeToReturn.Id }, employeeToReturn);
        }

        [HttpDelete("{id:Guid}")]
        public IActionResult DeleteEmployeeForCompany(Guid companyId, Guid id)
        {
            _service.EmployeeService.DeleteEmployeeForCompany(companyId, id, trackChanges: false);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateEmployeeForCompany(Guid companyId, Guid id,
                                  [FromBody] EmployeeForUpdateDto employee)
        {
            if (employee is null)
                return BadRequest("EmployeeForUpdateDto object is null");
            _service.EmployeeService.UpdateEmployeeForCompany(companyId, id, employee,
                                                               comTrackChaanges: false,
                                                               empTrackChanges: true);
            return NoContent();

        }
    }
}
