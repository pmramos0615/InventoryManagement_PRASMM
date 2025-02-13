using InventoryManagement_PRASMM.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.IO;

namespace InventoryManagement_PRASMM.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ProductsController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public void ViewsViewbag(int subscriptionId) {
            ViewBag.Category = ProductCategory.GetBySubscription(subscriptionId);
            ViewBag.SubCategory = ProductCategory.GetSubCategoryBySubscription(subscriptionId);
            ViewBag.Brand = Brands.GetBySubscription(subscriptionId);
            ViewBag.Unit = Units.GetAll();
            ViewBag.Discount = DiscountRate.GetAll();
            ViewBag.Status = ReferenceLookUp.GetByFilter("Product Status");
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            
            int subscriptionId = Convert.ToInt32(HttpContext.Session.GetInt32("SubscriptionID"));

            var _list = Products.GetBySuscriptionId(subscriptionId);
            return Json(new { data = _list });
        }
        // <------------------- I am still studying encryption for the parameters. Will be applied by finalization 
        public ActionResult Details(int ?id)
        {
            int subscriptionId = Convert.ToInt32(HttpContext.Session.GetInt32("SubscriptionID"));
            ViewsViewbag(subscriptionId);
            Products _details;
            if (!id.HasValue)     /* <--------------------- This is the Add or Create, the url will be like  hostaddress.com/Details */
            {
                ViewBag.Caption = "Create new product";

                _details = new Products();

                return View(_details);
            }
            else        /* <------------------ This is the  Edit, the url will be like  hostaddress.com/Details/encryptedValue (in the future for now just id)*/
            {
                ViewBag.Caption = "Edit product";

                int idValue = Convert.ToInt32(id);
                _details = Models.Products.GetById(idValue);  
                
                string[] parts = _details.FileName.Split('_');
                string fileNameOnly = parts[^1];

                _details.FileName = fileNameOnly;   /* <------------------ This is to show the filename that the user entered only*/

                return View(_details);
            }
        }
        
        public  ActionResult Save(Products _details)
        {
            Models.Products _transaction = null;
            
            int subscriptionId =  Convert.ToInt32(HttpContext.Session.GetInt32("SubscriptionID"));
            int userId = Convert.ToInt32(HttpContext.Session.GetInt32("UserID"));
            string msg = "";

            if (ModelState.IsValid)
            {
                if (_details.ID == 0)
                {
                    var Utils = new Utils();        
                    
                    _transaction = new Models.Products();
                    _transaction.SubscriptionID = subscriptionId;
                    _transaction.CreatedBy = userId;                                                                                            //get the max ID plus 1 to get the next current ID
                    _transaction.ImageURL = System.IO.Path.Combine(_hostingEnvironment.WebRootPath, "Uploads", "Products", subscriptionId + "_" + (Utils.GetNewTransactionID(1) + 1) + "_" + _details.Attachment.FileName);
                    _transaction.FileName = Path.Combine(subscriptionId + "_" + (Utils.GetNewTransactionID(1) + 1) + "_" + _details.Attachment.FileName);

                    msg = "Record created successfully!";
                }
                else
                {
                    _transaction = Models.Products.GetById(_details.ID);
                    _transaction.ModifiedBy = userId;
                    _transaction.ImageURL = System.IO.Path.Combine(_hostingEnvironment.WebRootPath, "Uploads", "Products", subscriptionId + "_" + _details.ID + "_" + _details.Attachment.FileName);
                    _transaction.FileName = Path.Combine(subscriptionId + "_" + _details.ID + "_" + _details.Attachment.FileName);

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

                string FilePath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads", "Products");
                string fileName = Path.GetFileName(_transaction.ImageURL);
                string fullPath = Path.Combine(FilePath, fileName);

                if (_details.Attachment != null && _details.Attachment.Length > 0)
                {
                    // ----------------------------------- FILE UPLOAD ------------------------------
                    if (!System.IO.File.Exists(FilePath))
                    {
                        System.IO.Directory.CreateDirectory(FilePath);
                    }
                    try
                    {
                        using (var stream = new FileStream(fullPath, FileMode.CreateNew))
                        {
                            _details.Attachment.CopyTo(stream);
                        }
                    }
                    catch (Exception ex)
                    {
                        TempData["error"] = ex.Message;
                        return RedirectToAction("Index");
                    }
                    // ----------------------------------- FILE UPLOAD ------------------------------
                }

                if (_transaction.Save())
                {

                    TempData["success"] = msg;
                    return RedirectToAction("Index");
                }
                else
                {
                    System.IO.File.Delete(fullPath);  // <---------------File deletion save storage if it fail to save.
                    TempData["error"] = "Problem occurred while saving record. Please try again!";
                    ViewsViewbag(subscriptionId);
                    return View("Details", _details);
                }
            }
            else {
                TempData["error"] = "Problem occurred while saving record. Please try again!";
                return RedirectToAction("Index");
            }
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
                msg = "Deleted Successfully";;
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
