using System;
using System.Globalization;
using System.Windows.Forms;

namespace PachidisStation
{
    public partial class Menu : Form
    {
        private Timer timer;
        string username;
        LoginForm login;
        SQLiteBackend DB = new SQLiteBackend();

        public Menu(LoginForm login, string username)
        {
            InitializeComponent();


            this.login = login;
            this.username = username;

            WelcomeLabel.Text = ("Welcome " + username);
            string perms = DB.ReadMultiData("SELECT perms FROM Users WHERE username = '" + username + "';")[0][0];
            DB.CloseConnection();

            if (perms == "admin" || perms == "worker")
            {
                adminBtn.Enabled = true;
                adminBtn.Visible = true;
            }

            Timer_Tick(this, null);

            // Initialize and start the timer
            timer = new Timer();
            timer.Interval = 1000; // Set the interval to 1000 milliseconds (1 second)
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Update the time every second
            DateTime currentDateTime = DateTime.Now;
            dateLabel.Text = currentDateTime.ToString("dd MMMM yyyy | hh:mm tt", new CultureInfo("en-US"));
        }

        private void ParkingBtn_Click(object sender, EventArgs e)
        {
            using (Parking frm2 = new Parking(this, username))
            {
                Hide();
                frm2.ShowDialog();
            }
        }

        private void FuelBtn_Click(object sender, EventArgs e)
        {
            using (Fuel frm2 = new Fuel(this, username))
            {
                Hide();
                frm2.ShowDialog();
            }
        }

        private void MyCarBtn_Click(object sender, EventArgs e)
        {
            using (MyCar frm2 = new MyCar(this, username))
            {
                Hide();
                frm2.ShowDialog();
            }
        }

        private void ServiceBtn_Click(object sender, EventArgs e)
        {
            using (Service frm2 = new Service(this, username))
            {
                Hide();
                frm2.ShowDialog();
            }
        }

        private void MyAccountBtn_Click(object sender, EventArgs e)
        {
            using (MyAccount frm2 = new MyAccount(this, username))
            {
                Hide();
                frm2.ShowDialog();
            }
        }

        private void notificationBtn_Click(object sender, EventArgs e)
        {
            using (Messages frm2 = new Messages(this, username))
            {
                Hide();
                frm2.ShowDialog();
            }
        }

        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Do you really want to logout?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialog == DialogResult.Yes)
            {
                username = "";

                timer.Enabled = false;
                timer.Stop();
                timer = null;

                login.Show();
                Dispose();
            }
            else
            {
                e.Cancel = true;
                Show();
            }
        }

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void adminBtn_Click(object sender, EventArgs e)
        {
            using (Administration frm2 = new Administration(this, username))
            {
                Hide();
                frm2.ShowDialog();
            }
        }

        private void feedbackBtn_Click(object sender, EventArgs e)
        {
            using (Feedback frm2 = new Feedback(this, username))
            {
                Hide();
                frm2.ShowDialog();
            }
        }
    }
}
