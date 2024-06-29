using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mohiuddin_EcommerceWebsite.Models
{
    public class Brand
    {
        public int BrandId { get; set; }

        [Required]
        [Display(Name ="Brand Name")]
        public string BrandName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }


}