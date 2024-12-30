using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhumlaKaMnandiHotelManagementSystem.Business
{
    public class MidSeason : Season
    {
        public MidSeason() : base()
        {
            getSeason = SeasonType.MidSeason;
            price = 750;
            deposit = 0.10 * 750;
        }
    }
}
