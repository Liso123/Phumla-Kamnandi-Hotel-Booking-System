using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhumlaKaMnandiHotelManagementSystem.Business
{
    public class LowSeason : Season
    {
        public LowSeason() : base()
        {
            getSeason = SeasonType.LowSeason;
            price = 550;
            deposit = 0.10 * 550;
        }
    }
}
