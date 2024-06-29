using Mohiuddin_EcommerceWebsite.DAL;
using Mohiuddin_EcommerceWebsite.Models;
using Mohiuddin_EcommerceWebsite.Models.ViewModels;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Mohiuddin_EcommerceWebsite.Controllers
{
    public class ProductController : Controller
    {
        MohiuddinEcommerceContext db = new MohiuddinEcommerceContext();
        public ActionResult Index()
        {
            var products = db.Products.Select(p => new ProductViewModel
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                CategoryName = p.ProductCategory.CategoryName,
                SubCategoryName = p.ProductSubCategory != null ? p.ProductSubCategory.SubCategoryName : "N/A",
                BrandName = p.Brand.BrandName,
                ImageUrl = p.ImageUrl,
                Quantity = p.Quantity
            }).ToList();

            return View(products);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new ProductViewModel
            {
                ProductCategories = db.ProductCategories.ToList(),
                ProductSubCategories = db.ProductSubCategories.ToList(),
                Units = db.Units.ToList(),
                Brands = db.Brands.ToList(),
                ProductSuppliers = db.ProductSuppliers.ToList(),
                Discounts = db.Discounts.ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    ProductName = model.ProductName,
                    Price = model.Price,
                    Description = model.Description,
                    IsActive = model.IsActive,
                    ProductCategoryId = model.ProductCategoryId,
                    ProductSubCategoryId = model.ProductSubCategoryId,
                    UnitId = model.UnitId,
                    BrandId = model.BrandId,
                    ProductSupplierId = model.ProductSupplierId,
                    DiscountId = model.DiscountId,
                    Quantity = model.Quantity > 0 ? model.Quantity : 1
                };

                if (model.ImageFile != null && model.ImageFile.ContentLength > 0)
                {
                    var timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    var fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
                    var fileExtension = Path.GetExtension(model.ImageFile.FileName);
                    var newFileName = $"{fileName}_{timestamp}{fileExtension}";
                    var path = Path.Combine(Server.MapPath("~/Images/products"), newFileName);
                    model.ImageFile.SaveAs(path);
                    product.ImageUrl = "~/Images/products/" + newFileName;
                }

                db.Products.Add(product);
                TempData["SuccessMessage"] = "Product Created successfully";
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            model.ProductCategories = db.ProductCategories.ToList();
            model.ProductSubCategories = db.ProductSubCategories.ToList();
            model.Units = db.Units.ToList();
            model.Brands = db.Brands.ToList();
            model.ProductSuppliers = db.ProductSuppliers.ToList();
            model.Discounts = db.Discounts.ToList();

            TempData["ErrorMessage"] = "Error Adding Product. Please try again.";
            return View(model);
        }


        public ActionResult Edit(int id)
        {
            var product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            var model = new ProductViewModel
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Price = product.Price,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Quantity = product.Quantity, 
                IsActive = product.IsActive,
                ProductCategoryId = product.ProductCategoryId,
                ProductSubCategoryId = product.ProductSubCategoryId,
                UnitId = product.UnitId,
                BrandId = product.BrandId,
                ProductSupplierId = product.ProductSupplierId,
                DiscountId = product.DiscountId,
                ProductCategories = db.ProductCategories.ToList(),
                ProductSubCategories = db.ProductSubCategories.ToList(),
                Units = db.Units.ToList(),
                Brands = db.Brands.ToList(),
                ProductSuppliers = db.ProductSuppliers.ToList(),
                Discounts = db.Discounts.ToList()
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductViewModel model)
        {

            if (ModelState.IsValid)
            {
                var product = db.Products.Find(model.ProductId);

                if (product == null)
                {
                    return HttpNotFound();
                }

                if (model.ImageFile != null && model.ImageFile.ContentLength > 0)
                {

                    if (!string.IsNullOrEmpty(product.ImageUrl))
                    {
                        var oldImagePath = Server.MapPath(product.ImageUrl);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    var timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    var fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
                    var fileExtension = Path.GetExtension(model.ImageFile.FileName);
                    var newFileName = $"{fileName}_{timestamp}{fileExtension}";
                    var path = Path.Combine(Server.MapPath("~/Images/products"), newFileName);
                    model.ImageFile.SaveAs(path);

                    product.ImageUrl = "~/Images/products/" + newFileName;
                }

                product.ProductName = model.ProductName;
                product.Price = model.Price;
                product.Description = model.Description;
                product.IsActive = model.IsActive;
                product.ProductCategoryId = model.ProductCategoryId;
                product.ProductSubCategoryId = model.ProductSubCategoryId;
                product.UnitId = model.UnitId;
                product.BrandId = model.BrandId;
                product.ProductSupplierId = model.ProductSupplierId;
                product.DiscountId = model.DiscountId;
                product.Quantity = model.Quantity;

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMessage"] = "Product Updated successfully";
                return RedirectToAction("Index");

            }


            model.ProductCategories = db.ProductCategories.ToList();
            model.ProductSubCategories = db.ProductSubCategories.ToList();
            model.Units = db.Units.ToList();
            model.Brands = db.Brands.ToList();
            model.ProductSuppliers = db.ProductSuppliers.ToList();
            model.Discounts = db.Discounts.ToList();

            TempData["ErrorMessage"] = "Error Updating Product. Please try again.";
            return View(model);
        }

        public ActionResult Details(int id)
        {
            var product = db.Products
                .Include(p => p.ProductCategory)         
                .Include(p => p.ProductSubCategory)      
                .Include(p => p.Unit)                    
                .Include(p => p.Brand)                   
                .Include(p => p.ProductSupplier)         
                .Include(p => p.Discount)                
                .SingleOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                return HttpNotFound();
            }

            var model = new ProductViewModel
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Price = product.Price,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Quantity = product.Quantity,
                IsActive = product.IsActive,
                ProductCategoryId = product.ProductCategoryId,
                ProductSubCategoryId = product.ProductSubCategoryId,
                UnitId = product.UnitId,
                BrandId = product.BrandId,
                ProductSupplierId = product.ProductSupplierId,
                DiscountId = product.DiscountId,
                CategoryName = product.ProductCategory.CategoryName,
                SubCategoryName = product.ProductSubCategory != null ? product.ProductSubCategory.SubCategoryName : "N/A",
                BrandName = product.Brand.BrandName
            };

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                var imagePath = Server.MapPath(product.ImageUrl);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }


            db.Entry(product).State = EntityState.Deleted;
            db.SaveChanges();
            TempData["DeleteMessage"] = "Product deleted successfully.";
            return RedirectToAction("Index");
        }


    }
}