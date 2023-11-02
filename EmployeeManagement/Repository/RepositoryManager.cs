using Contracts;

namespace EmployeeManagement.Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RespositoryContext _repositoryContext;
        private readonly Lazy<CompanyRepository> _companyRepository;
        private readonly Lazy<EmployeeRepository> _employeeRepository;


        public RepositoryManager(RespositoryContext respositoryContext) {

            _repositoryContext = respositoryContext;
            _companyRepository = new Lazy<CompanyRepository>(() => new CompanyRepository(respositoryContext));
            _employeeRepository = new Lazy<EmployeeRepository>(()=> new EmployeeRepository(respositoryContext));

        }
        public ICompanyRepository Company => _companyRepository.Value;

        public IEmployeeRepository Employee => _employeeRepository.Value;

        public async Task SaveAsync()
        {
           await _repositoryContext.SaveChangesAsync();
        }
    }
}
