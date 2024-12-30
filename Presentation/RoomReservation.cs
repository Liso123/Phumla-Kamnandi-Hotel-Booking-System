using PhumlaKaMnandiHotelManagementSystem.Business;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhumlaKaMnandiHotelManagementSystem.Presentation
{
    public partial class RoomReservation : Form
    {
        private List<Room> listOfRooms= new List<Room>();
        List<Room> availableRooms = new List<Room>();
        public Collection<Reservation> reservations = new Collection<Reservation>();
        DateTime checkInText;
        DateTime checkOutText;
        Room reservedRoom;
        double deposit;
        BookingController bookingController = new BookingController();
        public RoomReservation(BookingController bookingController)
        {
            InitializeComponent();
            reservations = bookingController.Allreservations;
            InitializeRooms();
            dateTimePicker1.Value = DateTime.Now;

            dateTimePicker2.Value = DateTime.Now.AddDays(1);
            this.bookingController = bookingController;
        }

        private void InitializeRooms()
        {
            // Create instances of Room for each single room and add them to the list
            Room roomA = new Room("A", Season.SeasonType.LowSeason);
            Room roomB = new Room("B", Season.SeasonType.LowSeason);
            Room roomC = new Room("C", Season.SeasonType.LowSeason);
            Room roomD = new Room("D", Season.SeasonType.LowSeason);
            Room roomE = new Room("E", Season.SeasonType.LowSeason);
            listOfRooms.Add(roomA);
            listOfRooms.Add(roomB);
            listOfRooms.Add(roomC);
            listOfRooms.Add(roomD);
            listOfRooms.Add(roomE);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            GuestDetailsForm newForm = new GuestDetailsForm(bookingController, reservedRoom, checkInText, checkOutText, deposit);

            // Show the new form
            newForm.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
             checkInText = dateTimePicker1.Value;
             checkOutText = dateTimePicker2.Value;

            if (checkInText > checkOutText)
            {
                MessageBox.Show("Check-Out date must be after Check-In date.");
            }
            else
            {
                // Call the method to check room availability
                CheckRoomAvailability(checkInText, checkOutText);
            }

        }
    

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void RoomReservation_Load(object sender, EventArgs e)
        {
            btnAvailable.Visible = false;
            btnUnavailable.Visible = false;
        }
        private void RoomReservation_Activated(object sender, EventArgs e)
        {
             btnAvailable.Visible = false;
            btnUnavailable.Visible = false;
        }
        private void CheckRoomAvailability(DateTime checkInDate, DateTime checkOutDate)
        {
            //listOfRooms = new Collection<Room>();
            availableRooms = listOfRooms;
            //List<Room> unavailableRooms = new List<Room>();

            List<Room> unavailableRooms = bookingController.Allreservations
       .Where(res => res.CheckInDate.Date == checkInDate.Date)
       .Select(res => res.reservedRoom)
       .ToList();

            // Remove unavailable rooms from available rooms
            availableRooms.RemoveAll(room => unavailableRooms.Any(ur => ur.roomID.Equals(room.roomID)));

            if (availableRooms.Count == 0)
            {                
                btnUnavailable.Visible = true;
            }
            else
            {
                btnAvailable.Visible=true;
            }
        }

        private void RoomReservation_Load_1(object sender, EventArgs e)
        {
            btnAvailable.Visible = false;
            btnUnavailable.Visible = false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void txtCheckout_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAvailable_Click(object sender, EventArgs e)
        {
            PrintRooms();   
        }

        private void PrintRooms()
        {

            StringBuilder message = new StringBuilder("Available rooms for the selected dates:\n");

            foreach (Room availableRoom in availableRooms)
            {
                message.AppendLine($" Room {availableRoom.roomID}");
            }

            LowSeason lowSeason = new LowSeason();
            MidSeason midSeason = new MidSeason();
            HighSeason highSeason = new HighSeason();
            MessageBox.Show(message.ToString());
            reservedRoom = availableRooms[0];
            reservedRoom.availability = false;
            DateTime lseason = new DateTime(2023, 12, 1);
            DateTime Mseason = new DateTime(2023, 12, 8);
            DateTime Hseason = new DateTime(2023, 12, 16);
            DateTime Yseason = new DateTime(2024, 1, 1);
            if (checkInText >= lseason && checkOutText < Mseason)
            {
                reservedRoom.price = lowSeason.price;
                deposit = lowSeason.deposit;
            }
            if (checkInText >= Mseason && checkInText < Hseason)
            {
                reservedRoom.price = midSeason.price;
                deposit = midSeason.deposit;
            }
            if (checkInText >= Hseason && checkInText < Yseason)
            {
                reservedRoom.price = highSeason.price;
                deposit = highSeason.deposit;
            }
        }

        private void btnUnavailable_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Sorry, no rooms are available for the selected dates.");
            RoomReservation newRoomReservation = new RoomReservation(bookingController);
            // Show the new form
            newRoomReservation.Show();
            this.Close();
        }

       
    }
}
