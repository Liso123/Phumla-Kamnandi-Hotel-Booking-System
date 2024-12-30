using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhumlaKaMnandiHotelManagementSystem.Business
{
    public class GuestAccount
    {
        public int _accountNumber;
        public Guest guest;
        public double _balance;
        public bool _depositPaid = false;

        public int accountNumber
        { 
            get { return _accountNumber; } 
            set { _accountNumber = value; } }
        
        public double balance
        { 
            get { return _balance; } 
            set { _balance = value; }
        }
        public bool depostPaid
        {
            get { return _depositPaid; }
            set { _depositPaid = value; }
        }
        public GuestAccount(int accNo, Guest guest, double bal)
        {
            _accountNumber = accNo;
            this.guest = guest;
            _balance = bal;
        }
        public GuestAccount()
        {
        }

        public void Pay(double amount)
        {
            balance = balance - amount;
        }
    }
}
