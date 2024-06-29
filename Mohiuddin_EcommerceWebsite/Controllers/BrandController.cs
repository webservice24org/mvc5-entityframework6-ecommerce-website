using Mohiuddin_EcommerceWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Data.Entity;
using System.Web.Mvc;
using Mohiuddin_EcommerceWebsite.DAL;

namespace Mohiuddin_EcommerceWebsite.Controllers
{
    public class BrandController : Controller
    {
        private MohiuddinEcommerceContext db = new MohiuddinEcommerceContext();
        public ActionResult Index()
        {
            var brands = db.Brands.ToList();
            return View(brands);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Brand brand)
        {
            if (ModelState.IsValid)
            {
                db.Brands.Add(brand);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Brand Created successfully.";
                return RedirectToAction("Index"); 
            }
            TempData["ErrorMessage"] = "Brand Not Created!. Please try again.";
            return View(brand); 
        }
        
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Brand brand = db.Brands.Find(id);

            if (brand == null)
            {
                return HttpNotFound();
            }

            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BrandId,BrandName")] Brand brand)
        {
            if (ModelState.IsValid)
            {
                db.Entry(brand).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMessage"] = "Brand updated successfully.";
                return RedirectToAction("Index");
            }
            TempData["ErrorMessage"] = "Error updating brand. Please try again.";
            return View(brand);
        }


        public ActionResult Delete(int? id)
        {
            Brand brand = db.Brands.Find(id);
            db.Entry(brand).State = EntityState.Deleted;
            db.SaveChanges();
            TempData["DeleteMessage"] = "Brand deleted successfully.";
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}