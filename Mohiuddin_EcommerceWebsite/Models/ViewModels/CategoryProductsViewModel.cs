using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mohiuddin_EcommerceWebsite.Models.ViewModels
{
    public class CategoryProductsViewModel
    {
        public string CategoryName { get; set; }
        public List<Product> Products { get; set; }
        public List<ProductCategory> Categories { get; set; } 
    }


}