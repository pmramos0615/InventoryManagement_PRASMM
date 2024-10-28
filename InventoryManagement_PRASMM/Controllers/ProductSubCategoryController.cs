using InventoryManagement_PRASMM.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement_PRASMM.Controllers
{
    public class ProductSubCategoryController : Controller
    {
        // GET: ProductSubCategory
        


        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult List()
        {
            List<ProductSubCategory> _list = ProductSubCategory.GetAll();
            return Json(new { data = _list });
        }
        public ActionResult Details(int id)
        {
            ProductSubCategory _details;

            ViewBag.Category = ProductCategory.GetAll();

            if (id == 0)
            {
                ViewBag.Caption = "Create new product Category";
                _details = new ProductSubCategory();
            }
            else
            {
                ViewBag.Caption = "Edit product Category";
                _details = Models.ProductSubCategory.GetById(id);
            }

            return View(_details);

        }

        public ActionResult Save(ProductSubCategory _details)
        {
            Models.ProductSubCategory _transaction = null;
            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));
            string msg = "";

            if (ModelState.IsValid)
            {
                if (_details.ID == 0)
                {
                    _transaction = new Models.ProductSubCategory();
                    _transaction.CreatedBy = Convert.ToInt32(userId);
                    msg = "Record created successfully!";
                }
                else
                {
                    _transaction = Models.ProductSubCategory.GetById(_details.ID);
                    _transaction.ModifiedBy = Convert.ToInt32(userId);
                    msg = "Record updated successfully!";
                }
                _transaction.CategoryID = _details.CategoryID;
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
        public ActionResult Delete(int id)
        {
            bool status = false;
            string msg = "";
            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));

            if (ProductSubCategory.Delete(id, Convert.ToInt32(userId)))
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
