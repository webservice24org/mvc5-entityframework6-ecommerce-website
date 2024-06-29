using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mohiuddin_EcommerceWebsite.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [Display(Name = "Product Title")]
        public string ProductName { get; set; }

        [Required]
        [Display(Name = "Product Price")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Product Description")]
        public string Description { get; set; }

        [Display(Name = "Product Image")]
        public string ImageUrl { get; set; }

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

        [Required]
        [Display(Name = "Product Quantity")]
        public int Quantity { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual ProductSubCategory ProductSubCategory { get; set; }
        public virtual Discount Discount { get; set; }
        public virtual ProductSupplier ProductSupplier { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

    }




}