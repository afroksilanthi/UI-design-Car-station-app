using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace PachidisStation
{
    public partial class Administration : Form
    {
        SQLiteBackend DB = new SQLiteBackend();
        private Menu menu;
        float maxCapacity = 10000;
        int itemsMaxCapacity = 100;
        private string username;

        public Administration(Menu menu, string username)
        {
            InitializeComponent();

            this.menu = menu;
            this.username = username;

            string perms = DB.ReadMultiData("SELECT perms FROM Users WHERE username = '" + username + "';")[0][0]; 

            if (perms == "worker")
            {
                UserGrid.Visible = false;
                orderGrid.Visible = false;
                
            } else
            {
                UserGrid.Visible = true;
                orderGrid.Visible=true;
            }


            // Users Tab
            ReloadUserGrid();

            // User Orders Tab
            reloadOrderGrid();

            // Products Tab
            reloadProductsTab();

            // Fuel Control Tab
            reloadFuelControlTab();
            AutoOrderBox.Text = DB.ReadMultiData("SELECT Value FROM Parameters WHERE Name == 'FuelOrderThreshold';")[0][0];

            // Parking Tab
            reloadParkingTab();

            //Feedback Tab
            reloadFeedbackGrid();
        }

        private void ReloadUserGrid()
        {
            UserGrid.Rows.Clear();
            List<string[]> userList = DB.ReadMultiData("SELECT userID, Username, Name, Surname, Address, Email, Password, Perms FROM Users;");

            short idx = 0;
            foreach (string[] user in userList)
            {
                // Hide current passwords for security
                //user[6] = string.Concat(Enumerable.Repeat('*', user[6].Length));

                // Add a new row to the DataGridView
                UserGrid.Rows.Add(user);
                UserGrid.Rows[idx].Cells[8].Value = "Update";
                UserGrid.Rows[idx].Cells[9].Value = "Delete";
                
                idx++;
            }

            // Insert new user row
            UserGrid.Rows.Add();
            UserGrid.Rows[idx].Cells[8].Value = "Add";
            UserGrid.Rows[idx].Cells[9].Value = "";

            UserGrid.AllowUserToAddRows = false;
        }

        private void reloadFeedbackGrid()
        {
            List<string[]> feedbackList = DB.ReadMultiData("SELECT * FROM Feedback;");

            foreach (string[] feedback in feedbackList)
            {
                // Add a new row to the DataGridView
                feedbackGrid.Rows.Add(feedback);
            }

            orderGrid.AllowUserToAddRows = false;
        }

        private void reloadOrderGrid()
        {
            List<string[]> orderList = DB.ReadMultiData("SELECT * FROM UserOrders;");

            foreach (string[] orders in orderList)
            {
                // Add a new row to the DataGridView
                orderGrid.Rows.Add(orders);
            }

            orderGrid.AllowUserToAddRows = false;
        }

        private void reloadProductsTab()
        {
            AutoOrderPBox.Text = DB.ReadMultiData("SELECT Value FROM Parameters WHERE Name == 'PartsOrderThreshold';")[0][0];

            // ProductGrid
            ProductGrid.Rows.Clear();
            List<string[]> productList = DB.ReadMultiData("SELECT Name, AvailableQuantity, Price FROM Products;");
            foreach (string[] product in productList)
            {
                if (product[0] == "svTyres" || product[0] == "svBattery" || product[0] == "svBattery" || product[0] == "svOil" || product[0] == "svGPS" || product[0] == "svTint" || product[0] == "svKeylocks" || product[0] == "svAC")
                {
                    // Add a new row to the DataGridView
                    ProductGrid.Rows.Add(product);
                }
            }

            // ProductOrdersGrid
            ProductOrdersGrid.Rows.Clear();
            List<string[]> productOrderList = DB.ReadMultiData("SELECT * FROM StationOrders;");
            foreach (string[] product in productOrderList)
            {
                if (product[1] == "svTyres" || product[1] == "svBattery" || product[1] == "svBattery" || product[1] == "svOil" || product[1] == "svGPS" || product[1] == "svTint" || product[1] == "svKeylocks" || product[1] == "svAC")
                {
                    // Add a new row to the DataGridView
                    ProductOrdersGrid.Rows.Add(product);
                }
            }
        }

        private void reloadFuelControlTab()
        {
            List<string[]> RON95 = DB.ReadMultiData("SELECT AvailableQuantity, Price FROM PRODUCTS WHERE Name == 'RON95';");
            u95AvailableLabel.Text = RON95[0][0] + 'L';
            u95PriceBox.Text = RON95[0][1];
            u95ProgressBar.Value = (int)((int.Parse(RON95[0][0]) / maxCapacity) * 100);

            List<string[]> RON100 = DB.ReadMultiData("SELECT AvailableQuantity, Price FROM PRODUCTS WHERE Name == 'RON100';");
            u100AvailableLabel.Text = RON100[0][0] + 'L';
            u100PriceBox.Text = RON100[0][1];
            u100ProgressBar.Value = (int)((int.Parse(RON100[0][0]) / maxCapacity) * 100);

            List<string[]> LPG = DB.ReadMultiData("SELECT AvailableQuantity, Price FROM PRODUCTS WHERE Name == 'LPG';");
            lpgAvailableLabel.Text = LPG[0][0] + 'L';
            lpgPriceBox.Text = LPG[0][1];
            lpgProgressBar.Value = (int)((int.Parse(LPG[0][0]) / maxCapacity) * 100);

            List<string[]> DIESEL = DB.ReadMultiData("SELECT AvailableQuantity, Price FROM PRODUCTS WHERE Name == 'DIESEL';");
            dieselAvailableLabel.Text = DIESEL[0][0] + 'L';
            dieselPriceBox.Text = DIESEL[0][1];
            dieselProgressBar.Value = (int)((int.Parse(DIESEL[0][0]) / maxCapacity) * 100);

            List<string[]> SDIESEL = DB.ReadMultiData("SELECT AvailableQuantity, Price FROM PRODUCTS WHERE Name == 'SDIESEL';");
            sDieselAvailableLabel.Text = SDIESEL[0][0] + 'L';
            sDieselPriceBox.Text = SDIESEL[0][1];
            sDieselProgressBar.Value = (int)((int.Parse(SDIESEL[0][0]) / maxCapacity) * 100);

            ordersGrid.Rows.Clear();
            List<string[]> orderList = DB.ReadMultiData("SELECT * FROM StationOrders;");
            foreach (string[] orders in orderList)
            {
                if (orders[1] == "LPG" || orders[1] == "RON95" || orders[1] == "RON100" || orders[1] == "DIESEL" || orders[1] == "SDIESEL")
                {
                    // Add a new row to the DataGridView
                    ordersGrid.Rows.Add(orders);
                }
            }
        }

        private void UserGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle user DELETE
            if ((e.ColumnIndex == UserGrid.Columns["Delete"].Index) && e.RowIndex >= 0 && e.RowIndex != (UserGrid.Rows.Count - 1))
            {
                string uid = (string)UserGrid.Rows[e.RowIndex].Cells[0].Value;
                DB.ReadMultiData("DELETE FROM Users WHERE UserID== '"+ uid + "'");
                ReloadUserGrid();
                MessageBox.Show("Deleted: " + uid);
            }

            // Handle user UPDATE
            if ((e.ColumnIndex == UserGrid.Columns["Update"].Index) && e.RowIndex >= 0 && e.RowIndex != (UserGrid.Rows.Count - 1))
            {
                string uid = (string)UserGrid.Rows[e.RowIndex].Cells[0].Value;
                string uname = (string)UserGrid.Rows[e.RowIndex].Cells[1].Value; 
                string fname = (string)UserGrid.Rows[e.RowIndex].Cells[2].Value; 
                string sname = (string)UserGrid.Rows[e.RowIndex].Cells[3].Value; 
                string addr = (string)UserGrid.Rows[e.RowIndex].Cells[4].Value; 
                string email = (string)UserGrid.Rows[e.RowIndex].Cells[5].Value;
                string pwd = (string)UserGrid.Rows[e.RowIndex].Cells[6].Value;
                string perms = (string)UserGrid.Rows[e.RowIndex].Cells[7].Value;

                if (uname != null && fname != null && sname != null && addr != null && email != null && pwd != null && perms != null)
                {
                    try
                    {
                        string test_username = DB.ReadMultiData("SELECT Username FROM Users where Username == '" + uname + "' AND UserID != " + uid + ";")[0][0];
                        string test_email = DB.ReadMultiData("SELECT Email FROM Users where Email == '" + email + "' AND UserID != " + uid + ";")[0][0];
                        MessageBox.Show("Username or Email already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch
                    {
                        try
                        {
                            DB.ReadData("UPDATE Users SET Username= '" + uname + "', Name = '" + fname + "', Surname = '" + sname + "', Address = '" + addr + "', Email = '" + email + "', Password = '" + pwd + "', perms = '" + perms + "' WHERE UserID == '" + uid + "';");
                            MessageBox.Show("User " + uname + " Updated! ", "MyAccount", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ReloadUserGrid();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("There was an error when processing your request, please try again");
                            MessageBox.Show(ex.ToString());
                        }
                    } 

                } else { MessageBox.Show("Fields cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                MessageBox.Show("Update: " + uid);
            }

            // Handle user ADD
            if ((e.ColumnIndex == UserGrid.Columns["Update"].Index) && e.RowIndex == (UserGrid.Rows.Count - 1))
            {
                int new_uid = 0;
                try
                {
                    string uid = (string)UserGrid.Rows[e.RowIndex].Cells[0].Value;
                    string uname = (string)UserGrid.Rows[e.RowIndex].Cells[1].Value;
                    string fname = (string)UserGrid.Rows[e.RowIndex].Cells[2].Value;
                    string sname = (string)UserGrid.Rows[e.RowIndex].Cells[3].Value;
                    string addr = (string)UserGrid.Rows[e.RowIndex].Cells[4].Value;
                    string email = (string)UserGrid.Rows[e.RowIndex].Cells[5].Value;
                    string pwd = (string)UserGrid.Rows[e.RowIndex].Cells[6].Value;
                    string perms = (string)UserGrid.Rows[e.RowIndex].Cells[7].Value;

                    // Attempt to get a UID based on the previous register
                    // **doesnt work when converting directly to int**
                    new_uid = int.Parse((string)UserGrid.Rows[e.RowIndex - 1].Cells[0].Value) + 1;

                    if (uname != null && fname != null && sname != null && addr != null && email != null && pwd != null && perms != null)
                        {
                        if (!(email == DB.ReadData("SELECT Username FROM Users where Username == '" + uname + "'") || email == DB.ReadData("SELECT Email FROM Users where Email == '" + email + "'")))
                        {
                            try
                            {
                                DB.ReadData("INSERT INTO Users(Username,name,Password,Surname,Email,Address,Perms) VALUES ('" + uname + "','" + fname + "','" + pwd + "','" + sname + "','" + email + "','" + addr + "','"+ perms + "');");
                                MessageBox.Show("User " + uname + " Created!", "Administration", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                            catch
                            {
                                MessageBox.Show("There was an error when processing your request, please try again");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Username or Email already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch
                {
                    // If no previous registers are found (Not possible since admin always exists)
                    new_uid = 0;
                }

                MessageBox.Show("Add: " + new_uid.ToString());
            }
        }

        private void ProductGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Handle order items
            if (e.ColumnIndex == ProductGrid.Columns["OrderItems"].Index)
            {
                List<string[]> item = DB.ReadMultiData("SELECT ID, Price FROM Products WHERE Name == '" + (string)ProductGrid.Rows[e.RowIndex].Cells[0].Value + "';");

                int reqQuantity = itemsMaxCapacity - int.Parse((string)ProductGrid.Rows[e.RowIndex].Cells[1].Value);
                float price = float.Parse(item[0][1].Replace('.', ',')) * 0.8f;
                float totalCost = reqQuantity * price;

                string pname = ProductGrid.Rows[e.RowIndex].Cells[0].Value.ToString();

                DB.ReadData("INSERT INTO StationOrders(FuelType, FuelQuantity, FuelPrice, TotalCost, OrderDate) VALUES('" + pname + "', '" + reqQuantity.ToString().Replace(',', '.') + "', '" + price.ToString().Replace(',', '.') + "', '" + totalCost.ToString().Replace(',', '.') + "', '" + DateTime.Now + "');");

                // Update available quantity
                DB.ReadData("UPDATE Products SET AvailableQuantity = '" + itemsMaxCapacity.ToString() + "' WHERE Name == '" + pname + "';");

                // Send notification
                List<string[]> uids = DB.ReadMultiData("SELECT UserId FROM Users WHERE (Perms == 'admin' OR Perms == 'worker');");
                foreach (string[] uid in uids)
                {
                    DB.ReadData("INSERT INTO Notifications(DestinationUser,Message,Date) VALUES('" + uid[0] + "','Item Order Success! (" + pname + ", " + totalCost + "€)','" + DateTime.Now.ToString() + "');");
                }

                MessageBox.Show("Ordered items successfully!");
                reloadProductsTab();
            }
            else if (e.ColumnIndex == ProductGrid.Columns["UpdateIt"].Index)
            {
                string val = (string)ProductGrid.Rows[e.RowIndex].Cells[2].Value;
                string pname = (string)ProductGrid.Rows[e.RowIndex].Cells[0].Value;
                DB.ReadData("UPDATE Products SET Price = '" + val + "' WHERE Name == '" + pname + "';");
                MessageBox.Show("Updated values");

                // Send notification
                List<string[]> uids = DB.ReadMultiData("SELECT UserId FROM Users WHERE (Perms == 'admin' OR Perms == 'worker');");
                foreach (string[] uid in uids)
                {
                    DB.ReadData("INSERT INTO Notifications(DestinationUser,Message,Date) VALUES('" + uid[0] + "','Updated Item Price! (" + pname + ", " + val + "€)','" + DateTime.Now.ToString() + "');");
                }
            }
        }

        private void CloseForm()
        {
            // Close DB
            DB.CloseConnection();
            DB = null;

            // Show menu
            menu.Show();

            // Close window
            Dispose();
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void reloadParkingTab()
        {
            ph24Box.Text = DB.ReadMultiData("SELECT Value FROM Parameters WHERE Name == 'Parking1D';")[0][0];
            ph48Box.Text = DB.ReadMultiData("SELECT Value FROM Parameters WHERE Name == 'Parking2D';")[0][0];
            ph48pBox.Text = DB.ReadMultiData("SELECT Value FROM Parameters WHERE Name == 'Parking3Dp';")[0][0];

            // Load visualization
            List<string[]> isEmpty = DB.ReadMultiData("SELECT isEmpty FROM Spots ORDER BY SpotID;");

            if (isEmpty == null)
            {
                return;
            }

            for (int i = 1; i <= 25; i++)
            {
                string buttonName = "P" + i;

                System.Windows.Forms.Button button = Controls.Find(buttonName, true).FirstOrDefault() as System.Windows.Forms.Button;

                if (button != null)
                {
                    if (i <= isEmpty.Count && isEmpty[i - 1].Length > 0 && isEmpty[i - 1][0] == "1")
                    {
                        button.BackColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        button.BackColor = System.Drawing.Color.Red;
                    }
                }
            }
        }

        private void AutoOrderBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (AutoOrderBox.Text != " ")
                {
                    try
                    {
                        DB.ReadData("UPDATE Parameters SET Value = '" + AutoOrderBox.Text + "' WHERE Name == 'FuelOrderThreshold';");
                        MessageBox.Show("Value updated!");
                        label.Focus();
                    }
                    catch
                    {
                        MessageBox.Show("There was an error updating the value");
                    }
                }
            }
        }

        private void U95PricePerLit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (u95PriceBox.Text != " ")
                {
                    try
                    {
                        DB.ReadData("UPDATE Products SET Price = '" + u95PriceBox.Text.Replace(',', '.') + "' WHERE Name == 'RON95';");
                        MessageBox.Show("Value updated!");
                        label.Focus();
                    }
                    catch
                    {
                        MessageBox.Show("There was an error updating the value");
                    }
                }
            }
        }

        private void U100PricePerLit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (u100PriceBox.Text != " ")
                {
                    try
                    {
                        DB.ReadData("UPDATE Products SET Price = '" + u100PriceBox.Text.Replace(',', '.') + "' WHERE Name == 'RON100';");
                        MessageBox.Show("Value updated!");
                        label.Focus();
                    }
                    catch
                    {
                        MessageBox.Show("There was an error updating the value");
                    }
                }
            }
        }

        private void DieselPricePerLit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (dieselPriceBox.Text != " ")
                {
                    try
                    {
                        DB.ReadData("UPDATE Products SET Price = '" + dieselPriceBox.Text.Replace(',', '.') + "' WHERE Name == 'DIESEL';");
                        MessageBox.Show("Value updated!");
                        label.Focus();
                    }
                    catch
                    {
                        MessageBox.Show("There was an error updating the value");
                    }
                }
            }
        }

        private void SDieselPricePerLit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (sDieselPriceBox.Text != " ")
                {
                    try
                    {
                        DB.ReadData("UPDATE Products SET Price = '" + sDieselPriceBox.Text.Replace(',', '.') + "' WHERE Name == 'SDIESEL';");
                        MessageBox.Show("Value updated!");
                        label.Focus();
                    }
                    catch
                    {
                        MessageBox.Show("There was an error updating the value");
                    }
                }
            }
        }

        private void LPGPricePerLit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (lpgPriceBox.Text != " ")
                {
                    try
                    {
                        DB.ReadData("UPDATE Products SET Price = '" + lpgPriceBox.Text.Replace(',', '.') + "' WHERE Name == 'LPG';");
                        MessageBox.Show("Value updated!");
                        label.Focus();
                    }
                    catch
                    {
                        MessageBox.Show("There was an error updating the value");
                    }
                }
            }
        }

        private void AutoOrderBox_Leave(object sender, EventArgs e)
        {
            AutoOrderBox.Text = DB.ReadMultiData("SELECT Value FROM Parameters WHERE Name == 'FuelOrderThreshold';")[0][0];
        }

        private void PricePerLit_Leave(object sender, EventArgs e)
        {
            reloadFuelControlTab();
        }

        private void PriceHour_Leave(object sender, EventArgs e)
        {
            reloadParkingTab();
        }

        private void u95OrderBtn_Click(object sender, EventArgs e)
        {
            // Insert order
            List<string[]> RON95 = DB.ReadMultiData("SELECT AvailableQuantity, Price FROM Products WHERE Name == 'RON95';");
            float reqQuantity = maxCapacity - float.Parse(RON95[0][0]);
            float price = float.Parse(RON95[0][1]) * 0.8f;
            float totalCost = reqQuantity * price;

            if (reqQuantity != 0)
            {
                DB.ReadData("INSERT INTO StationOrders(FuelType, FuelQuantity, FuelPrice, TotalCost, OrderDate) VALUES('RON95', '" + reqQuantity.ToString().Replace(',', '.') + "', '" + price.ToString().Replace(',', '.') + "', '" + totalCost.ToString().Replace(',', '.') + "', '" + DateTime.Now + "');");
                MessageBox.Show("The order was successfull!");

                // Update available quantity
                DB.ReadData("UPDATE Products SET AvailableQuantity = '" + maxCapacity.ToString() + "' WHERE Name == 'RON95';");

                // Send notification
                string uid = DB.ReadMultiData("SELECT UserId FROM Users WHERE Username == '" + username + "';")[0][0];
                DB.ReadData("INSERT INTO Notifications(DestinationUser,Message,Date) VALUES('" + uid + "','Station Fuel Order Success! (" + reqQuantity + "L, " + totalCost + "€)','" + DateTime.Now.ToString() + "');");
            }
            else
            {
                MessageBox.Show("Tank already full!");
            }

            reloadFuelControlTab();
        }

        private void u100OrderBtn_Click(object sender, EventArgs e)
        {
            // Insert order
            List<string[]> RON100 = DB.ReadMultiData("SELECT AvailableQuantity, Price FROM Products WHERE Name == 'RON100';");
            float reqQuantity = maxCapacity - float.Parse(RON100[0][0]);
            float price = float.Parse(RON100[0][1]) * 0.8f;
            float totalCost = reqQuantity * price;

            if (reqQuantity != 0)
            {
                DB.ReadData("INSERT INTO StationOrders(FuelType, FuelQuantity, FuelPrice, TotalCost, OrderDate) VALUES('RON100', '" + reqQuantity.ToString().Replace(',', '.') + "', '" + price.ToString().Replace(',', '.') + "', '" + totalCost.ToString().Replace(',', '.') + "', '" + DateTime.Now + "');");
                MessageBox.Show("The order was successfull!");

                // Update available quantity
                DB.ReadData("UPDATE Products SET AvailableQuantity = '" + maxCapacity.ToString() + "' WHERE Name == 'RON100';");

                // Send notification
                string uid = DB.ReadMultiData("SELECT UserId FROM Users WHERE Username == '" + username + "';")[0][0];
                DB.ReadData("INSERT INTO Notifications(DestinationUser,Message,Date) VALUES('" + uid + "','Station Fuel Order Success! (" + reqQuantity + "L, " + totalCost + "€)','" + DateTime.Now.ToString() + "');");
            }
            else
            {
                MessageBox.Show("Tank already full!");
            }

            reloadFuelControlTab();
        }

        private void DieselOrderBtn_Click(object sender, EventArgs e)
        {
            // Insert order
            List<string[]> DIESEL = DB.ReadMultiData("SELECT AvailableQuantity, Price FROM Products WHERE Name == 'DIESEL';");
            float reqQuantity = maxCapacity - float.Parse(DIESEL[0][0]);
            float price = float.Parse(DIESEL[0][1]) * 0.8f;
            float totalCost = reqQuantity * price;

            if (reqQuantity != 0)
            {
                DB.ReadData("INSERT INTO StationOrders(FuelType, FuelQuantity, FuelPrice, TotalCost, OrderDate) VALUES('DIESEL', '" + reqQuantity.ToString().Replace(',', '.') + "', '" + price.ToString().Replace(',', '.') + "', '" + totalCost.ToString().Replace(',', '.') + "', '" + DateTime.Now + "');");
                MessageBox.Show("The order was successfull!");

                // Update available quantity
                DB.ReadData("UPDATE Products SET AvailableQuantity = '" + maxCapacity.ToString() + "' WHERE Name == 'DIESEL';");

                // Send notification
                string uid = DB.ReadMultiData("SELECT UserId FROM Users WHERE Username == '" + username + "';")[0][0];
                DB.ReadData("INSERT INTO Notifications(DestinationUser,Message,Date) VALUES('" + uid + "','Station Fuel Order Success! (" + reqQuantity + "L, " + totalCost + "€)','" + DateTime.Now.ToString() + "');");
            }
            else
            {
                MessageBox.Show("Tank already full!");
            }

            reloadFuelControlTab();
        }

        private void SDieselOrderBtn_Click(object sender, EventArgs e)
        {
            // Insert order
            List<string[]> SDIESEL = DB.ReadMultiData("SELECT AvailableQuantity, Price FROM Products WHERE Name == 'SDIESEL';");
            float reqQuantity = maxCapacity - float.Parse(SDIESEL[0][0]);
            float price = float.Parse(SDIESEL[0][1]) * 0.8f;
            float totalCost = reqQuantity * price;

            if (reqQuantity != 0)
            {
                DB.ReadData("INSERT INTO StationOrders(FuelType, FuelQuantity, FuelPrice, TotalCost, OrderDate) VALUES('SDIESEL', '" + reqQuantity.ToString().Replace(',', '.') + "', '" + price.ToString().Replace(',', '.') + "', '" + totalCost.ToString().Replace(',', '.') + "', '" + DateTime.Now + "');");
                MessageBox.Show("The order was successfull!");

                // Update available quantity
                DB.ReadData("UPDATE Products SET AvailableQuantity = '" + maxCapacity.ToString() + "' WHERE Name == 'SDIESEL';");

                // Send notification
                string uid = DB.ReadMultiData("SELECT UserId FROM Users WHERE Username == '" + username + "';")[0][0];
                DB.ReadData("INSERT INTO Notifications(DestinationUser,Message,Date) VALUES('" + uid + "','Station Fuel Order Success! (" + reqQuantity + "L, " + totalCost + "€)','" + DateTime.Now.ToString() + "');");
            }
            else
            {
                MessageBox.Show("Tank already full!");
            }

            reloadFuelControlTab();
        }

        private void LPGOrderBtn_Click(object sender, EventArgs e)
        {
            // Insert order
            List<string[]> LPG = DB.ReadMultiData("SELECT AvailableQuantity, Price FROM Products WHERE Name == 'LPG';");
            float reqQuantity = maxCapacity - float.Parse(LPG[0][0]);
            float price = float.Parse(LPG[0][1]) * 0.8f;
            float totalCost = reqQuantity * price;

            if (reqQuantity != 0)
            {
                DB.ReadData("INSERT INTO StationOrders(FuelType, FuelQuantity, FuelPrice, TotalCost, OrderDate) VALUES('LPG', '" + reqQuantity.ToString().Replace(',', '.') + "', '" + price.ToString().Replace(',', '.') + "', '" + totalCost.ToString().Replace(',', '.') + "', '" + DateTime.Now + "');");
                MessageBox.Show("The order was successfull!");

                // Update available quantity
                DB.ReadData("UPDATE Products SET AvailableQuantity = '" + maxCapacity.ToString() + "' WHERE Name == 'LPG';");

                // Send notification
                string uid = DB.ReadMultiData("SELECT UserId FROM Users WHERE Username == '" + username + "';")[0][0];
                DB.ReadData("INSERT INTO Notifications(DestinationUser,Message,Date) VALUES('" + uid + "','Station Fuel Order Success! (" + reqQuantity + "L, " + totalCost + "€)','" + DateTime.Now.ToString() + "');");
            }
            else
            {
                MessageBox.Show("Tank already full!");
            }

            reloadFuelControlTab();
        }

        private void PriceHour24_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (ph24Box.Text != " ")
                {
                    try
                    {
                        DB.ReadData("UPDATE Parameters SET Value = '" + ph24Box.Text.Replace(',', '.') + "' WHERE Name == 'Parking1D';");
                        MessageBox.Show("Value updated!");
                        label30.Focus();
                    }
                    catch
                    {
                        MessageBox.Show("There was an error updating the value");
                    }
                }
            }
        }

        private void PriceHour48_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (ph24Box.Text != " ")
                {
                    try
                    {
                        DB.ReadData("UPDATE Parameters SET Value = '" + ph48Box.Text.Replace(',', '.') + "' WHERE Name == 'Parking2D';");
                        MessageBox.Show("Value updated!");
                        label30.Focus();
                    }
                    catch
                    {
                        MessageBox.Show("There was an error updating the value");
                    }
                }
            }
        }

        private void PriceHour48p_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (ph24Box.Text != " ")
                {
                    try
                    {
                        DB.ReadData("UPDATE Parameters SET Value = '" + ph48pBox.Text.Replace(',', '.') + "' WHERE Name == 'Parking3Dp';");
                        MessageBox.Show("Value updated!");
                        label30.Focus();
                    }
                    catch
                    {
                        MessageBox.Show("There was an error updating the value");
                    }
                }
            }
        }

        private void AutoOrderPBox_Leave(object sender, EventArgs e)
        {
            AutoOrderBox.Text = DB.ReadMultiData("SELECT Value FROM Parameters WHERE Name == 'PartsOrderThreshold';")[0][0];
        }
    }
}
