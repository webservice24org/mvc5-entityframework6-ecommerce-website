using Mohiuddin_EcommerceWebsite.DAL;
using Mohiuddin_EcommerceWebsite.Models.ViewModels;
using Mohiuddin_EcommerceWebsite.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Drawing.Drawing2D;

namespace Mohiuddin_EcommerceWebsite.Controllers
{
    public class ProductSubCategoryController : Controller
    {
        private MohiuddinEcommerceContext db = new MohiuddinEcommerceContext();
        public ActionResult Index()
        {
            return View(db.ProductSubCategories.ToList());
        }

        public ActionResult Create()
        {
            var model = new ProductSubCategoryViewModel
            {
                ProductCategories = db.ProductCategories.ToList() 
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductSubCategoryViewModel model)
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
                    var path = Path.Combine(Server.MapPath("~/Images/product-subcategory"), newFileName);
                    model.ImageFile.SaveAs(path);
                    imagePath = "~/Images/product-subcategory/" + newFileName;
                }

                var subCategory = new ProductSubCategory
                {
                    SubCategoryName = model.SubCategoryName,
                    SubCategoryImg = imagePath,
                    ProductCategoryId = model.ProductCategoryId
                };

                db.ProductSubCategories.Add(subCategory);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Category Created successfully";
                return RedirectToAction("Index");
            }

            model.ProductCategories = db.ProductCategories.ToList();
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var subCategory = db.ProductSubCategories.Find(id);
            if (subCategory == null)
            {
                return HttpNotFound();
            }

            var model = new ProductSubCategoryViewModel
            {
                ProductSubCategoryId = subCategory.ProductSubCategoryId,
                SubCategoryName = subCategory.SubCategoryName,
                SubCategoryImg = subCategory.SubCategoryImg,
                ProductCategoryId = subCategory.ProductCategoryId,
                ProductCategories = db.ProductCategories.ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductSubCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var subCategory = db.ProductSubCategories.Find(model.ProductSubCategoryId);
                if (subCategory == null)
                {
                    return HttpNotFound();
                }

                string imagePath = subCategory.SubCategoryImg;
                if (model.ImageFile != null && model.ImageFile.ContentLength > 0)
                {
                    var timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    var fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
                    var fileExtension = Path.GetExtension(model.ImageFile.FileName);
                    var newFileName = $"{fileName}_{timestamp}{fileExtension}";
                    var path = Path.Combine(Server.MapPath("~/Images/product-subcategory"), newFileName);
                    model.ImageFile.SaveAs(path);
                    imagePath = "~/Images/product-subcategory/" + newFileName;

                    if (!string.IsNullOrEmpty(subCategory.SubCategoryImg))
                    {
                        var oldImagePath = Server.MapPath(subCategory.SubCategoryImg);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                }

                subCategory.SubCategoryName = model.SubCategoryName;
                subCategory.SubCategoryImg = imagePath;
                subCategory.ProductCategoryId = model.ProductCategoryId;

                db.Entry(subCategory).State = EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMessage"] = "Category Updated successfully";
                return RedirectToAction("Index");
            }

            model.ProductCategories = db.ProductCategories.ToList();
            return View(model);
        }


        public ActionResult Delete(int id)
        {
            var subCategory = db.ProductSubCategories.Find(id);
            if (subCategory == null)
            {
                return HttpNotFound();
            }

            if (!string.IsNullOrEmpty(subCategory.SubCategoryImg))
            {
                var imagePath = Server.MapPath(subCategory.SubCategoryImg);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            db.Entry(subCategory).State = EntityState.Deleted;
            db.SaveChanges();
            TempData["DeleteMessage"] = "Sub Category deleted successfully.";
            return RedirectToAction("Index");
        }


    }
}