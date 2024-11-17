using Microsoft.Data.SqlClient;
using System.Data;

namespace InventoryManagement_PRASMM.Data
{
    internal class BrandsDAL : BaseDAL
    {

        public DataTable GetAll()
        {
            base.com.CommandText = "spBrands";
            base.com.Parameters.AddWithValue("@id", 0);
            return base.GetDataTable();

        }
        public DataTable GetBySubscription(int subscriptionId)
        {
            base.com.CommandText = "spBrandsBySubscriptionID";
            base.com.Parameters.AddWithValue("@subscriptionId", subscriptionId);
            return base.GetDataTable();

        }
        
        public DataRow GetById(int id)
        {
            base.com.CommandText = "spBrands";
            base.com.Parameters.AddWithValue("@id", id);
            return base.GetFirstRow();
        }

        public int Save(int id, int subscriptionId, string name, string description, int discontinued, int discontinuedby, DateTime datediscontinued, int createdby, DateTime datecreated, int modifiedby, DateTime datemodified, out string message)
        {
            message = "";
            base.com.CommandText = "spBrandsUpdate";
            base.com.Parameters.AddWithValue("@id", id);
            base.com.Parameters.AddWithValue("@subscriptionId", subscriptionId);
            base.com.Parameters.AddWithValue("@name", name);
            base.com.Parameters.AddWithValue("@description", description);
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
                        message = "Brands Name already exists!";
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
            base.com.CommandText = "spBrandsDelete";
            base.com.Parameters.AddWithValue("@id", id);
            base.com.Parameters.AddWithValue("@discontinuedby", discontinuedby);

            int ra;
            try
            {
                ra = Convert.ToInt32(base.com.ExecuteScalar());
            }
            catch
            {
                throw new Exception("Delete brands failed!");
            }
            return (ra > 0);
        }

    }
}
