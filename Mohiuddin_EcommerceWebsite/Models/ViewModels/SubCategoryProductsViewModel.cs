using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mohiuddin_EcommerceWebsite.Models.ViewModels
{
    public class SubCategoryProductsViewModel
    {
        public string SubCategoryName { get; set; }
        public List<Product> Products { get; set; }
        public List<ProductCategory> Categories { get; set; } 
    }

}