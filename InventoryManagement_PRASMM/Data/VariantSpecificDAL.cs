using System.Data;

namespace InventoryManagement_PRASMM.Data
{
    internal class VariantSpecificDAL:BaseDAL
    {
        public DataTable GetAllbyVariantType( int variantType, int subscriptionId) 
        {
            base.com.CommandText = "spGetVariantSpecifiedByVariantTypeID";
            base.com.Parameters.AddWithValue("@id", 0);
            base.com.Parameters.AddWithValue("@varianTypeid", variantType);
            base.com.Parameters.AddWithValue("@subscriptionid", subscriptionId);
            return base.GetDataTable();
        }
        public DataRow GetVariantTypeByID(int id, int variantType, int subscriptionId)
        {
            base.com.CommandText = "spGetVariantSpecifiedByVariantTypeID";
            base.com.Parameters.AddWithValue("@id", id);
            base.com.Parameters.AddWithValue("@varianTypeid", variantType);
            base.com.Parameters.AddWithValue("@subscriptionid", subscriptionId);
            return base.GetFirstRow();
        }
    }
}
