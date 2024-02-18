using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace PachidisStation
{
    internal class SQLiteBackend
    {
        SQLiteConnection sqlite_conn;
        bool hasLoaded;
        bool isBusy;

        private void CreateConnection()
        {
            // Create a new database connection
            sqlite_conn = new SQLiteConnection("Data Source=ParCar.db; Version = 3; New = True; Compress = True; ");

            // Open the connection
            try
            {
                sqlite_conn.Open();
                Console.Write("Opened DB");
                hasLoaded = true;
            }
            catch (Exception ex)
            {
                Console.Write("DB Failure: " + ex);
                hasLoaded = false;
            }
        }

        public string ReadData(string query)
        {
            if (!hasLoaded)
            {
                return null;
            }

            isBusy = true;

            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = query;

            SQLiteDataReader sqlite_datareader;
            sqlite_datareader = sqlite_cmd.ExecuteReader();

            while (sqlite_datareader.Read())
            {
                isBusy = false;
                return sqlite_datareader.GetValue(0).ToString();
            }

            isBusy = false;
            return null;
        }

        public List<string[]> ReadMultiData(string query)
        {
            if (!hasLoaded)
            {
                return null;
            }

            isBusy = true;

            SQLiteCommand sqlite_cmd;
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = query;

            SQLiteDataReader sqlite_datareader;
            sqlite_datareader = sqlite_cmd.ExecuteReader();

            List<string[]> values = new List<string[]>();
            while (sqlite_datareader.Read())
            {
                // Parsing manually is necessary for string conversion
                string[] items = new string[sqlite_datareader.FieldCount];
                for (int i = 0; i < sqlite_datareader.FieldCount; i++)
                {
                    items[i] = sqlite_datareader.GetValue(i).ToString();
                }
                values.Add(items);
            }

            isBusy = false;
            return values;
        }

        public void SpotCheck()
        {
            if (!isBusy)
            {
                List<string[]> Parked = ReadMultiData("SELECT * FROM Parking");
                foreach (string[] item in Parked)
                {
                    DateTime start = DateTime.Parse(item[3]);
                    DateTime end = DateTime.Parse(item[4]);

                    DateTime now = DateTime.Now;

                    if (now.Ticks >= start.Ticks && now.Ticks <= end.Ticks)
                    {
                        //ReadData("UPDATE Parking SET isParked = 1 WHERE ID == '" + item[0] + "';");
                        ReadData("UPDATE Spots SET isEmpty = 0 WHERE SpotID == '" + item[1] + "';");
                    }
                    else
                    {
                        //ReadData("UPDATE Parking SET isParked = 0 WHERE ID == '" + item[0] + "';");
                        ReadData("UPDATE Spots SET isEmpty = 1 WHERE SpotID == '" + item[1] + "';");
                    }
                }
            }
        }

        public void AvailabilityCheck()
        {
            if (!isBusy)
            {
                float fuelMaxCapacity = 10000;
                int itemsMaxCapacity = 100;

                List<string[]> Available = ReadMultiData("SELECT AvailableQuantity, Price FROM Products");
                float fuelThreshold = float.Parse(ReadMultiData("SELECT Value FROM Parameters WHERE Name == 'FuelOrderThreshold'")[0][0].Replace('.', ','));
                float itemThreshold = float.Parse(ReadMultiData("SELECT Value FROM Parameters WHERE Name == 'PartsOrderThreshold'")[0][0]);

                for (int i = 0; i < 12; i++)
                {
                    // Fuel
                    if (i <= 4)
                    {
                        if (float.Parse(Available[i][0].Replace('.', ',')) <= fuelThreshold)
                        {
                            // Insert order
                            float reqQuantity = fuelMaxCapacity - float.Parse(Available[i][0].Replace('.', ','));
                            float price = float.Parse(Available[i][1].Replace('.', ',')) * 0.8f;
                            float totalCost = reqQuantity * price;

                            string pname = ReadMultiData("SELECT Name FROM Products WHERE ID == '" + i + "';")[0][0];

                            ReadData("INSERT INTO StationOrders(FuelType, FuelQuantity, FuelPrice, TotalCost, OrderDate) VALUES('" + pname + "', '" + reqQuantity.ToString().Replace(',', '.') + "', '" + price.ToString().Replace(',', '.') + "', '" + totalCost.ToString().Replace(',', '.') + "', '" + DateTime.Now + "');");

                            // Update available quantity
                            ReadData("UPDATE Products SET AvailableQuantity = '" + fuelMaxCapacity.ToString() + "' WHERE Name == '" + pname + "';");

                            // Send notification
                            List<string[]> uids = ReadMultiData("SELECT UserId FROM Users WHERE (Perms == 'admin' OR Perms == 'worker');");
                            foreach (string[] uid in uids)
                            {
                                ReadData("INSERT INTO Notifications(DestinationUser,Message,Date) VALUES('" + uid[0] + "','Auto Fuel Order Success! (" + pname + ", " + totalCost + "€)','" + DateTime.Now.ToString() + "');");
                            }
                        }
                    }

                    // Service Parts
                    else if (i > 4 && i <= 11)
                    {
                        if (float.Parse(Available[i][0].Replace('.', ',')) <= itemThreshold)
                        {
                            // Insert order
                            int reqQuantity = itemsMaxCapacity - int.Parse(Available[i][0].Replace('.', ','));
                            float price = float.Parse(Available[i][1].Replace('.', ',')) * 0.8f;
                            float totalCost = reqQuantity * price;

                            string pname = ReadMultiData("SELECT Name FROM Products WHERE ID == '" + i + "';")[0][0];

                            ReadData("INSERT INTO StationOrders(FuelType, FuelQuantity, FuelPrice, TotalCost, OrderDate) VALUES('" + pname + "', '" + reqQuantity.ToString().Replace(',', '.') + "', '" + price.ToString().Replace(',', '.') + "', '" + totalCost.ToString().Replace(',', '.') + "', '" + DateTime.Now + "');");

                            // Update available quantity
                            ReadData("UPDATE Products SET AvailableQuantity = '" + itemsMaxCapacity.ToString() + "' WHERE Name == '" + pname + "';");

                            // Send notification
                            List<string[]> uids = ReadMultiData("SELECT UserId FROM Users WHERE (Perms == 'admin' OR Perms == 'worker');");
                            foreach (string[] uid in uids)
                            {
                                ReadData("INSERT INTO Notifications(DestinationUser,Message,Date) VALUES('" + uid[0] + "','Auto Item Order Success! (" + pname + ", " + totalCost + "€)','" + DateTime.Now.ToString() + "');");
                            }
                        }
                    }
                }
            }
        }

        public void CloseConnection()
        {
            sqlite_conn.Close();
        }

        public SQLiteBackend()
        {
            CreateConnection();
        }
    }
}
