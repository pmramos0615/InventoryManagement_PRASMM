﻿using InventoryManagement_PRASMM.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement_PRASMM.Controllers
{
    public class PurchaseOrderController : Controller
    {
        public ActionResult Index()
        {
            int subscriptionId = Convert.ToInt32(HttpContext.Session.GetInt32("SubscriptionID"));

            ViewBag.Suppliers = Suppliers.GetBySubscriptionID(subscriptionId);

            var model = new PurchaseOrderHeader.PurchaseOrderHeaderSearch();

            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            model.DateFrom = startDate;
            model.DateTo = endDate;

            return View(model);
        }
        public ActionResult List()
        {
            int subscriptionId = Convert.ToInt32(HttpContext.Session.GetInt32("SubscriptionID"));

            //jQuery DataTables Param
            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
            //Find paging info
            var start = HttpContext.Request.Form["start"].FirstOrDefault();
            var length = HttpContext.Request.Form["length"].FirstOrDefault();
            //Find order columns info
            var sortColumn = HttpContext.Request.Form["columns[" + HttpContext.Request.Form["order[0][column]"].FirstOrDefault()
                                    + "][name]"].FirstOrDefault();
            var sortColumnDir = HttpContext.Request.Form["order[0][dir]"].FirstOrDefault();
            //find search columns info
            var suppliername = HttpContext.Request.Form["columns[0][search][value]"].FirstOrDefault();
            var pono = HttpContext.Request.Form["columns[1][search][value]"].FirstOrDefault();
            var filterdatefrom = HttpContext.Request.Form["columns[2][search][value]"].FirstOrDefault();
            var filterdateto = HttpContext.Request.Form["columns[3][search][value]"].FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;

            int currentpage = (Convert.ToInt32(start) / Convert.ToInt32(length)) + 1;

            int recordsTotal = 0;

            int item_count = 0;

            int supplierId = 0;

            if (suppliername != "") 
            {
                supplierId = Convert.ToInt32(suppliername);
            }
            DateTime datefrom, dateto;
            if (filterdatefrom == "")
            {

                DateTime now = DateTime.Now;
                var startDate = new DateTime(now.Year, now.Month, 1);
                var endDate = startDate.AddMonths(1).AddDays(-1);

                datefrom = startDate;
                dateto = endDate;
            }
            else
            {
                datefrom =Convert.ToDateTime(filterdatefrom);
                dateto = Convert.ToDateTime(filterdateto);
            }

            List<PurchaseOrderHeader> _list = PurchaseOrderHeader.GetBySubscriptionID(subscriptionId, supplierId, pono, datefrom, dateto, out item_count, currentpage, pageSize);

            recordsTotal = item_count;

            var data = _list;
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

        }
        public ActionResult Details(int? id) 
        {
            int subscriptionId = Convert.ToInt32(HttpContext.Session.GetInt32("SubscriptionID"));
            PurchaseOrderHeader _details;
            if (!id.HasValue)     /* <--------------------- This is the Add or Create, the url will be like  hostaddress.com/Details */
            {
                ViewBag.Caption = "Create new product";

                _details = new PurchaseOrderHeader();

                return View(_details);
            }
            else        /* <------------------ This is the  Edit, the url will be like  hostaddress.com/Details/encryptedValue (in the future for now just id)*/
            {
                ViewBag.Caption = "Edit product";

                int idValue = Convert.ToInt32(id);
                _details = Models.PurchaseOrderHeader.GetById(idValue);


                return View(_details);
            }
            return View();
        }
    }
}
