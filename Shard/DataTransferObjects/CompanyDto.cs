using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Shard.DataTransferObjects
{

    public record CompanyDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public String? FullAddress { get; set; }
    }


    //(Guid Id, string Name, string FullAddress);

}