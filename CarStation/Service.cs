using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Globalization;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace PachidisStation
{
    public partial class Service : Form
    {
        SQLiteBackend DB = new SQLiteBackend();

        private Timer timer;
        private string username;
        private Menu menu;

        bool[] Selected = new bool[8];
        float[] prices = new float[9];

        public Service(Menu menu, string username)
        {
            InitializeComponent();

            this.menu = menu;
            this.username = username;

            WelcomeLabel.Text = ("Welcome " + username);

            DateTime currentDateTime = DateTime.Now;
            dateLabel.Text = currentDateTime.ToString("dd MMMM yyyy | hh:mm tt", new CultureInfo("en-US"));

            // Initialize and start the timer
            timer = new Timer();
            timer.Interval = 1000; // Set the interval to 1000 milliseconds (1 second)
            timer.Tick += Timer_Tick;
            timer.Start();

            // Fetch Cars
            List<string[]> carList = DB.ReadMultiData("SELECT Brand, Model, PlateID FROM Cars WHERE Owner == (SELECT UserId FROM Users WHERE Username == '" + username + "');");
            carList.ForEach(model => { Vehicles.Items.Add(model[0] + ' ' + model[1] + " " + model[2]); });

            prices[0] = float.Parse(DB.ReadData("SELECT Price FROM Products WHERE Name == 'svTyres';"));
            prices[1] = float.Parse(DB.ReadData("SELECT Price FROM Products WHERE Name == 'svBattery';"));
            prices[2] = float.Parse(DB.ReadData("SELECT Price FROM Products WHERE Name == 'svKeylocks';"));
            prices[3] = float.Parse(DB.ReadData("SELECT Price FROM Products WHERE Name == 'svAC';"));
            prices[4] = float.Parse(DB.ReadData("SELECT Price FROM Products WHERE Name == 'svOil';"));
            prices[5] = float.Parse(DB.ReadData("SELECT Price FROM Products WHERE Name == 'svTint';"));
            prices[6] = float.Parse(DB.ReadData("SELECT Price FROM Products WHERE Name == 'svGPS';"));
            prices[7] = float.Parse(DB.ReadData("SELECT Price FROM Products WHERE Name == 'svInspect';"));
            prices[8] = float.Parse(DB.ReadData("SELECT Price FROM Products WHERE Name == 'Service';"));

            for (int i = 0; i < 8; i++)
            {
                Selected[i] = false;
            }

            ApplyRoundedCorners(Vehicles, 12);
            ApplyRoundedCorners(dateBox, 12);
            ApplyRoundedCorners(noteBox, 12);

        }

        private void ApplyRoundedCorners(Control control, int radius)
        {
            // Create a GraphicsPath for the rounded rectangle
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius * 2, radius * 2, 180, 90);
            path.AddArc(control.Width - radius * 2, 0, radius * 2, radius * 2, 270, 90);
            path.AddArc(control.Width - radius * 2, control.Height - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(0, control.Height - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseFigure();

            // Set the Region property of the control to the rounded rectangle
            control.Region = new Region(path);
        }

        private void CloseForm()
        {
            // Close DB
            DB.CloseConnection();
            DB = null;

            // Stop timer
            timer.Enabled = false;
            timer.Stop();
            timer.Dispose();

            // Show menu
            menu.Show();

            // Close window
            Dispose();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Update the time every second
            DateTime currentDateTime = DateTime.Now;
            dateLabel.Text = currentDateTime.ToString("dd MMMM yyyy | hh:mm tt", new CultureInfo("en-US"));
        }
        
        private void BackBtn_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void notificationBtn_Click(object sender, EventArgs e)
        {
            using (Messages frm2 = new Messages(menu, username))
            {
                Hide();
                frm2.ShowDialog();
                CloseForm();
            }
        }

        private void Service_Load(object sender, EventArgs e)
        {

        }

        private void tyreBtn_Click(object sender, EventArgs e)
        {
            if (Selected[0] == false)
            {
                tyreBtn.BorderStyle = BorderStyle.Fixed3D;
                Selected[0] = true;
            }
            else
            {
                Selected[0] = false;
                tyreBtn.BorderStyle = BorderStyle.None;
            }
        }

        private void batteryBtn_Click(object sender, EventArgs e)
        {
            if (Selected[1] == false)
            {
                batteryBtn.BorderStyle = BorderStyle.Fixed3D;
                Selected[1] = true;
            }
            else
            {
                Selected[1] = false;
                batteryBtn.BorderStyle = BorderStyle.None;
            }
        }

        private void keylocksBtn_Click(object sender, EventArgs e)
        {
            if (Selected[2] == false)
            {
                keylocksBtn.BorderStyle = BorderStyle.Fixed3D;
                Selected[2] = true;
            }
            else
            {
                Selected[2] = false;
                keylocksBtn.BorderStyle = BorderStyle.None;
            }
        }

        private void acBtn_Click(object sender, EventArgs e)
        {
            if (Selected[3] == false)
            {
                acBtn.BorderStyle = BorderStyle.Fixed3D;
                Selected[3] = true;
            }
            else
            {
                Selected[3] = false;
                acBtn.BorderStyle = BorderStyle.None;
            }
        }

        private void oilBtn_Click(object sender, EventArgs e)
        {
            if (Selected[4] == false)
            {
                oilBtn.BorderStyle = BorderStyle.Fixed3D;
                Selected[4] = true;
            }
            else
            {
                Selected[4] = false;
                oilBtn.BorderStyle = BorderStyle.None;
            }
        }

        private void tintBtn_Click(object sender, EventArgs e)
        {
            if (Selected[5] == false)
            {
                tintBtn.BorderStyle = BorderStyle.Fixed3D;
                Selected[5] = true;
            }
            else
            {
                Selected[5] = false;
                tintBtn.BorderStyle = BorderStyle.None;
            }
        }

        private void gpsBtn_Click(object sender, EventArgs e)
        {
            if (Selected[6] == false)
            {
                gpsBtn.BorderStyle = BorderStyle.Fixed3D;
                Selected[6] = true;
            }
            else
            {
                Selected[6] = false;
                gpsBtn.BorderStyle = BorderStyle.None;
            }
        }

        private void inspectionBtn_Click(object sender, EventArgs e)
        {
            if (Selected[7] == false)
            {
                inspectionBtn.BorderStyle = BorderStyle.Fixed3D;
                Selected[7] = true;
            } else
            {
                Selected[7] = false;
                inspectionBtn.BorderStyle = BorderStyle.None;
            }
        }

        //private void Service_FormClosing(object sender, FormClosingEventArgs e)
        //{
        //    DialogResult dialog = MessageBox.Show("Do you really want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        //    if (dialog == DialogResult.No )
        //    {
        //        e.Cancel = true;
        //    }
        //}

        private void completeBtn_Click(object sender, EventArgs e)
        {
            float totalcost = 0;
            string[] selectedItem = Vehicles.SelectedItem.ToString().Split(' ');
            string selectedPlate = selectedItem[selectedItem.Length - 1];
            for (int i=0; i<=7; i++)
            {
                if (Selected[i] == true)
                {
                    totalcost += prices[i];
                }
            }
            totalcost += prices[8];

            string uid = DB.ReadMultiData("SELECT UserId FROM Users WHERE Username == '" + username + "'")[0][0];
            DateTime a = DateTime.Now;

            DB.ReadData("INSERT INTO UserOrders(UserID, ProductID, Quantity, TotalPrice, OrderDate, DueDate) VALUES('" + uid + "', '" + 99 + "', '" + 1 + "', '" + totalcost + "', '" + a + "', '" + a.AddDays(30) + "');");
            DB.ReadMultiData("INSERT INTO Services (UID,pID,svDate,Notes,TotalCost,svTyres,svBattery,svKeylocks,svAC,svOil,svTint,svGPS,svInspect) VALUES ( (SELECT UserId FROM Users WHERE Username == '" + username + "'), '" + selectedPlate + "', '" + dateBox.Value.ToString() + "', '" + noteBox.Text + "', '" + totalcost + "', '" + Selected[0] + "', '" + Selected[1] + "', '" + Selected[2] + "', '" + Selected[3] + "', '" + Selected[4] + "', '" + Selected[5] + "', '" + Selected[6] + "', '" + Selected[7] + "');");

            string oid = DB.ReadMultiData("SELECT OrderID FROM UserOrders WHERE UserID == '" + uid + "' AND OrderDate == '" + a + "'")[0][0];
            using (Billing frm2 = new Billing(this, username, int.Parse(oid), Selected))
            {
                Hide();
                frm2.ShowDialog();
            }
        }
    }
}
