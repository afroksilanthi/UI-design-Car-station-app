using System;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Collections.Generic;

namespace PachidisStation
{
    public partial class Fuel : Form
    {
        SQLiteBackend DB = new SQLiteBackend();
        private Timer timer;
        private string username;
        private Menu menu;

        public Fuel(Menu menu, string username)
        {
            InitializeComponent();

            this.menu = menu;
            this.username = username;

            WelcomeLabel.Text = ("Welcome " + username);

            Timer_Tick(this, null);

            // Fetch Cars
            List<string[]> carList = DB.ReadMultiData("SELECT Brand, Model, PlateID FROM Cars WHERE Owner == (SELECT UserId FROM Users WHERE Username == '" + username + "');");
            carList.ForEach(model => { Vehicles.Items.Add(model[0] + ' ' + model[1] + " " + model[2]); });

            // Initialize and start the timer
            timer = new Timer();
            timer.Interval = 1000; // Set the interval to 1000 milliseconds (1 second)
            timer.Tick += Timer_Tick;
            timer.Start();

            // Update default labels
            fuelType.SelectedIndex = 0;
            fuelType.SelectedText = "RON95";
            UpdateText();

            // Add rounded corners
            ApplyRoundedCorners(fuelType, 12);
            ApplyRoundedCorners(LitersTextBox, 12);
            ApplyRoundedCorners(Vehicles, 12);
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

        private void UpdateText()
        {
            float price = float.Parse(DB.ReadMultiData("SELECT Price FROM Products where Name == '" + fuelType.SelectedItem + "'")[0][0]);
            prliLabel.Text = price.ToString() + "€";

            try
            {
                float liters = float.Parse(LitersTextBox.Text);
                LitersTextBox.Text = liters.ToString();
                priceLabel.Text = (liters * price).ToString() + "€";
            }
            catch
            {
                MessageBox.Show("Check input data and try again");
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Update the time every second
            DateTime currentDateTime = DateTime.Now;
            dateLabel.Text = currentDateTime.ToString("dd MMMM yyyy | hh:mm tt", new CultureInfo("en-US"));
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

        private void BackBtn_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void AddQntBtn_Click(object sender, EventArgs e)
        {
            //Get fuel price from db
            float price = float.Parse(DB.ReadMultiData("SELECT Price FROM Products where Name == '" + fuelType.SelectedItem + "'")[0][0]);

            //if available quantity is >= with requested quantity
            try
            {
                if (float.Parse(DB.ReadMultiData("SELECT AvailableQuantity FROM Products where Name == '" + fuelType.SelectedItem + "'")[0][0]) > float.Parse(LitersTextBox.Text))
                {
                    float liters = float.Parse(LitersTextBox.Text) + 1;
                    LitersTextBox.Text = liters.ToString();
                    priceLabel.Text = (liters * price).ToString() + "€";
                }
                else
                {
                    MessageBox.Show("Not enough in store");
                }
            }
            catch
            {
                MessageBox.Show("Invalid input");
            }
        }

        private void RemoveQntBtn_Click(object sender, EventArgs e)
        {
            //Get fuel price from db
            float price = float.Parse(DB.ReadMultiData("SELECT Price FROM Products where Name == '" + fuelType.SelectedItem + "'")[0][0]);

            try
            {
                //if liters are > 0
                if (float.Parse(LitersTextBox.Text) > 0)
                {
                    float liters = float.Parse(LitersTextBox.Text) - 1;
                    LitersTextBox.Text = liters.ToString();
                    priceLabel.Text = (liters * price).ToString() + "€";
                }
            }
            catch
            {
                MessageBox.Show("Invalid input");
            }
        }

        private void FuelChanged(object sender, EventArgs e)
        {
            // Change fuel type
            UpdateText();
        }

        private void LitersOnChange(object sender, EventArgs e)
        {
            // Check availability on text box enter key press
            try
            {
                // Validity check
                if (float.Parse(LitersTextBox.Text) >= 0)
                {
                    if (float.Parse(DB.ReadMultiData("SELECT AvailableQuantity FROM Products where Name == '" + fuelType.SelectedItem + "'")[0][0]) >= float.Parse(LitersTextBox.Text))
                    {
                        UpdateText();
                    }
                    else
                    {
                        MessageBox.Show("Not enough in store");
                    }
                }
                else
                {
                    MessageBox.Show("Value must be >= 0");
                }
            }
            catch
            {
                MessageBox.Show("Check input data and try again");
            }
        }

        private void notificationBtn_Click(object sender, EventArgs e)
        {
            using (Messages frm2 = new Messages(menu, username))
            {
                Hide();
                frm2.ShowDialog();
            }
        }

        private void CheckoutBtn_Click(object sender, EventArgs e)
        {
            if (Vehicles.Text == "" || fuelType.Text == "" || LitersTextBox.Text == "" || LitersTextBox.Text == "0")
            {
                MessageBox.Show("Fields cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string uid = DB.ReadMultiData("SELECT UserId FROM Users WHERE Username == '" + username + "'")[0][0];
                string pid = DB.ReadMultiData("SELECT ID FROM Products WHERE Name == '" + fuelType.Text + "'")[0][0];

                DateTime a = DateTime.Now;

                DB.ReadData("INSERT INTO UserOrders(UserID, ProductID, Quantity, TotalPrice, OrderDate, DueDate) VALUES('" + uid + "', '" + pid + "', '" + LitersTextBox.Text + "', '" + priceLabel.Text.Replace('€', ' ').Replace(',', '.') + "', '" + a + "', '" + a.AddDays(30) + "');");
                string oid = DB.ReadMultiData("SELECT OrderID FROM UserOrders WHERE UserID == '" + uid + "' AND OrderDate == '" + a + "'")[0][0];

                using (Billing frm2 = new Billing(this, username, int.Parse(oid)))
                {
                    Hide();
                    frm2.ShowDialog();
                }
            }
        }
    }
}
