using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace PachidisStation
{
    public partial class MyAccount : Form
    {
        SQLiteBackend DB = new SQLiteBackend();

        private Timer timer;
        private string username;
        private Menu menu;

        public MyAccount(Menu menu, string username)
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

            // Initialize Textbox Values
            UsernameBox.Text = username;

            try
            {
                List<string[]> query = DB.ReadMultiData("SELECT Name, Surname, Address, Email FROM Users where Username == '" + username + "'");
                nameBox.Text = query[0][0];
                SurnameBox.Text = query[0][1];
                AddressBox.Text = query[0][2];
                EmailBox.Text = query[0][3];
            }
            catch
            {
                MessageBox.Show("There was an error when fetching your account, please logout and try again");
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            UsernameBox.Enabled = true;
            nameBox.Enabled = true;
            SurnameBox.Enabled = true;
            AddressBox.Enabled = true;
            EmailBox.Enabled = true;
            UpdateBtn.Enabled = true;
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            string uid = DB.ReadMultiData("SELECT UserId FROM Users where Username == '" + username + "'")[0][0];

            if (UsernameBox.Text == "" || nameBox.Text == "" || SurnameBox.Text == "" || EmailBox.Text == "" || AddressBox.Text == "")
            {
                MessageBox.Show("Fields cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Test availabilty, if the first try passes then the username / email already exist
                try
                {
                    string test_username = DB.ReadMultiData("SELECT Username FROM Users where Username == '" + UsernameBox.Text + "' AND UserID != " + uid + ";")[0][0];
                    string test_email = DB.ReadMultiData("SELECT Email FROM Users where Email == '" + EmailBox.Text + "' AND UserID != " + uid + ";")[0][0];
                    MessageBox.Show("Username or Email already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch
                {
                    try
                    {
                        DB.ReadData("UPDATE Users SET Username= '" + UsernameBox.Text + "', Name = '" + nameBox.Text + "', Surname = '" + SurnameBox.Text + "', Address = '" + AddressBox.Text + "', Email = '" + EmailBox.Text + "' WHERE UserID == '" + uid + "';");
                        MessageBox.Show("User " + UsernameBox.Text + " Updated! ", "MyAccount", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CloseForm();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("There was an error when processing your request, please try again");
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        private void CloseForm()
        {
            //Close DB
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

        private void notificationBtn_Click(object sender, EventArgs e)
        {
            using (Messages frm2 = new Messages(menu, username))
            {
                Hide();
                frm2.ShowDialog();
            }
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void myCardsBtn_Click(object sender, EventArgs e)
        {
            using (Payment frm2 = new Payment(menu, username))
            {
                Hide();
                frm2.ShowDialog();
            }
        }
    }
}
