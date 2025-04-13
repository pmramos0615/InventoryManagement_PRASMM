using InventoryManagement_PRASMM.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using System.Data;

namespace InventoryManagement_PRASMM.Data
{
    internal class ProductsDAL : BaseDAL
    {

        public DataTable GetAll()
        {
            base.com.CommandText = "spProducts";
            base.com.Parameters.AddWithValue("@id", 0);
            return base.GetDataTable();

        }
        public DataTable GetBySuscriptionId(int subscriptionId)
        {
            base.com.CommandText = "spProductsBySubscriptionID";
            base.com.Parameters.AddWithValue("@subscriptionId", subscriptionId);
            return base.GetDataTable();

        }

        public DataRow GetById(int id)
        {
            base.com.CommandText = "spProducts";
            base.com.Parameters.AddWithValue("@id", id);
            return base.GetFirstRow();
        }

        public int Save(int id,int subscriptionId,string name,string sku,int categoryid,int subcategoryid,int brandid,int unitid,string barcode,string itemcode,string description,decimal acquiredcost,decimal markupprice
            ,decimal srp,int minqty,int taxid,int taxamountid,int producttypeid,int varianttypeid,int specifiedvarianid,string filename,string imageurl,int createdby,DateTime datecreated,int modifiedby,DateTime datemodified)
        {
            base.com.CommandText = "spProductsUpdate";
            base.com.Parameters.AddWithValue("@id", id);
            base.com.Parameters.AddWithValue("@subscriptionId", subscriptionId);
            base.com.Parameters.AddWithValue("@name", name);
            base.com.Parameters.AddWithValue("@sku", sku);
            base.com.Parameters.AddWithValue("@categoryid", categoryid);
            base.com.Parameters.AddWithValue("@subcategoryid", subcategoryid);
            base.com.Parameters.AddWithValue("@brandid", brandid);
            base.com.Parameters.AddWithValue("@unitid", unitid);
            base.com.Parameters.AddWithValue("@barcode", barcode);
            base.com.Parameters.AddWithValue("@itemcode", itemcode);
            base.com.Parameters.AddWithValue("@description", description);
            base.com.Parameters.AddWithValue("@acquiredcost", acquiredcost);
            base.com.Parameters.AddWithValue("@markupprice", markupprice);
            base.com.Parameters.AddWithValue("@srp", srp);
            base.com.Parameters.AddWithValue("@minqty", minqty);
            base.com.Parameters.AddWithValue("@taxid", taxid);
            base.com.Parameters.AddWithValue("@taxamountid", taxamountid);
            base.com.Parameters.AddWithValue("@producttypeid", producttypeid);
            base.com.Parameters.AddWithValue("@varianttypeid", varianttypeid);
            base.com.Parameters.AddWithValue("@specifiedvariantid", specifiedvarianid);
            base.com.Parameters.AddWithValue("@filename", filename);
            base.com.Parameters.AddWithValue("@imageurl", imageurl);
            base.com.Parameters.AddWithValue("@createdby", createdby);
            base.com.Parameters.AddWithValue("@datecreated", datecreated);
            base.com.Parameters.AddWithValue("@modifiedby", modifiedby);
            base.com.Parameters.AddWithValue("@datemodified", datemodified);

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

        public bool Delete(int id, int discontinuedby)
        {
            base.com.CommandText = "spProductsDelete";
            base.com.Parameters.AddWithValue("@id", id);
            base.com.Parameters.AddWithValue("@discontinuedby", discontinuedby);

            int ra;
            try
            {
                ra = Convert.ToInt32(base.com.ExecuteScalar());
            }
            catch
            {
                throw new Exception("Delete products failed!");
            }
            return (ra > 0);
        }

    }
}