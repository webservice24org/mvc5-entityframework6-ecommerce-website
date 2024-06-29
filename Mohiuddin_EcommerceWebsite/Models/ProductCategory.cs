using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mohiuddin_EcommerceWebsite.Models
{
    public class ProductCategory
    {
        [Key]
        public int ProductCategoryId { get; set; }
        [Required]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
        [Required]
        [Display(Name = "Category Image")]
        public string CategoryImage { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<ProductSubCategory> ProductSubCategories { get; set; } 
    }


}