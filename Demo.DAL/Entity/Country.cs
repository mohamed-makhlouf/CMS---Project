using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.DAL.Entity
{

    [Table("Country")]
    public class Country
    {
        [Key]
        public int Id { get; set; }

        [Required , StringLength(50)]
        public string? Name { get; set; }
    }
}
