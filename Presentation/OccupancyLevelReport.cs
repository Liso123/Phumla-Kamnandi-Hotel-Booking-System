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

namespace PhumlaKaMnandiHotelManagementSystem.Presentation
{
    public partial class OccupancyLevelReport : Form
    {
        public bool listFormClosed;//= true;
        BookingsDB bookingsDB = new BookingsDB();
        public BookingController bookingController;
        DateTime start;
        DateTime end;
        public OccupancyLevelReport()
        {
            InitializeComponent();
            this.Load += OccupancyLevelReport_Load;
            this.Activated += OccupancyLevelReport_Activated;
        }

        public OccupancyLevelReport(BookingController bookingController)
        {
            InitializeComponent();
            this.bookingController = bookingController;
            bookingsDB = bookingController.bookingsDB;
            this.Load += OccupancyLevelReport_Load;
            this.Activated += OccupancyLevelReport_Activated;
            label1.Visible = false;
            txtOccupiedRooms.Visible = false;
            label2.Visible = false;
            txtPercent.Visible = false;
            label3.Visible = false;
            txtRooms.Visible = false;
            label4.Visible = false;
            textBox1.Visible = false;
        }

        public void PrintOccupancy(DateTime start, DateTime end)
        {
            int counter = 0;
            int perc = 0;
            int books = 0;
            foreach (Reservation reservation in bookingController.reservations)
            {
                if (reservation.CheckInDate >= start && reservation.CheckInDate <= end)
                {
                    if (counter < 5)
                    {
                        counter++;
                    }
                    books++;
                    perc += 20;
                }
            }
            label1.Visible = true;
            txtOccupiedRooms.Visible = true;
            label2.Visible = true;
            txtPercent.Visible = true;
            label3.Visible = true;
            txtRooms.Visible = true;
            label4.Visible = true;
            textBox1.Visible = true;
            textBox1.Text = books.ToString();
            txtOccupiedRooms.Text = counter.ToString();
            txtPercent.Text = perc.ToString();
            txtRooms.Text = "5";
        }
        private void OccupancyLevelReport_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            start = dateTimePicker2.Value.Date; 
            end = dateTimePicker1.Value.Date;
            if (end < start)
            {
                MessageBox.Show("End date must be less than Start date.");
            }
            else
            {
                PrintOccupancy(start, end);
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void OccupancyLevelReport_Activated(object sender, EventArgs e)
        {

        }

        private void OccupancyLevelReport_FormClosed(object sender, FormClosedEventArgs e)
        {
            listFormClosed = true;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
