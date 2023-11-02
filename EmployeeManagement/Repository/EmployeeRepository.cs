using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shard.RequestFeatures;

namespace EmployeeManagement.Repository
{
    public class EmployeeRepository :RepositoryBase<Employee>,IEmployeeRepository
       
    {
        public EmployeeRepository(RespositoryContext respositoryContext):base(respositoryContext)
        {
            
        }

       

        public async Task<PageList<Employee>> GetEmployeesAsync(Guid companyId,
                                                EmployeeParameters parameters,
                                                bool trackChanges)
        {
            var employees =  await 
                FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges).OrderBy( e=>e.Name)
                .Skip((parameters.PageNumber -1)* parameters.pageSize )
                .Take(parameters.pageSize)
                 .ToListAsync();
            var count = await FindByCondition(e => e.CompanyId.Equals(companyId),
                                trackChanges).CountAsync();

            return new PageList<Employee>(employees,count,parameters.PageNumber,parameters.pageSize);

        }

        public async Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId, bool trackChanges) =>
        
          await FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(employeeId), trackChanges).SingleOrDefaultAsync();

        public void CreateEmployeeForCompany(Guid companyId, Employee employee)
        {

            employee.CompanyId = companyId;
            Create(employee);
        }

        public void DeleteEmployee(Employee employee)
        {
            Delete(employee);
        }
    }
}
