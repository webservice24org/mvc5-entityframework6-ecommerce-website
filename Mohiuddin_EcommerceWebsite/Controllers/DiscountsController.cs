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
    public class DiscountsController : Controller
    {
        private MohiuddinEcommerceContext db = new MohiuddinEcommerceContext();

        public ActionResult Index()
        {
            return View(db.Discounts.ToList());
        }

       

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DiscountId,Name,Percentage")] Discount discount)
        {
            if (ModelState.IsValid)
            {
                db.Discounts.Add(discount);
                db.SaveChanges();
                TempData["SuccessMessage"] = "Discount Created successfully.";
                return RedirectToAction("Index");
            }
            TempData["ErrorMessage"] = "Error Creating Discount. Please try again.";
            return View(discount);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discount discount = db.Discounts.Find(id);
            if (discount == null)
            {
                return HttpNotFound();
            }
            return View(discount);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DiscountId,Name,Percentage")] Discount discount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(discount).State = EntityState.Modified;
                db.SaveChanges();
                TempData["SuccessMessage"] = "Discount updated successfully.";
                return RedirectToAction("Index");
            }
            TempData["ErrorMessage"] = "Error updating Discount. Please try again.";
            return View(discount);
        }


        public ActionResult Delete(int? id)
        {
            Discount discount = db.Discounts.Find(id);
            db.Entry(discount).State = EntityState.Deleted;
            db.SaveChanges();
            TempData["DeleteMessage"] = "Discount deleted successfully.";
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
