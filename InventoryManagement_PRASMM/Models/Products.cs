using InventoryManagement_PRASMM.Data;
using System.Data;

namespace InventoryManagement_PRASMM.Models
{
    public class Products
    {

        #region CONSTRUCTOR
        public Products()
        {
            this.init();
        }

        public void init()
        {
            this.ID = 0;
            this.Name = "";
            this.CategoryID = 0;
            this.SubCategoryID = 0;
            this.BrandID = 0;
            this.UnitID = 0;
            this.SKU = "";
            this.MinQty = 0;
            this.Qty = 0;
            this.Description = "";
            this.TaxID = 0;
            this.DiscountRate = 0;
            this.Cost = 0;
            this.MarkupRate = 0;
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
            this.ImageURL1 = string.Empty;

        }
        #endregion
        #region Properties
        public int ID { get; set; }
        public string Name { get; set; }
        public int CategoryID { get; set; }
        public int SubCategoryID { get; set; }
        public int BrandID { get; set; }
        public int UnitID { get; set; }
        public string SKU { get; set; }
        public int MinQty { get; set; }
        public int Qty { get; set; }
        public string Description { get; set; }
        public int TaxID { get; set; }
        public int DiscountRate { get; set; }
        public decimal Cost { get; set; }
        public decimal MarkupRate { get; set; }
        public decimal SRP { get; set; }
        public int StatusID { get; set; }

        //public HttpPostedFileBase Attachment { get; set; }

        public string ImageURL { get; set; }
        public int Discontinued { get; set; }
        public int DiscontinuedBy { get; set; }
        public DateTime DateDiscontinued { get; set; }
        public int CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime DateModified { get; set; }
        public string Category { get; private set; }
        public string Brand { get; private set; }
        public string Unit { get; private set; }
        public string FileName { get; internal set; }
        public string ImageURL1 { get; set; }

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
        public void Bind(DataRow row)
        {
            if (row != null)
            {
                this.ID = Convert.ToInt32(row["ID"]);
                this.Name = Convert.ToString(row["Name"]);
                this.Discontinued = Convert.ToInt32(row["Discontinued"]);
                this.DateCreated = Convert.ToDateTime(row["DateCreated"]);

                if (!DBNull.Value.Equals(row["CategoryID"]))
                    this.CategoryID = Convert.ToInt32(row["CategoryID"]);
                if (!DBNull.Value.Equals(row["SubCategoryID"]))
                    this.SubCategoryID = Convert.ToInt32(row["SubCategoryID"]);
                if (!DBNull.Value.Equals(row["BrandID"]))
                    this.BrandID = Convert.ToInt32(row["BrandID"]);
                if (!DBNull.Value.Equals(row["UnitID"]))
                    this.UnitID = Convert.ToInt32(row["UnitID"]);
                if (!DBNull.Value.Equals(row["SKU"]))
                    this.SKU = Convert.ToString(row["SKU"]);
                if (!DBNull.Value.Equals(row["MinQty"]))
                    this.MinQty = Convert.ToInt32(row["MinQty"]);
                if (!DBNull.Value.Equals(row["Qty"]))
                    this.Qty = Convert.ToInt32(row["Qty"]);
                if (!DBNull.Value.Equals(row["Description"]))
                    this.Description = Convert.ToString(row["Description"]);
                if (!DBNull.Value.Equals(row["TaxID"]))
                    this.TaxID = Convert.ToInt32(row["TaxID"]);
                if (!DBNull.Value.Equals(row["DiscountRate"]))
                    this.DiscountRate = Convert.ToInt32(row["DiscountRate"]);
                if (!DBNull.Value.Equals(row["Cost"]))
                    this.Cost = Convert.ToDecimal(row["Cost"]);
                if (!DBNull.Value.Equals(row["MarkupRate"]))
                    this.MarkupRate = Convert.ToDecimal(row["MarkupRate"]);
                if (!DBNull.Value.Equals(row["SRP"]))
                    this.SRP = Convert.ToDecimal(row["SRP"]);
                if (!DBNull.Value.Equals(row["StatusID"]))
                    this.StatusID = Convert.ToInt32(row["StatusID"]);

                if (!DBNull.Value.Equals(row["ImageURL"]))
                    this.ImageURL = Convert.ToString(row["ImageURL"]);

                if (!DBNull.Value.Equals(row["FileName"]))
                    this.FileName = Convert.ToString(row["FileName"]);

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

                if (!DBNull.Value.Equals(row["Category"]))
                    this.Category = Convert.ToString(row["Category"]);
                if (!DBNull.Value.Equals(row["Brand"]))
                    this.Brand = Convert.ToString(row["Brand"]);
                if (!DBNull.Value.Equals(row["Unit"]))
                    this.Unit = Convert.ToString(row["Unit"]);

                if (!DBNull.Value.Equals(row["ImageURL1"]))
                    this.ImageURL1 = Convert.ToString(row["ImageURL1"]);



            }
        }
        public bool Save()
        {
            var dal = new ProductsDAL();

            string message = "";
            int ret = dal.Save(this.ID, this.Name, this.CategoryID, this.SubCategoryID, this.BrandID, this.UnitID, this.SKU, this.MinQty, this.Qty, this.Description, this.TaxID, this.DiscountRate, this.Cost, this.MarkupRate, this.SRP, this.StatusID, this.ImageURL, this.FileName, this.Discontinued, this.DiscontinuedBy, this.DateDiscontinued, this.CreatedBy, this.DateCreated, this.ModifiedBy, this.DateModified, out message);

            this.ID = ret;
            return (ret > 0);
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
