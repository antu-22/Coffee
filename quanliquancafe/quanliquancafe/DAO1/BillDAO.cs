using quanliquancafe.DTO1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanliquancafe.DAO1
{
    public class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance
        {
            get { if (instance == null) instance = new BillDAO(); return BillDAO.instance; }
            private set { BillDAO.instance = value; }
        }

        private BillDAO() { }

        public int GetUnCheckBillIDByTableID(int id)
        {
            DataTable data = DataProvider1.Instance.ExecuteQuery("SELECT * FROM dbo.BILL WHERE idTable =" + id + "AND status = 0");
            if (data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.ID;
            }
            return -1;
            // có đơn chưa check: return BillID, k có return -1 
        }
        public void InsertBill(int id)
        {
            DataProvider1.Instance.ExecuteNonQuery("USP_InsertBill @idTable", new object[] { id });
        }
        public int GetMaxIDBill()
        {
            try
            {
                return (int) DataProvider1.Instance.ExecuteScalar("SELECT MAX(id) FROM dbo.BILL");
            }
            catch
            {
                return 1;
            }
        }
        public void CheckOut(int id)
		{
            string query = "UPDATE dbo.BILL SET status = 1 where id =" + id;
            DataProvider1.Instance.ExecuteNonQuery(query);
		}
    }
}
