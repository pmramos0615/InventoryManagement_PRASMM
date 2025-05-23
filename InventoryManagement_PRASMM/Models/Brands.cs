﻿using InventoryManagement_PRASMM.Data;
using System.Data;

namespace InventoryManagement_PRASMM.Models
{
    public class Brands
    {

        #region CONSTRUCTOR
        public Brands()
        {
            this.init();
        }

        public void init()
        {
            this.ID = 0;
            this.SubscriptionID = 0;
            this.Name = "";
            this.Description = "";
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
        public string Description { get; set; }
        public int Discontinued { get; set; }
        public int DiscontinuedBy { get; set; }
        public DateTime DateDiscontinued { get; set; }
        public int CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime DateModified { get; set; }

        #endregion

        #region Public Methods
        public static Brands GetById(int id)
        {
            var dal = new BrandsDAL();
            var instance = new Brands();
            instance.Bind(dal.GetById(id));
            return instance;
        }
        public static List<Brands> GetAll()
        {
            var dal = new BrandsDAL();
            var collection = new List<Brands>();
            foreach (DataRow row in dal.GetAll().Rows)
            {
                var instance = new Brands();
                instance.Bind(row);
                collection.Add(instance);
            }
            return collection;
        }
        public static List<Brands> GetBySubscription(int subscriptionId)
        {
            var dal = new BrandsDAL();
            var collection = new List<Brands>();
            foreach (DataRow row in dal.GetBySubscription(subscriptionId).Rows)
            {
                var instance = new Brands();
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

                if (!DBNull.Value.Equals(row["Description"]))
                    this.Description = Convert.ToString(row["Description"]);
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
            var dal = new BrandsDAL();

            string message = "";
            int ret = dal.Save(this.ID, this.SubscriptionID, this.Name, this.Description, this.Discontinued, this.DiscontinuedBy, this.DateDiscontinued, this.CreatedBy, this.DateCreated, this.ModifiedBy, this.DateModified, out message);

            this.ID = ret;
            return (ret > 0);
        }
        public static bool Delete(int id, int discontinuedby)
        {
            var dal = new BrandsDAL();
            bool ret = dal.Delete(id, discontinuedby);
            return ret;
        }
        #endregion
    }
}
