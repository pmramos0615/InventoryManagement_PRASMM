using InventoryManagement_PRASMM.Data;
using System.Data;

namespace InventoryManagement_PRASMM.Models
{
    public class Utils
    {
        public int GetNewTransactionID(int id)
        {
            var dal = new UtilsDAL();
            int returnValue = dal.GetNewTransactionID(id);
            return returnValue;
        }
    }
}
