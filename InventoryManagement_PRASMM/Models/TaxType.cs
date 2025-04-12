using System.Data;
using InventoryManagement_PRASMM.Data;

namespace InventoryManagement_PRASMM.Models
{
    public class TaxType
    {
        public int Id { get; set; }
        public int SubscriptionID { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int Discontinued { get; set; }
        public int DiscontinuedBy { get; set; }
        public DateTime DiscontinuedDate { get; set; }

        public void init()
        {
            this.Id = 0;
            this.SubscriptionID = 0;
            this.Description = string.Empty;
            this.CreatedBy = 0;
            this.CreatedDate = DateTime.Now;
            this.ModifiedBy = 0;
            this.ModifiedDate = DateTime.Now;
            this.Discontinued = 0;
            this.DiscontinuedBy = 0;
            this.DiscontinuedDate = DateTime.Now;
        }

        public TaxType()
        {
            this.init();
        }

        public void Bind(DataRow row)
        {
            if (row != null)
            {
                this.Id = Convert.ToInt32(row["ID"]);
                if (!DBNull.Value.Equals(row["SubscriptionID"]))
                    this.SubscriptionID = Convert.ToInt32(row["SubscriptionID"]);
                if (!DBNull.Value.Equals(row["Description"]))
                    this.Description = Convert.ToString(row["Description"]);
                if (!DBNull.Value.Equals(row["CreatedBy"]))
                    this.CreatedBy = Convert.ToInt32(row["CreatedBy"]);
                if (!DBNull.Value.Equals(row["CreatedDate"]))
                    this.CreatedDate = Convert.ToDateTime(row["CreatedDate"]);
                if (!DBNull.Value.Equals(row["ModifiedBy"]))
                    this.ModifiedBy = Convert.ToInt32(row["ModifiedBy"]);
                if (!DBNull.Value.Equals(row["ModifiedDate"]))
                    this.ModifiedDate = Convert.ToDateTime(row["ModifiedDate"]);
                if (!DBNull.Value.Equals(row["Discontinued"]))
                    this.Discontinued = Convert.ToInt32(row["Discontinued"]);
                if (!DBNull.Value.Equals(row["DiscontinuedBy"]))
                    this.DiscontinuedBy = Convert.ToInt32(row["DiscontinuedBy"]);
                if (!DBNull.Value.Equals(row["DiscontinuedDate"]))
                    this.DiscontinuedDate = Convert.ToDateTime(row["DiscontinuedDate"]);

            }
        }

        public static List<TaxType> GetAll(int subscriptionid)
        {
            var dal = new TaxexTypeDAL();
            var collection = new List<TaxType>();

            foreach(DataRow row in dal.GetAll(subscriptionid).Rows)
            {
                var instance = new TaxType();
                instance.Bind(row);
                collection.Add(instance);
            }
            return collection;
        }

        public static TaxType GetById(int id, int subscriptionid)
        {
            var dal = new TaxexTypeDAL();
            var instance = new TaxType();

            instance.Bind(dal.GetByID(id,subscriptionid));
            return instance;
        }
    }
}
