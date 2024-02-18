using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace PachidisStation
{
    
    public partial class Billing : Form
    {
        SQLiteBackend DB = new SQLiteBackend();
        
        public int OrderID;
        private Timer timer;
        private string username;
        private bool[] serviceSel;
        Form prev;

        public Billing(Form prev, string username, int OrderID, bool[] serviceSel = null)
        {
            InitializeComponent();
            
            this.prev = prev;
            this.OrderID = OrderID;
            this.username = username;
            this.serviceSel = serviceSel;

            WelcomeLabel.Text = ("Welcome " + username);

            DateTime currentDateTime = DateTime.Now;
            dateLabel.Text = currentDateTime.ToString("dd MMMM yyyy | hh:mm tt", new CultureInfo("en-US"));

            // Initialize and start the timer
            timer = new Timer();
            timer.Interval = 1000; // Set the interval to 1000 milliseconds (1 second)
            timer.Tick += Timer_Tick;
            timer.Start();

            //UPDATE INFO
            List<string[]> cardList = DB.ReadMultiData("SELECT CardID FROM Payinfo WHERE UserID == (SELECT UserId FROM Users WHERE Username == '" + username + "');");
            cardList.ForEach(card => { cardBox.Items.Add(card[0]); });

            // GET COST
            float cost = float.Parse(DB.ReadMultiData("SELECT TotalPrice FROM UserOrders WHERE OrderID == '" + OrderID + "'")[0][0]);
            costLabel.Text = "Total cost: " + cost + "€";
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Update the time every second
            DateTime currentDateTime = DateTime.Now;
            dateLabel.Text = currentDateTime.ToString("dd MMMM yyyy | hh:mm tt", new CultureInfo("en-US"));
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

        private void completeBtn_Click(object sender, EventArgs e)
        {
            if (cardBox.SelectedItem != null) {
                try
                {
                    DB.ReadData("UPDATE UserOrders SET PaidSuccessfully = '1', CardID = '" + cardBox.Text + "' WHERE OrderID == '" + OrderID + "';");
                    MessageBox.Show("Payment Success!");

                    float cost = float.Parse(DB.ReadMultiData("SELECT TotalPrice FROM UserOrders WHERE OrderID == '" + OrderID + "'")[0][0]);
                    string uid = DB.ReadMultiData("SELECT UserId FROM Users WHERE Username == '" + username + "';")[0][0];
                    DB.ReadData("INSERT INTO Notifications(DestinationUser,Message,Date) VALUES('" + uid + "','Payment Success! (" + cost + "€)','" + DateTime.Now.ToString() + "');");

                    if (serviceSel == null)
                    {
                        string pid = DB.ReadMultiData("SELECT ProductID FROM UserOrders WHERE OrderID == '" + OrderID + "';")[0][0];
                        string selectedQuantity = DB.ReadMultiData("SELECT Quantity FROM UserOrders WHERE OrderID == '" + OrderID + "';")[0][0];
                        float availableQuant = float.Parse(DB.ReadMultiData("SELECT AvailableQuantity FROM Products WHERE ID == '" + pid + "';")[0][0].Replace(',', '.')) - float.Parse(selectedQuantity.Replace(',', '.'));
                        DB.ReadData("UPDATE Products SET AvailableQuantity = '" + availableQuant + "' WHERE ID == '" + pid + "'");
                    }
                    else
                    {
                        if (serviceSel[0])
                        {
                            string pid = DB.ReadMultiData("SELECT ID FROM Products WHERE Name == 'svTyres';")[0][0];
                            int availableQuant = int.Parse(DB.ReadMultiData("SELECT AvailableQuantity FROM Products WHERE ID == '" + pid + "';")[0][0].Replace(',', '.')) - 1;
                            DB.ReadData("UPDATE Products SET AvailableQuantity = '" + availableQuant + "' WHERE ID == '" + pid + "'");
                        }
                        if (serviceSel[1])
                        {
                            string pid = DB.ReadMultiData("SELECT ID FROM Products WHERE Name == 'svBattery';")[0][0];
                            int availableQuant = int.Parse(DB.ReadMultiData("SELECT AvailableQuantity FROM Products WHERE ID == '" + pid + "';")[0][0].Replace(',', '.')) - 1;
                            DB.ReadData("UPDATE Products SET AvailableQuantity = '" + availableQuant + "' WHERE ID == '" + pid + "'");
                        }
                        if (serviceSel[2])
                        {
                            string pid = DB.ReadMultiData("SELECT ID FROM Products WHERE Name == 'svKeylocks';")[0][0];
                            int availableQuant = int.Parse(DB.ReadMultiData("SELECT AvailableQuantity FROM Products WHERE ID == '" + pid + "';")[0][0].Replace(',', '.')) - 1;
                            DB.ReadData("UPDATE Products SET AvailableQuantity = '" + availableQuant + "' WHERE ID == '" + pid + "'");
                        }
                        if (serviceSel[3])
                        {
                            string pid = DB.ReadMultiData("SELECT ID FROM Products WHERE Name == 'svAC';")[0][0];
                            int availableQuant = int.Parse(DB.ReadMultiData("SELECT AvailableQuantity FROM Products WHERE ID == '" + pid + "';")[0][0].Replace(',', '.')) - 1;
                            DB.ReadData("UPDATE Products SET AvailableQuantity = '" + availableQuant + "' WHERE ID == '" + pid + "'");
                        }
                        if (serviceSel[4])
                        {
                            string pid = DB.ReadMultiData("SELECT ID FROM Products WHERE Name == 'svOil';")[0][0];
                            int availableQuant = int.Parse(DB.ReadMultiData("SELECT AvailableQuantity FROM Products WHERE ID == '" + pid + "';")[0][0].Replace(',', '.')) - 1;
                            DB.ReadData("UPDATE Products SET AvailableQuantity = '" + availableQuant + "' WHERE ID == '" + pid + "'");
                        }
                        if (serviceSel[5])
                        {
                            string pid = DB.ReadMultiData("SELECT ID FROM Products WHERE Name == 'svTint';")[0][0];
                            int availableQuant = int.Parse(DB.ReadMultiData("SELECT AvailableQuantity FROM Products WHERE ID == '" + pid + "';")[0][0].Replace(',', '.')) - 1;
                            DB.ReadData("UPDATE Products SET AvailableQuantity = '" + availableQuant + "' WHERE ID == '" + pid + "'");
                        }
                        if (serviceSel[6])
                        {
                            string pid = DB.ReadMultiData("SELECT ID FROM Products WHERE Name == 'svGPS';")[0][0];
                            int availableQuant = int.Parse(DB.ReadMultiData("SELECT AvailableQuantity FROM Products WHERE ID == '" + pid + "';")[0][0].Replace(',', '.')) - 1;
                            DB.ReadData("UPDATE Products SET AvailableQuantity = '" + availableQuant + "' WHERE ID == '" + pid + "'");
                        }
                    }

                    CloseForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something went wrong when processing your payment");
                    MessageBox.Show(ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Please Select a card or add a card in the Cards Menu!");
            }
        }

        private void myCardsBtn_Click(object sender, EventArgs e)
        {
            using (Payment frm2 = new Payment(this, username))
            {
                Hide();
                frm2.ShowDialog();

                // Update cards
                cardBox.Items.Clear();
                List<string[]> cardList = DB.ReadMultiData("SELECT CardID FROM Payinfo WHERE UserID == (SELECT UserId FROM Users WHERE Username == '" + username + "');");
                cardList.ForEach(card => { cardBox.Items.Add(card[0]); });
            }

        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            CloseForm();
        }
    }
}