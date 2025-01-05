using InventoryManagement_PRASMM.Controllers;
using InventoryManagement_PRASMM.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace InventoryManagement_PRASMM.Data
{
    internal class PurchaseOrderHeaderDAL : BaseDAL
    {
        SqlDataReader _reader;
        public DataTable GetAll()
        {
            base.com.CommandText = "spPurchaseOrderHeader";
            base.com.Parameters.AddWithValue("@id", 0);
            return base.GetDataTable();

        }
        
        public List<PurchaseOrderHeader> GetBySubscriptionID(int storeId,int supplierId, string pono, DateTime datefrom, DateTime dateto, out int item_count, int curr_pageNumber, int page_size)
        {
            SqlParameter param_out = new SqlParameter("@itemcount", SqlDbType.Int, 4);

            base.com.CommandText = "spPurchaseOrderHeaderByStoreID";
            base.com.Parameters.AddWithValue("@storeId", storeId);
            base.com.Parameters.AddWithValue("@supplierId", supplierId);
            base.com.Parameters.AddWithValue("@pono", pono);
            base.com.Parameters.AddWithValue("@datefrom", datefrom);
            base.com.Parameters.AddWithValue("@dateto", dateto);
            base.com.Parameters.AddWithValue("@pagenumber", curr_pageNumber);
            base.com.Parameters.AddWithValue("@pagesize", page_size);

            param_out.Direction = ParameterDirection.Output;
            base.com.Parameters.Add(param_out);

            _reader = base.GetDataReader();

            List<PurchaseOrderHeader> list = new List<PurchaseOrderHeader>();
            while (_reader.Read())
            {
                Models.PurchaseOrderHeader item = new Models.PurchaseOrderHeader();
                item.ID = _reader.GetInt32(0);
                item.PONo = _reader.GetString(1);
                item.PODate = _reader.GetDateTime(2);
                item.OrderStatus = _reader.GetString(3);
                item.PaymentStatus = _reader.GetString(4);
                item.SupplierName = _reader.GetString(5);
                item.GrandTotal = _reader.GetDecimal(6);
                item.Paid = _reader.GetDecimal(7);
                item.Due = _reader.GetDecimal(8);
                list.Add(item);
            }

            _reader.Close();

            int.TryParse(param_out.Value.ToString(), out item_count);

            return list;
        }

        public DataRow GetById(int id)
        {
            base.com.CommandText = "spPurchaseOrderHeader";
            base.com.Parameters.AddWithValue("@id", id);
            return base.GetFirstRow();
        }

        public int Save(int id, int storeid, string pono, DateTime podate, int orderstatusid, int paymentstatusid, int supplierid, string deliveryaddress, int termsid, DateTime expecteddate, string remarks, int discontinued, int discontinuedby, DateTime datediscontinued, int createdby, DateTime datecreated, int modifiedby, DateTime datemodified, out string message)
        {
            message = "";
            base.com.CommandText = "spPurchaseOrderHeaderUpdate";
            base.com.Parameters.AddWithValue("@id", id);
            base.com.Parameters.AddWithValue("@storeid", storeid);
            base.com.Parameters.AddWithValue("@pono", pono);
            base.com.Parameters.AddWithValue("@podate", podate);
            base.com.Parameters.AddWithValue("@orderstatusid", orderstatusid);
            base.com.Parameters.AddWithValue("@paymentstatusid", paymentstatusid);
            base.com.Parameters.AddWithValue("@supplierid", supplierid);
            base.com.Parameters.AddWithValue("@deliveryaddress", deliveryaddress);
            base.com.Parameters.AddWithValue("@termsid", termsid);
            base.com.Parameters.AddWithValue("@expecteddate", expecteddate);
            base.com.Parameters.AddWithValue("@remarks", remarks);
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
                        message = "PurchaseOrderHeader Name already exists!";
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
            base.com.CommandText = "spPurchaseOrderHeaderDelete";
            base.com.Parameters.AddWithValue("@id", id);
            base.com.Parameters.AddWithValue("@discontinuedby", discontinuedby);

            int ra;
            try
            {
                ra = Convert.ToInt32(base.com.ExecuteScalar());
            }
            catch
            {
                throw new Exception("Delete purchaseorderheader failed!");
            }
            return (ra > 0);
        }

    }
}
