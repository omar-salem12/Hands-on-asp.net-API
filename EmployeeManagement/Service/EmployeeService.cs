using AutoMapper;
using Contracts;
using EmployeeManagement.Repository;
using Entities.Exceptions;
using Entities.Models;
using Shard.DataTransferObjects;
using Shard.RequestFeatures;

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

     

        public async Task<(IEnumerable<EmployeeDto> employees, MetaData metaData)> GetEmployeesAsync(Guid companyId,
                                                                      EmployeeParameters employeeParameters
                                                                   
                                                                      ,bool traceChanges)
        {
            var company = await _rpositoryManager.Company.GetCompanyAsync(companyId, traceChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);
            var employeeswithMetaData =
                await _rpositoryManager.Employee.GetEmployeesAsync(companyId,employeeParameters ,traceChanges);
            var employeseDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeeswithMetaData.ToList());
            var metaData = employeeswithMetaData.metaData;
            return (employeseDto,metaData);
        }

        public async Task<EmployeeDto> GetEmployeeAsync(Guid companyId, Guid employeeId, bool traceChanges)
        {
            var company = await _rpositoryManager.Company.GetCompanyAsync(companyId, traceChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);
            var employeeFromDb = await _rpositoryManager.Employee.GetEmployeeAsync(companyId,employeeId, traceChanges);
            if (employeeFromDb is null)
                throw new EmployeeNotFoundException(employeeId);
            var employeeDto = _mapper.Map<EmployeeDto>(employeeFromDb);
            return employeeDto;
        }

        public async Task<EmployeeDto> CreateEmployeeForCompanyAsync(Guid companyId, EmployeeForCreationDto employeeForCreationDto, bool traceChanges)
        {
            var company = await  _rpositoryManager.Company.GetCompanyAsync(companyId, traceChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);
            var employeeEntity = _mapper.Map<Employee>(employeeForCreationDto);

            _rpositoryManager.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
            await _rpositoryManager.SaveAsync();
            var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);
            return employeeToReturn;


        }

        public async Task DeleteEmployeeForCompanyAsync(Guid companyId, Guid id, bool trackChanges)
        {
            var company = await _rpositoryManager.Company.GetCompanyAsync(companyId, trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);

            var employeeForCompany = await _rpositoryManager.Employee.GetEmployeeAsync(companyId, id,trackChanges);

            if(employeeForCompany is null)
                 throw new CompanyNotFoundException(companyId);
            _rpositoryManager.Employee.DeleteEmployee(employeeForCompany);
           await _rpositoryManager.SaveAsync();
        }

        public async Task UpdateEmployeeForCompanyAsync(Guid companyId, Guid id, EmployeeForUpdateDto employeeForUpdate, bool comTrackChaanges, bool empTrackChanges)
        {
            var company = await _rpositoryManager.Company.GetCompanyAsync(companyId, comTrackChaanges);
            if(company is null)
                throw new CompanyNotFoundException(companyId);
            var employeeEntity = await _rpositoryManager.Employee.GetEmployeeAsync(companyId, id, empTrackChanges);
            if (employeeEntity is null)
                throw new EmployeeNotFoundException(id);

            _mapper.Map(employeeForUpdate, employeeEntity);
           await _rpositoryManager.SaveAsync();
        }

       
    }
}