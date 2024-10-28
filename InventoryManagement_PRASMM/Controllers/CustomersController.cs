using InventoryManagement_PRASMM.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement_PRASMM.Controllers
{
    public class CustomersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult List()
        {
            List<Customers> _list = Customers.GetAll();
            return Json(new { data = _list });
        }
        public ActionResult Details(int id)
        {
            Customers _details;

            ViewBag.Stores = Stores.GetAll();
            ViewBag.PaymentTerms = PaymentTerms.GetAll();


            if (id == 0)
            {
                ViewBag.Caption = "Create new customer";
                _details = new Customers();
            }
            else
            {
                ViewBag.Caption = "Edit customer";
                _details = Models.Customers.GetById(id);
            }

            return View(_details);

        }
        public ActionResult Save(Customers _details)
        {
            Models.Customers _transaction = null;
            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));
            int storeId = Convert.ToInt32(HttpContext.Session.GetInt32("StoreID"));

            string msg = "";

            if (ModelState.IsValid)
            {
                if (_details.ID == 0)
                {
                    _transaction = new Models.Customers();


                    _transaction.CreatedBy = Convert.ToInt32(userId);
                    _transaction.StoreID = storeId;

                    msg = "Record created successfully!";
                }
                else
                {
                    _transaction = Models.Customers.GetById(_details.ID);
                    _transaction.ModifiedBy = Convert.ToInt32(userId);
                    _transaction.StoreID = _details.StoreID;
                    msg = "Record updated successfully!";
                }
             
                _transaction.Name = _details.Name;
                _transaction.Address1 = _details.Address1;
                _transaction.Address2 = _details.Address2;
                _transaction.TermID = _details.TermID;
                _transaction.ContactNo= _details.ContactNo;
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

            if (Customers.Delete(id, userId))
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
