using System.Data;

namespace InventoryManagement_PRASMM.Data
{
    internal class ProductSubCategoryDAL:BaseDAL
    {
        public DataTable GetAllSubCategoryByCategoryID(int categoryid) 
        {
            base.com.CommandText = "spGetSubCategoryByCategoryID";
            base.com.Parameters.AddWithValue("@id", 0);
            base.com.Parameters.AddWithValue("@categoryId", categoryid);
            return base.GetDataTable();
        }
        public DataRow GetByIDSubCategoryByCategoryID(int id, int categoryid) 
        {
            base.com.CommandText = "spGetSubCategoryByCategoryID";
            base.com.Parameters.AddWithValue("@id", id);
            base.com.Parameters.AddWithValue("@categoryId", categoryid);
            return base.GetFirstRow();
        }
    }
}
