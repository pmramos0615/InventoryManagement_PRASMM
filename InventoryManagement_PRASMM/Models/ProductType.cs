using System.Data;
using InventoryManagement_PRASMM.Data;

namespace InventoryManagement_PRASMM.Models
{
    public class ProductType
    {
        public ProductType() 
        {
            this.init();
        }

        public void init() 
        {
            this.ID = 0;
            this.SubscriptionID = 0;
            this.Description = "";
            this.Discontinued = 0;
            this.DiscontinuedDate = DateTime.Now;
            this.DiscontinuedBy = 0;
            this.ModifiedBy = 0;
            this.ModifiedDate = DateTime.Now;
            this.Createdby = 0;
            this.CreatedDate = DateTime.Now;
        }
        public int ID { get; set; }
        public int SubscriptionID { get; set; }
        public string Description { get; set; }
        public int Discontinued { get; set; }
        public DateTime DiscontinuedDate { get; set; }
        public int DiscontinuedBy { get; set; }
        public int Createdby { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public void Bind(System.Data.DataRow row)
        {
            this.ID = Convert.ToInt32(row["ID"]);
            this.SubscriptionID = Convert.ToInt32(row["SubscriptionID"]);
            if (!DBNull.Value.Equals(row["Description"]))
                this.Description = Convert.ToString(row["Description"]);
            if (!DBNull.Value.Equals(row["Discontinued"]))
                this.Discontinued = Convert.ToInt32(row["Discontinued"]);
            if (!DBNull.Value.Equals(row["DiscontinuedDate"]))
                this.DiscontinuedDate = Convert.ToDateTime(row["DiscontinuedDate"]);
            if (!DBNull.Value.Equals(row["DiscontinuedBy"]))
                this.DiscontinuedBy = Convert.ToInt32(row["DiscontinuedBy"]);
            if (!DBNull.Value.Equals(row["CreatedBy"]))
                this.Createdby = Convert.ToInt32(row["CreatedBy"]);
            if (!DBNull.Value.Equals(row["CreatedDate"]))
                this.CreatedDate = Convert.ToDateTime(row["CreatedDate"]);

        }
        public static ProductType GetbyID(int subscriptionId, int id) 
        {
            var dal = new ProductTypeDAL();
            var instance = new ProductType();
            instance.Bind(dal.GetByID(subscriptionId, id));
            return instance;
        }

        public static List<ProductType> GetAllBySubscriptionID(int subscriptionID)
        {
            var dal = new ProductTypeDAL();
            var collection = new List<ProductType>();
            foreach (DataRow row in dal.GetallBySubscriptionID(subscriptionID).Rows)
            {
                var instance = new ProductType();
                instance.Bind(row);
                collection.Add(instance);
            }
            return collection;
        }
    }
}
