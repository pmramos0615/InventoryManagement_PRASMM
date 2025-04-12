using System.Data;

namespace InventoryManagement_PRASMM.Data
{
    internal class VariantTypeDAL:BaseDAL
    {
        public DataTable GetallBySubscriptionID(int subscriptionID)
        {
            base.com.CommandText = "spGetVariantTypeByID";
            base.com.Parameters.AddWithValue("@subscriptionid", subscriptionID);
            base.com.Parameters.AddWithValue("@id", 0);
            return base.GetDataTable();
        }
        public DataRow GetByID(int subscriptionID, int id)
        {
            base.com.CommandText = "spGetVariantTypeByID";
            base.com.Parameters.AddWithValue("@subscriptionid", subscriptionID);
            base.com.Parameters.AddWithValue("@id", id);
            return base.GetFirstRow();
        }
    }
}
