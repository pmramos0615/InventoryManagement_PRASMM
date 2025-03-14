using System.Numerics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using NuGet.Protocol;

namespace InventoryManagement_PRASMM.Controllers
{
    public class AuthenticationController : Controller // Inherit from Controller
    {
        public IActionResult Home() 
        {
            return View(); 
        }
        public IActionResult Login(string UserName, string Password)
        {
            bool isExist = Models.Users.Login(UserName, Password);
            if (isExist == true)
            {
                var model = Models.Users.GetUserByLogin(UserName, Password);
                var description = Models.Users.GetUserDescriptionByLogin(model.UserName, model.Password); ;
                HttpContext.Session.SetInt32("UserID", model.ID);
                HttpContext.Session.SetInt32("SubscriptionID", model.SubscriptionID);
                HttpContext.Session.SetInt32("UserRoleID", model.UserRoleID);
                HttpContext.Session.SetInt32("DepartmentID", model.DepartmentID);
                HttpContext.Session.SetString("Fullname", model.FullName);
                HttpContext.Session.SetString("Department", description.Department);
                HttpContext.Session.SetString("UserRole", description.UserRole);
                HttpContext.Session.SetString("Subscriber", description.Subscriber);
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                if (string.IsNullOrEmpty(UserName) && string.IsNullOrEmpty(Password))
                {
                    ViewBag.NotExist = "Authentication is required to login.";
                }
                else if (string.IsNullOrEmpty(UserName))
                {
                    ViewBag.NotExist = "Enter your username. ";
                }
                else if (string.IsNullOrEmpty(Password))
                {
                    ViewBag.NotExist = "Enter your password. ";
                }
                else
                {
                    ViewBag.NotExist = "Invalid credentials. Please try again.";
                }
                return View("Home");
            }
        }
    }
}
