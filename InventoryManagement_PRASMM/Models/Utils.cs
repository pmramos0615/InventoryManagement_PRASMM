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
        public void transactionRollback() {
            var dal =new UtilsDAL();
            dal.End(true);
        }

        public string Decrypt(string cypherString, bool useHasing)
        {
            var dal = new UtilsDAL();
            string decryptedString = dal.Decrypt(cypherString, useHasing);
            return decryptedString;
        }

        public string Encrypt(string cypherString, bool useHasing) 
        { 
            var dal = new UtilsDAL();
            string encryptedString = dal.Encrypt(cypherString,useHasing);
            return encryptedString;
        }
    }
}
