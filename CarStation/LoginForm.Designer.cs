using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PachidisStation
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.PasswordBox = new System.Windows.Forms.TextBox();
            this.UsernameBox = new System.Windows.Forms.TextBox();
            this.registerLink = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.LoginBtn = new System.Windows.Forms.Button();
            this.forgotLink = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.mail = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.phone = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.phone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.CausesValidation = false;
            this.panel1.Controls.Add(this.PasswordBox);
            this.panel1.Controls.Add(this.UsernameBox);
            this.panel1.Controls.Add(this.registerLink);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.LoginBtn);
            this.panel1.Controls.Add(this.forgotLink);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(895, 137);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(449, 613);
            this.panel1.TabIndex = 1;
            // 
            // PasswordBox
            // 
            this.PasswordBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.PasswordBox.Location = new System.Drawing.Point(100, 291);
            this.PasswordBox.MinimumSize = new System.Drawing.Size(200, 40);
            this.PasswordBox.Name = "PasswordBox";
            this.PasswordBox.PasswordChar = '●';
            this.PasswordBox.Size = new System.Drawing.Size(264, 35);
            this.PasswordBox.TabIndex = 11;
            // 
            // UsernameBox
            // 
            this.UsernameBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.UsernameBox.Location = new System.Drawing.Point(100, 184);
            this.UsernameBox.MinimumSize = new System.Drawing.Size(200, 40);
            this.UsernameBox.Name = "UsernameBox";
            this.UsernameBox.Size = new System.Drawing.Size(264, 35);
            this.UsernameBox.TabIndex = 10;
            // 
            // registerLink
            // 
            this.registerLink.AutoSize = true;
            this.registerLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.registerLink.LinkColor = System.Drawing.Color.DarkTurquoise;
            this.registerLink.Location = new System.Drawing.Point(321, 525);
            this.registerLink.Name = "registerLink";
            this.registerLink.Size = new System.Drawing.Size(55, 25);
            this.registerLink.TabIndex = 9;
            this.registerLink.TabStop = true;
            this.registerLink.Text = "here";
            this.registerLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.registerLink_LinkClicked);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label4.Location = new System.Drawing.Point(86, 525);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(239, 25);
            this.label4.TabIndex = 8;
            this.label4.Text = "No account? Register";
            // 
            // LoginBtn
            // 
            this.LoginBtn.BackColor = System.Drawing.Color.DarkTurquoise;
            this.LoginBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.LoginBtn.ForeColor = System.Drawing.Color.White;
            this.LoginBtn.Location = new System.Drawing.Point(139, 395);
            this.LoginBtn.Name = "LoginBtn";
            this.LoginBtn.Size = new System.Drawing.Size(164, 41);
            this.LoginBtn.TabIndex = 7;
            this.LoginBtn.Text = "Login";
            this.LoginBtn.UseVisualStyleBackColor = false;
            this.LoginBtn.Click += new System.EventHandler(this.LoginBtn_Click);
            // 
            // forgotLink
            // 
            this.forgotLink.AutoSize = true;
            this.forgotLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.forgotLink.LinkColor = System.Drawing.Color.Black;
            this.forgotLink.Location = new System.Drawing.Point(102, 347);
            this.forgotLink.Name = "forgotLink";
            this.forgotLink.Size = new System.Drawing.Size(116, 16);
            this.forgotLink.TabIndex = 6;
            this.forgotLink.TabStop = true;
            this.forgotLink.Text = "Forgot Password?";
            this.forgotLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.forgotLink_LinkClicked);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label3.Location = new System.Drawing.Point(99, 249);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 31);
            this.label3.TabIndex = 4;
            this.label3.Text = "Password:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label2.Location = new System.Drawing.Point(99, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 31);
            this.label2.TabIndex = 1;
            this.label2.Text = "Username:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 35F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label1.Location = new System.Drawing.Point(140, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 54);
            this.label1.TabIndex = 0;
            this.label1.Text = "Login";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(197)))), ((int)(((byte)(207)))));
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.mail);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.phone);
            this.panel3.Controls.Add(this.label9);
            this.panel3.Location = new System.Drawing.Point(-9, 868);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1513, 66);
            this.panel3.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(845, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(131, 17);
            this.label5.TabIndex = 15;
            this.label5.Text = "info@parcar.com";
            this.label5.UseMnemonic = false;
            // 
            // mail
            // 
            this.mail.BackColor = System.Drawing.Color.Transparent;
            this.mail.Image = global::PachidisStation.Properties.Resources.mail;
            this.mail.Location = new System.Drawing.Point(820, 7);
            this.mail.Name = "mail";
            this.mail.Size = new System.Drawing.Size(20, 20);
            this.mail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mail.TabIndex = 17;
            this.mail.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(800, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(15, 20);
            this.label6.TabIndex = 18;
            this.label6.Text = "|";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label7.Location = new System.Drawing.Point(684, 27);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(194, 17);
            this.label7.TabIndex = 11;
            this.label7.Text = "© Copyright parCAR 2023";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(570, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 17);
            this.label8.TabIndex = 10;
            this.label8.Text = "Contact us:";
            // 
            // phone
            // 
            this.phone.BackColor = System.Drawing.Color.Transparent;
            this.phone.Image = global::PachidisStation.Properties.Resources.phone;
            this.phone.Location = new System.Drawing.Point(662, 5);
            this.phone.Name = "phone";
            this.phone.Size = new System.Drawing.Size(25, 25);
            this.phone.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.phone.TabIndex = 16;
            this.phone.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(693, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(107, 17);
            this.label9.TabIndex = 13;
            this.label9.Text = "+6900000000";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.Image = global::PachidisStation.Properties.Resources.parcar;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(12, 203);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(674, 451);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(197)))), ((int)(((byte)(207)))));
            this.ClientSize = new System.Drawing.Size(1492, 915);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ParCAR Enterpise 2023";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.phone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button LoginBtn;
        private System.Windows.Forms.LinkLabel forgotLink;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel registerLink;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox PasswordBox;
        private System.Windows.Forms.TextBox UsernameBox;
        private Panel panel3;
        private Label label5;
        private PictureBox mail;
        private Label label6;
        private Label label7;
        private Label label8;
        private PictureBox phone;
        private Label label9;
    }
    


}

