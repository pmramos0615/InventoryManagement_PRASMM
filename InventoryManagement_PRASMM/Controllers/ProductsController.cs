using Humanizer;
using InventoryManagement_PRASMM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            ViewBag.Brand = Brands.GetBySubscription(subscriptionId);
            ViewBag.Unit = Units.GetAllBySubscriptionID(subscriptionId);
            ViewBag.Status = ReferenceLookUp.GetByFilter("Product Status");
            ViewBag.TaxTypes = TaxType.GetAll(subscriptionId);
            ViewBag.Tax = Models.Taxes.GetAll(subscriptionId);
            ViewBag.Store = Models.Stores.GetBySubscription(subscriptionId);   
            ViewBag.ProductType = Models.ProductType.GetAllBySubscriptionID(subscriptionId);
        }
        public ActionResult Index()
        {
            Products.Search _details = new Products.Search();

            int subscriptionId = Convert.ToInt32(HttpContext.Session.GetInt32("SubscriptionID"));
            if (subscriptionId == 0 || subscriptionId == null)
            {
                return RedirectToAction("Home", "Authentication");
            }

            ViewsViewbag(subscriptionId);

            DateTime date = DateTime.Now;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            _details.DateFrom = firstDayOfMonth;
            _details.DateTo = lastDayOfMonth;
            return View(_details);
        }
        //public ActionResult List()
        //{

        //    int subscriptionId = Convert.ToInt32(HttpContext.Session.GetInt32("SubscriptionID"));
        //    if (subscriptionId == 0 || subscriptionId == null)
        //    {
        //        return RedirectToAction("Home", "Authentication");
        //    }

        //    var _list = Products.GetBySuscriptionId(subscriptionId);
        //    return Json(new { data = _list });
        //}
        [HttpPost]
        public IActionResult List()
        {
            int subscriptionId = HttpContext.Session.GetInt32("SubscriptionID") ?? 0;
            if (subscriptionId == 0)
            {
                return RedirectToAction("Home", "Authentication");
            }

            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int currentpage = (Convert.ToInt32(start) / pageSize) + 1;

            var productName = Request.Form["columns[0][search][value]"].FirstOrDefault();
            var brandid = Request.Form["columns[1][search][value]"].FirstOrDefault();
            var categoryid = Request.Form["columns[2][search][value]"].FirstOrDefault();
            var variantid = Request.Form["columns[3][search][value]"].FirstOrDefault();
            var taxtypeid = Request.Form["columns[4][search][value]"].FirstOrDefault();
            var dateFrom = Request.Form["columns[5][search][value]"].FirstOrDefault();
            var dateTo = Request.Form["columns[6][search][value]"].FirstOrDefault();

            int BrandID = string.IsNullOrWhiteSpace(brandid) ? 0 : Convert.ToInt32(brandid);
            int CategoryID = string.IsNullOrWhiteSpace(categoryid) ? 0 : Convert.ToInt32(categoryid);
            int VariantID = string.IsNullOrWhiteSpace(variantid) ? 0 : Convert.ToInt32(variantid);
            int TaxTypeID = string.IsNullOrWhiteSpace(taxtypeid) ? 0 : Convert.ToInt32(taxtypeid);

            DateTime dtFrom, dtTo;
            if (!DateTime.TryParse(dateFrom, out dtFrom))
            {
                dtFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            }
            if (!DateTime.TryParse(dateTo, out dtTo))
            {
                dtTo = dtFrom.AddMonths(1).AddDays(-1);
            }

            int item_count;
            var data = Products.ProductGetFilteredBySubscriptionID(
                subscriptionId, BrandID, CategoryID, VariantID, TaxTypeID, dtFrom, dtTo, currentpage, pageSize, out item_count
            );

            return Json(new
            {
                draw = draw,
                recordsTotal = item_count,
                recordsFiltered = item_count,
                data = data
            });
        }
        // <------------------- I am still studying encryption for the parameters. Will be applied by finalization 
        public ActionResult Details(int ?id)
        {
            int subscriptionId = Convert.ToInt32(HttpContext.Session.GetInt32("SubscriptionID"));
            if (subscriptionId == 0 || subscriptionId == null)
            {
                return RedirectToAction("Home", "Authentication");
            }
            else {
                ViewsViewbag(subscriptionId);
                Products _details;
                if (!id.HasValue || id == 0)     /* <--------------------- This is the Add or Create, the url will be like  hostaddress.com/Details */
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

                    _transaction = new Models.Products();
                    _transaction.CreatedBy = userId; 
                    msg = "Record created successfully!";
                }
                else
                {
                    _transaction = Models.Products.GetById(_details.ID);
                    _transaction.ModifiedBy = userId;
                    //_transaction.ImageURL = System.IO.Path.Combine(_hostingEnvironment.WebRootPath, "Uploads", "Products", subscriptionId + "_" + _details.ID + "_" + _details.Attachment.FileName);
                    //_transaction.FileName = Path.Combine(subscriptionId + "_" + _details.ID + "_" + _details.Attachment.FileName);
                    msg = "Record updated successfully!";
                }

                _transaction.SubscriptionID = subscriptionId;
                _transaction.Name = _details.Name;
                _transaction.SKU = _details.SKU;
                _transaction.CategoryID = _details.CategoryID;
                _transaction.SubCategoryID = _details.SubCategoryID;
                _transaction.BrandID = _details.BrandID;
                _transaction.UnitID = _details.UnitID;
                _transaction.BarCode = _details.BarCode;
                _transaction.ItemCode = _details.ItemCode;
                _transaction.Description = _details.Description;
                _transaction.AcquiredCost = _details.AcquiredCost;
                _transaction.MarkupPrice = _details.MarkupPrice;
                _transaction.SRP = _details.SRP;
                _transaction.MinQty = _details.MinQty;
                _transaction.TaxID = _details.TaxID;
                _transaction.TaxAmountID = _details.TaxAmountID;
                _transaction.ProductTypeID = _details.ProductTypeID;
                _transaction.VariantTypeID = _details.VariantTypeID;
                _transaction.VarianSpecifiedID = _details.VarianSpecifiedID;


                string fullPath = "";

                string guidString = Guid.NewGuid().ToString("N");
                if (_details.Attachment != null && _details.Attachment.Length > 0) 
                {
                                                                    //_hostingEnvironment.WebRootPath, "Uploads", "Products"   YOU MAY TURN IT BACK INTO THIS FORMAT IF IT IS RETURNING ABSOLUTE FILE LOCATION INSTEAD OF RELATIVE
                    _transaction.ImageURL = System.IO.Path.Combine("\\","Uploads", "Products", subscriptionId + "_" + guidString + "_" + _details.Attachment.FileName);
                    _transaction.FileName = Path.Combine(subscriptionId + "_" + guidString + "_" + _details.Attachment.FileName);


                    string FilePath = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads", "Products");
                    string fileName = Path.GetFileName(_transaction.ImageURL);
                    fullPath = Path.Combine(FilePath, fileName);


                    // ----------------------------------- FILE UPLOAD ------------------------------
                    if (!System.IO.File.Exists(FilePath))
                    {
                        System.IO.Directory.CreateDirectory(FilePath);
                        try
                        {
                            using (var stream = new FileStream(fullPath, FileMode.OpenOrCreate))
                            {
                                _details.Attachment.CopyTo(stream);
                            }
                        }
                        catch (Exception ex)
                        {
                            TempData["error"] = ex.Message;
                            return RedirectToAction("Index");
                        }
                    }
                    // ----------------------------------- FILE UPLOAD ------------------------------

                }


                if (_transaction.Save())
                {
                    TempData["success"] = msg;
                    return NoContent();
                }
                else
                {
                    System.IO.File.Delete(fullPath);  // <---------------File deletion save storage if it fail to save.
                    TempData["error"] = "Problem occurred while saving record. Please try again!";
                    ViewsViewbag(subscriptionId);
                    return View("Details", _details);
                }
            }
            else 
            {
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

        [HttpGet]
        public ActionResult GetVarianTypeBySubscriptionID()
        {
            List<Models.VariantType> _list = Models.VariantType.GetallBySubscriptionID(Convert.ToInt32(HttpContext.Session.GetInt32("SubscriptionID")));
            var data = _list;
            return Json(new { data = data });
        }
        public ActionResult GetSpecifiedVariantbyID(int variantTypeID) 
        {
            List<Models.VariantSpecific> _list = Models.VariantSpecific.GetAllByVariantType(variantTypeID, Convert.ToInt32(HttpContext.Session.GetInt32("SubscriptionID")));
            var data = _list;
            return Json(new { data = data });
        }
        public ActionResult GetProductSubCategoryByCategoryID(int categoryId) 
        {
            List<Models.ProductSubCategory> _list = Models.ProductSubCategory.GetAllSubCategoryByCategoryID(categoryId);
            var data = _list;
            return Json(new { data = data });
        }
        public ActionResult GetTaxByTaxTypeID(int taxtypeId) {
            List<Models.Taxes> _list = Models.Taxes.GetByTaxTypeID(taxtypeId, Convert.ToInt32(HttpContext.Session.GetInt32("SubscriptionID")));
            var data = _list;
            return Json(new { data = data });
        }
    }
}
