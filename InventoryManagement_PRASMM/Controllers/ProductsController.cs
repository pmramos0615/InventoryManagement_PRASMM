using InventoryManagement_PRASMM.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace InventoryManagement_PRASMM.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            List<Products> _list = Products.GetAll();
            return Json(new { data = _list });
        }

        public ActionResult Details(int id)
        {
            Products _details;

            ViewBag.Category = ProductCategory.GetAll();
            ViewBag.SubCategory = ProductSubCategory.GetAll();
            ViewBag.Brand = Brands.GetAll();
            ViewBag.Unit = Units.GetAll();
            ViewBag.Discount = DiscountRate.GetAll();
            ViewBag.Status = ProductStatus.GetAll();

            if (id == 0)
            {
                ViewBag.Caption = "Create new product";
                _details = new Products();
            }
            else
            {
                ViewBag.Caption = "Edit product";
                _details = Models.Products.GetById(id);
            }

            return View(_details);

        }

        public ActionResult Save(Products _details)
        {
            Models.Products _transaction = null;
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
                    msg = "Record updated successfully!";
                }
                _transaction.Name = _details.Name;
                _transaction.CategoryID = _details.CategoryID;
                _transaction.SubCategoryID = _details.SubCategoryID;
                _transaction.BrandID = _details.BrandID;
                _transaction.UnitID = _details.UnitID;
                _transaction.SKU = _details.SKU;
                _transaction.MinQty = _details.MinQty;
                _transaction.Qty = _details.Qty;
                _transaction.Description = _details.Description;
                _transaction.TaxID = 0;
                _transaction.DiscountRate = _details.DiscountRate;
                _transaction.Cost = _details.Cost;
                _transaction.SRP = _details.SRP;
                _transaction.StatusID = _details.StatusID;

                string file = "", randomfilename = "", filepath = "";

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
