using PhumlaKaMnandiHotelManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PhumlaKaMnandiHotelManagementSystem.Business
{
    public class BookingController
    {
        #region Data Members
        public BookingsDB bookingsDB;
        public Collection<Reservation> reservations = new Collection<Reservation>();
        public Collection<Reservation> deletedRevs = new Collection<Reservation>();
        public Collection<Guest> guests = new Collection<Guest>();
        public Collection<Room> rooms = new Collection<Room>();
        public Collection<GuestAccount> guestAccounts = new Collection<GuestAccount>();
        #endregion

        #region Properties
        public Collection<Reservation> Allreservations
        {
            get
            {
                return reservations;
            }
        }

        public Collection<Guest> Allguests
        {
            get
            {
                return guests;
            }
        }
        #endregion

        #region Constructor
        public BookingController()
        {
            bookingsDB = new BookingsDB();
            reservations = bookingsDB.Allreservations;
            InitializeRooms();
            AddReservations();
        }
        #endregion

        private void InitializeRooms()
        {
            // Create instances of Room for each single room and add them to the list
            Room roomA = new Room("A", Season.SeasonType.LowSeason);
            Room roomB = new Room("B", Season.SeasonType.LowSeason);
            Room roomC = new Room("C", Season.SeasonType.LowSeason);
            Room roomD = new Room("D", Season.SeasonType.LowSeason);
            Room roomE = new Room("E", Season.SeasonType.LowSeason);
            rooms.Add(roomA);
            rooms.Add(roomB);
            rooms.Add(roomC);
            rooms.Add(roomD);
            rooms.Add(roomE);
        }
        private void AddReservations()
        {
            int counter = 0;
            foreach (Reservation reservation in bookingsDB.reservations)
            {
                foreach (Reservation res in reservations)
                {
                    if (reservation.refNo == res.refNo)
                    {
                        break;
                    }
                    if (counter == reservations.Count)
                    {
                        reservations.Add(reservation);
                        counter = 0;
                    }
                }
            }
        }
        #region Database Communication
        public void DataMaintenance(Reservation reservation,string table, DB.DBOperation operation)
        {
            //perform a given database operation to the dataset in memory; 
            int index = 0;
            bookingsDB.DataSetChange(reservation,table);
            switch (operation)
            {
                case DB.DBOperation.Add:

                    if (table == "Reservation")
                    {
                        reservations.Add(reservation);
                        bookingsDB.UpdateDataSource(reservation, table);

                    }
                    break;

                case DB.DBOperation.Delete:
                    int myCount = 0;
                    foreach (Reservation res in reservations)
                    {
                        if (res.refNo == reservation.refNo)
                        {
                            index = myCount;
                        }
                        myCount++;
                    }
                    reservations.RemoveAt(index);
                    bookingsDB.UpdateDataSourceD(reservation, table);
                    break;

            }
          }

        //***Commit the changes to the database
        public bool FinalizeChanges(Reservation reservation,string table)
        {
            //***call the BookingDB method that will commit the changes to the database
            return bookingsDB.UpdateDataSource(reservation,table);
        }
        #endregion
    }
}
