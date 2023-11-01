
using Contracts;
using Microsoft.AspNetCore.Mvc;
using Shard.DataTransferObjects;

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
          
                var companies = _servic.CompanyService.GetAllCompanies(trackChanges: false);
                return Ok(companies);
           
        }

        [HttpGet("{id:guid}",Name ="CompanyById")]
        public IActionResult GetCompany(Guid id)
        {
            var company = _servic.CompanyService.GetCompany(id,trackChanges:false);
            return Ok(company);
        }

        [HttpPost]
        public IActionResult CreateCompany([FromBody] CompanyForCreationDto company)
        {
            if (company is null)
                return BadRequest("CompanyForCreationDto object is null");
            if(!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            var companyDto = _servic.CompanyService.CreateCompany(company);
            return CreatedAtRoute("CompanyById", new { id = companyDto.Id }, companyDto);
        }


        [HttpGet("collection/({ids})", Name ="CompanyCollection")]
        public IActionResult GetCompanyCollection(IEnumerable<Guid> ids)
        {
            var companies = _servic.CompanyService.GetByIds(ids, trackChanges: false);
            return Ok(companies);
        }


        [HttpDelete("{id:guid}")]
        public IActionResult DeleteCompany(Guid Id)
        {
            _servic.CompanyService.DeleteCompany(Id, trackchanges: false);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdateCompany(Guid id , [FromBody] CompanyForUpdateDto company)
        {
            if(company is null)
                return BadRequest("ComapanyForUpdateDto object is null");
            if(!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            _servic.CompanyService.UpdateCompany(id, company, trackChanges: true);
            return NoContent();
            
        }

    }
}
