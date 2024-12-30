using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhumlaKaMnandiHotelManagementSystem.Business
{
    public class HighSeason : Season
    {
        public HighSeason() : base()
        {
            getSeason = SeasonType.HighSeason;
            price = 995;
            deposit = 0.10 * 995; ;
        }
    }
}
