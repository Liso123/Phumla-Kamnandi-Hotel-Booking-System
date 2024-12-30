using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhumlaKaMnandiHotelManagementSystem.Business
{
    public class Address
    {
        public int guestID;
        private string city = "";
        private string town = "";
        private int postal_code = 0;
        private string street = "";

        public string City
        {
            get { return city; }
            set { city = value; } }
        public string Town { get { return town; } set { town = value; } }
        public int PostalCode
        {
            get { return postal_code; }
            set { postal_code = value; } }
        public string Street
        {
            get { return street; }
            set { street = value; } }
        public Address()
            {
        }
        public Address(string NewCity, string newTown, int newPostal_code, string newStreet)
        {
            City = NewCity;
            Town = newTown;
            postal_code = newPostal_code;
            Street = newStreet;
           
        }
        public override string ToString()
        {
            return $"{Street}, {Town}, {City}, {PostalCode}";
        }

    }
}
