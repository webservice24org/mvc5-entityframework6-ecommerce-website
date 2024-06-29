using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mohiuddin_EcommerceWebsite.Models
{
    public class ProductSupplier
    {
        [Key]
        public int ProductSupplierId { get; set; }
        [Required]
        [Display(Name = "Supplier Name")]
        public string SupplierName { get; set; }
        [Required]
        [Display(Name = "Supplier Email")]
        public string ContactEmail { get; set; }
        [Required]
        [Display(Name = "Supplier Phone No")]
        public string ContactPhone { get; set; }
        [Required]
        [Display(Name = "Supplier Address")]
        public string Address { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }

}