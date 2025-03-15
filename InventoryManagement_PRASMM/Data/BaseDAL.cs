
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace InventoryManagement_PRASMM.Data
{
    internal abstract class BaseDAL
    {
        protected SqlConnection cnn;
        protected SqlCommand com;
        protected SqlTransaction tran;

        #region CTOR



        protected BaseDAL()
        {
            //add reference to System.Configuration.dll
            var configurations = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            bool isOnlineServer =Convert.ToBoolean(configurations["ServerType:IsOnline"]);

            string serverName = "";
            string userName = "";
            string password = "";
            string database = "";

            if (isOnlineServer)
            {
                serverName = configurations["ConnectionStrings:Server"];
                userName = configurations["ConnectionStrings:UserName"];
                password = configurations["ConnectionStrings:Password"];
                database = configurations["ConnectionStrings:Database"];
            }
            else
            {
                serverName =configurations["ConnectionStrings_Local:Server"];
                userName = configurations["ConnectionStrings_Local:UserName"];
                password = configurations["ConnectionStrings_Local:Password"];
                database = configurations["ConnectionStrings_Local:Database"];
            }
            




            string connStr = "Data Source=" + Decrypt(serverName, true) + ";";
            connStr += "user=" + Decrypt(userName, true) + ";";
            connStr += "password=" + Decrypt(password, true) + ";";
            connStr += " database=" + Decrypt(database, true) + ";";

            //string connStr = "Server=sql5111.site4now.net;Database=db_aaeb12_inventory;User Id=db_aaeb12_inventory_admin;Password=N0rthf4c3;";

            ////connection pooling
            ///**/
            //connStr += "connection reset=false;";
            //connStr += "connection lifetime=0;";
            //connStr += "enlist=true;";
            //connStr += "min pool size=1;";
            //connStr += "max pool size=500";
            ///**/
            ///



            cnn = new SqlConnection(connStr);


            this.com = new SqlCommand();
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandTimeout = 1000;
            //SqlDependency.Start(connD);
            try
            {
                if (this.cnn.State == ConnectionState.Closed) this.cnn.Open();
                this.com.Connection = cnn;

                //cnn.ConnectionString = connStr;
                //this.cnn.Open();
                //this.com.Connection = cnn;
            }
            catch (Exception ex)
            {
#if DEBUG
                throw new Exception(ex.Message);
#else
                throw new Exception(ex.Message);
                //throw new Exception("error");
#endif
            }
            finally
            {
                //if (this.com != null) this.com = null;
                //if (this.cnn.State == ConnectionState.Open) this.cnn.Close();
                //if (this.cnn != null) this.cnn = null;
                //if (this.cnn.State == ConnectionState.Open) this.cnn.Close();
            }
        }

        ~BaseDAL()
        {
            try
            {
                //if (cnn != null)
                //    cnn.Dispose();
            }
            finally { }
        }

        #endregion

        #region Methods

        protected DataRow GetFirstRow()
        {
            DataTable dt = this.GetDataTable();

            if (dt.Rows.Count > 0)
                return dt.Rows[0];
            else
                return null;

        }

        protected DataTable GetDataTable()
        {
            DataTable ret = new DataTable();
            try
            {
                ret.Load(this.com.ExecuteReader(CommandBehavior.CloseConnection));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
                throw ex;
            }

            cnn.Close();
                
            return ret;
        }

        protected SqlDataReader GetDataReader()
        {
            return this.com.ExecuteReader(CommandBehavior.CloseConnection);
        }

        protected DataSet GetDataSet2(string parentname, string childname)
        {
            DataSet ds = new DataSet();

            DataTable parent = new DataTable(parentname);
            DataTable child = new DataTable(childname);

            try
            {
                SqlDataReader dr = this.com.ExecuteReader();

                parent.Load(dr);
                ds.Tables.Add(parent);

                dr.NextResult();

                child.Load(dr);

                ds.Tables.Add(child);


                dr.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print(ex.Message);
            }


            cnn.Close();

            return ds;
        }

        void dep_OnChange(object sender, SqlNotificationEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Received OnChange Event");
            return;
        }

        protected DataSet GetDataSet(string tablename)
        {
            SqlDataAdapter da = new SqlDataAdapter(this.com);

            DataSet ret = new DataSet();
            da.Fill(ret, tablename);

            cnn.Close();
            da.Dispose();

            return ret;
        }

        internal void Begin()
        {
            tran = this.cnn.BeginTransaction();
            this.com.Transaction = tran;
        }
        internal void End(bool Commit)
        {
            if (Commit)
                tran.Commit();
            else
                tran.Rollback();
        }

        #endregion

        public static string Decrypt(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            //System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
            //Get your key from config file to open the lock!

            //string key = (string)settingsReader.GetValue("SecurityKey", typeof(String));
            string key = "progRAMOS";// Security Key

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            tdes.Clear();
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
    }
}
