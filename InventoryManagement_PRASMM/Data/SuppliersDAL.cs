using Microsoft.Data.SqlClient;
using System.Data;

namespace InventoryManagement_PRASMM.Data
{
    internal class SuppliersDAL : BaseDAL
    {

        public DataTable GetAll()
        {
            base.com.CommandText = "spSuppliers";
            base.com.Parameters.AddWithValue("@id", 0);
            return base.GetDataTable();

        }
        public DataTable GetBySubscriptionID(int subscriptionId)
        {
            base.com.CommandText = "spSuppliersBySubscriptionID";
            base.com.Parameters.AddWithValue("@subscriptionId", subscriptionId);
            return base.GetDataTable();

        }

        

        public DataRow GetById(int id)
        {
            base.com.CommandText = "spSuppliers";
            base.com.Parameters.AddWithValue("@id", id);
            return base.GetFirstRow();
        }

        public int Save(int id, int subscriptionId, string name, string address1, string address2, int termid, string contactno, string contactperson, string emailaddress, string description, string vatref, int taxtypeid, int discontinued, int discontinuedby, DateTime datediscontinued, int createdby, DateTime datecreated, int modifiedby, DateTime datemodified, out string message)
        {
            message = "";
            base.com.CommandText = "spSuppliersUpdate";
            base.com.Parameters.AddWithValue("@id", id);
            base.com.Parameters.AddWithValue("@name", name);
            base.com.Parameters.AddWithValue("@subscriptionId", subscriptionId);
            base.com.Parameters.AddWithValue("@address1", address1);
            base.com.Parameters.AddWithValue("@address2", address2);
            base.com.Parameters.AddWithValue("@termid", termid);
            base.com.Parameters.AddWithValue("@contactno", contactno);
            base.com.Parameters.AddWithValue("@contactperson", contactperson);
            base.com.Parameters.AddWithValue("@emailaddress", emailaddress);
            base.com.Parameters.AddWithValue("@description", description);
            base.com.Parameters.AddWithValue("@vatref", vatref);
            base.com.Parameters.AddWithValue("@taxtypeid", taxtypeid);
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
                        message = "Suppliers Name already exists!";
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
            base.com.CommandText = "spSuppliersDelete";
            base.com.Parameters.AddWithValue("@id", id);
            base.com.Parameters.AddWithValue("@discontinuedby", discontinuedby);

            int ra;
            try
            {
                ra = Convert.ToInt32(base.com.ExecuteScalar());
            }
            catch
            {
                throw new Exception("Delete suppliers failed!");
            }
            return (ra > 0);
        }

    }
}
