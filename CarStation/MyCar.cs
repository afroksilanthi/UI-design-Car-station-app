using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace PachidisStation
{
    public partial class MyCar : Form
    {
        SQLiteBackend DB = new SQLiteBackend();

        private Timer timer;
        private string username;
        private Menu menu;

        public MyCar(Menu menu, string username)
        {
            InitializeComponent();

            this.menu = menu;
            this.username = username;

            WelcomeLabel.Text = ("Welcome " + username);

            DateTime currentDateTime = DateTime.Now;
            dateLabel.Text = currentDateTime.ToString("dd MMMM yyyy | hh:mm tt", new CultureInfo("en-US"));

            // Fetch Cars
            ReloadVehicles();

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
            menu.Show();

            // Close window
            Dispose();
        }

        private void ReloadVehicles()
        {
            Vehicles.Items.Clear();

            List<string[]> carList = DB.ReadMultiData("SELECT Brand, Model, PlateID FROM Cars WHERE Owner == (SELECT UserId FROM Users WHERE Username == '" + username + "');");
            carList.ForEach(model => { Vehicles.Items.Add(model[0] + ' ' + model[1] + " " + model[2]); });
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
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            string uid = DB.ReadMultiData("SELECT UserId FROM Users where Username == '" + username + "'")[0][0];

            if (typeBox.Text == "" || manufBox.Text == "" || modelBox.Text == "" || plateBox.Text == "" || yearBox.Text == "" || capBox.Text == "")
            {
                MessageBox.Show("Fields cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                // Test availabilty, if the first try passes then the PLATE is updated
                try
                {
                    string test_plate = DB.ReadMultiData("SELECT PlateID FROM Cars WHERE (PlateID == '" + plateBox.Text + "');")[0][0];

                    try
                    {
                        string test_ownership = DB.ReadMultiData("SELECT PlateID FROM Cars WHERE (Owner == '" + uid + "' AND PlateID == '" + plateBox.Text + "');")[0][0];
                        DB.ReadData("UPDATE Cars SET Type = '" + typeBox.Text + "', Brand = '" + manufBox.Text + "', Model = '" + modelBox.Text + "', Year = '" + yearBox.Text + "', Capacity = '" + capBox.Text + "' WHERE PlateID == '" + plateBox.Text + "';");
                        MessageBox.Show("Updated car info", "MyCar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ReloadVehicles();
                    }
                    catch
                    {
                        MessageBox.Show("Plate is already registered by another user", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch
                {
                    try
                    {
                        DB.ReadData("INSERT INTO Cars(PlateID,Owner,Type,Brand,Model,Year,Capacity) VALUES ('" + plateBox.Text + "','" + uid + "','" + typeBox.Text + "','" + manufBox.Text + "','" + modelBox.Text + "','" + yearBox.Text + "','" + capBox.Text + "');");
                        MessageBox.Show("Vehicle with PlateID " + plateBox.Text + " added!", "MyCar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ReloadVehicles();
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
            // Load vehicle values
            string[] selectedItem = Vehicles.SelectedItem.ToString().Split(' ');
            string selectedPlate = selectedItem[selectedItem.Length - 1];

            List<string[]> carList = DB.ReadMultiData("SELECT * FROM Cars WHERE (PlateID == '" + selectedPlate + "');");
            plateBox.Text = carList[0][0];
            typeBox.Text = carList[0][2];
            manufBox.Text = carList[0][3];
            modelBox.Text = carList[0][4];
            yearBox.Text = carList[0][5];
            capBox.Text = carList[0][6];
        }

        private void removeBtn_Click(object sender, EventArgs e)
        {
            // Load vehicle values
            string[] selectedItem = Vehicles.SelectedItem.ToString().Split(' ');
            string selectedPlate = selectedItem[selectedItem.Length - 1];

            try
            {
                DB.ReadData("DELETE FROM Cars WHERE PlateID == '" + selectedPlate + "'");
                MessageBox.Show("Vehicle with PlateID " + plateBox.Text + " removed!", "MyCar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReloadVehicles();
            }
            catch
            {
                MessageBox.Show("There was an error when processing your request, please try again");
            }
        }
    }
}
