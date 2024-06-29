using Mohiuddin_EcommerceWebsite.DAL;
using Mohiuddin_EcommerceWebsite.Models;
using Mohiuddin_EcommerceWebsite.Models.ViewModels;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

using System.Linq;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web;


namespace Mohiuddin_EcommerceWebsite.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        MohiuddinEcommerceContext db = new MohiuddinEcommerceContext();

        public AdminController()
        {
            var context = new MohiuddinEcommerceContext();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            _roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
        }
        

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AllOrders()
        {
            var orders = await db.Orders
                .Include(o => o.OrderDetails.Select(od => od.Product))
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
                OrderDate = o.OrderDate,
                OrderDetails = o.OrderDetails.Select(od => new OrderDetailViewModel
                {
                    ProductName = od.Product.ProductName,
                    Quantity = od.Quantity,
                    Price = od.Price
                }).ToList()
            }).ToList();

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> UpdateOrderStatus(int orderId, OrderStatus orderStatus)
        {
            var order = await db.Orders.FindAsync(orderId);
            if (order == null)
            {
                return HttpNotFound();
            }

            order.OrderStatus = orderStatus;
            await db.SaveChangesAsync();

            return RedirectToAction("AllOrders");
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> AdminOrderDetails(int orderId)
        {
            var order = await db.Orders
                .Include(o => o.OrderDetails.Select(od => od.Product))
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null)
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
                OrderDate = order.OrderDate,
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


        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Customers()
        {
            var role = await _roleManager.FindByNameAsync("Customer");
            if (role == null)
            {
                return HttpNotFound("Role not found");
            }

            var usersInRole = _userManager.Users
                .Where(u => u.Roles.Any(r => r.RoleId == role.Id))
                .ToList();

            var userIds = usersInRole.Select(u => u.Id).ToList();

            var userProfiles = db.UserProfiles
                .Where(up => userIds.Contains(up.UserId))
                .ToList();

            var model = usersInRole.Select(u => new CustomerViewModel
            {
                UserId = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                PicturePath = userProfiles.FirstOrDefault(up => up.UserId == u.Id)?.PicturePath
            }).ToList();

            return View(model);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> CustomerProfile(string customerId)
        {
            var user = await _userManager.FindByIdAsync(customerId);

            if (user == null)
            {
                return HttpNotFound();
            }

            var userProfile = await db.UserProfiles.SingleOrDefaultAsync(up => up.UserId == customerId);

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




    }


}