using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhumlaKaMnandiHotelManagementSystem.Business
{
    public class Season
    {
        public enum SeasonType
        {
            LowSeason = 0,
            MidSeason = 1,
            HighSeason = 2
        }
        protected SeasonType seasonType;
        public double price;
        public double deposit;

        public SeasonType getSeason
        {
            get { return seasonType; }
            set { seasonType = value; }
        }

        public double getPrice
        {
            get { return price; }
            set { price = value; }
        }
        
    }
}
