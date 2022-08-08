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
    public partial class flogin : Form
    {
        public flogin()
        {
            InitializeComponent();
        }

        private void flogin_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
           

        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e) // login
        {
            string userName = textUsername.Text;
            string password = textPassword.Text;
            if (Login(userName,password))
            {
                Account loginAccount = AccountDAO.Instance.GeteAccountByUserName(userName);
                ftablemanager f = new ftablemanager(loginAccount);
                this.Hide();
                f.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Sai tên tài khoản hoặc mật khẩu!");
            }
        }
        bool Login(string userName, string passWord)
        {
            return AccountDAO.Instance.Login(userName, passWord);
        }

        private void textUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void flogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("Bạn có thật sự muốn thoát chương trình?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }    
        }
    }
}
