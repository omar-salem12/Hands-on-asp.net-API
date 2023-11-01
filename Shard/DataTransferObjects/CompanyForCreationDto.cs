using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shard.DataTransferObjects
{
    public record CompanyForCreationDto(
        [Required(ErrorMessage ="Comapany name is a required field.")]
        [MaxLength(30, ErrorMessage ="Maximum length for the Name is 30 charachters.")]
        string Name,
        [Required(ErrorMessage ="Comapany Address is a required field.")]
        [MaxLength(30, ErrorMessage ="Maximum length for the Name is 30 charachters.")]
        string Address,
        [Required(ErrorMessage ="Comapany country is a required field.")]
        [MaxLength(30, ErrorMessage ="Maximum length for the Name is 30 charachters.")]
        string Country,
        IEnumerable<EmployeeForCreationDto> Employees);
    
    
}
