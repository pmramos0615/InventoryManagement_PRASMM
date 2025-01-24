using InventoryManagement_PRASMM.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement_PRASMM.Controllers
{
    public class ProductsController : Controller
    {
        public void ViewsViewbag(int subscriptionId) {
            ViewBag.Category = ProductCategory.GetBySubscription(subscriptionId);
            ViewBag.SubCategory = ProductCategory.GetSubCategoryBySubscription(subscriptionId);
            ViewBag.Brand = Brands.GetBySubscription(subscriptionId);
            ViewBag.Unit = Units.GetAll();
            ViewBag.Discount = DiscountRate.GetAll();
            ViewBag.Status = ReferenceLookUp.GetByFilter("Product Status");
        }
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            int subscriptionId = Convert.ToInt32(HttpContext.Session.GetInt32("SubscriptionID"));

            List<Products> _list = Products.GetBySuscriptionId(subscriptionId);
            return Json(new { data = _list });
        }
        public ActionResult Details(int ?id)
        {
            if (id.HasValue && id >= 0)
            {
                int idValue = Convert.ToInt32(id);
                Products _details;

                int subscriptionId = Convert.ToInt32(HttpContext.Session.GetInt32("SubscriptionID"));
                ViewsViewbag(subscriptionId);
                if (id == 0)
                {
                    ViewBag.Caption = "Create new product";
                    _details = new Products();
                }
                else
                {
                    ViewBag.Caption = "Edit product";
                    _details = Models.Products.GetById(idValue);
                }
                
                return View(_details);
            }
            else 
            { 
            return RedirectToAction("Index");
            }
        }
        
        public ActionResult Save(Products _details)
        {
            Models.Products _transaction = null;
            
            int subscriptionId =  Convert.ToInt32(HttpContext.Session.GetInt32("SubscriptionID"));
            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));
            string msg = "";

            if (ModelState.IsValid)
            {
                if (_details.ID == 0)
                {
                    _transaction = new Models.Products();
                    _transaction.SubscriptionID = subscriptionId;
                    _transaction.CreatedBy = userId;

                    msg = "Record created successfully!";
                }
                else
                {
                    _transaction = Models.Products.GetById(_details.ID);
                    _transaction.ModifiedBy = userId;
                    msg = "Record updated successfully!";
                }
               
                _transaction.CategoryID = _details.CategoryID;
                _transaction.SubCategoryID = _details.SubCategoryID;
                _transaction.BrandID = _details.BrandID;
                _transaction.UnitID = _details.UnitID;
                _transaction.SKU = _details.SKU;
                _transaction.TaxID = 0;
                _transaction.DiscountRateID = _details.DiscountRateID;
                _transaction.StatusID = _details.StatusID;
                _transaction.MinQty = _details.MinQty;
                _transaction.Qty = _details.Qty;
                _transaction.Name = _details.Name;
                _transaction.Description = _details.Description;
                _transaction.Cost = _details.Cost;
                _transaction.MarkupRate = _details.MarkupRate;
                _transaction.SRP = _details.SRP;

                if (_details.Attachment != null && _details.Attachment.Length > 0) 
                {
                    _transaction.FileName = Path.GetFileName(_details.Attachment.FileName);
                    _transaction.ImageURL = Path.Combine("~/Uploads/Products/", _details.Attachment.FileName);

                }
                
                //if (_details.Attachment != null)
                //{
                //    file = Path.GetFileName(_details.Attachment.FileName).ToString().Replace(" ", "_");
                //    randomfilename = Guid.NewGuid().ToString().Substring(0, 6) + Path.GetExtension(file);
                //    filepath = "~/UploadedFiles/" + randomfilename;

                //    HttpPostedFileBase fileBase = _details.Attachment;
                //    fileBase.SaveAs(Server.MapPath(filepath));

                //    _transaction.FileName = randomfilename;
                //    _transaction.ImageURL = filepath;
                //}

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

            if (Products.Delete(id, userId))
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
