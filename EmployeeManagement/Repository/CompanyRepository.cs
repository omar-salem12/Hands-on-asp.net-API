
using Contracts;
using Entities.Models;

namespace EmployeeManagement.Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RespositoryContext repositoryContext) : base(repositoryContext)
        {
            
        }

        public IEnumerable<Company> GetAllCompanies(bool trackChanges)
        {
                return FindAll(trackChanges).OrderBy(c => c.Name)
                                             .ToList();
        }
    }
}
