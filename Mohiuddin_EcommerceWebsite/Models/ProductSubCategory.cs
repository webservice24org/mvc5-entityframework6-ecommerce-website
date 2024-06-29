using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mohiuddin_EcommerceWebsite.Models
{
    public class ProductSubCategory
    {
        [Key]
        public int ProductSubCategoryId { get; set; }
        [Required]
        [Display(Name = "Sub Category Name")]
        public string SubCategoryName { get; set; }
        [Display(Name = "Image")]
        public string SubCategoryImg { get; set; }
        [Required]
        [Display(Name = "Main Category")]
        public int ProductCategoryId { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ICollection<Product> Products { get; set; }

    }

}