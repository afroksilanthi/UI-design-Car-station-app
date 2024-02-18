using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Forms;

namespace PachidisStation
{
    
    public partial class LoginForm : Form
    {
        private Timer timer;
        SQLiteBackend DB = new SQLiteBackend();

        public LoginForm(string text)
        {
            InitializeComponent();
            ApplyRoundedCorners(panel1, 20);

            // Enable KeyPreview to handle key events at the form level
            this.KeyPreview = true;
            // Attach the event handler for the KeyDown event
            this.KeyDown += new KeyEventHandler(Form_KeyDown);

            // Initialize and start the auto order timer
            timer = new Timer();
            timer.Interval = 10000; // Set the interval to 1000 milliseconds (1 second)
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DB.AvailabilityCheck();
            DB.SpotCheck();
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            // Check if the pressed key is Enter
            if (e.KeyCode == Keys.Enter)
            {
                // Trigger the login button click event
                LoginBtn_Click(this, new EventArgs());
            }
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (PasswordBox.Text == "" || UsernameBox.Text == "")
            {
                MessageBox.Show("Username or password cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {   
                if (PasswordBox.Text == DB.ReadData("SELECT Password FROM Users WHERE Username == '" + UsernameBox.Text + "'"))
                {
                    // Hide window
                    Hide();

                    // Show menu
                    Menu frm2 = new Menu(this, UsernameBox.Text);
                    frm2.ShowDialog();

                    // Empty boxes
                    PasswordBox.Clear();
                    UsernameBox.Clear();
                }
                else
                {
                    MessageBox.Show("Wrong Credentials", "Login Failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ApplyRoundedCorners(System.Windows.Forms.Control control, int radius)
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

        private void forgotLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Please contact an administrator for password recovery!");
        }

        private void registerLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (Register regForm = new Register(this))
            {
                Hide();
                regForm.ShowDialog();
            }
        }
    }
}

