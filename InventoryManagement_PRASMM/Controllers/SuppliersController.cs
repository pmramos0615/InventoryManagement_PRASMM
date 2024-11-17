using InventoryManagement_PRASMM.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement_PRASMM.Controllers
{
    public class SuppliersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult List()
        {
            //List<Suppliers> _list = Suppliers.GetAll();
            int subscriptionId = Convert.ToInt32(HttpContext.Session.GetInt32("SubscriptionID"));
            List<Suppliers> _list = Suppliers.GetBySubscriptionID(subscriptionId);
            
            return Json(new { data = _list });
        }
        public ActionResult Details(int id)
        {
            Suppliers _details;

            ViewBag.Stores = Stores.GetAll();
            ViewBag.PaymentTerms = PaymentTerms.GetAll();


            if (id == 0)
            {
                ViewBag.Caption = "Create new supplier";
                _details = new Suppliers();
            }
            else
            {
                ViewBag.Caption = "Edit supplier";
                _details = Models.Suppliers.GetById(id);
            }

            return View(_details);

        }
        public ActionResult Save(Suppliers _details)
        {
            Models.Suppliers _transaction = null;
            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));
            int subscriptionId = Convert.ToInt32(HttpContext.Session.GetInt32("SubscriptionID"));

            string msg = "";

            if (ModelState.IsValid)
            {
                if (_details.ID == 0)
                {
                    _transaction = new Models.Suppliers();
                    _transaction.CreatedBy = Convert.ToInt32(userId);
                    _transaction.SubscriptionID = subscriptionId;
                    msg = "Record created successfully!";
                }
                else
                {
                    _transaction = Models.Suppliers.GetById(_details.ID);
                    _transaction.ModifiedBy = Convert.ToInt32(userId);
                    msg = "Record updated successfully!";
                }

                _transaction.Name = _details.Name;
                _transaction.Address1 = _details.Address1;
                _transaction.Address2 = _details.Address2;
                _transaction.TermID = _details.TermID;
                _transaction.ContactNo = _details.ContactNo;
                _transaction.ContactPerson = _details.ContactPerson;
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

            if (Suppliers.Delete(id, userId))
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
