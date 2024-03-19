﻿// atmLogIn.Designer.cs
// first page that appears when 'using an atm'
// used to log in by entering account number and pin

using System;
using System.Drawing;
using System.Windows.Forms;

namespace ATM
{
    partial class atmLogIn
    {
        // Declare labels, text boxes, and button for account number, PIN, and submit button
        private Label lblAccountNumber;
        private Label lblPin;
        private TextBox txtAccountNumber;
        private TextBox txtPin;
        private Button btnSubmit;
        private Account[] accounts;
        private int colour;
        Color[] colours = new Color[]{
            Color.Red,
            Color.Blue,
            Color.Green,
        };

        public atmLogIn(Account[] accounts, int colourID)
        {
            this.colour = colourID;
            InitializeComponent();
            this.accounts = accounts;
            
        }
        private void InitializeComponent()
        {
            // Initialize form properties
            this.Text = "ATM Login";
            this.Size = new System.Drawing.Size(300, 200);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = colours[colour];

            // Initialize controls
            this.lblAccountNumber = new Label();
            this.lblAccountNumber.Text = "Account Number:";
            this.lblAccountNumber.Location = new System.Drawing.Point(20, 20);
            this.Controls.Add(lblAccountNumber);

            this.txtAccountNumber = new TextBox();
            this.txtAccountNumber.Location = new System.Drawing.Point(140, 20);
            this.txtAccountNumber.Size = new System.Drawing.Size(120, 20);
            this.Controls.Add(txtAccountNumber);

            this.lblPin = new Label();
            this.lblPin.Text = "PIN:";
            this.lblPin.Location = new System.Drawing.Point(20, 50);
            this.Controls.Add(lblPin);

            this.txtPin = new TextBox();
            this.txtPin.Location = new System.Drawing.Point(140, 50);
            this.txtPin.Size = new System.Drawing.Size(120, 20);
            this.txtPin.PasswordChar = '*'; // Mask the PIN input
            this.Controls.Add(txtPin);

            this.btnSubmit = new Button();
            this.btnSubmit.Text = "Submit";
            btnSubmit.Click += btnSubmit_Click;
            this.btnSubmit.Location = new System.Drawing.Point(100, 100);
            
            this.Controls.Add(btnSubmit);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            int accountNumber = int.Parse(txtAccountNumber.Text);
            int pin = int.Parse(txtPin.Text);

            Account activeAccount = null;

            foreach (Account acc in accounts)
            {
                if (acc.getAccountNum() == accountNumber && acc.checkPin(pin))
                {
                    activeAccount = acc;
                    break;
                }
            }

            if (activeAccount != null)
            {
                // If login successful, open the options form
                atmOptions optionsForm = new atmOptions(activeAccount, colour);
                optionsForm.Show(); // Show the options form
            }
            else
            {
                MessageBox.Show("Invalid account number or PIN. Please try again.");
            }
        }
    }
}