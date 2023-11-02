
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
        public async Task<IActionResult> GetCompanies()
        {
          
                var companies =  await _servic.CompanyService.GetAllCompaniesAsync(trackChanges: false);
                return Ok(companies);
           
        }

        [HttpGet("{id:guid}",Name ="CompanyById")]
        public async Task<IActionResult> GetCompany(Guid id)
        {
            var company =await _servic.CompanyService.GetCompanyAsync(id,trackChanges:false);
            return Ok(company);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyForCreationDto company)
        {
            if (company is null)
                return BadRequest("CompanyForCreationDto object is null");
            if(!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            var companyDto = await _servic.CompanyService.CreateCompanyAsync(company);
            return CreatedAtRoute("CompanyById", new { id = companyDto.Id }, companyDto);
        }


        [HttpGet("collection/({ids})", Name ="CompanyCollection")]
        public async Task <IActionResult> GetCompanyCollection(IEnumerable<Guid> ids)
        {
            var companies = await _servic.CompanyService.GetByIdsAsync(ids, trackChanges: false);
            return Ok(companies);
        }


        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCompany(Guid Id)
        {
           await _servic.CompanyService.DeleteCompanyAsync(Id, trackchanges: false);
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCompany(Guid id , [FromBody] CompanyForUpdateDto company)
        {
            if(company is null)
                return BadRequest("ComapanyForUpdateDto object is null");
            if(!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
           await _servic.CompanyService.UpdateCompanyAsync(id, company, trackChanges: true);
            return NoContent();
            
        }

    }
}
