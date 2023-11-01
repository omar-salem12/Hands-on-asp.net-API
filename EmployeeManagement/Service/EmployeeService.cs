using AutoMapper;
using Contracts;
using EmployeeManagement.Repository;
using Entities.Exceptions;
using Entities.Models;
using Shard.DataTransferObjects;

namespace EmployeeManagement.Service
{
    internal class EmployeeService : IEmployeeService
    {
        private readonly  RepositoryManager _rpositoryManager;
        private readonly  ILoggerManager _logger;
        private readonly IMapper _mapper;

        public EmployeeService(RepositoryManager rpositoryManager, ILoggerManager logger,IMapper mapper)
        {
            _rpositoryManager = rpositoryManager;
            _logger = logger;
            _mapper = mapper;
        }

     

        public IEnumerable<EmployeeDto> GetEmployees(Guid companyId, bool traceChanges)
        {
            var company = _rpositoryManager.Company.GetCompany(companyId, traceChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);
            var employeesFromDb = _rpositoryManager.Employee.GetEmployees(companyId, traceChanges);
            var employeseDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesFromDb);
            return employeseDto;
        }

        public EmployeeDto GetEmployee(Guid companyId, Guid employeeId, bool traceChanges)
        {
            var company = _rpositoryManager.Company.GetCompany(companyId, traceChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);
            var employeeFromDb = _rpositoryManager.Employee.GetEmployee(companyId,employeeId, traceChanges);
            if (employeeFromDb is null)
                throw new EmployeeNotFoundException(employeeId);
            var employeeDto = _mapper.Map<EmployeeDto>(employeeFromDb);
            return employeeDto;
        }

        public EmployeeDto CreateEmployeeForCompany(Guid companyId, EmployeeForCreationDto employeeForCreationDto, bool traceChanges)
        {
            var company = _rpositoryManager.Company.GetCompany(companyId, traceChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);
            var employeeEntity = _mapper.Map<Employee>(employeeForCreationDto);

            _rpositoryManager.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
            _rpositoryManager.Save();
            var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);
            return employeeToReturn;


        }

        public void DeleteEmployeeForCompany(Guid companyId, Guid id, bool trackChanges)
        {
            var company = _rpositoryManager.Company.GetCompany(companyId, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var employeeForCompany = _rpositoryManager.Employee.GetEmployee(companyId, id,trackChanges);

            if(employeeForCompany is null)
                 throw new CompanyNotFoundException(companyId);
            _rpositoryManager.Employee.DeleteEmployee(employeeForCompany);
            _rpositoryManager.Save();
        }

        public void UpdateEmployeeForCompany(Guid companyId, Guid id, EmployeeForUpdateDto employeeForUpdate, bool comTrackChaanges, bool empTrackChanges)
        {
            var company = _rpositoryManager.Company.GetCompany(companyId, comTrackChaanges);
            if(company is null)
                throw new CompanyNotFoundException(companyId);
            var employeeEntity = _rpositoryManager.Employee.GetEmployee(companyId, id, empTrackChanges);
            if (employeeEntity is null)
                throw new EmployeeNotFoundException(id);

            _mapper.Map(employeeForUpdate, employeeEntity);
            _rpositoryManager.Save();
        }
    }
}