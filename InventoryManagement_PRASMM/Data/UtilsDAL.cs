using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;
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
        public void End(bool Commit)
        {
            if (!Commit)
                base.tran.Commit();
            else
                base.tran.Rollback();
        }
        public string Decrypt(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            // Security Key
            string key = "$unR1s3";

            // Use hashing if specified
            if (useHashing)
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    keyArray = sha256.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                }
            }
            else
            {
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = keyArray;
                aesAlg.Mode = CipherMode.ECB; // ECB mode for simplicity
                aesAlg.Padding = PaddingMode.PKCS7;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor();

                using (MemoryStream msDecrypt = new MemoryStream(toEncryptArray))
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    return srDecrypt.ReadToEnd();
                }
            }
        }

        public string Encrypt(string plainText, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(plainText);

            // Security Key
            string key = "$unR1s3";

            // Use hashing if specified
            if (useHashing)
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    keyArray = sha256.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                }
            }
            else
            {
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = keyArray;
                aesAlg.Mode = CipherMode.ECB; // ECB mode for simplicity
                aesAlg.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor();

                using (MemoryStream msEncrypt = new MemoryStream())
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }                
            }
        }

    }
}

