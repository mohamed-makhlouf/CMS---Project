using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.DAL.Entity
{

    [Table("City")]
    public class City
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string? Name { get; set; }
        public int CountryId { get; set; }
        public Country? Country { get; set; }

    }
}

