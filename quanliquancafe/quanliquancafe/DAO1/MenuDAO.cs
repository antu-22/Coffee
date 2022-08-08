using quanliquancafe.DTO1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanliquancafe.DAO1
{
    public class MenuDAO
    {
        private static MenuDAO instance;

        public static MenuDAO Instance
        {
            get { if (instance == null) instance = new MenuDAO(); return MenuDAO.instance; }
            private set => instance = value;
        }
        private MenuDAO() { }

       public List<MenuDTO> GetListMenuByTable(int id)
		{
            List<MenuDTO> listMenu = new List<MenuDTO>();
            string query = "SELECT f.name, bi.count, f.price, f.price*bi.count AS totalPrice FROM dbo.BILLINFO AS bi, dbo.BILL AS b, dbo.FOOD AS f WHERE bi.idBill = b.id AND bi.idFood = f.id AND b.status = 0 AND b.idTable = " + id;
            DataTable data = DataProvider1.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
			{
                MenuDTO menu = new MenuDTO(item);
                listMenu.Add(menu);
			}
            return listMenu;
		}
    }
}
