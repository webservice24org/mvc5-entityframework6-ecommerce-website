using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mohiuddin_EcommerceWebsite.Models.ViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }

        [Required]
        [Display(Name = "Product Title")]
        public string ProductName { get; set; }

        [Required]
        [Display(Name = "Product Price")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Product Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Product Image")]
        public string ImageUrl { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }

        [Required]
        [Display(Name = "Product Status")]
        public bool IsActive { get; set; }

        [Required]
        [Display(Name = "Category ID")]
        public int ProductCategoryId { get; set; }

        [Display(Name = "Sub Category ID")]
        public int? ProductSubCategoryId { get; set; }

        [Required]
        [Display(Name = "Unit ID")]
        public int UnitId { get; set; }

        [Required]
        [Display(Name = "Brand ID")]
        public int BrandId { get; set; }

        [Required]
        [Display(Name = "Supplier ID")]
        public int ProductSupplierId { get; set; }

        [Display(Name = "Discount ID")]
        public int? DiscountId { get; set; }

        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [Display(Name = "Subcategory Name")]
        public string SubCategoryName { get; set; }

        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }

        [Required]
        [Display(Name = "Product Quantity")]
        public int Quantity { get; set; }

        public IEnumerable<ProductCategory> ProductCategories { get; set; }
        public IEnumerable<ProductSubCategory> ProductSubCategories { get; set; }
        public IEnumerable<Unit> Units { get; set; }
        public IEnumerable<Brand> Brands { get; set; }
        public IEnumerable<ProductSupplier> ProductSuppliers { get; set; }
        public IEnumerable<Discount> Discounts { get; set; }
    }

}