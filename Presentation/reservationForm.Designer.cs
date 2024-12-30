namespace PhumlaKaMnandiHotelManagementSystem.Presentation
{
    partial class reservationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listViewRes = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // listViewRes
            // 
            this.listViewRes.HideSelection = false;
            this.listViewRes.Location = new System.Drawing.Point(15, 25);
            this.listViewRes.Name = "listViewRes";
            this.listViewRes.Size = new System.Drawing.Size(912, 413);
            this.listViewRes.TabIndex = 0;
            this.listViewRes.UseCompatibleStateImageBehavior = false;
            this.listViewRes.SelectedIndexChanged += new System.EventHandler(this.listViewRes_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Reservation List";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // reservationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listViewRes);
            this.Name = "reservationForm";
            this.Text = " ";
            this.Activated += new System.EventHandler(this.reservationForm_Activated);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.reservationForm_FormClosed);
            this.Load += new System.EventHandler(this.reservationForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listViewRes;
        private System.Windows.Forms.Label label1;
    }
}