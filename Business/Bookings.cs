using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhumlaKaMnandiHotelManagementSystem.Business
{
    public class Bookings
    {
        public Collection<Reservation> reservations;

        public bool CheckAvailability(DateTime checkInDate, DateTime checkOutDate)
        {
            foreach (var reservation in reservations)
            {
                if (checkInDate > reservation.CheckOutDate && checkInDate < reservation.CheckInDate)
                {
                    return true;
                }
            }
            return false;
        }
    }    
}
