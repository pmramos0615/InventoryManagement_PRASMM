using InventoryManagement_PRASMM.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement_PRASMM.Controllers
{
    public class StoresController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            //   List<Stores> _list = Stores.GetAll();
            int subscriptionId = Convert.ToInt32(HttpContext.Session.GetInt32("SubscriptionID"));
            List<Stores> _list = Stores.GetBySubscription(subscriptionId);
            return Json(new { data = _list });
        }

        public ActionResult Details(int id)
        {
            Stores _details;

            int subscriptionId = Convert.ToInt32(HttpContext.Session.GetInt32("SubscriptionID"));
            ViewBag.Employees = Users.GetBySubscription(subscriptionId);
           

            if (id == 0)
            {
                ViewBag.Caption = "Create new store";
                _details = new Stores();
            }
            else
            {
                ViewBag.Caption = "Edit store";
                _details = Models.Stores.GetById(id);
            }

            return View(_details);

        }
        public ActionResult Save(Stores _details)
        {
            Models.Stores _transaction = null;
            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));
            int subscriptionId = Convert.ToInt32(HttpContext.Session.GetInt32("SubscriptionID"));

            string msg = "";

            if (ModelState.IsValid)
            {
                if (_details.ID == 0)
                {
                    _transaction = new Models.Stores();
                    _transaction.CreatedBy = Convert.ToInt32(userId);
                    _transaction.SubscriptionID = subscriptionId;
                    msg = "Record created successfully!";
                }
                else
                {
                    _transaction = Models.Stores.GetById(_details.ID);
                    _transaction.ModifiedBy = Convert.ToInt32(userId);
                    msg = "Record updated successfully!";
                }

                _transaction.Name = _details.Name;
                _transaction.Address = _details.Address;
                _transaction.EmployeeID = _details.EmployeeID;
                _transaction.ContactNo = _details.ContactNo;
                _transaction.EmailAddress = _details.EmailAddress;


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

            if (Stores.Delete(id, userId))
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
