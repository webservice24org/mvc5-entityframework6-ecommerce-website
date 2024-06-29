﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mohiuddin_EcommerceWebsite.Models.ViewModels
{
    public class CustomerViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PicturePath { get; set; }
    }

}