using Entities.Models;
using Shard.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICompanyService
    {
        IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges);
    }
}
