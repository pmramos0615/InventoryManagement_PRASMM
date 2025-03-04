﻿using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement_PRASMM.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            HttpContext.Session.SetInt32("SubscriptionID", 1); /*PMR Enterprise*/
            HttpContext.Session.SetInt32("UserID", 1);
            HttpContext.Session.SetInt32("StoreID", 3); /*Head Office*/

            return View();
        }
    }
}
