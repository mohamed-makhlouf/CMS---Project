﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Demo.BL.Models
{
    public class DepartmentVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Required")]
        [MinLength(3,ErrorMessage = "Min Len 3")]
        [MaxLength(50 , ErrorMessage = "Max Len")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Code Required")]
        public string? Code { get; set; }
    }
}
