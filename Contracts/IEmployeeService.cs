﻿using Shard.DataTransferObjects;

namespace Contracts
{
    public interface IEmployeeService
    {
     Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(Guid companyId,bool traceChanges);

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