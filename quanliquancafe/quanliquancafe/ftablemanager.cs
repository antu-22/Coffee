using quanliquancafe.DAO1;
using quanliquancafe.DTO1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanliquancafe
{ 
    public partial class ftablemanager : Form
    {
        private Account loginAccount;

		public Account LoginAccount 
        { 
            get => loginAccount;
            set { loginAccount = value; ChangeAccount(loginAccount.Type); }
        }

		public ftablemanager(Account acc)
        {
            InitializeComponent();
            this.LoginAccount = acc;
            LoadTable();
            LoadCategory();

        }
        #region Method
        void ChangeAccount (int type)
		{
            adminToolStripMenuItem.Enabled = type ==1;

        }
        void LoadCategory()
		{
            List<Category> listCategory = CategoryDAO.Instance.GetListCategory();
            cbCategory.DataSource = listCategory;
            cbCategory.DisplayMember = "Name";
		}

        void LoadFoodListByCategoryID(int id)
		{
            List<Food> listFood = FoodDAO.Instance.GetFoodByCategoryID(id);
            cbFood.DataSource = listFood;
            cbFood.DisplayMember = "Name";

        }

        void LoadTable()
        {
            flpTable.Controls.Clear();

            List<Table> tableList = TableDAO.Instance.LoadTableList();

            foreach (Table item in tableList)
            {
                Button btn = new Button() { Width = TableDAO.TableWidth, Height = TableDAO.TableHeigh };
                btn.Text = item.Name + Environment.NewLine + item.Status;
                btn.Click += btn_Click;
                btn.Tag = item; // show tableID tới hàm ShowBill

                switch (item.Status)
                {
                    case "Trống":
                        btn.BackColor = Color.Aqua;
                        break;
                    default: btn.BackColor = Color.LightPink;
                        break;
                }
                flpTable.Controls.Add(btn);
            }
        }
        void ShowBill(int id)
        {
            lsvBill.Items.Clear();

            List<MenuDTO> listBillInfo = MenuDAO.Instance.GetListMenuByTable(id);

            float totalPrice = 0;

            foreach (MenuDTO item in listBillInfo)
            {
                ListViewItem lsvItem = new ListViewItem(item.FoodName.ToString());
                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.Price.ToString());
                lsvItem.SubItems.Add(item.TotalPrice.ToString());
                totalPrice += item.TotalPrice;
                lsvBill.Items.Add(lsvItem);
            }
            CultureInfo culture =new CultureInfo("vi-VN");
            txbTotalPrice.Text = totalPrice.ToString("c",culture);
            
        }
        #endregion

        #region Events

        void btn_Click(object sender, EventArgs e)
        {
            int tableID = ((sender as Button).Tag as Table).ID;
            lsvBill.Tag = (sender as Button).Tag;
            ShowBill(tableID);
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;

            int idBill = BillDAO.Instance.GetUnCheckBillIDByTableID(table.ID);
            int foodID = (cbFood.SelectedItem as Food).ID;
            int count = (int)nmFoodCount.Value;

            if (idBill == -1) // k có bill nào
            {
                BillDAO.Instance.InsertBill(table.ID);
                BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.GetMaxIDBill(), foodID, count); // lấy billid max để tạo bill mới
            }
            else // bill đã tồn tại
            {
                BillInfoDAO.Instance.InsertBillInfo(idBill, foodID, count);

            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e) // Thanh toán
        {
            Table table = lsvBill.Tag as Table;
            int idBill = BillDAO.Instance.GetUnCheckBillIDByTableID(table.ID);
            if (idBill != -1)
			{
                if (MessageBox.Show("Bạn có muốn thanh toán hóa đơn cho " + table.Name, "Thông báo", MessageBoxButtons.OKCancel)== System.Windows.Forms.DialogResult.OK)
				{
                    BillDAO.Instance.CheckOut(idBill);
                    ShowBill(table.ID);
                    LoadTable();
				}
			}
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccountProfile f = new fAccountProfile (LoginAccount );
            f.ShowDialog();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin();
            f.ShowDialog();
        }

        private void ftablemanager_Load(object sender, EventArgs e)
        {

        }
		

		private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
		{
            int id;
            ComboBox cb = sender as ComboBox;
            if (cb == null)
			{
				return;
			}
            Category selected = cb.SelectedItem as Category;
            id = selected.ID;
            LoadFoodListByCategoryID(id);
		}
        private void btnAddFood_Click(object sender, EventArgs e) // them mon
		{
            Table table = lsvBill.Tag as Table;

            int idBill = BillDAO.Instance.GetUnCheckBillIDByTableID(table.ID);
            int foodID = (cbFood.SelectedItem as Food).ID;
            int count = (int)nmFoodCount.Value;

            if (idBill == -1) // k có bill nào
			{
                BillDAO.Instance.InsertBill(table.ID);
                BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.GetMaxIDBill(),foodID,count); // lấy billid max để tạo bill mới
			} else // bill đã tồn tại
			{
                BillInfoDAO.Instance.InsertBillInfo(idBill, foodID, count);

            }
            ShowBill(table.ID);
            LoadTable();
		}
        #endregion
	}
}
