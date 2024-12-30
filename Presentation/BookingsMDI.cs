using PhumlaKaMnandiHotelManagementSystem.Business;
using PhumlaKaMnandiHotelManagementSystem.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhumlaKaMnandiHotelManagementSystem.Presentation
{
    public partial class BookingsMDI : Form
    {
        private int childFormNumber = 0;
        Booking bookingForm;
        reservationForm ReservationForm;
        OccupancyLevelReport occupancyLevelReport;
        BookingController bookingController;
        BookingsDB bookingsDB = new BookingsDB();

        public BookingsMDI()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
             bookingController= new BookingController();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }
    #region Clutter
        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void toolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        #endregion

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        #region Create a New ChildForm  

        private void CreateBookingForm()
        {
            bookingForm = new Booking(bookingController);
            bookingForm.MdiParent = this;
            bookingForm.StartPosition = FormStartPosition.CenterParent;
        }
        private void CreateOccupancyForm()
        {
            occupancyLevelReport = new OccupancyLevelReport(bookingController);
            occupancyLevelReport.MdiParent = this;
            occupancyLevelReport.StartPosition = FormStartPosition.CenterParent;
        }

        public void CreateResrvationForm()
        {

           
            
                ReservationForm = new reservationForm(bookingController);
                ReservationForm.MdiParent = this;
                ReservationForm.StartPosition = FormStartPosition.CenterParent;
                ReservationForm.Show();
            
        }


        #endregion

        private void reseToolStripMenuItem_Click(object sender, EventArgs e)
        {

           
        }

        private void makeReservationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (bookingForm == null)
            {
                CreateBookingForm();               
               
            }
            if (bookingForm.BookingFormClosed)
            {
                CreateBookingForm();
               
            }
            bookingForm.Show();
        }

        private void printMenustrip_Click(object sender, EventArgs e)
        {
            if (ReservationForm == null)
            {
                CreateResrvationForm();
            }
            if (ReservationForm.listFormClosed)
            {
                CreateResrvationForm();
            }

            
            ReservationForm.setUpReservationListView();
            
            ReservationForm.Show();
        }

        private void bookingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void BookingsMDI_Load(object sender, EventArgs e)
        {
        }

        private void occupancyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (occupancyLevelReport == null)
            {
                CreateOccupancyForm();

            }
            if (occupancyLevelReport.listFormClosed)
            {
                CreateOccupancyForm();

            }
            occupancyLevelReport.Show();
        }
    }
}
