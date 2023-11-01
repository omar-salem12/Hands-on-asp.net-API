using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class EmployeeNotFoundException :Exception
    {
        public EmployeeNotFoundException(Guid EmployeeId) 
           :base($"The company with id: {EmployeeId} doesn't exist in the database.")
        { 
        }

    }
}
