using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PachidisStation
{
    public partial class Payment : Form
    {
        SQLiteBackend DB = new SQLiteBackend();

        private Timer timer;
        private string username;
        private Form prev;

        public Payment(Form prev, string username)
        {
            InitializeComponent();

            this.prev = prev;
            this.username = username;

            WelcomeLabel.Text = ("Welcome " + username);

            // Load Cards
            ReloadCards();

            DateTime currentDateTime = DateTime.Now;
            dateLabel.Text = currentDateTime.ToString("dd MMMM yyyy | hh:mm tt", new CultureInfo("en-US"));

            // Initialize and start the timer
            timer = new Timer();
            timer.Interval = 1000; // Set the interval to 1000 milliseconds (1 second)
            timer.Tick += Timer_Tick;
            timer.Start();
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
            prev.Show();

            // Close window
            Dispose();
        }

        private void ReloadCards()
        {
            Cards.Items.Clear();

            List<string[]> cardList = DB.ReadMultiData("SELECT CardID, Type FROM PayInfo WHERE UserId == (SELECT UserId FROM Users WHERE Username == '" + username + "');");
            cardList.ForEach(card => { Cards.Items.Add(card[0] + ' ' + card[1]); });
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Update the time every second
            DateTime currentDateTime = DateTime.Now;
            dateLabel.Text = currentDateTime.ToString("dd MMMM yyyy | hh:mm tt", new CultureInfo("en-US"));
        }

        private void backBox_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            string uid = DB.ReadMultiData("SELECT UserId FROM Users where Username == '" + username + "'")[0][0];

            if (typeBox.Text == "" || IDBox.Text == "" || CVVBox.Text == "" || dateBox.Text == "" || holderBox.Text == "")
            {
                MessageBox.Show("Fields cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    string test_existence = DB.ReadMultiData("SELECT CardID FROM PayInfo WHERE CardID == '" + IDBox.Text + "' AND UserId == '" + uid + "'")[0][0];

                    try
                    {
                        DB.ReadData("UPDATE PayInfo SET Type = '" + typeBox.Text + "', CardID = '" + IDBox.Text + "', CardCVV = '" + CVVBox.Text + "', CardDATE = '" + dateBox.Text + "', Holder = '" + holderBox.Text + "' WHERE UserId == '" + uid + "';");
                        MessageBox.Show("Updated card info", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch {
                        MessageBox.Show("There was an error when processing your request, please try again");
                    }
                }
                catch
                {
                    try
                    {
                        DB.ReadData("INSERT INTO PayInfo(UserId,Type,CardID,CardCVV,CardDATE,Holder) VALUES ('" + uid + "','" + typeBox.Text + "','" + IDBox.Text + "','" + CVVBox.Text + "','" + dateBox.Text + "','" + holderBox.Text + "');");
                        MessageBox.Show("Card with ID " + IDBox.Text + " added!", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ReloadCards();
                    }
                    catch
                    {
                        MessageBox.Show("There was an error when processing your request, please try again");
                    }
                }
            }
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            // Load card values
            if (Cards.SelectedItem != null)
            {
                string[] selectedItem = Cards.SelectedItem.ToString().Split(' ');
                string selectedID = selectedItem[0];

                List<string[]> cardList = DB.ReadMultiData("SELECT Type, CardID, CardCVV, CardDATE, Holder FROM PayInfo WHERE (CardID == '" + selectedID + "' AND UserId == (SELECT UserId FROM Users WHERE Username == '" + username + "'));");
                typeBox.Text = cardList[0][0];
                IDBox.Text = cardList[0][1];
                CVVBox.Text = cardList[0][2];
                dateBox.Text = cardList[0][3];
                holderBox.Text = cardList[0][4];
            }
        }

        private void removeBtn_Click(object sender, EventArgs e)
        {
            // Load card values
            if (Cards.SelectedItem != null)
            {
                string[] selectedItem = Cards.SelectedItem.ToString().Split(' ');
                string selectedID = selectedItem[0];

                try
                {
                    DB.ReadData("DELETE FROM PayInfo WHERE CardID == '" + selectedID + "' AND UserId == (SELECT UserId FROM Users WHERE Username == '" + username + "')");
                    MessageBox.Show("Card with ID " + IDBox.Text + " removed!", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ReloadCards();
                }
                catch
                {
                    MessageBox.Show("There was an error when processing your request, please try again");
                }
            }
        }

        private void notificationBtn_Click(object sender, EventArgs e)
        {
            using (Messages frm2 = new Messages((Menu)prev, username))
            {
                Hide();
                frm2.ShowDialog();
            }
        }
    }
}
