﻿using Microsoft.Data.SqlClient;
using System.Data;

namespace InventoryManagement_PRASMM.Data
{
    internal class UnitsDAL : BaseDAL
    {

        public DataTable GetAll()
        {
            base.com.CommandText = "spUnits";
            base.com.Parameters.AddWithValue("@id", 0);
            return base.GetDataTable();

        }

        public DataRow GetById(int id)
        {
            base.com.CommandText = "spUnits";
            base.com.Parameters.AddWithValue("@id", id);
            return base.GetFirstRow();
        }
        public DataTable GetAllBySubscriptionID(int subscriptionid) 
        {
            base.com.CommandText = "spUnitsBySubscriptionID";
            base.com.Parameters.AddWithValue("@subscriptionid", subscriptionid);
            base.com.Parameters.AddWithValue("@id", 0);
            return base.GetDataTable();
        }
        public DataRow GetBySubscriptionID(int subscriptionid,int id) 
        {
            base.com.CommandText = "spUnitsBySubscriptionID";
            base.com.Parameters.AddWithValue("@subscriptionid", subscriptionid);
            base.com.Parameters.AddWithValue("@id", id);
            return base.GetFirstRow();
        }
        public int Save(int id, string name, int discontinued, int discontinuedby, DateTime datediscontinued, int createdby, DateTime datecreated, int modifiedby, DateTime datemodified, out string message)
        {
            message = "";
            base.com.CommandText = "spUnitsUpdate";
            base.com.Parameters.AddWithValue("@id", id);
            base.com.Parameters.AddWithValue("@name", name);
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
                        message = "Units Name already exists!";
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
            base.com.CommandText = "spUnitsDelete";
            base.com.Parameters.AddWithValue("@id", id);
            base.com.Parameters.AddWithValue("@discontinuedby", discontinuedby);

            int ra;
            try
            {
                ra = Convert.ToInt32(base.com.ExecuteScalar());
            }
            catch
            {
                throw new Exception("Delete units failed!");
            }
            return (ra > 0);
        }

    }
}
