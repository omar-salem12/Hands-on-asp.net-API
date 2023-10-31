using Contracts;
using Entities.Models;

namespace EmployeeManagement.Repository
{
    public class EmployeeRepository :RepositoryBase<Employee>,IEmployeeRepository
       
    {
        public EmployeeRepository(RespositoryContext respositoryContext):base(respositoryContext)
        {
            
        }
    }
}
