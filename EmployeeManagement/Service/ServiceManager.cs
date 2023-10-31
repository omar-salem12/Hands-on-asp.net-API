using AutoMapper;
using Contracts;
using EmployeeManagement.Repository;

namespace EmployeeManagement.Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICompanyService> _companyService;
        private readonly Lazy<IEmployeeService> _employeeService;

        public ServiceManager(ILoggerManager logger, RepositoryManager rpositoryManager ,IMapper mapper)
        {
           _companyService = new Lazy<ICompanyService>(() => new CompanyService(rpositoryManager,logger,mapper));
            _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(rpositoryManager, logger, mapper));

        }
        public ICompanyService CompanyService => _companyService.Value;

        public IEmployeeService EmployeeService => _employeeService.Value;
    }
}
