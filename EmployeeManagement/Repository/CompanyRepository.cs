
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace EmployeeManagement.Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RespositoryContext repositoryContext) : base(repositoryContext)
        {
            
        }

        public void CreateCompany(Company company)
        {
            Create(company);
           
        }

        public void DeleteCompany(Company company)
        {
            Delete(company);
        }

        public async Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges)
        {
                return await FindAll(trackChanges).OrderBy(c => c.Name)
                                             .ToListAsync();
        }

        public async Task<IEnumerable<Company>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
        {
          return await FindByCondition(c => ids.Contains(c.Id), trackChanges).ToListAsync();
        }

        public async Task<Company?> GetCompanyAsync(Guid companyId, bool trackChanges)
        {
          return await FindByCondition(c => c.Id == companyId, trackChanges).SingleOrDefaultAsync();
        } 


    }
}
