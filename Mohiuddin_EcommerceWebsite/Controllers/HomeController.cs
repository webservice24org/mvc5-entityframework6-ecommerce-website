using Mohiuddin_EcommerceWebsite.DAL;
using Mohiuddin_EcommerceWebsite.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Threading.Tasks;
using Mohiuddin_EcommerceWebsite.Models;

namespace Mohiuddin_EcommerceWebsite.Controllers
{
    public class HomeController : Controller
    {
        private MohiuddinEcommerceContext db = new MohiuddinEcommerceContext();

        public ActionResult Index()
        {
            var categories = db.ProductCategories
                               .Include(c => c.ProductSubCategories)
                               .ToList();

            var featuredCategories = (from c in db.ProductCategories
                                      where c.CategoryName == "Vegetables" ||
                                            c.CategoryName == "Fish" ||
                                            c.CategoryName == "Meat" ||
                                            c.CategoryName == "Fruits"
                                      select c).ToList();

            foreach (var category in featuredCategories)
            {
                db.Entry(category).Collection(c => c.Products).Load();
            }

            var discountedProducts = (from p in db.Products
                                      where p.DiscountId != null
                                      orderby p.ProductId descending
                                      select p).Take(10).ToList();

            foreach (var product in discountedProducts)
            {
                db.Entry(product).Reference(p => p.Discount).Load();
            }

            var latestProducts = db.Products
                              .OrderByDescending(p => p.ProductId)
                              .Take(8)
                              .ToList();

            var viewModel = new HomePageFeatureViewModel
            {
                Categories = categories,
                FeaturedCategories = featuredCategories,
                DiscountedProducts = discountedProducts,
                LatestProducts = latestProducts
            };

            return View(viewModel);
        }

        public async Task<ActionResult> CategoryProducts(int id)
        {
            var category = await db.ProductCategories
                                  .Include(c => c.Products)
                                  .Include(c => c.ProductSubCategories)
                                  .FirstOrDefaultAsync(c => c.ProductCategoryId == id);

            if (category == null)
            {
                return HttpNotFound();
            }

            var allCategories = await db.ProductCategories
                                        .Include(c => c.ProductSubCategories)
                                        .ToListAsync();

            var model = new CategoryProductsViewModel
            {
                CategoryName = category.CategoryName,
                Products = category.Products.ToList(),
                Categories = allCategories 
            };

            return View(model);
        }


        public async Task<ActionResult> SubCategoryProducts(int id)
        {
            var subCategory = await db.ProductSubCategories
                                      .Include(sc => sc.Products)
                                      .Include(sc => sc.ProductCategory)
                                      .FirstOrDefaultAsync(sc => sc.ProductSubCategoryId == id);

            if (subCategory == null)
            {
                return HttpNotFound();
            }

            var allCategories = await db.ProductCategories
                                        .Include(c => c.ProductSubCategories)
                                        .ToListAsync();

            var model = new SubCategoryProductsViewModel
            {
                SubCategoryName = subCategory.SubCategoryName,
                Products = subCategory.Products.ToList(),
                Categories = allCategories 
            };

            return View(model);
        }


        public async Task<ActionResult> Shop(int? page)
        {
            int pageSize = 12;

            var productsQuery = db.Products.OrderByDescending(p => p.ProductId);

            var paginatedProducts = await PaginatedList<Product>.CreateAsync(productsQuery, page ?? 1, pageSize);


            return View(paginatedProducts);
        }


        public async Task<ActionResult> ProductDetails(int id)
        {
            var product = await db.Products
                                 .Include(p => p.ProductCategory)
                                 .Include(p => p.ProductCategory.ProductSubCategories)
                                 .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }


        public async Task<ActionResult> LiveSearch(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return Json(new List<Product>(), JsonRequestBehavior.AllowGet);
            }

            var products = await db.Products
                .Where(p => p.ProductName.Contains(searchTerm))
                .Select(p => new {
                    p.ProductId,
                    p.ProductName,
                    p.ImageUrl
                })
                .ToListAsync();

            

            return Json(products, JsonRequestBehavior.AllowGet);
        }





    }





}