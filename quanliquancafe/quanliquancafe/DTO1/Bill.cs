using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanliquancafe.DTO1
{
    internal class Bill
    {
        public Bill(int id, DateTime? dateCheckIn, DateTime? dateCheckOut, int status)
        {
            this.ID = id;
            this.DateCheckIn = dateCheckIn;
            this.DateCheckOut = dateCheckOut;
            this.Status = status;
        }

        public Bill(DataRow row)
        {
            this.ID = (int)row ["id"];
            this.DateCheckIn = (DateTime?)row["dateCheckIn"];
            var dataCheckOutTemp = row["dateCheckOut"]; // debug datecheckout = null
            if (dataCheckOutTemp.ToString() != "")
            {
                this.DateCheckOut = (DateTime?)row["dateCheckOut"];

            }

            this.Status = (int)row["status"];
        }
        
        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        private DateTime? dateCheckIn;

        public DateTime? DateCheckIn
        {
            get { return dateCheckIn; }
            set { dateCheckIn= value; }
        }

        private DateTime? dateCheckOut;

        public DateTime? DateCheckOut
        {
            get { return dateCheckOut; }
            set { dateCheckOut = value; }
        }
        private int status;

        public int Status
        {
            get { return status; }
            set { status = value; }
        }
    }
}
