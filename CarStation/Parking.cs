using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace PachidisStation
{
    public partial class Parking : Form
    {
        SQLiteBackend DB = new SQLiteBackend();
        private Timer timer;
        string username;
        Menu menu;

        public Parking(Menu menu, string username)
        {
            InitializeComponent();

            this.username = username;
            this.menu = menu;

            WelcomeLabel.Text = ("Welcome " + username);

            Timer_Tick(this, null);

            // Initialize and start the timer
            timer = new Timer();
            timer.Interval = 1000; // Set the interval to 1000 milliseconds (1 second)
            timer.Tick += Timer_Tick;
            timer.Start();

            // Add rounded corners
            ApplyRoundedCorners(Vehicles, 12);

            // Fetch Cars
            List<string[]> carList = DB.ReadMultiData("SELECT Brand, Model, PlateID FROM Cars WHERE Owner == (SELECT UserId FROM Users WHERE Username == '" + username + "');");
            carList.ForEach(model => { Vehicles.Items.Add(model[0] + ' ' + model[1] + " " + model[2]); });

            // Select spot
            string emptySpot = DB.ReadMultiData("SELECT SpotID FROM Spots WHERE isEmpty == '1';")[0][0];
            spotText.Text = emptySpot;

            //Calculate time
            EndDatePicker.Value = EndDatePicker.Value.AddDays(1);
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

        private void cbxDesign_DrawItem(object sender, DrawItemEventArgs e)
        {
            // By using Sender, one method could handle multiple ComboBoxes
            ComboBox cbx = sender as ComboBox;
            if (cbx != null)
            {
                // Always draw the background
                e.DrawBackground();

                // Drawing one of the items?
                if (e.Index >= 0)
                {
                    // Set the string alignment. Choices are Center, Near and Far
                    StringFormat sf = new StringFormat();
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Center;

                    // Set the Brush to ComboBox ForeColor to maintain any ComboBox color settings
                    // Assumes Brush is solid
                    Brush brush = new SolidBrush(cbx.ForeColor);

                    // If drawing highlighted selection, change brush
                    if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                    {
                        brush = SystemBrushes.HighlightText;
                    }

                    // Draw the string
                    e.Graphics.DrawString(cbx.Items[e.Index].ToString(), cbx.Font, brush, e.Bounds, sf);
                }
            }
        }

        private Double CalculateTime()
        {
            DateTime a = StartDatePicker.Value;
            DateTime b = EndDatePicker.Value;
            int diff = (int)Math.Round(EndDatePicker.Value.Subtract(a).TotalHours);
            if (diff >= 0 )
            {
                hoursText.Text = diff.ToString() + " Hours";
                priceCalc(diff);
                return diff;
                
            }
            else {
                
                StartDatePicker.Value = DateTime.Now;
                EndDatePicker.Value = EndDatePicker.Value.AddDays(1);
                CalculateTime();
                return double.NaN;

            }
        }

        private float priceCalc(int hours)
        {
            //get price per hour from db

            // 1 - 24
            float pricePerHour1D = float.Parse(DB.ReadMultiData("SELECT Value FROM Parameters WHERE Name == 'Parking1D';")[0][0]);

            // 25 - 48
            float pricePerHour2D = float.Parse(DB.ReadMultiData("SELECT Value FROM Parameters WHERE Name == 'Parking2D';")[0][0]);

            // 49 +
            float pricePerHour3Dp = float.Parse(DB.ReadMultiData("SELECT Value FROM Parameters WHERE Name == 'Parking3Dp';")[0][0]);

            float cost = 0;
            if (hours <= 24)
            {
                cost = hours * pricePerHour1D;
            }
            else if (hours <= 48)
            {
                cost = (24 * pricePerHour1D) + ((hours - 24) * pricePerHour2D);
            }
            else if (hours > 48)
            {
                cost = (24 * pricePerHour1D) + (24 * pricePerHour2D) + ((hours - 48) * pricePerHour3Dp);
            }

            HourlyPriceText.Text = (Math.Truncate(100 * (cost / hours)) / 100).ToString() + " €";

            TotalPriceText.Text = cost.ToString() + " €";
            return cost;
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

        private void StartDatePicker_ValueChanged(object sender, EventArgs e)
        {
            CalculateTime();
        }

        private void EndDatePicker_ValueChanged(object sender, EventArgs e)
        {
            CalculateTime();
        }

        private void bookBtn_Click(object sender, EventArgs e)
        {
            if (Vehicles.Text == "" || StartDatePicker.Text == "" || EndDatePicker.Text == "")
            {
                MessageBox.Show("Fields cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string uid = DB.ReadMultiData("SELECT UserId FROM Users WHERE Username == '" + username + "'")[0][0];
                string[] selectedItem = Vehicles.SelectedItem.ToString().Split(' ');
                string selectedPlate = selectedItem[selectedItem.Length - 1];

                DateTime a = DateTime.Now;

                DB.ReadData("INSERT INTO Parking(Spot,CarID,StartDate,EndDate,isParked) VALUES('" + spotText.Text + "','" + selectedPlate + "','" + StartDatePicker.Text + "','" + EndDatePicker.Text + "','0');");
                DB.ReadData("INSERT INTO UserOrders(UserID, ProductID, Quantity, TotalPrice, OrderDate, DueDate) VALUES('" + uid + "', '" + 98 + "', '" + 0 + "', '" + TotalPriceText.Text.Replace('€', ' ').Replace(',', '.') + "', '" + a + "', '" + a.AddDays(30) + "');");
                string oid = DB.ReadMultiData("SELECT OrderID FROM UserOrders WHERE UserID == '" + uid + "' AND OrderDate == '" + a + "'")[0][0];

                using (Billing frm2 = new Billing(this, username, int.Parse(oid)))
                {
                    Hide();
                    frm2.ShowDialog();
                }
            }

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            using (Messages frm2 = new Messages(menu, username))
            {
                Hide();
                frm2.ShowDialog();
                CloseForm();
            }
        }
    }
}
