using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using InventoryManagement_PRASMM.Models;
using InventoryManagement_PRASMM.Data;

namespace InventoryManagement_PRASMM.Models
{
    public class PurchaseOrderHeader
    {
        public class PurchaseOrderHeaderSearch {
            public string PONo { get; set; }
            public string OrderStatusID { get; set; }
            public DateTime DateFrom { get; set; }
            public DateTime DateTo { get; set; }

            public int SupplierId { get; set; }
        }
        #region Properties
        public int ID { get; set; }
        public int StoreID { get; set; }
        public int SubscriptionID { get; set; }
        public string PONo { get; set; }
        public int POStatusID { get; set; }
        public string POStatusName { get; set; }
        public DateTime PODate { get; set; }
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }
        public int SuppliersTermsID { get; set; }
        public string SuppliersTermsName { get; set; }
        public string Remarks { get; set; }
        public int Discontinued { get; set; }
        public int DiscontinuedBy { get; set; }
        public DateTime DateDiscontinued { get; set; }
        public int CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime DateModified { get; set; }
        public int isVoid { get; set; }
        public int isVoidBy { get; set; }
        public DateTime isVoidDate { get; set; }

        //public List<PurchaseOrderDetails> _PODetails { get; set; }
        public string PODateStr {
            get { return this.PODate.ToString("MM/dd/yyyy"); }
        }
        #endregion

        #region CONSTRUCTOR
        public PurchaseOrderHeader()
        {
            this.init();
        }

        public void init()
        {
            this.ID = 0;
            this.StoreID = 0;
            this.SubscriptionID = 0;
            this.PONo = "";
            this.PODate = DateTime.Now;
            this.SupplierID = 0;
            this.SupplierName = "";
            this.SupplierAddress = "";
            this.SuppliersTermsID = 0;
            this.SuppliersTermsName = "";
            this.Remarks = "";
            this.Discontinued = 0;
            this.DiscontinuedBy = 0;
            this.DateDiscontinued = DateTime.Now;
            this.CreatedBy = 0;
            this.DateCreated = DateTime.Now;
            this.ModifiedBy = 0;
            this.DateModified = DateTime.Now;
            this.isVoidBy = 0;
            this.isVoid = 0;
            this.isVoidDate = DateTime.Now;
        }
        #endregion

        #region Public Methods
        public static PurchaseOrderHeader GetById(int id)
        {
            var dal = new PurchaseOrderHeaderDAL();
            var instance = new PurchaseOrderHeader();
            instance.Bind(dal.GetById(id));
            return instance;
        }
        public static List<PurchaseOrderHeader> GetAll()
        {
            var dal = new PurchaseOrderHeaderDAL();
            var collection = new List<PurchaseOrderHeader>();
            foreach (DataRow row in dal.GetAll().Rows)
            {
                var instance = new PurchaseOrderHeader();
                instance.Bind(row);
                collection.Add(instance);
            }
            return collection;
        }

        
        public static List<PurchaseOrderHeader> GetBySubscriptionID(int storeId, int supplierId, string pono, DateTime datefrom, DateTime dateto, out int item_count, int curr_pageNumber, int page_size)
        {
            var dal = new PurchaseOrderHeaderDAL();
            return dal.GetBySubscriptionID(storeId, supplierId, pono, datefrom, dateto, out item_count, curr_pageNumber, page_size);
        }
        public void Bind(DataRow row)
        {
            if (row != null)
            {
                this.ID = Convert.ToInt32(row["ID"]);
                this.PONo = Convert.ToString(row["PONo"]);
                //this.DeliveryAddress = Convert.ToString(row["DeliveryAddress"]);
                this.Remarks = Convert.ToString(row["Remarks"]);
                this.Discontinued = Convert.ToInt32(row["Discontinued"]);
                this.DateCreated = Convert.ToDateTime(row["DateCreated"]);

                if (!DBNull.Value.Equals(row["StoreID"]))
                    this.StoreID = Convert.ToInt32(row["StoreID"]);
                if (!DBNull.Value.Equals(row["PODate"]))
                    this.PODate = Convert.ToDateTime(row["PODate"]);
                //if (!DBNull.Value.Equals(row["OrderStatusID"]))
                //    this.OrderStatusID = Convert.ToInt32(row["OrderStatusID"]);
                //if (!DBNull.Value.Equals(row["PaymentStatusID"]))
                //    this.PaymentStatusID = Convert.ToInt32(row["PaymentStatusID"]);
                if (!DBNull.Value.Equals(row["SupplierID"]))
                    this.SupplierID = Convert.ToInt32(row["SupplierID"]);
                //if (!DBNull.Value.Equals(row["TermsID"]))
                //    this.TermsID = Convert.ToInt32(row["TermsID"]);
                //if (!DBNull.Value.Equals(row["ExpectedDate"]))
                //    this.ExpectedDate = Convert.ToDateTime(row["ExpectedDate"]);
                if (!DBNull.Value.Equals(row["DiscontinuedBy"]))
                    this.DiscontinuedBy = Convert.ToInt32(row["DiscontinuedBy"]);
                if (!DBNull.Value.Equals(row["DateDiscontinued"]))
                    this.DateDiscontinued = Convert.ToDateTime(row["DateDiscontinued"]);
                if (!DBNull.Value.Equals(row["CreatedBy"]))
                    this.CreatedBy = Convert.ToInt32(row["CreatedBy"]);
                if (!DBNull.Value.Equals(row["ModifiedBy"]))
                    this.ModifiedBy = Convert.ToInt32(row["ModifiedBy"]);
                if (!DBNull.Value.Equals(row["DateModified"]))
                    this.DateModified = Convert.ToDateTime(row["DateModified"]);

            }
        }
        public bool Save()
        {
            var dal = new PurchaseOrderHeaderDAL();

            string message = "";
            //int ret = dal.Save(this.ID, this.StoreID, this.PONo, this.PODate, this.OrderStatusID, this.PaymentStatusID, this.SupplierID, this.DeliveryAddress, this.TermsID, this.ExpectedDate, this.Remarks, this.Discontinued, this.DiscontinuedBy, this.DateDiscontinued, this.CreatedBy, this.DateCreated, this.ModifiedBy, this.DateModified, out message);

            //this.ID = ret;
            //return (ret > 0);
            return true; 
        }
        public static bool Delete(int id, int discontinuedby)
        {
            var dal = new PurchaseOrderHeaderDAL();
            bool ret = dal.Delete(id, discontinuedby);
            return ret;
        }
        #endregion
    }
}
