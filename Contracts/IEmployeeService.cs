using Shard.DataTransferObjects;

namespace Contracts
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeDto> GetEmployees(Guid companyId,bool traceChanges);

        EmployeeDto GetEmployee(Guid companyId, Guid employeeId, bool traceChanges);

        
        EmployeeDto CreateEmployeeForCompany(Guid companyId,
               EmployeeForCreationDto employeeForCreationDto, bool trackChanges);


        void DeleteEmployeeForCompany(Guid companyId , Guid id, bool trackChanges);


        void UpdateEmployeeForCompany(Guid companyId, Guid id,
                                      EmployeeForUpdateDto employeeForUpdate,
                                      bool comTrackChaanges,
                                      bool empTrackChanges);
    }

}