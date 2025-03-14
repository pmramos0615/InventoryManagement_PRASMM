using System.Data;

namespace InventoryManagement_PRASMM.Data
{
    internal class TaxexTypeDAL :BaseDAL
    {
        public DataTable GetAll(int subscriptionid)
        {
            base.com.CommandText = "spTaxTypeGetByID";
            base.com.Parameters.AddWithValue("@ID", 0);
            base.com.Parameters.AddWithValue("@subscriptionID", subscriptionid);
            return base.GetDataTable();
        }

        public DataRow GetByID(int id,int subscriptionid)
        {
            base.com.CommandText = "spTaxTypeGetByID";
            base.com.Parameters.AddWithValue("@ID", id);
            base.com.Parameters.AddWithValue("@subscriptionID", subscriptionid);
            return base.GetFirstRow();
        }
    }
}
