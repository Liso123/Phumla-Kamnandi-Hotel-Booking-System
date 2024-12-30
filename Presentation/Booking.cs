using PhumlaKaMnandiHotelManagementSystem.Business;
using PhumlaKaMnandiHotelManagementSystem.Data;
using PhumlaKaMnandiHotelManagementSystem.Presentation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PhumlaKaMnandiHotelManagementSystem
{
    public partial class Booking : Form
    {
        private Reservation reservation;
        private BookingController bookingController;
        BookingsDB bookingsDB = new BookingsDB();
        public bool BookingFormClosed ;
        public Booking(BookingController aController)
        {
            InitializeComponent();
            bookingController = aController;
         //   reservations = bookingController.Allreservations; // Retrieve reservations from the controller
            bookingsDB = aController.bookingsDB;

        }

        public Booking()
        {
            InitializeComponent();
           // bookingController = new BookingController();

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            RoomReservation newRoomReservation = new RoomReservation(bookingController);

            // Show the new form
            newRoomReservation.Show();
            this.Close();
        }

        private void Booking_Load(object sender, EventArgs e)
        {

        }

        private void Booking_FormClosed(object sender, FormClosedEventArgs e)
        {
            BookingFormClosed = true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            bool foundRes = false;
            foreach (Reservation res in bookingController.bookingsDB.reservations) 
            {
                //MessageBox.Show(res.refNo);
                if (res.refNo.Trim() == Convert.ToString(textBox2.Text.TrimEnd()))
                {
                    foundRes = true;
                    Confirmation newForm = new Confirmation(bookingController, res.reservedGuest, res.reservedRoom, 0, res.refNo, res.CheckOutDate, res.CheckOutDate);

                    newForm.Show();
                    this.Close();
                    break;
                }
            }
            if (foundRes == false)
            {
                MessageBox.Show("Reservation " + textBox2.Text + " not found.");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
        }
    }
}
