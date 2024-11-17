using InventoryManagement_PRASMM.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement_PRASMM.Controllers
{
    public class CategoryController : Controller
    {
      
        // GET: Category
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult List()
        {
            int subscriptionId = Convert.ToInt32(HttpContext.Session.GetInt32("SubscriptionID"));
            List<ProductCategory> _list = ProductCategory.GetBySubscription(subscriptionId);
            return Json(new { data = _list });
        }

        public IActionResult Details(int id)
        {
            ProductCategory _details;



            if (id == 0)
            {
                ViewBag.Caption = "Create new product Category";
                _details = new ProductCategory();
            }
            else
            {
                ViewBag.Caption = "Edit product Category";
                _details = Models.ProductCategory.GetById(id);
            }

            return View(_details);

        }

        public IActionResult Save(ProductCategory _details)
        {
            Models.ProductCategory _transaction = null;
            int subscriptionId = Convert.ToInt32(HttpContext.Session.GetInt32("SubscriptionID"));
            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));
            string msg = "";

            if (ModelState.IsValid)
            {
                if (_details.ID == 0)
                {
                    _transaction = new Models.ProductCategory();
                    _transaction.CreatedBy = userId;
                    _transaction.SubscriptionID = subscriptionId;
                    msg = "Record created successfully!";
                }
                else
                {
                    _transaction = Models.ProductCategory.GetById(_details.ID);
                    _transaction.ModifiedBy = userId;
                    msg = "Record updated successfully!";
                }

                _transaction.Name = _details.Name;
                _transaction.Code = _details.Code;
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
        public IActionResult Delete(int id)
        {
            bool status = false;
            string msg = "";
            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));

            if (ProductCategory.Delete(id, userId))
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
