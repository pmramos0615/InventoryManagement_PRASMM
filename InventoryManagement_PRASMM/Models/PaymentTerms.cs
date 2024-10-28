using InventoryManagement_PRASMM.Data;
using System.Data;

namespace InventoryManagement_PRASMM.Models
{
    public class PaymentTerms
    {

        #region CONSTRUCTOR
        public PaymentTerms()
        {
            this.init();
        }

        public void init()
        {
            this.ID = 0;
            this.Name = "";
            this.NoOfDays = 0;
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
        public string Name { get; set; }
        public int NoOfDays { get; set; }
        public int Discontinued { get; set; }
        public int DiscontinuedBy { get; set; }
        public DateTime DateDiscontinued { get; set; }
        public int CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime DateModified { get; set; }

        #endregion

        #region Public Methods
        public static PaymentTerms GetById(int id)
        {
            var dal = new PaymentTermsDAL();
            var instance = new PaymentTerms();
            instance.Bind(dal.GetById(id));
            return instance;
        }
        public static List<PaymentTerms> GetAll()
        {
            var dal = new PaymentTermsDAL();
            var collection = new List<PaymentTerms>();
            foreach (DataRow row in dal.GetAll().Rows)
            {
                var instance = new PaymentTerms();
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
                this.NoOfDays = Convert.ToInt32(row["NoOfDays"]);
                this.Discontinued = Convert.ToInt32(row["Discontinued"]);
                this.DateCreated = Convert.ToDateTime(row["DateCreated"]);

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
            var dal = new PaymentTermsDAL();

            string message = "";
            int ret = dal.Save(this.ID, this.Name, this.NoOfDays, this.Discontinued, this.DiscontinuedBy, this.DateDiscontinued, this.CreatedBy, this.DateCreated, this.ModifiedBy, this.DateModified, out message);

            this.ID = ret;
            return (ret > 0);
        }
        public static bool Delete(int id, int discontinuedby)
        {
            var dal = new PaymentTermsDAL();
            bool ret = dal.Delete(id, discontinuedby);
            return ret;
        }
        #endregion
    }
}