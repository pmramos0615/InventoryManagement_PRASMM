using Microsoft.Data.SqlClient;
using System.Data;
namespace InventoryManagement_PRASMM.Data
{
    internal class UtilsDAL : BaseDAL
    {
        public int GetNewTransactionID(int id) {
            base.com.CommandText = "spGetTransactionNewID";
            base.com.Parameters.AddWithValue("@id", id);
            int ra = 0;
            try
            {
                ra = Convert.ToInt32(base.com.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ra;
        }
    }
}
