using Mohiuddin_EcommerceWebsite.DAL;
using Mohiuddin_EcommerceWebsite.Models;
using Mohiuddin_EcommerceWebsite.Models.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Mvc;

namespace Mohiuddin_EcommerceWebsite.Controllers
{
    public class OrderController : Controller
    {
        private MohiuddinEcommerceContext db = new MohiuddinEcommerceContext();

        [HttpGet]
        public ActionResult PlaceOrder()
        {
            var cartItems = Session["CartItems"] as List<CartItem>;
            if (cartItems == null || !cartItems.Any())
            {
                return RedirectToAction("Index", "Cart");
            }

            var orderViewModel = new OrderViewModel
            {
                OrderDetails = cartItems.Select(item => new OrderDetailViewModel
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    Subtotal = item.Quantity * item.Price
                }).ToList()
            };

            return View(orderViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PlaceOrder(OrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var cartItems = Session["CartItems"] as List<CartItem>;
                if (cartItems != null)
                {
                    foreach (var item in cartItems)
                    {
                        var product = await db.Products.FindAsync(item.ProductId);
                        model.OrderDetails.Add(new OrderDetailViewModel
                        {
                            ProductId = item.ProductId,
                            ProductName = product.ProductName,
                            Quantity = item.Quantity,
                            Price = item.Price,
                            Subtotal = item.Quantity * item.Price
                        });
                    }
                }

                model.TotalPayable = model.OrderDetails.Sum(od => od.Subtotal) + (model.ShippingArea == "InsideDhaka" ? 100 : 200);

                return View(model);
            }

            try
            {
                decimal shippingCost = model.ShippingArea == "InsideDhaka" ? 100 : 200;
                decimal subtotal = model.OrderDetails.Sum(item => item.Quantity * item.Price);
                decimal totalPayable = subtotal + shippingCost;

                string userId = User.Identity.GetUserId();

                var order = new Order
                {
                    UserId = userId,
                    Name = model.Name,
                    Mobile = model.Mobile,
                    ShippingAddress = model.ShippingAddress,
                    OrderNote = model.OrderNote,
                    TotalPayable = totalPayable,
                    ShippingArea = model.ShippingArea,
                    PaymentMethod = "CashOnDelivery",
                    OrderDate = DateTime.Now,
                    OrderDetails = new List<OrderDetail>()
                };

                foreach (var od in model.OrderDetails)
                {
                    var orderDetail = new OrderDetail
                    {
                        ProductId = od.ProductId,
                        Quantity = od.Quantity,
                        Price = od.Price,
                        Subtotal = od.Subtotal,
                        Order = order
                    };
                    order.OrderDetails.Add(orderDetail);
                }

                db.Orders.Add(order);
                await db.SaveChangesAsync();

                Session["CartItems"] = new List<CartItem>();

                return RedirectToAction("OrderConfirmation", new { orderId = order.OrderId });
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occurred while placing the order. Please try again.");
                return View(model);
            }
        }




        public async Task<ActionResult> OrderConfirmation(int orderId)
        {
            var order = await db.Orders
                .Include(o => o.OrderDetails.Select(od => od.Product))
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null || order.UserId != User.Identity.GetUserId())
            {
                return HttpNotFound();
            }

            var viewModel = new OrderConfirmationViewModel
            {
                OrderId = order.OrderId,
                Name = order.Name,
                Mobile = order.Mobile,
                ShippingAddress = order.ShippingAddress,
                OrderNote = order.OrderNote,
                TotalPayable = order.TotalPayable,
                ShippingArea = order.ShippingArea,
                PaymentMethod = order.PaymentMethod,
                OrderDetails = order.OrderDetails.Select(od => new OrderDetailViewModel
                {
                    ProductName = od.Product.ProductName,
                    Quantity = od.Quantity,
                    Price = od.Price,
                    Subtotal = od.Subtotal
                }).ToList()
            };

            return View(viewModel);
        }




    }
}