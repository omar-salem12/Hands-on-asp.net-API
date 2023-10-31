using AutoMapper;
using Contracts;
using EmployeeManagement.Repository;

namespace EmployeeManagement.Service
{
    internal class EmployeeService : IEmployeeService
    {
        private RepositoryManager rpositoryManager;
        private ILoggerManager logger;
        private readonly IMapper _mapper;

        public EmployeeService(RepositoryManager rpositoryManager, ILoggerManager logger,IMapper mapper)
        {
            this.rpositoryManager = rpositoryManager;
            this.logger = logger;
            _mapper = mapper;
        }
    }
}