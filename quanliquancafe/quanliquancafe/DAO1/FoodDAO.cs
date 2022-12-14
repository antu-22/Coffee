using quanliquancafe.DTO1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanliquancafe.DAO1
{
	public class FoodDAO
	{
		private static FoodDAO instance;
		
		public static FoodDAO Instance
		{
			get { if (instance == null) instance = new FoodDAO();return FoodDAO.instance; }
			private set { FoodDAO.instance = value; }
		}

		private FoodDAO () { }

		public List<Food> GetFoodByCategoryID (int id)
		{
			List<Food> list = new List<Food>();

			string query = "select * from Food where idCategory = " + id;

			DataTable data = DataProvider1.Instance.ExecuteQuery(query);

			foreach (DataRow item in data.Rows)
			{
				Food food = new Food(item);
				list.Add(food);
			}

			return list;
		}
		public List<Food> GetListFood ()
		{
			List<Food> list = new List<Food> ();
			DataTable data = DataProvider1.Instance.ExecuteQuery("select * from Food");
			foreach (DataRow item in data.Rows)
			{
				Food food = new Food(item);
				list.Add(food);
			}
			return list;
		}
		


	}
}
