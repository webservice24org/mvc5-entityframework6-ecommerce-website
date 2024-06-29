using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mohiuddin_EcommerceWebsite.Models.ViewModels
{
    public class ProductSubCategoryViewModel
    {
        public int ProductSubCategoryId { get; set; }

        [Required]
        [Display(Name = "Sub Category Name")]
        public string SubCategoryName { get; set; }

        [Display(Name = "Image")]
        public string SubCategoryImg { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }

        [Required]
        [Display(Name = "Main Category")]
        public int ProductCategoryId { get; set; }

        public IEnumerable<ProductCategory> ProductCategories { get; set; }
    }
}