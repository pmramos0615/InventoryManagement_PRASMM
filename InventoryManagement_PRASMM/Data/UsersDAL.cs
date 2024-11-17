using System.Data;
using System.Data.SqlClient;

namespace InventoryManagement_PRASMM.Data
{
    internal class UsersDAL : BaseDAL
    {

        public DataTable GetAll()
        {
            base.com.CommandText = "spUsers";
            base.com.Parameters.AddWithValue("@id", 0);
            return base.GetDataTable();

        }
        
        public DataTable GetBySubscription(int subscriptionId)
        {
            base.com.CommandText = "spUsersBySubscriptionID";
            base.com.Parameters.AddWithValue("@subscriptionId", subscriptionId);
            return base.GetDataTable();

        }
        public DataRow GetById(int id)
        {
            base.com.CommandText = "spUsers";
            base.com.Parameters.AddWithValue("@id", id);
            return base.GetFirstRow();
        }

        public int Save(int id, int subscriptionid, string username, string password, string firstname, string mi, string lastname, int departmentid, string contactno, string emailaddress, string address1, string address2, string address3, string address4, int discontinued, int discontinuedby, DateTime datediscontinued, int createdby, DateTime datecreated, int modifiedby, DateTime datemodified, out string message)
        {
            message = "";
            base.com.CommandText = "spUsersUpdate";
            base.com.Parameters.AddWithValue("@id", id);
            base.com.Parameters.AddWithValue("@subscriptionid", subscriptionid);
            base.com.Parameters.AddWithValue("@username", username);
            base.com.Parameters.AddWithValue("@password", password);
            base.com.Parameters.AddWithValue("@firstname", firstname);
            base.com.Parameters.AddWithValue("@mi", mi);
            base.com.Parameters.AddWithValue("@lastname", lastname);
            base.com.Parameters.AddWithValue("@departmentid", departmentid);
            base.com.Parameters.AddWithValue("@contactno", contactno);
            base.com.Parameters.AddWithValue("@emailaddress", emailaddress);
            base.com.Parameters.AddWithValue("@address1", address1);
            base.com.Parameters.AddWithValue("@address2", address2);
            base.com.Parameters.AddWithValue("@address3", address3);
            base.com.Parameters.AddWithValue("@address4", address4);
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
                        message = "Users Name already exists!";
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
            base.com.CommandText = "spUsersDelete";
            base.com.Parameters.AddWithValue("@id", id);
            base.com.Parameters.AddWithValue("@discontinuedby", discontinuedby);

            int ra;
            try
            {
                ra = Convert.ToInt32(base.com.ExecuteScalar());
            }
            catch
            {
                throw new Exception("Delete users failed!");
            }
            return (ra > 0);
        }

    }
}
