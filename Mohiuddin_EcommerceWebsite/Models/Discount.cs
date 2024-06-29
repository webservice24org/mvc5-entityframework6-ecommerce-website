using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mohiuddin_EcommerceWebsite.Models
{
    public class Discount
    {
        public int DiscountId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Percentage must be between 0 and 100.")]
        public decimal Percentage { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }

}