using PhumlaKaMnandiHotelManagementSystem.Business;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PhumlaKaMnandiHotelManagementSystem.Data
{
    public class BookingsDB : DB
    {
        private string table1 = "Guest";
        private string sqlLocal1 = "SELECT * FROM Guest";
        private string table2 = "GuestAccount";
        private string sqlLocal2 = "SELECT * FROM GuestAccount";
        private string table3 = "Reservation";
        private string sqlLocal3 = "SELECT * FROM Reservation";
        private string table4 = "Room";
        private string sqlLocal4 = "SELECT * FROM Room";
        private string table5 = "Address";
        private string sqlLocal5 = "SELECT * FROM Address";
        public Collection<Reservation> reservations;
        private Collection<Room> rooms;
        public Collection<Guest> guests;
        private Collection<GuestAccount> guestAccounts;
        private Collection<Address> addresses;

        public BookingsDB() : base()
        {
            reservations = new Collection<Reservation>();
            rooms = new Collection<Room>();
            guests = new Collection<Guest>();
            guestAccounts = new Collection<GuestAccount>();
            addresses = new Collection<Address>();
            FillDataSet(sqlLocal5, table5);
            Add2Collection(table5);
            FillDataSet(sqlLocal1, table1);
            Add2Collection(table1);
            FillDataSet(sqlLocal2, table2);
            Add2Collection(table2);
            FillDataSet(sqlLocal3, table3);
            Add2Collection(table3);
            FillDataSet(sqlLocal4, table4);
            Add2Collection(table4);
        }

        public Collection<Reservation> Allreservations
        {
            get
            {
                return reservations;
            }
        }
        public Collection<Room> Allrooms
        {
            get
            {
                return rooms;
            }
        }

        public Collection<Guest> Allguests
        {
            get
            {
                return guests;
            }
        }
       
        #region Utility Methods
        public DataSet GetDataSet()
        {
            return dsMain;
        }

        private void Add2Collection(string table)
        {
            //Declare references to a myRow object 
            DataRow myRow = null;
            // Reservation aReservationp;
            foreach (DataRow myRow_loopVariable in dsMain.Tables[table].Rows)
            {
                myRow = myRow_loopVariable;
                if (!(myRow.RowState == DataRowState.Deleted))
                {
                    // Instantiate a new Reservation object
                    Reservation reservation = new Reservation();
                    reservation.reservedGuest = new Guest();
                    reservation.reservedRoom = new Room();
                    Guest guest = new Guest();
                    guest.account = new GuestAccount();
                    guest.address = new Address();
                    GuestAccount guestAcc = new GuestAccount();
                    Room guestRoom = new Room();
                    Address addr = new Address();
                    switch (table)
                    {
                        case "Address":
                            addr.guestID = Convert.ToInt32(myRow["GuestID"]);
                            addr.City = Convert.ToString(myRow["City"]).TrimEnd();
                            addr.Town = Convert.ToString(myRow["Town"]).TrimEnd();
                            addr.Street = Convert.ToString(myRow["Street"]).TrimEnd();
                            addr.PostalCode = Convert.ToInt32(myRow["Postal Code"]);
                            addresses.Add(addr);
                            break;

                        case "Guest":
                            guest.Id = Convert.ToInt32(myRow["GuestID"]);
                            guest.firstName = Convert.ToString(myRow["FirstName"]).TrimEnd();
                            guest.lastName = Convert.ToString(myRow["LastName"]).TrimEnd();
                            guest.gender = Convert.ToString(myRow["Gender"]).TrimEnd();
                            guest.age = Convert.ToInt32(myRow["Age"]);
                            guest.Phone = Convert.ToString(myRow["Phone"]);
                            guest.Email = Convert.ToString(myRow["EmailAddress"]).TrimEnd();
                            foreach (Address address in addresses)
                            {
                                if (address.guestID == Convert.ToInt32(myRow["GuestID"]))
                                {
                                    guest.address = address;
                                    break;
                                }
                            }
                            guests.Add(guest);
                            break;

                        // Obtain each reservation attribute from the specific field in the row in the table
                        case "Reservation":
                        reservation.refNo = Convert.ToString(myRow["ReservationID"]);
                        foreach (Guest rGuest in guests)
                        {
                            if (rGuest.Id == Convert.ToInt32(myRow["GuestID"]))
                            {
                                reservation.reservedGuest = rGuest;
                            }
                        }
                        reservation.reservedRoom.roomID = Convert.ToString(myRow["RoomID"]).TrimEnd();
                        reservation.CheckInDate = Convert.ToDateTime(myRow["CheckInDate"]);
                        reservation.CheckOutDate = Convert.ToDateTime(myRow["CheckOutDate"]);
                        reservations.Add(reservation);
                        break;
                                    
                        case "GuestAccount":
                        guestAcc.accountNumber = Convert.ToInt32(myRow["AccountNumber"]);
                        foreach (Guest rGuest in guests)
                        {
                            if (rGuest.Id == Convert.ToInt32(myRow["GuestID"]))
                            {
                                guestAcc.guest = rGuest;
                            }
                        }
                        guestAcc.balance = Convert.ToDouble(myRow["Balance"]);
                        guestAcc.depostPaid = Convert.ToBoolean(myRow["Balance"]);
                        guestAccounts.Add(guestAcc);
                        break;

                        case "Room":
                        guestRoom.roomID = Convert.ToString(myRow["RoomID"]).TrimEnd();
                        guestRoom.availability = Convert.ToBoolean(myRow["Availability"]);
                        guestRoom.price = Convert.ToDouble(myRow["Price"]);
                        break;
                    }
                }
            }
        }
            
       

        private void FillRow(DataRow aRow, Reservation reservation,string table)

        {
           //Reservation reservation = new Reservation();
           // reservation.reservedGuest = new Guest();
            //reservation.reservedRoom = new Room();

            switch (table)
            {
                case "Guest":
                    aRow["GuestID"] = reservation.reservedGuest.Id;
                    aRow["FirstName"] = reservation.reservedGuest.firstName;
                    aRow["LastName"] = reservation.reservedGuest.lastName;
                    aRow["Gender"] = reservation.reservedGuest.gender;
                    aRow["Phone"] = reservation.reservedGuest.Phone;
                    aRow["EmailAddress"] = reservation.reservedGuest.Email;
                    aRow["Age"] = reservation.reservedGuest.age;
                   // aRow["AccountNumber"]= reservation.reservedGuest.accountNumber;
                    break;
                case "GuestAccount":
                    aRow["AccountNumber"] = reservation.reservedGuest.accountNumber;
                    aRow["GuestID"] = reservation.reservedGuest.Id;
                    aRow["Balance"] = reservation.reservedGuest.account.balance;
                    aRow["DepositPaid"] = reservation.reservedGuest.account.depostPaid;
                    break;
                case "Reservation":
                    aRow["ReservationID"] = reservation.refNo;
                    aRow["GuestID"] = reservation.reservedGuest.Id;
                    aRow["RoomID"] = reservation.reservedRoom.roomID;
                    aRow["CheckInDate"] = reservation.CheckInDate;
                    aRow["CheckOutDate"] = reservation.CheckOutDate;
                    break;
                case "Room":
                    aRow["RoomID"] = reservation.reservedRoom.roomID;
                    aRow["Availability"] = reservation.reservedRoom.availability;
                    aRow["Price"] = reservation.reservedRoom.price;
                    break;
                case "Address":
                    aRow["GuestID"] = reservation.reservedGuest.Id;
                    aRow["City"] = reservation.reservedGuest.address.City;
                    aRow["Town"] = reservation.reservedGuest.address.Town;
                    aRow["Street"] = reservation.reservedGuest.address.Street;
                    aRow["Postal Code"] = reservation.reservedGuest.address.PostalCode;
                    break;
            }    
          
          
           

            
        }
        #endregion
        public void DataSetChange(Reservation reservation, string table)
        {
            DataRow aRow = null;
            switch (table)
            {
                case "Guest":
                    aRow = dsMain.Tables[table1].NewRow();
                    FillRow(aRow, reservation, table1);
                    //Add to the dataset
                    dsMain.Tables[table1].Rows.Add(aRow);
                    break;
                case "GuestAccount":

                    aRow = dsMain.Tables[table2].NewRow();
                    FillRow(aRow, reservation, table2);
                    //Add to the dataset
                    dsMain.Tables[table2].Rows.Add(aRow);
                    break;
                case "Reservation":
                    aRow = dsMain.Tables[table3].NewRow();
                    FillRow(aRow, reservation, table);
                    //Add to the dataset
                    dsMain.Tables[table3].Rows.Add(aRow);
                    break;
                case "Room":
                    aRow = dsMain.Tables[table4].NewRow();
                    FillRow(aRow, reservation, table4);
                    //Add to the dataset
                    dsMain.Tables[table4].Rows.Add(aRow);
                    break;
                case "Address":
                    aRow = dsMain.Tables[table5].NewRow();
                    FillRow(aRow, reservation, table5);
                    //Add to the dataset
                    dsMain.Tables[table5].Rows.Add(aRow);
                    break;
            }
        }

        public void DataSetChangeD(Reservation reservation, string table)
        {
            DataRow aRow = null;
            switch (table)
            {
                case "Guest":
                    aRow = dsMain.Tables[table1].NewRow();
                    //Add to the dataset
                    dsMain.Tables[table1].Rows.Add(aRow);
                    break;
                case "GuestAccount":

                    aRow = dsMain.Tables[table2].NewRow();
                    //Add to the dataset
                    dsMain.Tables[table2].Rows.Add(aRow);
                    break;
                case "Reservation":
                    aRow = dsMain.Tables[table3].NewRow();
                    //Add to the dataset
                    dsMain.Tables[table3].Rows.Remove(aRow);
                    break;
                case "Room":
                    aRow = dsMain.Tables[table4].NewRow();
                    //Add to the dataset
                    dsMain.Tables[table4].Rows.Add(aRow);
                    break;
                case "Address":
                    aRow = dsMain.Tables[table5].NewRow();
                    //Add to the dataset
                    dsMain.Tables[table5].Rows.Add(aRow);
                    break;
            }
        }
        #region Build Parameters, Create Commands & Update database
        private void Build_INSERT_Parameters(Reservation reservation, string table)
        {
            
            SqlParameter param;
            

           switch (table)
            {
                case "Guest":
                    param = new SqlParameter("@GuestID", SqlDbType.Int, 15, "GuestID");
                    daMain.InsertCommand.Parameters.Add(param);
                    param = new SqlParameter("@FirstName", SqlDbType.NVarChar, 100, "FirstName");
                    daMain.InsertCommand.Parameters.Add(param);
                    param = new SqlParameter("@LastName", SqlDbType.NVarChar, 100, "LastName");
                    daMain.InsertCommand.Parameters.Add(param);
                    param = new SqlParameter("@Gender", SqlDbType.NVarChar, 15, "Gender");
                    daMain.InsertCommand.Parameters.Add(param);
                    param = new SqlParameter("@Phone", SqlDbType.NVarChar, 15, "Phone");
                    daMain.InsertCommand.Parameters.Add(param);
                    param = new SqlParameter("@EmailAddress", SqlDbType.NVarChar, 100, "EmailAddress");
                    daMain.InsertCommand.Parameters.Add(param);
                    param = new SqlParameter("@Age", SqlDbType.Int, 100, "Age");
                    daMain.InsertCommand.Parameters.Add(param);
                   // param = new SqlParameter("@AccountNumber", SqlDbType.Int, 15, "AccountNumber");
                   // daMain.InsertCommand.Parameters.Add(param);

                    break;
                case "GuestAccount":
                    param = new SqlParameter("@AccountNumber", SqlDbType.Int, 15, "AccountNumber");
                    daMain.InsertCommand.Parameters.Add(param);
                    param = new SqlParameter("@GuestID", SqlDbType.Int, 15, "ID");
                    daMain.InsertCommand.Parameters.Add(param);
                    param = new SqlParameter("@Balance", SqlDbType.Money, 8, "Balance");
                    daMain.InsertCommand.Parameters.Add(param);
                    param = new SqlParameter("@DepositPaid", SqlDbType.Money, 8, "DepositPaid");
                    daMain.InsertCommand.Parameters.Add(param);

                    break;
                case "Reservation":
                    param = new SqlParameter("@ReservationID", SqlDbType.NVarChar, 15, "ReservationID");
                    daMain.InsertCommand.Parameters.Add(param);
                    param = new SqlParameter("@GuestID", SqlDbType.Int, 15, "GuestID");
                    daMain.InsertCommand.Parameters.Add(param);
                    param = new SqlParameter("@RoomID", SqlDbType.NVarChar, 15, "RoomID");
                    daMain.InsertCommand.Parameters.Add(param);
                    param = new SqlParameter("@CheckInDate", SqlDbType.Date, 15, "CheckInDate");
                    daMain.InsertCommand.Parameters.Add(param);
                    param = new SqlParameter("@CheckoutDate", SqlDbType.Date, 15, "CheckOutDate");
                    daMain.InsertCommand.Parameters.Add(param);
                    break;
                case "Room":
                    param = new SqlParameter("@RoomID", SqlDbType.NVarChar, 15, "RoomID");
                    daMain.InsertCommand.Parameters.Add(param);// Add parameters for the Room table here.
                    param = new SqlParameter("@Availability", SqlDbType.Bit, 4, "Availability");
                    daMain.InsertCommand.Parameters.Add(param);
                    param = new SqlParameter("@Price", SqlDbType.Money, 8, "Price");
                    daMain.InsertCommand.Parameters.Add(param);
                    break;
                case "Address":
                    param = new SqlParameter("@GuestID", SqlDbType.Int, 15, "GuestID");
                    daMain.InsertCommand.Parameters.Add(param);
                    param = new SqlParameter("@City", SqlDbType.NVarChar, 100, "City");
                    daMain.InsertCommand.Parameters.Add(param);
                    param = new SqlParameter("@Town", SqlDbType.NVarChar, 100, "Town");
                    daMain.InsertCommand.Parameters.Add(param);
                    param = new SqlParameter("@Street", SqlDbType.NVarChar, 15, "Street");
                    daMain.InsertCommand.Parameters.Add(param);
                    param = new SqlParameter("@Postal Code", SqlDbType.Int, 15, "Postal Code");
                    daMain.InsertCommand.Parameters.Add(param);
                    break;
            }
        }

        private void Build_Delete_Parameter(Reservation res)
        {
            SqlParameter param = default(SqlParameter);
            param = new SqlParameter("@OriginalID", SqlDbType.NVarChar, 15, "ReservationID");
            param.SourceVersion = DataRowVersion.Original;
            daMain.DeleteCommand.Parameters.Add(param);
        }

        private void Create_Delete_Command(Reservation reservation)
        {
            daMain.DeleteCommand = new SqlCommand("DELETE from Reservation where ReservationID = @OriginalID", cnMain);
            Build_Delete_Parameter(reservation);
        }

            private void Create_INSERT_Command(Reservation reservation, string table)
        {
            
            switch (table)
            {
                case "Guest":
                    daMain.InsertCommand = new SqlCommand("INSERT into Guest (GuestID, FirstName, LastName, Gender, Phone, EmailAddress, Age) " +
                        "VALUES (@GuestID, @FirstName, @LastName, @Gender, @Phone, @EmailAddress, @Age)", cnMain);
                    break;
                case "GuestAccount":
                    daMain.InsertCommand = new SqlCommand("INSERT into GuestAccount (AccountNumber, GuestID, Balance, DepositPaid) " +
                        "VALUES (@AccountNumber, @GuestID, @Balance, @DepositPaid)", cnMain);
                    break;
                case "Room":
                    daMain.InsertCommand = new SqlCommand("INSERT into Room (RoomID, Availability, Price) " +
                        "VALUES (@RoomID, @Availability, @Price)", cnMain);
                    break;
                case "Reservation":
                    daMain.InsertCommand = new SqlCommand("INSERT into Reservation (ReservationID, GuestID, RoomID, CheckInDate, CheckOutDate) " +
                        "VALUES (@ReservationID, @GuestID, @RoomID, @CheckInDate, @CheckOutDate)", cnMain);
                    break;
                case "Address":
                    daMain.InsertCommand = new SqlCommand("INSERT into Guest (GuestID, City, Town, Street, Postal Code) " +
                        "VALUES (@GuestID, @City, @Town, @Street, @Postal Code)", cnMain);
                    break;
            }

            Build_INSERT_Parameters(reservation, table);
        }
        public bool UpdateDataSource(Reservation reservation, string table)
        {
            bool success = true;
            Create_INSERT_Command(reservation, table);
            switch (table)
            {
                case "Guest":
                    success = UpdateDataSource(sqlLocal1, table);
                    break;
                case "GuestAccount":
                    success = UpdateDataSource(sqlLocal2, table);
                    break;
                case "Reservation":
                    success = UpdateDataSource(sqlLocal3, table);
                    break;
                case "Room":
                    success = UpdateDataSource(sqlLocal4, table);
                    break;
                case "Address":
                    success = UpdateDataSource(sqlLocal5, table);
                    break;
            }

            return success;
        }

        public bool UpdateDataSourceD(Reservation reservation, string table)
        {
            bool success = true;
            Create_Delete_Command(reservation);
            switch (table)
            {
                case "Guest":
                    success = UpdateDataSource(sqlLocal1, table);
                    break;
                case "GuestAccount":
                    success = UpdateDataSource(sqlLocal2, table);
                    break;
                case "Reservation":
                    success = UpdateDataSource(sqlLocal3, table);
                    break;
                case "Room":
                    success = UpdateDataSource(sqlLocal4, table);
                    break;
                case "Address":
                    success = UpdateDataSource(sqlLocal5, table);
                    break;
            }

            return success;
        }
        #endregion

    }
}
