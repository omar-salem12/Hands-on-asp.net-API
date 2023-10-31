using Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Presentations.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController :ControllerBase
    {
        private readonly IServiceManager _servic;

        public CompaniesController(IServiceManager service)
        {
            _servic = service;
        }

        [HttpGet]
        public IActionResult GetCompanies()
        {
            try
            {
                var companies = _servic.CompanyService.GetAllCompanies(trackChanges: false);
                return Ok(companies);
            }
            catch (Exception)
            {

                return StatusCode(500, "Internal server error");
            }
        }
    }
}
