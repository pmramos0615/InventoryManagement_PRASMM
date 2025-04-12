using System.Data;

namespace InventoryManagement_PRASMM.Models
{
    public class VariantSpecific
    {
        VariantSpecific() 
        {
            this.init();
        }
        public void init() {
            this.ID = 0;
            this.SubscriptionID = 0;
            this.VariantTypeID = 0;
            this.Description = "";
            this.Discontinued = 0;
            this.DiscontinuedBy = 0;
            this.DiscontinuedDate = DateTime.Now;
            this.ModifiedBy = 0;
            this.ModifiedDate = DateTime.Now;
            this.CreatedBy = 0;
            this.CreatedDate    = DateTime.Now;
        }

        public int ID { get; set; }
        public int SubscriptionID { get; set; }
        public int VariantTypeID { get; set; }
        public string Description { get; set; }
        public int Discontinued { get; set; }
        public int DiscontinuedBy { get; set; }
        public DateTime DiscontinuedDate { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public void Bind(DataRow row) 
        {
            if (row != null) 
            {
                this.ID = Convert.ToInt32(row["ID"]);
                this.SubscriptionID = Convert.ToInt32(row["SubscriptionID"]);
                if (!DBNull.Value.Equals(row["VariantTypeID"]))
                    this.VariantTypeID = Convert.ToInt32(row["VariantTypeID"]);
                if (!DBNull.Value.Equals(row["Description"]))
                    this.Description = Convert.ToString(row["Description"]);
                if (!DBNull.Value.Equals(row["Discontinued"]))
                    this.Discontinued = Convert.ToInt32(row["Discontinued"]);
                if (!DBNull.Value.Equals(row["DiscontinuedBy"]))
                    this.DiscontinuedBy = Convert.ToInt32(row["DiscontinuedBy"]);
                if (!DBNull.Value.Equals(row["DiscontinuedDate"]))
                    this.DiscontinuedDate = Convert.ToDateTime(row["DiscontinuedDate"]);
                if (!DBNull.Value.Equals(row["ModifiedBy"]))
                    this.ModifiedBy = Convert.ToInt32(row["ModifiedBy"]);
                if (!DBNull.Value.Equals(row["ModifiedDate"]))
                    this.ModifiedDate = Convert.ToDateTime(row["ModifiedDate"]);
                if (!DBNull.Value.Equals(row["CreatedBy"]))
                    this.CreatedBy = Convert.ToInt32(row["CreatedBy"]);
                if (!DBNull.Value.Equals(row["CreatedDate"]))
                    this.CreatedDate = Convert.ToDateTime(row["CreatedDate"]);
            }
        }
        public static List<VariantSpecific> GetAllByVariantType(int variantType, int subscriptionId)
        {
            var dal = new Data.VariantSpecificDAL();
            var collection = new List<VariantSpecific>();
            foreach (DataRow row in dal.GetAllbyVariantType(variantType, subscriptionId).Rows)
            {
                var instance = new VariantSpecific();
                instance.Bind(row);
                collection.Add(instance);
            }
            return collection;
        }

    }
}
