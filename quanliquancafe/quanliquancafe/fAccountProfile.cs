using quanliquancafe.DAO1;
using quanliquancafe.DTO1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanliquancafe
{
    public partial class fAccountProfile : Form
    {
        private Account loginAccount;

        public Account LoginAccount
        {
            get => loginAccount;
            set { loginAccount = value; ChangeAccount(loginAccount); }
        }
        public fAccountProfile(Account acc )
        {
            InitializeComponent();
            LoginAccount = acc;

        }
        void ChangeAccount (Account acc)
		{
            txbUsername.Text = LoginAccount.UserName;
            txbDisplayName.Text = LoginAccount.DisplayName;
		}
        void UpdateAccount()
		{
            string displayName = txbDisplayName.Text;
            string password = txbPassword.Text;
            string newpass = txbNewPass.Text;
            string userName = txbUsername.Text;
            string reenterPass = txbReEnterPass.Text;
            if (!newpass.Equals(reenterPass))
			{
                MessageBox.Show("Mật khẩu không khớp!!");
			} else
			{
                if (AccountDAO.Instance.UpdateAccount (userName, displayName, password, newpass))
				{
                    MessageBox.Show("Cập nhật thành công");
				} else
				{
                    MessageBox.Show("Vui lòng điền đúng mật khẩu");
				}
			}
		}
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void fAccountProfile_Load(object sender, EventArgs e)
        {

        }

		private void button1_Click(object sender, EventArgs e)
		{
            UpdateAccount();
		}
	}
}
