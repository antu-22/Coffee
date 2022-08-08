using quanliquancafe.DTO1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanliquancafe.DAO1
{
    public class AccountDAO
    {
        private static AccountDAO instance;
        // mô hình singleton
        public  static AccountDAO Instance 
        {
            get { if (instance == null) instance = new AccountDAO();return instance; } 
            set => instance = value; 
        }
        private AccountDAO() { }
        public bool Login(string userName, string passWord)
        {
            string query = "USP_Login @userName , @passWord";
            DataTable  result = DataProvider1.Instance.ExecuteQuery(query,new object[] {userName,passWord}); // trả về kết quả, non- số dòng đc thực thi
            return result.Rows.Count > 0;
        }
        public bool UpdateAccount(string userName, string displayName, string pass, string newPass)
		{
            int result = DataProvider1.Instance.ExecuteNonQuery("USP_UpdatAccount @userName, @displayName, @password, @newPassword", new object[] {userName, displayName, pass, newPass});

            return result > 0;
		}
        public Account GeteAccountByUserName(string userName)
		{
            DataTable data = DataProvider1.Instance.ExecuteQuery("select * from account where userName = '"+ userName + "'");

            foreach (DataRow item in data.Rows)
			{
                return new Account(item);
			}
            return null;
		}
    }
}
