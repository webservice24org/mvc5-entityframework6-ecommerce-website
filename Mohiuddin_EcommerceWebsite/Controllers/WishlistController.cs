﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mohiuddin_EcommerceWebsite.Controllers
{
    public class WishlistController : Controller
    {
        // GET: Wishlist
        public ActionResult Index()
        {
            return View();
        }
    }
}