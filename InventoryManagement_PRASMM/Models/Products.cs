using InventoryManagement_PRASMM.Data;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace InventoryManagement_PRASMM.Models
{
    public class Products
    {
        public class Search
        {
            public string ProductName { get; set; }
            public int CategoryID { get; set; }
            public int BrandID { get; set; }
            public int VariantID { get; set; }
            public int TaxID { get; set; }
            public DateTime DateFrom { get; set; }
            public DateTime DateTo { get; set; }
        }
        #region CONSTRUCTOR
        public Products()
        {
            this.init();
        }

        public void init()
        {
            this.ID = 0;
            this.Name = "";
            this.ProductName = "";
            this.CategoryID = 0;
            this.Category = "";
            this.SubCategoryID = 0;
            this.Subcategory = "";
            this.BrandID = 0;
            this.Brand = "";
            this.UnitID = 0;
            this.Unit = "";
            this.SKU = "";
            this.BarCode = "";
            this.MinQty = 0;
            this.Qty = 0;
            this.Description = "";
            this.TaxID = 0;
            this.Cost = 0;
            this.SRP = 0;
            this.StatusID = 0;
            this.ImageURL = "";
            this.Discontinued = 0;
            this.DiscontinuedBy = 0;
            this.DateDiscontinued = DateTime.Now;
            this.CreatedBy = 0;
            this.DateCreated = DateTime.Now;
            this.ModifiedBy = 0;
            this.DateModified = DateTime.Now;
            this.FileName = "";
            this.Unit=string.Empty;
            this.Brand = string.Empty;
            this.Category = string.Empty;
            this.VariantTypeID = 0;
        }
        #endregion
        #region Properties
        public int ID { get; set; }
        public int SubscriptionID { get; set; }
        public string Name { get; set; }
        public string ProductName { get; set; }
        public int CategoryID { get; set; }
        public string Category { get; set; }
        public int SubCategoryID { get; set; }
        public string ?Subcategory{ get; set; }
        public int BrandID { get; set; }
        public int StoreID { get; set; }
        public string Brand { get; set; }
        public int UnitID { get; set; }
        public int ?ProductTypeID { get; set; }
        public string ?ProductType { get; set; }
        public int VariantTypeID { get; set; }
        public int VarianSpecifiedID { get; set; }
        public string SKU { get; set; }
        public string BarCode { get; set; }
        public string ItemCode { get; set; }
        public int MinQty { get; set; }
        public int Qty { get; set; }
        public string Description { get; set; }
        public int TaxID { get; set; }
        public int TaxAmountID { get; set; }
        public decimal Cost { get; set; }
        public decimal AcquiredCost { get; set; }
        public decimal MarkupPrice { get; set; }
        public decimal SRP { get; set; }
        public int StatusID { get; set; }
        public IFormFile? Attachment { get; set; }
        public string ImageURL { get; set; }
        public int Discontinued { get; set; }
        public int DiscontinuedBy { get; set; }
        public DateTime DateDiscontinued { get; set; }
        public int CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime DateModified { get; set; }
        public string Unit { get; set; }
        public string FileName { get; internal set; }
        public IWebHostEnvironment ?_webHostEnvironment{ get; set; }         
        #endregion

        #region Public Methods
        public static Products GetById(int id)
        {
            var dal = new ProductsDAL();
            var instance = new Products();
            instance.Bind(dal.GetById(id));
            return instance;
        }
        public static List<Products> GetAll()
        {
            var dal = new ProductsDAL();
            var collection = new List<Products>();
            foreach (DataRow row in dal.GetAll().Rows)
            {
                var instance = new Products();
                instance.Bind(row);
                collection.Add(instance);
            }
            return collection;
        }

        
        public static List<Products> GetBySuscriptionId(int subscriptionId)
        {
            var dal = new ProductsDAL();
            var collection = new List<Products>();
            foreach (DataRow row in dal.GetBySuscriptionId(subscriptionId).Rows)
            {
                var instance = new Products();
                instance.Bind(row);
                collection.Add(instance);
            }
            return collection;
        }
        public void Bind(DataRow row)
        {
            if (row != null)
            {
                this.ID = Convert.ToInt32(row["ID"]);
                this.SubscriptionID = Convert.ToInt32(row["SubscriptionID"]);
                if (!DBNull.Value.Equals(row["Name"]))
                    this.Name = Convert.ToString(row["Name"]);
                if (!DBNull.Value.Equals(row["SKU"]))
                    this.SKU = Convert.ToString(row["SKU"]);
                if (!DBNull.Value.Equals(row["CategoryID"]))
                    this.CategoryID = Convert.ToInt32(row["CategoryID"]);
                if (!DBNull.Value.Equals(row["Category"]))
                    this.Category = Convert.ToString(row["Category"]);
                if (!DBNull.Value.Equals(row["SubCategoryID"]))
                    this.SubCategoryID = Convert.ToInt32(row["SubCategoryID"]);
                if (!DBNull.Value.Equals(row["SubCategory"]))
                    this.Subcategory = Convert.ToString(row["Subcategory"]);
                if (!DBNull.Value.Equals(row["BrandID"]))
                    this.BrandID = Convert.ToInt32(row["BrandID"]);
                if (!DBNull.Value.Equals(row["Brand"]))
                    this.Brand = Convert.ToString(row["Brand"]);
                if (!DBNull.Value.Equals(row["UnitID"]))
                    this.UnitID = Convert.ToInt32(row["UnitID"]);
                if (!DBNull.Value.Equals(row["Unit"]))
                    this.Unit = Convert.ToString(row["Unit"]);
                if (!DBNull.Value.Equals(row["BarCode"]))
                    this.BarCode = Convert.ToString(row["BarCode"]);
                if (!DBNull.Value.Equals(row["ItemCode"]))
                    this.ItemCode = Convert.ToString(row["ItemCode"]);
                if (!DBNull.Value.Equals(row["Description"]))
                    this.Description = Convert.ToString(row["Description"]);
                if (!DBNull.Value.Equals(row["AcquiredCost"]))
                    this.AcquiredCost = Convert.ToDecimal(row["AcquiredCost"]);
                if (!DBNull.Value.Equals(row["MarkupPrice"]))
                    this.MarkupPrice = Convert.ToDecimal(row["MarkupPrice"]);
                if (!DBNull.Value.Equals(row["SRP"]))
                    this.SRP = Convert.ToDecimal(row["SRP"]);
                if (!DBNull.Value.Equals(row["MinQty"]))
                    this.MinQty = Convert.ToInt32(row["MinQty"]);
                if (!DBNull.Value.Equals(row["TaxID"]))
                    this.TaxID = Convert.ToInt32(row["TaxID"]);
                if (!DBNull.Value.Equals(row["TaxAmountID"]))
                    this.TaxAmountID = Convert.ToInt32(row["TaxAmountID"]);
                if (!DBNull.Value.Equals(row["ProductTypeID"]))
                    this.ProductTypeID = Convert.ToInt32(row["ProductTypeID"]);
                if (!DBNull.Value.Equals(row["VariantTypeID"]))
                    this.VariantTypeID = Convert.ToInt32(row["VariantTypeID"]);
                if (!DBNull.Value.Equals(row["SpecifiedVariantID"]))
                    this.VarianSpecifiedID = Convert.ToInt32(row["SpecifiedVariantID"]);
                if (!DBNull.Value.Equals(row["FileName"]))
                    this.FileName = Convert.ToString(row["FileName"]);
                if (!DBNull.Value.Equals(row["ImageURL"]))
                    this.ImageURL = Convert.ToString(row["ImageURL"]);
                if (DBNull.Value.Equals(row["Discontinued"]))
                    this.Discontinued = Convert.ToInt32(row["Discontinued"]);
                if (!DBNull.Value.Equals(row["DiscontinuedBy"]))
                    this.DiscontinuedBy = Convert.ToInt32(row["DiscontinuedBy"]);
                if (!DBNull.Value.Equals(row["DateDiscontinued"]))
                    this.DateDiscontinued = Convert.ToDateTime(row["DateDiscontinued"]);
                if (!DBNull.Value.Equals(row["CreatedBy"]))
                    this.CreatedBy = Convert.ToInt32(row["CreatedBy"]);
                if (!DBNull.Value.Equals(row["DateCreated"]))
                    this.DateCreated = Convert.ToDateTime(row["DateCreated"]);
                if (!DBNull.Value.Equals(row["ModifiedBy"]))
                    this.ModifiedBy = Convert.ToInt32(row["ModifiedBy"]);
                if (!DBNull.Value.Equals(row["DateModified"]))
                    this.DateModified = Convert.ToDateTime(row["DateModified"]);    
            }
        }
        public bool Save()
        {
            var dal = new ProductsDAL();

            string message = "";
            int ret = dal.Save(this.ID,this.SubscriptionID,this.Name,this.SKU,this.CategoryID,this.SubCategoryID,this.BrandID,this.UnitID,this.BarCode,this.ItemCode,this.Description,this.AcquiredCost,this.MarkupPrice,this.SRP,this.MinQty,this.TaxID,this.TaxAmountID,this.ProductTypeID,this.VariantTypeID,this.VarianSpecifiedID,this.FileName,this.ImageURL,this.CreatedBy,this.DateCreated,this.ModifiedBy,this.DateModified);

            this.ID = ret;
            return (ret > 0);
        }

        internal static List<Products> ProductGetFilteredBySubscriptionID(int subscriptionid,int  brandid,int categoryid,int variantid,int taxtypeid,DateTime dateFrom,DateTime dateTo,int currentpage,int pagesize,out int item_count)
        {
            var dal = new ProductsDAL();
            return dal.ProductGetFilteredBySubscriptionID(subscriptionid, brandid, categoryid,variantid,taxtypeid,dateFrom,dateTo,currentpage,pagesize,out item_count);
        }
        public static bool Delete(int id, int discontinuedby)
        {
            var dal = new ProductsDAL();
            bool ret = dal.Delete(id, discontinuedby);
            return ret;
        }
        #endregion
    }
}
