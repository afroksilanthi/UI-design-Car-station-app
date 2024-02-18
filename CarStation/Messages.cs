using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace PachidisStation
{
    public partial class Messages : Form
    {
        private SQLiteBackend DB = new SQLiteBackend();
        private Timer timer;
        private Menu menu;

        public Messages(Menu menu, string username)
        {
            InitializeComponent();

            this.menu = menu;

            WelcomeLabel.Text = ("Welcome " + username);

            DateTime currentDateTime = DateTime.Now;
            dateLabel.Text = currentDateTime.ToString("dd MMMM yyyy | hh:mm tt", new CultureInfo("en-US"));

            List<string[]> notifList = DB.ReadMultiData("SELECT Message, Date FROM Notifications WHERE DestinationUser == (SELECT UserId FROM Users WHERE Username == '" + username + "');");
            notifList.ForEach(message => { Notifications.Rows.Add(message); });

            // Initialize and start the timer
            timer = new Timer();
            timer.Interval = 1000; // Set the interval to 1000 milliseconds (1 second)
            timer.Tick += Timer_Tick;
            timer.Start();

            Notifications.Sort(Date, System.ComponentModel.ListSortDirection.Descending);
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

        private void BackBtn_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Update the time every second
            DateTime currentDateTime = DateTime.Now;
            dateLabel.Text = currentDateTime.ToString("dd MMMM yyyy | hh:mm tt", new CultureInfo("en-US"));
        }
    }
}
