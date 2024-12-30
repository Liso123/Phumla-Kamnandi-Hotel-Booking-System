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
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PhumlaKaMnandiHotelManagementSystem.Presentation
{
    public partial class GuestDetailsForm : Form
    {   
        BookingController bookingController;
        Room reservedRoom;
        DateTime checkInDate, checkOutDate;
        double deposit;
        Collection<Reservation> reservations  = new Collection<Reservation>();
        Collection<Guest> guests = new Collection<Guest>();
        bool found = false;

        public GuestDetailsForm(BookingController bContr, Room room, DateTime checkIn, DateTime checkOut, double deposit)
        {
            InitializeComponent();
            bookingController = bContr;
            reservedRoom = room;
            checkInDate = checkIn;
            checkOutDate = checkOut;
            this.deposit = deposit;
        }

        private void GuestDetailsForm_Load(object sender, EventArgs e)
        {
            txtDateIn.Text = checkInDate.ToString("yyyy-MM-dd");
            txtDateOut.Text = checkOutDate.ToString("yyyy-MM-dd");
            txtRoom.Text = reservedRoom.roomID.ToString();
            txtPrice.Text = reservedRoom.price.ToString() + ".00";
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void Gender_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textSearchGuest_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                foreach (Guest guest in bookingController.bookingsDB.guests)
                {
                    if (guest.Id == Convert.ToInt32(textSearchGuest.Text))
                    {
                        found = true;
                        txtName.Text = guest.firstName.ToString();
                        txtSurname.Text = guest.lastName.ToString();
                        txtAge.Text = guest.age.ToString();
                        cmbGender.SelectedIndex = 0;
                        txtPhone.Text = guest.Phone.ToString();
                        txtEmail.Text = guest.Email.ToString();
                        txtCity.Text = guest.address.City.ToString();
                        txtTown.Text = guest.address.Town.ToString();
                        txtStreet.Text = guest.address.Street.ToString();
                        txtPostalCode.Text = guest.address.PostalCode.ToString();
                        break;
                    }
                }
                if (found == false)
                {
                    MessageBox.Show("Guest " + textSearchGuest.Text + " not found in the system.");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during database operations
                MessageBox.Show("Enter Booking Reference " + ex.Message);
            }
        }

        private void textSearchGuest_Enter(object sender, EventArgs e)
        {
            if (textSearchGuest.Text == "Search existing guest")
            {
                textSearchGuest.Text = ""; // Clear the default text
            }
        }

      private void textSearchGuest_Leave(object sender, EventArgs e)
        {
            // Code to run when textbox1 loses focus (LostFocus or Leave event)
/* if (string.IsNullOrWhiteSpace(textSearchGuest.Text))
 {
     textSearchGuest.Text = "Search existing guest"; // Restore the default tex xt
 }*/
}

private void btnConfirmReservation_Click(object sender, EventArgs e)
{
            if (isValidInput())
            {
                guests = new Collection<Guest>();
                int guestID;
                if (found == false)
                {
                    guestID = 1006 + guests.Count + 1;
                }
                else
                {
                    guestID = Convert.ToInt32(textSearchGuest.Text);
                }
                Address addr = new Address(txtCity.Text, txtTown.Text, Convert.ToInt32(txtPostalCode.Text), txtStreet.Text);
                Guest guest = new Guest(txtName.Text, txtSurname.Text, Convert.ToInt32(txtAge.Text), cmbGender.SelectedItem.ToString(), guestID, txtEmail.Text, (txtPhone.Text), addr);
                bookingController.guests.Add(guest);
                // Generate the reservation ID based on the selected room
                int val = bookingController.Allreservations.Count + 1;
                string refID = "PKH" + val.ToString();

                Reservation reservation = new Reservation(guest, reservedRoom, refID, checkInDate, checkOutDate);
                reservations.Add(reservation);
                bookingController.Allreservations.Add(reservation);



                try
                {

                    // Call DataMaintenance to add the reservation to the in-memory collection
                    bookingController.DataMaintenance(reservation, "Reservation", DB.DBOperation.Add);
                    //bookingController.DataMaintenance(reservation, "GuestAccount");
                    
                    if (found == false)
                    {
                        //bookingController.DataMaintenance(reservation, "Room");
                        bookingController.DataMaintenance(reservation, "Guest", DB.DBOperation.Add);
                    }
                    //bookingController.DataMaintenance(reservation, "Address");

                    // Call FinalizeChanges to insert the data into the database for all four tables
                    if (found == false)
                    {
                        bookingController.FinalizeChanges(reservation, "Guest");
                        //bookingController.FinalizeChanges(reservation, "Room");
                    }

                    //bookingController.FinalizeChanges(reservation, "GuestAccount");

                    bookingController.FinalizeChanges(reservation, "Reservation");
                    //bookingController.FinalizeChanges(reservation, "Address");
                    MessageBox.Show("Added " + " \n" + reservation.ToString());

                }
                catch (Exception ex)
                {
                    // Handle any exceptions that occur during database operations
                    MessageBox.Show("An error occurred while saving the reservation: " + ex.Message);
                }
                PaymentForm newForm = new PaymentForm(bookingController, guest, reservedRoom, deposit, refID, checkInDate, checkOutDate);

                newForm.Show();
                this.Close();
            }
}


        private bool isValidInput()
        {
            if (!isValidName())
            {
                return false;
            }
            if (!isValidSName())
            {
                return false;
            }
            else if (!isValidEmail())
            {
                return false;
            }
            else if (!isValidPhone())
            {
                return false;
            }
            else if (!isValidAge())
            {
                return false;
            }
            else if (!isValidTown())
            {
                return false;
            }
            else if (!isValidPostalCode())
            {
                return false;
            }



            return true;
        }
        private bool isValidName()
        {
            string name = txtName.Text.TrimEnd();
            if (name.Length == 0)
            {
                DialogResult dialogResult;
                dialogResult = MessageBox.Show("Full name required\n", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }
        private bool isValidSName()
        {
            string name = txtName.Text.TrimEnd();
            if (name.Length == 0)
            {
                DialogResult dialogResult;
                dialogResult = MessageBox.Show(" Surname required\n", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (IsDigitsOnly(name))
            {
                DialogResult dialogResult = MessageBox.Show("Age must be valid\n", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        private bool isValidEmail()
        {
            string email = txtEmail.Text.TrimEnd();
            if (!IsValidEmail(email))
            {
                DialogResult dialogResult = MessageBox.Show("Email must be valid\n", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }
        private bool isValidAge()
        {
           string age = txtAge.Text.TrimEnd();
            if (!IsDigitsOnly(age))
            {
                DialogResult dialogResult = MessageBox.Show("Age must be valid\n", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }
        private bool isValidPhone()
        {
            string phone = txtPhone.Text.TrimEnd();

            if (string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Phone required", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            else if (!phone.All(char.IsDigit))
            {
                MessageBox.Show("Phone must contain digits only", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            else if (phone.Length != 10)
            {
                MessageBox.Show("Phone must be 10 digits in length", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        

        private bool isValidCity()
        {
            string address = txtCity.Text.TrimEnd();

            if (string.IsNullOrEmpty(address))
            {
                MessageBox.Show("City required for Address", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }
        private bool isValidTown()
        {
            string address = txtTown.Text.TrimEnd();

            if (string.IsNullOrEmpty(address))
            {
                MessageBox.Show("Town required for Address", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }
        private bool isValidPostalCode()
        {
            string address = txtPostalCode.Text.TrimEnd();
            if (!IsDigitsOnly(address))
            {
                DialogResult dialogResult = MessageBox.Show("Postal Code must be valid\n", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            

            return true;
        }
        private bool IsDigitsOnly(string str)
        {
            return !string.IsNullOrEmpty(str) && str.All(char.IsDigit);
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private bool IsValidEmail(string email)
        {
            if (email.Trim().EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
