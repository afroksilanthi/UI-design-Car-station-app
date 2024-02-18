using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PachidisStation
{
    partial class Menu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu));
            this.panel1 = new System.Windows.Forms.Panel();
            this.adminBtn = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.notificationBtn = new System.Windows.Forms.PictureBox();
            this.dateLabel = new System.Windows.Forms.Label();
            this.WelcomeLabel = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.mail = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.phone = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.feedbackBtn = new System.Windows.Forms.PictureBox();
            this.logoutBtn = new System.Windows.Forms.PictureBox();
            this.MyAccountBtn = new System.Windows.Forms.PictureBox();
            this.MyCarBtn = new System.Windows.Forms.PictureBox();
            this.ServiceBtn = new System.Windows.Forms.PictureBox();
            this.FuelBtn = new System.Windows.Forms.PictureBox();
            this.ParkingBtn = new System.Windows.Forms.PictureBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.adminBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.notificationBtn)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.phone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.feedbackBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoutBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MyAccountBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MyCarBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ServiceBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FuelBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ParkingBtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(4)))), ((int)(((byte)(193)))), ((int)(((byte)(205)))));
            this.panel1.Controls.Add(this.adminBtn);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.notificationBtn);
            this.panel1.Controls.Add(this.dateLabel);
            this.panel1.Controls.Add(this.WelcomeLabel);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(-9, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1513, 170);
            this.panel1.TabIndex = 2;
            // 
            // adminBtn
            // 
            this.adminBtn.BackColor = System.Drawing.Color.Transparent;
            this.adminBtn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.adminBtn.Enabled = false;
            this.adminBtn.Image = ((System.Drawing.Image)(resources.GetObject("adminBtn.Image")));
            this.adminBtn.Location = new System.Drawing.Point(1374, 50);
            this.adminBtn.Name = "adminBtn";
            this.adminBtn.Size = new System.Drawing.Size(68, 68);
            this.adminBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.adminBtn.TabIndex = 20;
            this.adminBtn.TabStop = false;
            this.adminBtn.Visible = false;
            this.adminBtn.Click += new System.EventHandler(this.adminBtn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::PachidisStation.Properties.Resources.parcar;
            this.pictureBox1.Location = new System.Drawing.Point(1104, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(264, 164);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // notificationBtn
            // 
            this.notificationBtn.Image = ((System.Drawing.Image)(resources.GetObject("notificationBtn.Image")));
            this.notificationBtn.Location = new System.Drawing.Point(128, 81);
            this.notificationBtn.Name = "notificationBtn";
            this.notificationBtn.Size = new System.Drawing.Size(43, 36);
            this.notificationBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.notificationBtn.TabIndex = 10;
            this.notificationBtn.TabStop = false;
            this.notificationBtn.Click += new System.EventHandler(this.notificationBtn_Click);
            // 
            // dateLabel
            // 
            this.dateLabel.AutoSize = true;
            this.dateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.dateLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.dateLabel.Location = new System.Drawing.Point(123, 116);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(316, 29);
            this.dateLabel.TabIndex = 9;
            this.dateLabel.Text = "1 January 2000 | 14:14 PM";
            // 
            // WelcomeLabel
            // 
            this.WelcomeLabel.AutoSize = true;
            this.WelcomeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.WelcomeLabel.Location = new System.Drawing.Point(174, 87);
            this.WelcomeLabel.Name = "WelcomeLabel";
            this.WelcomeLabel.Size = new System.Drawing.Size(222, 29);
            this.WelcomeLabel.TabIndex = 4;
            this.WelcomeLabel.Text = "Welcome user101";
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(3, 166);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1489, 747);
            this.panel2.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.mail);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.phone);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(-9, 868);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1513, 50);
            this.panel3.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(845, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 17);
            this.label4.TabIndex = 15;
            this.label4.Text = "info@parcar.com";
            this.label4.UseMnemonic = false;
            // 
            // mail
            // 
            this.mail.BackColor = System.Drawing.Color.Transparent;
            this.mail.Image = global::PachidisStation.Properties.Resources.mail;
            this.mail.Location = new System.Drawing.Point(820, 7);
            this.mail.Name = "mail";
            this.mail.Size = new System.Drawing.Size(20, 20);
            this.mail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mail.TabIndex = 17;
            this.mail.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(800, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 20);
            this.label5.TabIndex = 18;
            this.label5.Text = "|";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label2.Location = new System.Drawing.Point(684, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "© Copyright parCAR 2023";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(570, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "Contact us:";
            // 
            // phone
            // 
            this.phone.BackColor = System.Drawing.Color.Transparent;
            this.phone.Image = global::PachidisStation.Properties.Resources.phone;
            this.phone.Location = new System.Drawing.Point(662, 5);
            this.phone.Name = "phone";
            this.phone.Size = new System.Drawing.Size(25, 25);
            this.phone.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.phone.TabIndex = 16;
            this.phone.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(693, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 17);
            this.label3.TabIndex = 13;
            this.label3.Text = "+6900000000";
            // 
            // feedbackBtn
            // 
            this.feedbackBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(220)))), ((int)(((byte)(226)))));
            this.feedbackBtn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.feedbackBtn.Image = global::PachidisStation.Properties.Resources.feedback;
            this.feedbackBtn.Location = new System.Drawing.Point(119, 747);
            this.feedbackBtn.Name = "feedbackBtn";
            this.feedbackBtn.Size = new System.Drawing.Size(99, 74);
            this.feedbackBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.feedbackBtn.TabIndex = 22;
            this.feedbackBtn.TabStop = false;
            this.feedbackBtn.Click += new System.EventHandler(this.feedbackBtn_Click);
            // 
            // logoutBtn
            // 
            this.logoutBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(220)))), ((int)(((byte)(226)))));
            this.logoutBtn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.logoutBtn.Image = ((System.Drawing.Image)(resources.GetObject("logoutBtn.Image")));
            this.logoutBtn.Location = new System.Drawing.Point(1365, 765);
            this.logoutBtn.Name = "logoutBtn";
            this.logoutBtn.Size = new System.Drawing.Size(68, 56);
            this.logoutBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.logoutBtn.TabIndex = 11;
            this.logoutBtn.TabStop = false;
            this.logoutBtn.Click += new System.EventHandler(this.logoutBtn_Click);
            // 
            // MyAccountBtn
            // 
            this.MyAccountBtn.BackColor = System.Drawing.Color.Transparent;
            this.MyAccountBtn.Image = ((System.Drawing.Image)(resources.GetObject("MyAccountBtn.Image")));
            this.MyAccountBtn.Location = new System.Drawing.Point(850, 513);
            this.MyAccountBtn.Name = "MyAccountBtn";
            this.MyAccountBtn.Size = new System.Drawing.Size(263, 262);
            this.MyAccountBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.MyAccountBtn.TabIndex = 9;
            this.MyAccountBtn.TabStop = false;
            this.MyAccountBtn.Click += new System.EventHandler(this.MyAccountBtn_Click);
            // 
            // MyCarBtn
            // 
            this.MyCarBtn.BackColor = System.Drawing.Color.Transparent;
            this.MyCarBtn.Image = ((System.Drawing.Image)(resources.GetObject("MyCarBtn.Image")));
            this.MyCarBtn.Location = new System.Drawing.Point(420, 513);
            this.MyCarBtn.Name = "MyCarBtn";
            this.MyCarBtn.Size = new System.Drawing.Size(263, 262);
            this.MyCarBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.MyCarBtn.TabIndex = 7;
            this.MyCarBtn.TabStop = false;
            this.MyCarBtn.Click += new System.EventHandler(this.MyCarBtn_Click);
            // 
            // ServiceBtn
            // 
            this.ServiceBtn.BackColor = System.Drawing.Color.Transparent;
            this.ServiceBtn.Image = ((System.Drawing.Image)(resources.GetObject("ServiceBtn.Image")));
            this.ServiceBtn.Location = new System.Drawing.Point(1015, 221);
            this.ServiceBtn.Name = "ServiceBtn";
            this.ServiceBtn.Size = new System.Drawing.Size(263, 262);
            this.ServiceBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ServiceBtn.TabIndex = 6;
            this.ServiceBtn.TabStop = false;
            this.ServiceBtn.Click += new System.EventHandler(this.ServiceBtn_Click);
            // 
            // FuelBtn
            // 
            this.FuelBtn.BackColor = System.Drawing.Color.Transparent;
            this.FuelBtn.Image = ((System.Drawing.Image)(resources.GetObject("FuelBtn.Image")));
            this.FuelBtn.Location = new System.Drawing.Point(633, 221);
            this.FuelBtn.Name = "FuelBtn";
            this.FuelBtn.Size = new System.Drawing.Size(263, 262);
            this.FuelBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.FuelBtn.TabIndex = 5;
            this.FuelBtn.TabStop = false;
            this.FuelBtn.Click += new System.EventHandler(this.FuelBtn_Click);
            // 
            // ParkingBtn
            // 
            this.ParkingBtn.BackColor = System.Drawing.Color.Transparent;
            this.ParkingBtn.Image = ((System.Drawing.Image)(resources.GetObject("ParkingBtn.Image")));
            this.ParkingBtn.Location = new System.Drawing.Point(239, 221);
            this.ParkingBtn.Name = "ParkingBtn";
            this.ParkingBtn.Size = new System.Drawing.Size(266, 262);
            this.ParkingBtn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ParkingBtn.TabIndex = 4;
            this.ParkingBtn.TabStop = false;
            this.ParkingBtn.Click += new System.EventHandler(this.ParkingBtn_Click);
            // 
            // pictureBox7
            // 
            this.pictureBox7.Image = global::PachidisStation.Properties.Resources.bg;
            this.pictureBox7.Location = new System.Drawing.Point(-152, 454);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(1792, 477);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox7.TabIndex = 8;
            this.pictureBox7.TabStop = false;
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(197)))), ((int)(((byte)(207)))));
            this.ClientSize = new System.Drawing.Size(1492, 915);
            this.Controls.Add(this.feedbackBtn);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.logoutBtn);
            this.Controls.Add(this.MyAccountBtn);
            this.Controls.Add(this.MyCarBtn);
            this.Controls.Add(this.ServiceBtn);
            this.Controls.Add(this.FuelBtn);
            this.Controls.Add(this.ParkingBtn);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox7);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Menu_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.adminBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.notificationBtn)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.phone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.feedbackBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logoutBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MyAccountBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MyCarBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ServiceBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FuelBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ParkingBtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox ParkingBtn;
        private System.Windows.Forms.Label WelcomeLabel;
        private System.Windows.Forms.PictureBox FuelBtn;
        private System.Windows.Forms.PictureBox ServiceBtn;
        private System.Windows.Forms.PictureBox MyCarBtn;
        private System.Windows.Forms.Label dateLabel;
        private System.Windows.Forms.PictureBox pictureBox7;
        private PictureBox MyAccountBtn;
        private PictureBox notificationBtn;
        private PictureBox logoutBtn;
        private PictureBox adminBtn;
        private Panel panel3;
        private Label label4;
        private PictureBox mail;
        private Label label5;
        private Label label2;
        private Label label1;
        private PictureBox phone;
        private Label label3;
        private PictureBox feedbackBtn;
    }
   
}