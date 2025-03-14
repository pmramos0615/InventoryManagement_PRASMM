using InventoryManagement_PRASMM.Data;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace InventoryManagement_PRASMM.Models
{
    public class Users
    {

        #region CONSTRUCTOR
        public Users()
        {
            this.init();
        }

        public void init()
        {
            this.ID = 0;
            this.SubscriptionID = 0;
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

        }
        #endregion
        #region Properties
        public int ID { get; set; }
        public int SubscriptionID { get; set; }
        public string Subscriber { get; set; }
        public int DepartmentID { get; set; }
        public string Department { get; set; }
        public int UserRoleID { get; set; }
        public string UserRole { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }
        [Display(Name = "Password")]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string MI { get; set; }
        public string LastName { get; set; }
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

        public string FullName {
            get { return string.Format("{2} {1}. {0}", this.LastName,this.MI, this.FirstName); }
        }
        public string FullnameFormalFormat 
        { 
            get 
            {
                return string.Format("{0}, {2} {1}.", this.LastName, this.MI, this.FirstName);
            }
        }
        #endregion
        public void Bind(DataRow row)
        {
            if (row != null)
            {
                this.ID = Convert.ToInt32(row["ID"]);
                this.UserName = Convert.ToString(row["UserName"]);
                this.Discontinued = Convert.ToInt32(row["Discontinued"]);
                this.DateCreated = Convert.ToDateTime(row["CreatedDate"]);

                if (!DBNull.Value.Equals(row["SubscriptionID"]))
                    this.SubscriptionID = Convert.ToInt32(row["SubscriptionID"]);
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
                if (!DBNull.Value.Equals(row["DiscontinuedBy"]))
                    this.DiscontinuedBy = Convert.ToInt32(row["DiscontinuedBy"]);
                if (!DBNull.Value.Equals(row["DiscontinuedDate"]))
                    this.DateDiscontinued = Convert.ToDateTime(row["DiscontinuedDate"]);
                if (!DBNull.Value.Equals(row["CreatedBy"]))
                    this.CreatedBy = Convert.ToInt32(row["CreatedBy"]);
                if (!DBNull.Value.Equals(row["ModifiedBy"]))
                    this.ModifiedBy = Convert.ToInt32(row["ModifiedBy"]);
                if (!DBNull.Value.Equals(row["ModifiedDate"]))
                    this.DateModified = Convert.ToDateTime(row["ModifiedDate"]);
            }
        }
        public void BindUserDescription(DataRow row) 
        {
            if (row != null) 
            {
                this.ID = Convert.ToInt32(row["ID"]);
                if (!DBNull.Value.Equals(row["Department"]))
                this.Department = Convert.ToString(row["Department"]);
                if (!DBNull.Value.Equals(row["Subscriber"]))
                this.Subscriber = Convert.ToString(row["Subscriber"]);
                if (!DBNull.Value.Equals(row["UserRole"]))
                this.UserRole = Convert.ToString(row["UserRole"]);
            }
        }
        #region Public Methods
        public static Users GetById(int id)
        {
            var dal = new UsersDAL();
            var instance = new Users();
            instance.Bind(dal.GetById(id));
            return instance;
        }
        public static Users GetUserDescriptionByLogin(string username, string password) 
        {
            var dal = new UsersDAL();
            var instance = new Users();
            instance.BindUserDescription(dal.GetUserDescriptionByLogin(username, password));
            return instance;
        }
        public static List<Users> GetAll()
        {
            var dal = new UsersDAL();
            var collection = new List<Users>();
            foreach (DataRow row in dal.GetAll().Rows)
            {
                var instance = new Users();
                instance.Bind(row);
                collection.Add(instance);
            }
            return collection;
        }
        public static List<Users> GetBySubscription(int subscriptionId)
        {
            var dal = new UsersDAL();
            var collection = new List<Users>();
            foreach (DataRow row in dal.GetBySubscription(subscriptionId).Rows)
            {
                var instance = new Users();
                instance.Bind(row);
                collection.Add(instance);
            }
            return collection;
        }
        public bool Save()
        {
            var dal = new UsersDAL();

            string message = "";
            int ret = dal.Save(this.ID, this.SubscriptionID, this.UserName, this.Password, this.FirstName, this.MI, this.LastName, this.DepartmentID, this.ContactNo, this.EmailAddress, this.Address1, this.Address2, this.Address3, this.Address4, this.Discontinued, this.DiscontinuedBy, this.DateDiscontinued, this.CreatedBy, this.DateCreated, this.ModifiedBy, this.DateModified, out message);

            this.ID = ret;
            return (ret > 0);
        }
        public static Users GetUserByLogin(string username, string password)
        {
            var dal = new UsersDAL();
            var instance = new Users();
            instance.Bind(dal.GetUserByLogin(username, password));
            return instance;
        }
        public static bool Login(string username,string password)
        {
            var dal = new UsersDAL();
            bool ra = dal.Login(username, password);
            return ra;
        }
        public static bool Delete(int id, int discontinuedby)
        {
            var dal = new UsersDAL();
            bool ret = dal.Delete(id, discontinuedby);
            return ret;
        }
        #endregion
    }
}
