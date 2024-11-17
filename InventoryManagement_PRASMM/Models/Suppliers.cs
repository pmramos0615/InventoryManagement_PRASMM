using InventoryManagement_PRASMM.Data;
using System.Data;

namespace InventoryManagement_PRASMM.Models
{
    public class Suppliers
    {

        #region CONSTRUCTOR
        public Suppliers()
        {
            this.init();
        }

        public void init()
        {
            this.ID = 0;
            this.SubscriptionID = 0;
            this.Name = "";
            this.Address1 = "";
            this.Address2 = "";
            this.TermID = 0;
            this.ContactNo = "";
            this.ContactPerson = "";
            this.EmailAddress = "";
            this.Description = "";
            this.VATRef = "";
            this.TaxTypeID = 0;
            this.Discontinued = 0;
            this.DiscontinuedBy = 0;
            this.DateDiscontinued = DateTime.Now;
            this.CreatedBy = 0;
            this.DateCreated = DateTime.Now;
            this.ModifiedBy = 0;
            this.DateModified = DateTime.Now;
            this.Terms = string.Empty;
        }
        #endregion
        #region Properties
        public int ID { get; set; }
        public int SubscriptionID { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int TermID { get; set; }
        public string ContactNo { get; set; }
        public string ContactPerson { get; set; }
        public string EmailAddress { get; set; }
        public string Description { get; set; }
        public string VATRef { get; set; }
        public int TaxTypeID { get; set; }
        public int Discontinued { get; set; }
        public int DiscontinuedBy { get; set; }
        public DateTime DateDiscontinued { get; set; }
        public int CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime DateModified { get; set; }

        public string FullAddress
        {
            get
            {
                return string.Format("{0} {1}", this.Address1, this.Address2);
            }
        }
        public string Terms { get; set; }

        #endregion

        #region Public Methods
        public static Suppliers GetById(int id)
        {
            var dal = new SuppliersDAL();
            var instance = new Suppliers();
            instance.Bind(dal.GetById(id));
            return instance;
        }
        public static List<Suppliers> GetAll()
        {
            var dal = new SuppliersDAL();
            var collection = new List<Suppliers>();
            foreach (DataRow row in dal.GetAll().Rows)
            {
                var instance = new Suppliers();
                instance.Bind(row);
                collection.Add(instance);
            }
            return collection;
        }
        public static List<Suppliers> GetBySubscriptionID(int subscriptionId)
        {
            var dal = new SuppliersDAL();
            var collection = new List<Suppliers>();
            foreach (DataRow row in dal.GetBySubscriptionID(subscriptionId).Rows)
            {
                var instance = new Suppliers();
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
                this.Discontinued = Convert.ToInt32(row["Discontinued"]);
                this.DateCreated = Convert.ToDateTime(row["DateCreated"]);

                if (!DBNull.Value.Equals(row["Address1"]))
                    this.Address1 = Convert.ToString(row["Address1"]);
                if (!DBNull.Value.Equals(row["Address2"]))
                    this.Address2 = Convert.ToString(row["Address2"]);
                if (!DBNull.Value.Equals(row["TermID"]))
                    this.TermID = Convert.ToInt32(row["TermID"]);
                if (!DBNull.Value.Equals(row["ContactNo"]))
                    this.ContactNo = Convert.ToString(row["ContactNo"]);
                if (!DBNull.Value.Equals(row["ContactPerson"]))
                    this.ContactPerson = Convert.ToString(row["ContactPerson"]);
                if (!DBNull.Value.Equals(row["EmailAddress"]))
                    this.EmailAddress = Convert.ToString(row["EmailAddress"]);
                if (!DBNull.Value.Equals(row["Description"]))
                    this.Description = Convert.ToString(row["Description"]);
                if (!DBNull.Value.Equals(row["VATRef"]))
                    this.VATRef = Convert.ToString(row["VATRef"]);
                if (!DBNull.Value.Equals(row["TaxTypeID"]))
                    this.TaxTypeID = Convert.ToInt32(row["TaxTypeID"]);
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
            var dal = new SuppliersDAL();

            string message = "";
            int ret = dal.Save(this.ID, this.SubscriptionID, this.Name, this.Address1, this.Address2, this.TermID, this.ContactNo, this.ContactPerson, this.EmailAddress, this.Description, this.VATRef, this.TaxTypeID, this.Discontinued, this.DiscontinuedBy, this.DateDiscontinued, this.CreatedBy, this.DateCreated, this.ModifiedBy, this.DateModified, out message);

            this.ID = ret;
            return (ret > 0);
        }
        public static bool Delete(int id, int discontinuedby)
        {
            var dal = new SuppliersDAL();
            bool ret = dal.Delete(id, discontinuedby);
            return ret;
        }
        #endregion
    }
}