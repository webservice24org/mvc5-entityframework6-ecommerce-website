using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mohiuddin_EcommerceWebsite.Models.ViewModels
{
    public class ProfileViewModel
    {

        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string Thana { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string PicturePath { get; set; }
    }

}