﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shard.DataTransferObjects
{
    public record EmployeeManpulateDto
    {
        [Required(ErrorMessage = "Employee name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public  string? Name { get; init; }
        [Required(ErrorMessage = "Age is a requied field.")]
       public  int Age { get; init; }
        [Required(ErrorMessage = "Positionj is required field")]
        [MaxLength(20, ErrorMessage = "Maximum length for the Position is 20 characters.")]

       public string? Position { get; init; }
    }
}
