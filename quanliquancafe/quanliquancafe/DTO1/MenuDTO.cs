using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanliquancafe.DTO1
{
    public class MenuDTO
    {
        public MenuDTO(string foodName, int count, float price, float totalPrice)
        {
            this.TotalPrice = totalPrice;
            this.FoodName = foodName;
            this.Count = count;
            this.Price = price;
        }
        public MenuDTO(DataRow row)
        {
            this.TotalPrice = (float)Convert.ToDouble(row["totalPrice"].ToString());
            this.FoodName = row["Name"].ToString();
            this.Count = (int)row["count"];
            this.Price = (float)Convert.ToDouble (row["price"].ToString());
        }   
        private string foodName;

        public string FoodName
        {
            get { return foodName; }
            set { foodName = value; }
        }
        private int count;

        public int Count
        {
            get { return count; }
            set { count = value; }
        }
        private float price;

        public float Price
        {
            get { return price; }
            set { price = value; }
        }

        private float totalPrice;

        public float TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }

    }
}
