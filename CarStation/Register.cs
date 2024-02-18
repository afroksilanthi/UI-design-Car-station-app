using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PachidisStation
{
    public partial class Register : Form
    {
        SQLiteBackend DB = new SQLiteBackend();
        LoginForm login;

        public Register(LoginForm login)
        {
            InitializeComponent();
            ApplyRoundedCorners(panel1, 20);

            this.login = login;

            // Enable KeyPreview to handle key events at the form level
            this.KeyPreview = true;
            // Attach the event handler for the KeyDown event
            this.KeyDown += new KeyEventHandler(Form_KeyDown);
        }

        private void CloseForm()
        {
            // Close DB
            DB.CloseConnection();
            DB = null;

            // Show menu
            login.Show();

            // Close window
            Dispose();
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the pressed key is Enter
            if (e.KeyCode == Keys.Enter)
            {
                // Trigger the login button click event
                RegisterBtn_Click(this, new EventArgs());
            }
        }

        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            if (PasswordBox.Text == "" || UsernameBox.Text == "" || nameBox.Text == "" || SurnameBox.Text == "" || EmailBox.Text == "" || AddressBox.Text == "")
            {
                MessageBox.Show("Fields cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (!(UsernameBox.Text == DB.ReadData("SELECT Username FROM Users where Username == '" + UsernameBox.Text + "'") || EmailBox.Text == DB.ReadData("SELECT Email FROM Users where Email == '" + EmailBox.Text + "'")))
                {
                    try
                    {
                        DB.ReadData("INSERT INTO Users(Username,name,Password,Surname,Email,Address,Perms) VALUES ('" + UsernameBox.Text + "','" + nameBox.Text + "','" + PasswordBox.Text + "','" + SurnameBox.Text + "','" + EmailBox.Text + "','" + AddressBox.Text + "','user');");
                        
                        // Create Notification when user registers
                        string uid = DB.ReadMultiData("SELECT UserId FROM Users WHERE Username == '" + UsernameBox.Text + "';")[0][0];
                        DB.ReadData("INSERT INTO Notifications(DestinationUser,Message,Date) VALUES('" + uid + "','Thank you for registering " + UsernameBox.Text + ". Welcome To ParCar!','" + DateTime.Now.ToString() + "');");

                        MessageBox.Show("User " + UsernameBox.Text + " Created! Please login.", "Register", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Empty boxes
                        PasswordBox.Clear();
                        UsernameBox.Clear();
                        nameBox.Clear();
                        SurnameBox.Clear();
                        EmailBox.Clear();
                        AddressBox.Clear();

                        CloseForm();
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

        private void loginLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CloseForm();
        }
    }
}

