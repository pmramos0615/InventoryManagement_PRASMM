using System.Data;

namespace InventoryManagement_PRASMM.Data
{
    internal class TaxesDAL : BaseDAL
    {
        public DataTable GetAll(int subscriptionid)
        {
            base.com.CommandText = "spTaxGetByID";
            base.com.Parameters.AddWithValue("@id", 0);
            base.com.Parameters.AddWithValue("@subscriptionID", subscriptionid);
            return base.GetDataTable();
        }

        public DataTable GetByTaxTypeID(int taxtypeid, int subscriptionid) 
        {
            base.com.CommandText = "spTaxGetByTaxTypeID";
            base.com.Parameters.AddWithValue("@taxtypeId",taxtypeid);
            base.com.Parameters.AddWithValue("@subscriptionID", subscriptionid);
            return base.GetDataTable();
        }
        public DataRow GetByID(int id, int subscriptionid)
        {
            base.com.CommandText = "spTaxGetByID";
            base.com.Parameters.AddWithValue("@id", id);
            base.com.Parameters.AddWithValue("@subscriptionID", subscriptionid);
            return base.GetFirstRow();
        }
    }
}
