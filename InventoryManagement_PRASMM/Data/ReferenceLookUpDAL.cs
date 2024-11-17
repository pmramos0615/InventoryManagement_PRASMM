using System.Data;
using System.Data.SqlClient;

namespace InventoryManagement_PRASMM.Data
{
    internal class ReferenceLookUpDAL : BaseDAL
    {

        public DataTable GetAll()
        {
            base.com.CommandText = "spReferenceLookUp";
            base.com.Parameters.AddWithValue("@id", 0);
            return base.GetDataTable();

        }
        public DataTable GetByFilter(string filterDescription)
        {
            base.com.CommandText = "spReferenceLookUpByDescription";
            base.com.Parameters.AddWithValue("@filterDescription", filterDescription);
            return base.GetDataTable();

        }
        

        public DataRow GetById(int id)
        {
            base.com.CommandText = "spReferenceLookUp";
            base.com.Parameters.AddWithValue("@id", id);
            return base.GetFirstRow();
        }

        public int Save(int id, string name, string description, int discontinued, int discontinuedby, DateTime datediscontinued, int createdby, DateTime datecreated, int modifiedby, DateTime datemodified, out string message)
        {
            message = "";
            base.com.CommandText = "spReferenceLookUpUpdate";
            base.com.Parameters.AddWithValue("@id", id);
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
                        message = "ReferenceLookUp Name already exists!";
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
            base.com.CommandText = "spReferenceLookUpDelete";
            base.com.Parameters.AddWithValue("@id", id);
            base.com.Parameters.AddWithValue("@discontinuedby", discontinuedby);

            int ra;
            try
            {
                ra = Convert.ToInt32(base.com.ExecuteScalar());
            }
            catch
            {
                throw new Exception("Delete referencelookup failed!");
            }
            return (ra > 0);
        }

    }
}
