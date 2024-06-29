using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;
using Mohiuddin_EcommerceWebsite.DAL;
using Mohiuddin_EcommerceWebsite.Models;
using System.ComponentModel.DataAnnotations;

namespace Mohiuddin_EcommerceWebsite.DAL
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
        {
            // Generate the user's identity here
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom claims or additional user properties as needed
            return userIdentity;
        }

        public virtual UserProfile UserProfile { get; set; }

        [Phone]
        public new string PhoneNumber { get; set; }
    }
}
