using System.Data;

namespace InventoryManagement_PRASMM.Data

{
    internal class PurchaseOrderDAL:BaseDAL
    {
        public DataTable GetAll()
        {
            base.com.CommandText = "spBrands";
            base.com.Parameters.AddWithValue("@id", 0);
            return base.GetDataTable();

        }
        public DataTable GetBySubscription(int subscriptionId)
        {
            base.com.CommandText = "spBrandsBySubscriptionID";
            base.com.Parameters.AddWithValue("@subscriptionId", subscriptionId);
            return base.GetDataTable();

        }

        public DataRow GetById(int id)
        {
            base.com.CommandText = "spBrands";
            base.com.Parameters.AddWithValue("@id", id);
            return base.GetFirstRow();
        }


    }
}
