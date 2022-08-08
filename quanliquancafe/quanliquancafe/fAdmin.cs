using quanliquancafe.DAO1;
using quanliquancafe.DTO1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanliquancafe
{
    public partial class fAdmin : Form
    {
        public fAdmin()
        {
            InitializeComponent();
            LoadListFood();
            AddFoodBinding();
            LoadCategoryIntoCombobox(cbFoodCategory);
        }
        void LoadListFood()
		{
            dtgvFood.DataSource = FoodDAO.Instance.GetListFood();
		}

        void AddFoodBinding()
		{
            txbFoodName.DataBindings.Add(new Binding("Text", dtgvFood.DataSource,"Name"));
            txbFoodID.DataBindings.Add(new Binding ("Text",dtgvFood.DataSource,"ID"));
            nmFoodprice.DataBindings.Add(new Binding("Value",dtgvFood.DataSource,"Price"));
		}
        void LoadCategoryIntoCombobox(ComboBox cb)
		{
            cb.DataSource = FoodDAO.Instance.GetListFood();
            cb.DisplayMember = "Name";
		}  
        #region Events
        private void tabPage5_Click(object sender, EventArgs e)
        {

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel13_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dtgvAccount_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void fAdmin_Load(object sender, EventArgs e)
        {

        }

		private void btShowFood_Click(object sender, EventArgs e)
		{
            LoadListFood();
		}
        private void cbFoodCategory_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
	
		private void dtgvFood_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
       
        }
        private void label3_Click(object sender, EventArgs e)
		{

		}

		#endregion

		private void txbFoodID_TextChanged(object sender, EventArgs e)
		{
            if (dtgvFood.SelectedCells.Count > 0)
			{
                int id = (int)dtgvFood.SelectedCells[0].OwningRow.Cells["CategoryID"].Value; // lấy 1 cell bất kì từ dtgv
                Category category = CategoryDAO.Instance.GetCategoryByID(id);
                cbFoodCategory.SelectedItem = category;
                int index = -1; 
                int i = 0;

                foreach (Category item in cbFoodCategory.Items)
				{
                    if (item.ID == category.ID)
					{
                        index = 0;
                        break;
					}
                    i++;
				}
                cbFoodCategory.SelectedIndex = index;
            }
        }
	}	
}
