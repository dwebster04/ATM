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
            Color.Magenta,
        };

        private Label atmScreen;
        private Button[] buttons = new Button[5];
        private Button[] pinButtons = new Button[9];
        private Button confirm; private Button clear;

        public atmLogIn(Account[] accounts, int colourID)
        {
            this.colour = colourID;
            InitializeComponent();
            this.accounts = accounts;
            
        }
        private void InitializeComponent()
        {
            // atm screen
            Label atmScreen = new Label();
            atmScreen.BackColor = Color.LightBlue;
            atmScreen.Size = new System.Drawing.Size(300, 300);
            atmScreen.Location = new System.Drawing.Point(50, 25);
            this.Controls.Add(atmScreen);

            buttons = new Button[5];
            buttons = new Button[5];
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i] = new Button(); // Initialize each button in the array
            }
            InitializeButton(buttons[0], "1", new System.Drawing.Point(0, 50));
            InitializeButton(buttons[1], "1", new System.Drawing.Point(0, 100));
            InitializeButton(buttons[2], "1", new System.Drawing.Point(350, 50));
            InitializeButton(buttons[3], "1", new System.Drawing.Point(350, 100));
            InitializeButton(buttons[4], "1", new System.Drawing.Point(0, 150));

            for (int i = 0; i < 5; i++)
            {
                this.Controls.Add(buttons[i]); // Add the button to the form's controls
            }

            // Create buttons for PIN input
            int buttonSize = 30;
            for (int i = 0; i < 9; i++)
            {
                pinButtons[i] = new Button();
                pinButtons[i].Text = (i + 1).ToString(); // Adjust button labels to start from 1
                pinButtons[i].Size = new Size(buttonSize, buttonSize);
                pinButtons[i].Location = new Point(50 + (i % 3) * (buttonSize + 0), 335 + (i / 3) * (buttonSize + 0));
                pinButtons[i].Click += PinButton_Click; // Assign a click event handler
                pinButtons[i].BackColor = Color.Gray;
                pinButtons[i].ForeColor = Color.White;
                this.Controls.Add(pinButtons[i]); // Add the button to the form's controls
            }

            // confirm button
            this.confirm = new Button();
            this.confirm.Text = "Y";
            this.confirm.Location = new System.Drawing.Point(150, 350);
            this.confirm.Size = new Size(buttonSize, buttonSize);
            this.confirm.BackColor = Color.ForestGreen;
            this.confirm.ForeColor = Color.White;
            this.Controls.Add(confirm);


            // clear button
            this.clear = new Button();
            this.clear.Text = "X";
            this.clear.Location = new System.Drawing.Point(150, 380);
            this.clear.Size = new Size(buttonSize, buttonSize);
            this.clear.BackColor = Color.Crimson;
            this.clear.ForeColor = Color.White;
            this.Controls.Add(clear);

            // Initialize form properties
            this.Text = "ATM Login";
            this.Size = new System.Drawing.Size(415, 475);
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
        private void PinButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string buttonText = button.Text;

            // Append the clicked button's text to the PIN textbox
            txtPin.Text += buttonText;
        }
        private void InitializeButton(Button button, string text, System.Drawing.Point location)
        {
            button.Text = text;
            button.Size = new System.Drawing.Size(50, 50); // Adjust button size as needed
            button.Location = location;
            button.BackColor = Color.Gray;
            button.ForeColor = Color.White;
            //button.Click += btnFixedAmount_Click;
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