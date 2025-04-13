using System.Data;
using InventoryManagement_PRASMM.Data;

namespace InventoryManagement_PRASMM.Models
{
    public class ProductSubCategory
    {
        ProductSubCategory()
        {
            this.init();
        }
        public void init()
        {

        }

        public int ID { get; set; }
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Discontinued { get; set; }
        public int DiscontinuedBy { get; set; }
        public DateTime DateDiscontinued { get; set; }
        public int CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime DateModified { get; set; }

        public void Bind(DataRow row)
        {
            if (row != null)
            {
                this.ID = Convert.ToInt32(row["ID"]);
                this.CategoryID = Convert.ToInt32(row["CategoryID"]);

                if (!DBNull.Value.Equals(row["Name"]))
                    this.Name = Convert.ToString(row["Name"]);
                if (!DBNull.Value.Equals(row["Description"]))
                    this.Description = Convert.ToString(row["Description"]);
                if (!DBNull.Value.Equals(row["Discontinued"]))
                    this.Discontinued = Convert.ToInt32(row["Discontinued"]);
                if (!DBNull.Value.Equals(row["DateDiscontinued"]))
                    this.DateDiscontinued = Convert.ToDateTime(row["DateDiscontinued"]);
                if (!DBNull.Value.Equals(row["DiscontinuedBy"]))
                    this.DiscontinuedBy = Convert.ToInt32(row["DiscontinuedBy"]);
                if (!DBNull.Value.Equals(row["CreatedBy"]))
                    this.CreatedBy = Convert.ToInt32(row["CreatedBy"]);
                if (!DBNull.Value.Equals(row["DateCreated"]))
                    this.DateCreated = Convert.ToDateTime(row["DateCreated"]);
                if (!DBNull.Value.Equals(row["ModifiedBy"]))
                    this.ModifiedBy = Convert.ToInt32(row["ModifiedBy"]);
                if (!DBNull.Value.Equals(row["DateModified"]))
                    this.DateModified = Convert.ToDateTime(row["DateModified"]);
            }
        }

        public static List<ProductSubCategory> GetAllSubCategoryByCategoryID(int categoryid)
        {
            List<ProductSubCategory> collection = new List<ProductSubCategory>();
            ProductSubCategoryDAL dal = new ProductSubCategoryDAL();
            DataTable dt = dal.GetAllSubCategoryByCategoryID(categoryid);
            foreach (DataRow row in dt.Rows)
            {
                ProductSubCategory instance = new ProductSubCategory();
                instance.Bind(row);
                collection.Add(instance);
            }
            return collection;
        }
        public static ProductSubCategory GetByIDSubCategoryByCategoryID(int id, int categoryid)
        {
            ProductSubCategory instance = new ProductSubCategory();
            ProductSubCategoryDAL dal = new ProductSubCategoryDAL();
            DataRow row = dal.GetByIDSubCategoryByCategoryID(id, categoryid);
            instance.Bind(row);
            return instance;
        }
    }
}
