using System.Data;
using InventoryManagement_PRASMM.Data;

namespace InventoryManagement_PRASMM.Models
{
    public class TaxType
    {
        public int Id { get; set; }
        public int SubscriptionID { get; set; }
        public string Description { get; set; } = string.Empty;
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
            init();
        }

        public void Bind(DataRow row)
        {
            if (row != null)
            {
                this.Id = Convert.ToInt32(row["ID"]);
                this.SubscriptionID = Convert.ToInt32(row["SubscriptionID"]);
                this.Description = row["Description"] != DBNull.Value ? Convert.ToString(row["Description"]) : string.Empty;
                this.CreatedBy = row["CreatedBy"] != DBNull.Value ? Convert.ToInt32(row["CreatedBy"]) : 0;
                this.CreatedDate = row["CreatedDate"] != DBNull.Value ? Convert.ToDateTime(row["CreatedDate"]) : DateTime.Now;
                this.ModifiedBy = row["ModifiedBy"] != DBNull.Value ? Convert.ToInt32(row["ModifiedBy"]) : 0;
                this.ModifiedDate = row["ModifiedDate"] != DBNull.Value ? Convert.ToDateTime(row["ModifiedDate"]) : DateTime.Now;
                this.Discontinued = row["Discontinued"] != DBNull.Value ? Convert.ToInt32(row["Discontinued"]) : 0;
                this.DiscontinuedBy = row["DiscontinuedBy"] != DBNull.Value ? Convert.ToInt32(row["DiscontinuedBy"]) : 0;
                this.DiscontinuedDate = row["DiscontinuedDate"] != DBNull.Value ? Convert.ToDateTime(row["DiscontinuedDate"]) : DateTime.Now;
            }
        }

        public static List<TaxType> GetAll(int subscriptionid)
        {
            var dal = new TaxexTypeDAL();
            var collection = new List<TaxType>();
            var dataTable = dal.GetAll(subscriptionid);
            if (dataTable != null)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    var instance = new TaxType();
                    instance.Bind(row);
                    collection.Add(instance);
                }
            }
            return collection;
        }

        public static TaxType GetById(int id, int subscriptionid)
        {
            var dal = new TaxexTypeDAL();
            var instance = new TaxType();
            var dataRow = dal.GetByID(id, subscriptionid);
            if (dataRow != null)
            {
                instance.Bind(dataRow);
            }
            return instance;
        }
    }
}
