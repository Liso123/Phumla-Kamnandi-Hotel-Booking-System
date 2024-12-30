using System;
using System.Net.Sockets;

namespace PhumlaKaMnandiHotelManagementSystem.Business
{
	public class Guest : Person
	{
		private int _id;
		private string _email;
		private string _phone;
		private string _accountNumber;
		public Address address= new Address();
		public GuestAccount account;

		public Guest() : base()
		{

		}

		public Guest(string newFirstName, string newLastName, int newAge, string newGender, 
					 int id, string email, string phone, string accountNumber, Address addr, GuestAccount gAcc): 
					 base(newFirstName, newLastName, newAge, newGender)
		{
			_id = id;
			_email = email;	
			_phone = phone;
			_accountNumber = accountNumber;
			address = addr;
			account = gAcc;
		}
        public Guest(string newFirstName, string newLastName, int newAge, string newGender, int id, string email, string phone, Address addr) : 
					 base(newFirstName, newLastName, newAge, newGender)
        {
            _id = id;
            _email = email;
            _phone = phone;
            address = addr;
        }
        public string accountNumber
		{
			get { return _accountNumber; }
			set { _accountNumber = value; }
		}

        public int Id
		{
			get { return _id; }
			set { _id = value; }
		}
		public string Email
		{
			get { return _email; }
			set { _email = value; }
		}
		public string Phone
		{
			get { return _phone; }
			set { _phone = value; }
		}
		public void reserveGuest(string Id)
		{

		}
        public override string ToString()
        {
            return $"Guest {firstName} {lastName} with guest ID {Id}, Physical Address{address.ToString()}";
        }
    }
}
