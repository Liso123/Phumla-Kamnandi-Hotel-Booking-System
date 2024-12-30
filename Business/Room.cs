using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PhumlaKaMnandiHotelManagementSystem.Business

{
	public class Room
	{
		private string _roomID;
		public Season season;
        public double price = 0;
		private bool _availability=true;
        
        public List<Reservation> Reservations { get; private set; }
        public Room()
        {

        }


        public Room(string newRoomID,Season.SeasonType seasonType)
			
		{
            roomID = newRoomID;
            availability = true;
			Reservations = new List<Reservation>();
            switch (seasonType)
			{
				case Season.SeasonType.LowSeason:
					season = new LowSeason();
					break;
				case Season.SeasonType.MidSeason:
					season = new MidSeason();
					break;
				case Season.SeasonType.HighSeason:
					season = new HighSeason();
					break;
			}
		}

		public string roomID
		{
			get { return _roomID; }
			set { _roomID = value; }
		}
        /*public double price
		{
			get { return _price; }
			set { _price = value; }
		}*/
        public bool availability
        {
			get { return _availability; }
			set { _availability = value; }
		}

      /*  public void ReserveRoom(Guest guest,string refNo, DateTime checkInDate, DateTime checkOutDate)
        {
            if (availability)
            {
                availability = false;
                var reservation = new Reservation(guest,,refNo, checkInDate, checkOutDate);
                Reservations.Add(reservation);

                MessageBox.Show($"Room {roomID} has been reserved by {guest.firstName}.");

            }
            else
            {
               MessageBox.Show($"Room {roomID} is already reserved.");
            }

        }
        public void CheckOutRoom(DateTime checkOutDate)
        {
            var reservationToRemove = Reservations.Find(r => r.CheckOutDate == checkOutDate);
            if (reservationToRemove != null)
            {
                Reservations.Remove(reservationToRemove);
                availability = true;
                MessageBox.Show($"Room {roomID} has been checked out.");
            }
            else
            {
                MessageBox.Show($"No reservation found for check-out on {checkOutDate}.");
            }
        }
        */
        public string AssignReservationNumber()
        {
            // Find the maximum reservation number for this room
            string maxReservationNumber = Reservations
                .Where(r => r.reservedRoom.roomID == roomID)
                .Max(r => r.refNo);

            // Generate the next reservation number
            int nextReservationNumber = 1;
            if (!string.IsNullOrEmpty(maxReservationNumber))
            {
                // Extract the numeric part of the last reservation number and increment it
                if (int.TryParse(maxReservationNumber.Substring(1), out int lastNumber))
                {
                    nextReservationNumber = lastNumber + 1;
                }
            }

            // Format the next reservation number (e.g., A001)
            string formattedReservationNumber = roomID + nextReservationNumber.ToString("D3");

            return formattedReservationNumber;
        }

        
    }
}
