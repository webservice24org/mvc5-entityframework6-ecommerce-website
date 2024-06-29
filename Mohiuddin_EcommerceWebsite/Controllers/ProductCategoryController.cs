using Mohiuddin_EcommerceWebsite.DAL;
using Mohiuddin_EcommerceWebsite.Models.ViewModels;
using Mohiuddin_EcommerceWebsite.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Mohiuddin_EcommerceWebsite.Controllers
{
    public class ProductCategoryController : Controller
    {
        private MohiuddinEcommerceContext db = new MohiuddinEcommerceContext();
        public ActionResult Index()
        {
            var cats = db.ProductCategories.ToList();
            return View(cats);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                string imagePath = null;
                if (model.ImageFile != null && model.ImageFile.ContentLength > 0)
                {
                    var timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    var fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
                    var fileExtension = Path.GetExtension(model.ImageFile.FileName);
                    var newFileName = $"{fileName}_{timestamp}{fileExtension}";
                    var path = Path.Combine(Server.MapPath("~/Images/product-category"), newFileName);
                    model.ImageFile.SaveAs(path);
                    imagePath = "~/Images/product-category/" + newFileName;
                }


                var category = new ProductCategory
                {
                    CategoryName = model.CategoryName,
                    CategoryImage = imagePath
                };

                db.ProductCategories.Add(category);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Category Created successfully";
                return RedirectToAction("Index");
            }

            return View(model);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var category = db.ProductCategories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            var model = new ProductCategoryViewModel
            {
                ProductCategoryId = category.ProductCategoryId,
                CategoryName = category.CategoryName,
                CategoryImage = category.CategoryImage
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = db.ProductCategories.Find(model.ProductCategoryId);
                if (category == null)
                {
                    return HttpNotFound();
                }

                string imagePath = category.CategoryImage;

                if (model.ImageFile != null && model.ImageFile.ContentLength > 0)
                {
                    if (!string.IsNullOrEmpty(category.CategoryImage))
                    {
                        var oldImagePath = Server.MapPath(category.CategoryImage);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    var timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    var fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
                    var fileExtension = Path.GetExtension(model.ImageFile.FileName);
                    var newFileName = $"{fileName}_{timestamp}{fileExtension}";
                    var path = Path.Combine(Server.MapPath("~/Images/product-category"), newFileName);
                    model.ImageFile.SaveAs(path);
                    imagePath = "~/Images/product-category/" + newFileName;
                }

                category.CategoryName = model.CategoryName;
                category.CategoryImage = imagePath;

                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMessage"] = "Category Updated successfully";
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var category = db.ProductCategories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var category = db.ProductCategories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }

            if (!string.IsNullOrEmpty(category.CategoryImage))
            {
                var imagePath = Server.MapPath(category.CategoryImage);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            db.ProductCategories.Remove(category);
            TempData["DeleteMessage"] = "Category deleted successfully.";
            db.SaveChanges();

            return RedirectToAction("Index");
        }


    }
}