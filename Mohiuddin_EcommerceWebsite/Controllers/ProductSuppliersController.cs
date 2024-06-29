using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mohiuddin_EcommerceWebsite.DAL;
using Mohiuddin_EcommerceWebsite.Models;

namespace Mohiuddin_EcommerceWebsite.Controllers
{
    public class ProductSuppliersController : Controller
    {
        private MohiuddinEcommerceContext db = new MohiuddinEcommerceContext();

        public ActionResult Index()
        {
            return View(db.ProductSuppliers.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSupplier productSupplier = db.ProductSuppliers.Find(id);
            if (productSupplier == null)
            {
                return HttpNotFound();
            }
            return View(productSupplier);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductSupplierId,SupplierName,ContactEmail,ContactPhone,Address")] ProductSupplier productSupplier)
        {
            if (ModelState.IsValid)
            {
                db.ProductSuppliers.Add(productSupplier);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Supplier Added successfully.";
                return RedirectToAction("Index");
            }
            TempData["ErrorMessage"] = "Error Adding Supplier. Please try again.";
            return View(productSupplier);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSupplier productSupplier = db.ProductSuppliers.Find(id);
            if (productSupplier == null)
            {
                return HttpNotFound();
            }
            return View(productSupplier);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductSupplierId,SupplierName,ContactEmail,ContactPhone,Address")] ProductSupplier productSupplier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productSupplier).State = EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMessage"] = "Supplier updated successfully.";
                return RedirectToAction("Index");
            }
            TempData["ErrorMessage"] = "Error updating Supplier. Please try again.";
            return View(productSupplier);
        }


        public ActionResult Delete(int? id)
        {
            ProductSupplier supplier = db.ProductSuppliers.Find(id);
            db.Entry(supplier).State = EntityState.Deleted;
            db.SaveChanges();
            TempData["DeleteMessage"] = "Supplier deleted successfully.";
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
