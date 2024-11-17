using InventoryManagement_PRASMM.Data;
using System.Data;

namespace InventoryManagement_PRASMM.Models
{
    public class Stores
    {

        #region CONSTRUCTOR
        public Stores()
        {
            this.init();
        }

        public void init()
        {
            this.ID = 0;
            this.SubscriptionID = 0;
            this.Name = "";
            this.Address = "";
            this.EmployeeID = 0;
            this.ContactNo = "";
            this.EmailAddress = "";
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
        public int SubscriptionID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int EmployeeID { get; set; }
        public string ContactNo { get; set; }
        public string EmailAddress { get; set; }
        public int Discontinued { get; set; }
        public int DiscontinuedBy { get; set; }
        public DateTime DateDiscontinued { get; set; }
        public int CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime DateModified { get; set; }

        #endregion

        #region Public Methods
        public static Stores GetById(int id)
        {
            var dal = new StoresDAL();
            var instance = new Stores();
            instance.Bind(dal.GetById(id));
            return instance;
        }
        public static List<Stores> GetAll()
        {
            var dal = new StoresDAL();
            var collection = new List<Stores>();
            foreach (DataRow row in dal.GetAll().Rows)
            {
                var instance = new Stores();
                instance.Bind(row);
                collection.Add(instance);
            }
            return collection;
        }
        public static List<Stores> GetBySubscription(int subscriptionId)
        {
            var dal = new StoresDAL();
            var collection = new List<Stores>();
            foreach (DataRow row in dal.GetBySubscription(subscriptionId).Rows)
            {
                var instance = new Stores();
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
                this.Name = Convert.ToString(row["Name"]);
                this.Address = Convert.ToString(row["Address"]);
                this.Discontinued = Convert.ToInt32(row["Discontinued"]);
                this.DateCreated = Convert.ToDateTime(row["DateCreated"]);

                if (!DBNull.Value.Equals(row["EmployeeID"]))
                    this.EmployeeID = Convert.ToInt32(row["EmployeeID"]);
                if (!DBNull.Value.Equals(row["ContactNo"]))
                    this.ContactNo = Convert.ToString(row["ContactNo"]);
                if (!DBNull.Value.Equals(row["EmailAddress"]))
                    this.EmailAddress = Convert.ToString(row["EmailAddress"]);
                if (!DBNull.Value.Equals(row["DiscontinuedBy"]))
                    this.DiscontinuedBy = Convert.ToInt32(row["DiscontinuedBy"]);
                if (!DBNull.Value.Equals(row["DateDiscontinued"]))
                    this.DateDiscontinued = Convert.ToDateTime(row["DateDiscontinued"]);
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
            var dal = new StoresDAL();

            string message = "";
            int ret = dal.Save(this.ID,this.SubscriptionID, this.Name, this.Address, this.EmployeeID, this.ContactNo, this.EmailAddress, this.Discontinued, this.DiscontinuedBy, this.DateDiscontinued, this.CreatedBy, this.DateCreated, this.ModifiedBy, this.DateModified, out message);

            this.ID = ret;
            return (ret > 0);
        }
        public static bool Delete(int id, int discontinuedby)
        {
            var dal = new StoresDAL();
            bool ret = dal.Delete(id, discontinuedby);
            return ret;
        }
        #endregion
    }
}
