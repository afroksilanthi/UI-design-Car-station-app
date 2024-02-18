using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PachidisStation
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CopyDatabaseFromResources();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm(""));
        }
        private static void CopyDatabaseFromResources()
        {
            // Specify the local path where the database file will be copied
            string localDbPath = Path.Combine(Environment.CurrentDirectory, "ParCar.db");

            // Check if the database file doesn't exist in the local path
            if (!File.Exists(localDbPath))
            {
                // Copy the database file from resources to the local path
                File.WriteAllBytes(localDbPath, Properties.Resources.ParCar1);
                
             
            }
        }
    }
}
