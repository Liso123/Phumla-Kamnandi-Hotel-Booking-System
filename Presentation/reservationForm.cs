using PhumlaKaMnandiHotelManagementSystem.Business;
using PhumlaKaMnandiHotelManagementSystem.Data;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PhumlaKaMnandiHotelManagementSystem.Presentation
{
    public partial class reservationForm : Form
    {
        public bool listFormClosed;//= true;
        Collection<Reservation> reservations;
        BookingsDB bookingsDB = new BookingsDB();
        public BookingController bookingController;
        
        public reservationForm()
        {
            InitializeComponent();
            this.Load += reservationForm_Load;
            this.Activated += reservationForm_Activated;
        }
        public reservationForm(BookingController aController)
        {

            InitializeComponent();
            bookingController = aController;
            reservations = bookingController.Allreservations; // Retrieve reservations from the controller
            bookingsDB = aController.bookingsDB;
            this.Load += reservationForm_Load;
            this.Activated += reservationForm_Activated;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void reservationForm_Load(object sender, EventArgs e)
        {
            listViewRes.View = View.Details;
        }

        private void reservationForm_Activated(object sender, EventArgs e)
        {
            listViewRes.View = View.Details;
        }

        private void reservationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            listFormClosed = true;
        }
        #region ListView set up
        public void setUpReservationListView()
        {
            // Initialize the reservations collection 
            //reservations = new Collection<Reservation>();
            
             ListViewItem reservationDetails;
            //reservations = null;
            listViewRes.Clear();

            listViewRes.Columns.Insert(0, "ReservationID", 120, HorizontalAlignment.Left);
            listViewRes.Columns.Insert(1, "FirstName", 120, HorizontalAlignment.Left);
            listViewRes.Columns.Insert(2, "LastName", 120, HorizontalAlignment.Left);
            listViewRes.Columns.Insert(3, "RoomID", 120, HorizontalAlignment.Left);
            listViewRes.Columns.Insert(4, "Check-In Date", 120, HorizontalAlignment.Left);
            listViewRes.Columns.Insert(5, "Check-out Date", 120, HorizontalAlignment.Left);

            foreach (Reservation res in bookingsDB.Allreservations)
              {
                bool deleted = false;
                foreach (Reservation delRev in bookingController.deletedRevs)
                {
                    if (res.refNo == delRev.refNo)
                    {
                        deleted = true;
                        break;
                    }
                }
                if (res.refNo == null || deleted == true)
                {
                    continue;
                }
                reservationDetails = new ListViewItem();
                if (res != null && res.refNo != null)
                {
                    reservationDetails.Text = res.refNo.ToString();
                }
                reservationDetails.SubItems.Add(res.reservedGuest.firstName);
                reservationDetails.SubItems.Add(res.reservedGuest.lastName);


                if (res.reservedRoom != null)
                {
                    reservationDetails.SubItems.Add(res.reservedRoom.roomID != null ? res.reservedRoom.roomID.ToString() : string.Empty);
                }
                else
                {
                    reservationDetails.SubItems.Add(string.Empty);
                }
                    reservationDetails.SubItems.Add(res.CheckInDate.ToString("yyyy-MM-dd"));
                    reservationDetails.SubItems.Add(res.CheckOutDate.ToString("yyyy-MM-dd"));

                    listViewRes.Items.Add(reservationDetails);
                    listViewRes.Refresh();
                    listViewRes.GridLines = true;                
            }
        }
        #endregion

        private void listViewRes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
