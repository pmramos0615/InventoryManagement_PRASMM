using InventoryManagement_PRASMM.Data;
using System.Data;

namespace InventoryManagement_PRASMM.Models
{
    public class Units
    {

        #region CONSTRUCTOR
        public Units()
        {
            this.init();
        }

        public void init()
        {
            this.ID = 0;
            this.Name = "";
            this.Discontinued = 0;
            this.DiscontinuedBy = 0;
            this.DateDiscontinued = DateTime.Now;
            this.CreatedBy = 0;
            this.DateCreated = DateTime.Now;
            this.ModifiedBy = 0;
            this.DateModified = DateTime.Now;

        }
        #endregion
        #region Properties
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Discontinued { get; set; }
        public int DiscontinuedBy { get; set; }
        public DateTime DateDiscontinued { get; set; }
        public int CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime DateModified { get; set; }

        #endregion

        #region Public Methods
        public static Units GetById(int id)
        {
            var dal = new UnitsDAL();
            var instance = new Units();
            instance.Bind(dal.GetById(id));
            return instance;
        }
        public static List<Units> GetAll()
        {
            var dal = new UnitsDAL();
            var collection = new List<Units>();
            foreach (DataRow row in dal.GetAll().Rows)
            {
                var instance = new Units();
                instance.Bind(row);
                collection.Add(instance);
            }
            return collection;
        }
        public static Units GetbySubscriptionID(int subscriptionid, int id)
        {
            var dal = new UnitsDAL();
            var instance = new Units();
            instance.Bind(dal.GetBySubscriptionID(subscriptionid, id));
            return instance;
        }
        public static List<Units> GetAllBySubscriptionID(int subscriptionid)
        {
            var dal = new UnitsDAL();
            var collection = new List<Units>();
            foreach (DataRow row in dal.GetAllBySubscriptionID(subscriptionid).Rows)
            {
                var instance = new Units();
                instance.Bind(row);
                collection.Add(instance);
            }
            return collection;
        }
        public void Bind(DataRow row)
        {
            if (row != null)
            {
                this.ID = Convert.ToInt32(row["ID"]);

                if(!DBNull.Value.Equals(row["Name"]))
                    this.Name = Convert.ToString(row["Name"]);
                if (!DBNull.Value.Equals(row["Code"]))
                    this.Code = Convert.ToString(row["Code"]);
                if(!DBNull.Value.Equals(row["Discontinued"]))
                    this.Discontinued = Convert.ToInt32(row["Discontinued"]);
                if (!DBNull.Value.Equals(row["DiscontinuedBy"]))
                    this.DiscontinuedBy = Convert.ToInt32(row["DiscontinuedBy"]);
                if (!DBNull.Value.Equals(row["DateDiscontinued"]))
                    this.DateDiscontinued = Convert.ToDateTime(row["DateDiscontinued"]);
                if (!DBNull.Value.Equals(row["DateCreated"]))
                    this.DateCreated = Convert.ToDateTime(row["DateCreated"]);
                if (!DBNull.Value.Equals(row["CreatedBy"]))
                    this.CreatedBy = Convert.ToInt32(row["CreatedBy"]);
                if (!DBNull.Value.Equals(row["ModifiedBy"]))
                    this.ModifiedBy = Convert.ToInt32(row["ModifiedBy"]);
                if (!DBNull.Value.Equals(row["DateModified"]))
                    this.DateModified = Convert.ToDateTime(row["DateModified"]);

            }
        }
        public bool Save()
        {
            var dal = new UnitsDAL();

            string message = "";
            int ret = dal.Save(this.ID, this.Name, this.Discontinued, this.DiscontinuedBy, this.DateDiscontinued, this.CreatedBy, this.DateCreated, this.ModifiedBy, this.DateModified, out message);

            this.ID = ret;
            return (ret > 0);
        }
        public static bool Delete(int id, int discontinuedby)
        {
            var dal = new UnitsDAL();
            bool ret = dal.Delete(id, discontinuedby);
            return ret;
        }
        #endregion
    }
}
