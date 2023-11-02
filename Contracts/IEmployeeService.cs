using Shard.DataTransferObjects;
using Shard.RequestFeatures;
using System.Reflection.Metadata.Ecma335;

namespace Contracts
{
    public interface IEmployeeService
    {
        Task<(IEnumerable<EmployeeDto> employees,MetaData metaData)> GetEmployeesAsync(Guid companyId,
                                           EmployeeParameters employeeParameters,
                                                   bool traceChanges);

        Task<EmployeeDto> GetEmployeeAsync(Guid companyId, Guid employeeId, bool traceChanges);


        Task<EmployeeDto> CreateEmployeeForCompanyAsync(Guid companyId,
               EmployeeForCreationDto employeeForCreationDto, bool trackChanges);


       Task  DeleteEmployeeForCompanyAsync(Guid companyId , Guid id, bool trackChanges);


        Task UpdateEmployeeForCompanyAsync(Guid companyId, Guid id,
                                      EmployeeForUpdateDto employeeForUpdate,
                                      bool comTrackChaanges,
                                      bool empTrackChanges);
    }

}