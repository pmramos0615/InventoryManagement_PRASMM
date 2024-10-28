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

        public DataRow GetById(int id)
        {
            base.com.CommandText = "spProducts";
            base.com.Parameters.AddWithValue("@id", id);
            return base.GetFirstRow();
        }

        public int Save(int id, string name, int categoryid, int subcategoryid, int brandid, int unitid, string sku, int minqty, int qty, string description, int taxid, int discountrate, decimal cost, decimal markuprate, decimal srp, int statusid, string imageurl, string filename, int discontinued, int discontinuedby, DateTime datediscontinued, int createdby, DateTime datecreated, int modifiedby, DateTime datemodified, out string message)
        {
            message = "";
            base.com.CommandText = "spProductsUpdate";
            base.com.Parameters.AddWithValue("@id", id);
            base.com.Parameters.AddWithValue("@name", name);
            base.com.Parameters.AddWithValue("@categoryid", categoryid);
            base.com.Parameters.AddWithValue("@subcategoryid", subcategoryid);
            base.com.Parameters.AddWithValue("@brandid", brandid);
            base.com.Parameters.AddWithValue("@unitid", unitid);
            base.com.Parameters.AddWithValue("@sku", sku);
            base.com.Parameters.AddWithValue("@minqty", minqty);
            base.com.Parameters.AddWithValue("@qty", qty);
            base.com.Parameters.AddWithValue("@description", description);
            base.com.Parameters.AddWithValue("@taxid", taxid);
            base.com.Parameters.AddWithValue("@discountrate", discountrate);
            base.com.Parameters.AddWithValue("@cost", cost);
            base.com.Parameters.AddWithValue("@markuprate", markuprate);
            base.com.Parameters.AddWithValue("@srp", srp);
            base.com.Parameters.AddWithValue("@statusid", statusid);
            base.com.Parameters.AddWithValue("@imageurl", imageurl);
            base.com.Parameters.AddWithValue("@filename", filename);
            base.com.Parameters.AddWithValue("@discontinued", discontinued);
            base.com.Parameters.AddWithValue("@discontinuedby", discontinuedby);
            base.com.Parameters.AddWithValue("@datediscontinued", datediscontinued);
            base.com.Parameters.AddWithValue("@createdby", createdby);
            base.com.Parameters.AddWithValue("@datecreated", datecreated);
            base.com.Parameters.AddWithValue("@modifiedby", modifiedby);
            base.com.Parameters.AddWithValue("@datemodified", datemodified);


            int ra = 0;
            try
            {
                ra = Convert.ToInt32(base.com.ExecuteScalar());
            }
            catch (SqlException sqlex)
            {
                switch (sqlex.Number)
                {
                    case 2601:
                        message = "Products Name already exists!";
                        break;
                    default:
                        message = "Update failed!";
                        break;
                }
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
