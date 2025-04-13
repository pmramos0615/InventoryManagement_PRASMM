using InventoryManagement_PRASMM.Data;
using Microsoft.AspNetCore.SignalR.Protocol;
using System.Data;

namespace InventoryManagement_PRASMM.Models
{
    public class ProductCategory
    {

        #region CONSTRUCTOR
        public ProductCategory()
        {
            this.init();
        }

        public void init()
        {
            this.ID = 0;
            this.SubscriptionID = 0;
            this.ParentID = null;
            this.Code = "";
            this.Name = "";
            this.Description = "";
            this.Discontinued = 0;
            this.DiscontinuedBy = 0;
            this.DateDiscontinued = DateTime.Now;
            this.CreatedBy = 0;
            this.DateCreated = DateTime.Now;
            this.ModifiedBy = 0;
            this.DateModified = DateTime.Now;
            this.ParentCategory = "";

        }
        #endregion
        #region Properties
        public int ID { get; set; }
        public int SubscriptionID { get; set; } 
        public int? ParentID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Discontinued { get; set; }
        public int DiscontinuedBy { get; set; }
        public DateTime DateDiscontinued { get; set; }
        public int CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime DateModified { get; set; }
        public string ParentCategory { get; set; }

        #endregion

        #region Public Methods
        public static ProductCategory GetById(int id)
        {
            var dal = new ProductCategoryDAL();
            var instance = new ProductCategory();
            instance.Bind(dal.GetById(id));
            return instance;
        }
        public static List<ProductCategory> GetAll()
        {
            var dal = new ProductCategoryDAL();
            var collection = new List<ProductCategory>();
            foreach (DataRow row in dal.GetAll().Rows)
            {
                var instance = new ProductCategory();
                instance.Bind(row);
                collection.Add(instance);
            }
            return collection;
        }
        public static List<ProductCategory> GetBySubscription(int subscriptionId)
        {
            var dal = new ProductCategoryDAL();
            var collection = new List<ProductCategory>();
            foreach (DataRow row in dal.GetBySubscription(subscriptionId).Rows)
            {
                var instance = new ProductCategory();
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
                if (!DBNull.Value.Equals(row["Description"]))
                    this.Description = Convert.ToString(row["Description"]);
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
            var dal = new ProductCategoryDAL();

            string message = "";
            int ret = dal.Save(this.ID,this.SubscriptionID,this.ParentID, this.Code, this.Name, this.Description, this.Discontinued, this.DiscontinuedBy, this.DateDiscontinued, this.CreatedBy, this.DateCreated, this.ModifiedBy, this.DateModified, out message);

            this.ID = ret;
            return (ret > 0);
        }
        public static bool Delete(int id, int discontinuedby)
        {
            var dal = new ProductCategoryDAL();
            bool ret = dal.Delete(id, discontinuedby);
            return ret;
        }
        #endregion
    }
}
