using PhumlaKaMnandiHotelManagementSystem.Business;
using PhumlaKaMnandiHotelManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhumlaKaMnandiHotelManagementSystem.Presentation
{
    public partial class Confirmation : Form
    {
        Guest guest;
        Room reservedRoom;
        double deposit;
        string refID;
        DateTime checkInDate, checkOutDate;
        BookingController bookingController=new BookingController();
        reservationForm ReservationForm= new reservationForm();
        BookingsMDI bookingsMDI = new BookingsMDI();
        BookingsDB bookingsDB = new BookingsDB();
        GuestAccount guestAccount = new GuestAccount();
        
        public Confirmation(BookingController bc, Guest guest, Room room, double deposit, string refID, DateTime inDate, DateTime outDate)
        
        {
            InitializeComponent();
            bookingController = bc;
            this.guest = guest;
            reservedRoom = room;
            this.deposit = deposit;
            this.refID = refID;
            checkOutDate = outDate;
            checkInDate = inDate;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Confirmation email sent to Guest.");
            this.Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            RoomReservation newForm = new RoomReservation(bookingController);

            // Show the new form
            newForm.Show();
        }

        private void Confirmation_Load(object sender, EventArgs e)
        { 
            txtBkref.Text = refID.ToString();
            txtRoomNo.Text = reservedRoom.roomID.ToString();
            txtStatus.Text = deposit.ToString();
            txtDateIn.Text = checkInDate.ToString("yyyy-MM-dd");
            txtDateOut.Text = checkOutDate.ToString("yyyy-MM-dd");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtDateIn_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCancelBooking_Click(object sender, EventArgs e)
        {
            Reservation reservation = new Reservation();
            bool found = false;
            foreach (Reservation res in bookingController.reservations)
            {
                if (res.refNo == txtBkref.Text)
                {
                    reservation = res;
                    found = true;
                    break;
                }
            }
            if (found == true)
            {
                bookingController.DataMaintenance(reservation, "Reservation", DB.DBOperation.Delete);
                MessageBox.Show("Succesfully Cancelled Reservation " + Convert.ToString(txtBkref.Text) + ".");
                this.Close();
            }
            else
            {
                MessageBox.Show("Booking not found.");
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
