using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhumlaKaMnandiHotelManagementSystem.Business
{
    public class Reservation
    {
       
        private string _refNo;
        public Room reservedRoom;
       
        public Guest reservedGuest;
        public DateTime CheckInDate { get; set; }        
        public DateTime CheckOutDate { get; set; }       
       

        public string refNo
        { get { return _refNo; } 
            set { _refNo = value; } 
        }
        public Reservation() 
        { 
        }
        public Reservation(Guest ReservedGuest, Room room, string ReservationID, DateTime checkInDate, DateTime checkOutDate)
        {
            refNo = ReservationID;
            reservedGuest = ReservedGuest;
            reservedRoom = room;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            
        }
       
       
       
        public override string ToString()
        {
            return $"{reservedGuest.ToString()} Reservation ID: {refNo} in Room {reservedRoom.roomID} Check-in date is {CheckInDate} and Check-out date {CheckOutDate}.";
        }

    }
}
