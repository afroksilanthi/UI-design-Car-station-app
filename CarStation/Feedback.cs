using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PachidisStation
{
    public partial class Feedback : Form
    {
        SQLiteBackend DB = new SQLiteBackend();
        private Timer timer;
        private string username;
        private Menu menu;

        public Feedback(Menu menu, string username)
        {
            InitializeComponent();

            this.menu = menu;
            this.username = username;

            WelcomeLabel.Text = ("Welcome " + username);

            Timer_Tick(this, null);

            // Initialize and start the timer
            timer = new Timer();
            timer.Interval = 1000; // Set the interval to 1000 milliseconds (1 second)
            timer.Tick += Timer_Tick;
            timer.Start();

            // Add rounded corners
            //ApplyRoundedCorners(fuelType, 12);
            //ApplyRoundedCorners(LitersTextBox, 12);
            //ApplyRoundedCorners(Vehicles, 12);

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


        private void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (HeaderText.Text != "" && MessageText.Text != "")
            {
                //POST MESSAGE
                try {
                    string uid = DB.ReadMultiData("SELECT UserId FROM Users WHERE Username == '" + username + "'")[0][0];
                    DB.ReadData("INSERT INTO Feedback (UID, Header, Message) VALUES ('" + uid + "', '" + HeaderText.Text + "', '" + MessageText.Text + "');");

                    MessageBox.Show("Feedback Submited successfuly!");
                    HeaderText.Text = ""; 
                    MessageText.Text = "";
                }
                catch {
                    MessageBox.Show("Something Went Wrong!");
                }
                
            }
            else {
                MessageBox.Show("Please fill every input");
            }
        }

        private void BackBtn_Click_1(object sender, EventArgs e)
        {
                CloseForm();
        }
    }
}
