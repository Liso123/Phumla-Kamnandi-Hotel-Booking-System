using PhumlaKaMnandiHotelManagementSystem.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace PhumlaKaMnandiHotelManagementSystem.Presentation
{
    public partial class PaymentForm : Form
    {
        Guest guest;
        Room reservedRoom;
        double deposit;
        string refID;
        DateTime checkInDate, checkOutDate;
        BookingController bookingController = new BookingController();
        public PaymentForm(BookingController bc, Guest guest, Room room, double deposit, string refID, DateTime inDate, DateTime outDate)
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

        private void button2_Click(object sender, EventArgs e)
        {
            bool correct = false;
            if (isValidInput())
            {
                try
                {
                    DateTime dateTime = Convert.ToDateTime(txtDueDate.Text);
                    correct = true;
                }
                catch 
                {
                    MessageBox.Show("Invalid expiry date.");
                    correct = false;
                }
                if (correct == true)
                {
                    deposit = 0; // Convert.ToInt32(txtRoomPrice.Text) - Convert.ToInt32(txtDeposit.Text);
                    guest.accountNumber = Convert.ToString(txtCardNumber.Text);
                    GuestAccount account = new GuestAccount();
                    account.balance = 0;
                    account.guest = guest;
                    bookingController.guestAccounts.Add(account);
                    Confirmation newForm = new Confirmation(bookingController, guest, reservedRoom, deposit, refID, checkInDate, checkOutDate);
                    // Show the new form
                    newForm.Show();
                    this.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            deposit = reservedRoom.price * 0.1;
            GuestAccount account = new GuestAccount();
            account.balance = deposit;
            account.guest = guest;
            bookingController.guestAccounts.Add(account);
            Confirmation newForm = new Confirmation(bookingController, guest, reservedRoom, deposit, refID, checkInDate, checkOutDate);
            // Show the new form
            newForm.Show();
            this.Close();
        }

        private void PaymentForm_Load(object sender, EventArgs e)
        {
            txtRoomPrice.Text= reservedRoom.price.ToString() + ".00";
            txtrefNo.Text= refID.ToString();
            txtDeposit.Text = deposit.ToString() + "0";
            txtDue.Text = deposit.ToString() + "0";
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCardNumber_TextChanged(object sender, EventArgs e)
        {

        }
        private bool isValidInput()
        {
            if (!isValidName())
            {
                return false;
            }


            else if (!isValidCreditCardNumber())
            {
                return false;
            }

           

            return true;
        }
        private bool isValidCreditCardNumber()
        {
            string ccNum = txtCardNumber.Text.TrimEnd();

            if (string.IsNullOrEmpty(ccNum))
            {
                MessageBox.Show("Credit card number required", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            else if (!ccNum.All(char.IsDigit))
            {
                MessageBox.Show("Credit card number must contain digits only", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            else if (ccNum.Length != 16)
            {
                MessageBox.Show("Credit card number  must be 16 digits in length", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        private void txtRoomPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDeposit_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDueDate_TextChanged(object sender, EventArgs e)
        {

        }

        private bool isValidName()
        {
            string name = txtAccHolder.Text.TrimEnd();
            if (name.Length == 0)
            {
                DialogResult dialogResult;
                dialogResult = MessageBox.Show("Full name required\n", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }
    }
}
