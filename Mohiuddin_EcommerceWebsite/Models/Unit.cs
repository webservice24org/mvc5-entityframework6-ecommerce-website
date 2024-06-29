using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mohiuddin_EcommerceWebsite.Models
{
    public class Unit
    {
        [Key]
        public int UnitId { get; set; }
        [Required]
        [Display(Name = "Unit Name")]
        public string UnitName { get; set; }

        public virtual ICollection<Product> Products { get; set; }

    }
}