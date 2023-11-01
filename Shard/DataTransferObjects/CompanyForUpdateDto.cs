using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shard.DataTransferObjects
{
    public record CompanyForUpdateDto(string Name, string Adress, string Country,
             IEnumerable<EmployeeForCreationDto> Employees);


    
}
