using Mohiuddin_EcommerceWebsite.DAL;
using Mohiuddin_EcommerceWebsite.Models;
using Mohiuddin_EcommerceWebsite.Models.ViewModels;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.EntityFramework;
using System.IO;
using System.Linq;
using System.Data.Entity;
using System;


namespace Mohiuddin_EcommerceWebsite.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        MohiuddinEcommerceContext db = new MohiuddinEcommerceContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, RoleManager<IdentityRole> roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _roleManager = roleManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var signInManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();

            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    var user = await UserManager.FindByNameAsync(model.Email);
                    var roles = await UserManager.GetRolesAsync(user.Id);

                    if (roles.Contains("Admin"))
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (roles.Contains("Customer"))
                    {
                        return RedirectToAction("Index", "Cart");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }

                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                };

                var result = await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var userProfile = new UserProfile
                    {
                        UserId = user.Id,
                    };

                    using (var context = new MohiuddinEcommerceContext())
                    {
                        context.UserProfiles.Add(userProfile);
                        await context.SaveChangesAsync();
                    }

                    var roleResult = await UserManager.AddToRoleAsync(user.Id, "Customer");
                    if (!roleResult.Succeeded)
                    {
                        AddErrors(roleResult);
                        return View(model);
                    }

                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    return RedirectToAction("Login", "Account");
                }
                AddErrors(result);
            }

            return View(model);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<ActionResult> EditProfile()
        {
            var userId = User.Identity.GetUserId();
            var user = await UserManager.FindByIdAsync(userId);

            if (user == null)
            {
                return HttpNotFound();
            }

            var userProfile = user.UserProfile ?? new UserProfile(); 

            var model = new EditProfileViewModel
            {
                FirstName = userProfile.FirstName,
                LastName = userProfile.LastName,
                State = userProfile.State,
                District = userProfile.District,
                Thana = userProfile.Thana,
                PostalCode = userProfile.PostalCode,
                Address = userProfile.Address,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditProfile(EditProfileViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = User.Identity.GetUserId();
            var user = await UserManager.FindByIdAsync(userId);

            if (user == null)
            {
                return HttpNotFound();
            }

            using (var context = new MohiuddinEcommerceContext())
            {
                var userProfile = context.UserProfiles.SingleOrDefault(up => up.UserId == userId) ?? new UserProfile { UserId = userId };

                userProfile.FirstName = model.FirstName;
                userProfile.LastName = model.LastName;
                userProfile.State = model.State;
                userProfile.District = model.District;
                userProfile.Thana = model.Thana;
                userProfile.PostalCode = model.PostalCode;
                userProfile.Address = model.Address;

                if (model.Picture != null && model.Picture.ContentLength > 0)
                {

                    if (!string.IsNullOrEmpty(userProfile.PicturePath))
                    {
                        var oldPicturePath = Server.MapPath(userProfile.PicturePath);
                        if (System.IO.File.Exists(oldPicturePath))
                        {
                            System.IO.File.Delete(oldPicturePath);
                        }
                    }

                    var timestamp = DateTime.Now.ToString("yyyyMMdd");
                    var fileName = Path.GetFileNameWithoutExtension(model.Picture.FileName);
                    var fileExtension = Path.GetExtension(model.Picture.FileName);
                    var newFileName = $"{fileName}_{timestamp}{fileExtension}";
                    var path = Path.Combine(Server.MapPath("~/Images/Users"), newFileName);
                    model.Picture.SaveAs(path);

                    userProfile.PicturePath = "~/Images/Users/" + newFileName;
                }

                user.UserName = model.Email; 
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;


                if (context.Entry(userProfile).State == System.Data.Entity.EntityState.Detached)
                {
                    context.UserProfiles.Add(userProfile);
                }
                context.SaveChanges();
            }


            var result = await UserManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("Profile", "Account");
            }

            AddErrors(result);
            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> Profile()
        {
            var userId = User.Identity.GetUserId();
            var user = await UserManager.FindByIdAsync(userId);

            if (user == null)
            {
                return HttpNotFound();
            }

            var userProfile = await db.UserProfiles.SingleOrDefaultAsync(up => up.UserId == userId);

            var model = new ProfileViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = userProfile?.FirstName,
                LastName = userProfile?.LastName,
                State = userProfile?.State,
                District = userProfile?.District,
                Thana = userProfile?.Thana,
                PostalCode = userProfile?.PostalCode,
                Address = userProfile?.Address,
                PicturePath = userProfile?.PicturePath
            };

            return View(model);
        }


        [Authorize]
        public async Task<ActionResult> MyOrders()
        {
            var userId = User.Identity.GetUserId();
            var orders = await db.Orders
                .Include(o => o.OrderDetails.Select(od => od.Product))
                .Where(o => o.UserId == userId)
                .ToListAsync();

            var model = orders.Select(o => new OrderViewModel
            {
                OrderId = o.OrderId,
                Name = o.Name,
                Mobile = o.Mobile,
                ShippingAddress = o.ShippingAddress,
                OrderNote = o.OrderNote,
                TotalPayable = o.TotalPayable,
                ShippingArea = o.ShippingArea,
                OrderStatus = o.OrderStatus, 
                OrderDetails = o.OrderDetails.Select(od => new OrderDetailViewModel
                {
                    ProductName = od.Product.ProductName,
                    Quantity = od.Quantity,
                    Price = od.Price
                }).ToList()
            }).ToList();

            return View(model);
        }


        [HttpGet]
        public async Task<ActionResult> CancelOrder(int id)
        {
            var order = await db.Orders.Include(o => o.OrderDetails.Select(od => od.Product)).FirstOrDefaultAsync(o => o.OrderId == id);
            if (order == null)
            {
                return HttpNotFound();
            }

            var orderDetail = order.OrderDetails.FirstOrDefault();
            if (orderDetail == null)
            {
                return HttpNotFound();
            }

            var cancelOrderViewModel = new CancelOrderViewModel
            {
                OrderId = id,
                ProductName = orderDetail.Product.ProductName,
                ImageUrl = orderDetail.Product.ImageUrl, 
                Quantity = orderDetail.Quantity,
                TotalPayable = order.TotalPayable
            };

            return View(cancelOrderViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CancelOrder(CancelOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var order = await db.Orders.FindAsync(model.OrderId);
                if (order == null)
                {
                    return HttpNotFound();
                }

                order.OrderStatus = OrderStatus.Cancelled; 
                db.Entry(order).State = EntityState.Modified;

                var orderCancel = new OrderCancel
                {
                    OrderId = model.OrderId,
                    CancelDate = DateTime.Now,
                    Reason = model.Reason
                };
                db.OrderCancels.Add(orderCancel);

                await db.SaveChangesAsync();

                return RedirectToAction("MyOrders");
            }

            
            var userId = User.Identity.GetUserId();
            var orderDetails = await db.Orders
                .Include(o => o.OrderDetails.Select(od => od.Product))
                .Where(o => o.UserId == userId && o.OrderId == model.OrderId)
                .Select(o => new CancelOrderViewModel
                {
                    OrderId = o.OrderId,
                    Reason = model.Reason,
                    OrderDetails = o.OrderDetails.Select(od => new OrderDetailViewModel
                    {
                        ProductName = od.Product.ProductName,
                        Quantity = od.Quantity,
                        Price = od.Price
                    }).ToList()
                }).FirstOrDefaultAsync();

            return View(orderDetails);
        }


        public async Task<ActionResult> OrderDetails(int orderId)
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
                OrderDate=order.OrderDate,
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