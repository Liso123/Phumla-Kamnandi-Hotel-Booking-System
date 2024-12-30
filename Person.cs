using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhumlaKaMnandiHotelManagementSystem
{
    public class Person
    {
        #region Data Members
        private string _FirstName;
        private string _LastName;
        private int _age;
        private string _gender;
        #endregion

        #region Constructers
        public Person()
        {
        }
        public Person(string newFirstName, string newLastName, int newAge, string newGender)
        {
            
            _FirstName = newFirstName;
            _LastName = newLastName;
            this._age = newAge;
            this._gender = newGender;
        }

        
        #endregion

        #region Property Methods
       
        public string firstName
        {
            get { return this._FirstName; }
            set { this._FirstName = value; }
        }
        public string lastName
        {
            get { return this._LastName; }
            set { this._LastName = value; }
        }
        public int age
        {
            get { return this._age; }
            set { this._age = value; }
        }
        public string gender
        {
            get { return this._gender; }
            set { this._gender = value; }
        }
        #endregion

        public override string ToString()
        {
            return $" Name : {_FirstName}, Surname: {_LastName}, Age: {_age}, Gender: {_gender}";
        }
    }
}
