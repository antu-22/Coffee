using quanliquancafe.DTO1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanliquancafe.DAO1
{
    public class BillInfoDAO
    {
        private static BillInfoDAO instance;

        public static BillInfoDAO Instance 
        {
            get { if (instance == null) instance = new BillInfoDAO(); return BillInfoDAO.instance; }
            private set => instance = value; 
        }
        private BillInfoDAO() { }

        public List<BillInfo> GetListBillInfo (int id) // id của Bill
        {
            List<BillInfo> listBillInfo = new List<BillInfo>();

            DataTable data = DataProvider1.Instance.ExecuteQuery("SELECT * FROM dbo.BILLINFO WHERE idBill = "+ id);

            foreach (DataRow item in data.Rows)
            {
                BillInfo info = new BillInfo(item);
                listBillInfo.Add(info);
            }

            return listBillInfo;
        }
        public void InsertBillInfo(int idBill, int idFood, int count)
		{
			DataProvider1.Instance.ExecuteNonQuery("USP_InsertBillInfo @idBill, @idFood, @count", new object[] {idBill, idFood, count});
		}
    }
}
