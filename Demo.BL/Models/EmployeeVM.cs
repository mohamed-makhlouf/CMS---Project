using Demo.DAL.Entity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.Models
{
    public class EmployeeVM
    {

        public EmployeeVM()
        {
            this.IsDeleted = false;
            this.CreationDate = DateTime.Now;
            this.IsUpdated = false;
        }
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Required")]
        [MinLength(3,ErrorMessage = "Min Len 3")]
        [MaxLength(50, ErrorMessage = "Max Len 50")]
        public string Name { get; set; }

        [Range(3000 , 50000 , ErrorMessage = "Range Btw 3K : 50K")]
        public double Salary { get; set; }
        public DateTime HireDate { get; set; }
        public string? Notes { get; set; }

        [EmailAddress(ErrorMessage = "invalid email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "EmpAddress Required")]
        [RegularExpression("[0-9]{2,5}-[a-zA-Z]{1,10}-[a-zA-Z]{1,10}-[a-zA-Z]{1,10}", ErrorMessage ="12-StreetName-City-Country")]
        public string EmpAddress { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsUpdated { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public int DepartmentId { get; set; }

        // Navegation Property
        //[ForeignKey("DepartmentId")]
        public Department? Department { get; set; }
        public int? DistrictId { get; set; }
        public District? District { get; set; }
        public string? CvName { get; set; }
        public string? ImageName { get; set; }
        public IFormFile? Cv { get; set; }
        public IFormFile? Image { get; set; }


    }
}
