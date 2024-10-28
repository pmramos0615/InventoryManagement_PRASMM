using InventoryManagement_PRASMM.Data;
using System.Data;

namespace InventoryManagement_PRASMM.Models
{
    public class Employees
    {

        #region CONSTRUCTOR
        public Employees()
        {
            this.init();
        }

        public void init()
        {
            this.ID = 0;
            this.UserName = "";
            this.Password = "";
            this.FirstName = "";
            this.MI = "";
            this.LastName = "";
            this.DepartmentID = 0;
            this.ContactNo = "";
            this.EmailAddress = "";
            this.Address1 = "";
            this.Address2 = "";
            this.Address3 = "";
            this.Address4 = "";
            this.Discontinued = 0;
            this.DiscontinuedBy = 0;
            this.DateDiscontinued = DateTime.Now;
            this.CreatedBy = 0;
            this.DateCreated = DateTime.Now;
            this.ModifiedBy = 0;
            this.DateModified = DateTime.Now;
            this.FullName = string.Empty;
            

        }
        #endregion
        #region Properties
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string MI { get; set; }
        public string LastName { get; set; }
        public int DepartmentID { get; set; }
        public string ContactNo { get; set; }
        public string EmailAddress { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public int Discontinued { get; set; }
        public int DiscontinuedBy { get; set; }
        public DateTime DateDiscontinued { get; set; }
        public int CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime DateModified { get; set; }
        public string FullName { get; set; }

        #endregion

        #region Public Methods
        public static Employees GetById(int id)
        {
            var dal = new EmployeesDAL();
            var instance = new Employees();
            instance.Bind(dal.GetById(id));
            return instance;
        }
        public static List<Employees> GetAll()
        {
            var dal = new EmployeesDAL();
            var collection = new List<Employees>();
            foreach (DataRow row in dal.GetAll().Rows)
            {
                var instance = new Employees();
                instance.Bind(row);
                collection.Add(instance);
            }
            return collection;
        }

        public static List<Employees> LookUp()
        {
            var dal = new EmployeesDAL();
            var collection = new List<Employees>();
            foreach (DataRow row in dal.GetAll().Rows)
            {
                var instance = new Employees();
                instance.BindLookUp(row);
                collection.Add(instance);
            }
            return collection;
        }

        public void BindLookUp(DataRow row)
        {
            if (row != null)
            {
                this.ID = Convert.ToInt32(row["ID"]);
                this.FullName = string.Format("{0} {1}", row["FirstName"], row["LastName"]);

            }
        }
        public void Bind(DataRow row)
        {
            if (row != null)
            {
                this.ID = Convert.ToInt32(row["ID"]);
                this.UserName = Convert.ToString(row["UserName"]);
                this.Discontinued = Convert.ToInt32(row["Discontinued"]);
                this.DateCreated = Convert.ToDateTime(row["DateCreated"]);

                if (!DBNull.Value.Equals(row["Password"]))
                    this.Password = Convert.ToString(row["Password"]);
                if (!DBNull.Value.Equals(row["FirstName"]))
                    this.FirstName = Convert.ToString(row["FirstName"]);
                if (!DBNull.Value.Equals(row["MI"]))
                    this.MI = Convert.ToString(row["MI"]);
                if (!DBNull.Value.Equals(row["LastName"]))
                    this.LastName = Convert.ToString(row["LastName"]);
                if (!DBNull.Value.Equals(row["DepartmentID"]))
                    this.DepartmentID = Convert.ToInt32(row["DepartmentID"]);
                if (!DBNull.Value.Equals(row["ContactNo"]))
                    this.ContactNo = Convert.ToString(row["ContactNo"]);
                if (!DBNull.Value.Equals(row["EmailAddress"]))
                    this.EmailAddress = Convert.ToString(row["EmailAddress"]);
                if (!DBNull.Value.Equals(row["Address1"]))
                    this.Address1 = Convert.ToString(row["Address1"]);
                if (!DBNull.Value.Equals(row["Address2"]))
                    this.Address2 = Convert.ToString(row["Address2"]);
                if (!DBNull.Value.Equals(row["Address3"]))
                    this.Address3 = Convert.ToString(row["Address3"]);
                if (!DBNull.Value.Equals(row["Address4"]))
                    this.Address4 = Convert.ToString(row["Address4"]);
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
            var dal = new EmployeesDAL();

            string message = "";
            int ret = dal.Save(this.ID, this.UserName, this.Password, this.FirstName, this.MI, this.LastName, this.DepartmentID, this.ContactNo, this.EmailAddress, this.Address1, this.Address2, this.Address3, this.Address4, this.Discontinued, this.DiscontinuedBy, this.DateDiscontinued, this.CreatedBy, this.DateCreated, this.ModifiedBy, this.DateModified, out message);

            this.ID = ret;
            return (ret > 0);
        }
        public static bool Delete(int id, int discontinuedby)
        {
            var dal = new EmployeesDAL();
            bool ret = dal.Delete(id, discontinuedby);
            return ret;
        }
        #endregion
    }
}
