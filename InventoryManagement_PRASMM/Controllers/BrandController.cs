using InventoryManagement_PRASMM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace InventoryManagement_PRASMM.Controllers
{
    public class BrandController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult List()
        {
            int subscriptionId = Convert.ToInt32(HttpContext.Session.GetInt32("SubscriptionID"));
            List<Brands> _list = Brands.GetBySubscription(subscriptionId);
            return Json(new { data = _list });
        }

        public ActionResult Details(int id)
        {
            Brands _details;



            if (id == 0)
            {
                ViewBag.Caption = "Create new Brand";
                _details = new Brands();
            }
            else
            {
                ViewBag.Caption = "Edit Brand";
                _details = Models.Brands.GetById(id);
            }

            return View(_details);

        }

        public ActionResult Save(Brands _details)
        {
            Models.Brands _transaction = null;
            int userId =Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));
            int subscriptionId = Convert.ToInt32(HttpContext.Session.GetInt32("SubscriptionID"));

            string msg = "";

            if (ModelState.IsValid)
            {
                if (_details.ID == 0)
                {
                    _transaction = new Models.Brands();
                   _transaction.SubscriptionID= subscriptionId; 

                    _transaction.CreatedBy = Convert.ToInt32(userId);
                    msg = "Record created successfully!";
                }
                else
                {
                    _transaction = Models.Brands.GetById(_details.ID);
                    _transaction.ModifiedBy = Convert.ToInt32(userId);
                    msg = "Record updated successfully!";
                }

                _transaction.Name = _details.Name;
                _transaction.Description = _details.Description;


                if (_transaction.Save())
                {
                    TempData["success"] = msg;
                }
                else
                {
                    TempData["error"] = "Problem occurred while saving record. Please try again!";
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            bool status = false;
            string msg = "";
            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));

            if (Brands.Delete(id, userId))
            {
                status = true;
                msg = "Deleted Successfully";
            }
            else
            {
                status = false;
                msg = "Delete Failed";
            }


            return Json(new { status = status });
        }
    }
}
