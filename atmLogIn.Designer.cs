// atmLogIn.Designer.cs
// first page that appears when 'using an atm'
// used to log in by entering account number and pin

using System;
using System.Drawing;
using System.Windows.Forms;

namespace ATM
{
    partial class atmLogIn
    {
        private Label lblAccountNumber;
        private Label lblPin;
        private TextBox txtAccountNumber;
        private TextBox txtPin;
        private Button btnSubmit;
        private Account[] accounts;
        int clickedCount = 0;

        private int colour;
        Color[] colours = new Color[]{
            Color.LightSeaGreen,
            Color.Salmon,
            Color.DeepPink,
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
            InitializeButton(buttons[1], "2", new System.Drawing.Point(0, 100));
            InitializeButton(buttons[2], "3", new System.Drawing.Point(350, 50));
            InitializeButton(buttons[3], "4", new System.Drawing.Point(350, 100));
            InitializeButton(buttons[4], "5", new System.Drawing.Point(0, 150));

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
                pinButtons[i].Click += PinButton_Click1; // Assign a click event handler
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
            this.confirm.Click += btnConfirm_Click;
            this.Controls.Add(confirm);


            // clear button
            this.clear = new Button();
            this.clear.Text = "X";
            this.clear.Location = new System.Drawing.Point(150, 380);
            this.clear.Size = new Size(buttonSize, buttonSize);
            this.clear.BackColor = Color.Crimson;
            this.clear.ForeColor = Color.White;
            this.clear.Click += btnClear_Click;
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
            this.lblAccountNumber.Location = new System.Drawing.Point(230, 325);
            this.lblAccountNumber.ForeColor = Color.White;
            this.Controls.Add(lblAccountNumber);

            this.txtAccountNumber = new TextBox();
            this.txtAccountNumber.Location = new System.Drawing.Point(230, 350);
            this.txtAccountNumber.Size = new System.Drawing.Size(120, 20);
            this.Controls.Add(txtAccountNumber);

            this.lblPin = new Label();
            this.lblPin.Text = "PIN:";
            this.lblPin.Location = new System.Drawing.Point(230, 375);
            this.lblPin.ForeColor = Color.White;
            this.Controls.Add(lblPin);

            this.txtPin = new TextBox();
            this.txtPin.Location = new System.Drawing.Point(230, 400);
            this.txtPin.Size = new System.Drawing.Size(120, 20);
            this.txtPin.PasswordChar = '*'; // Mask the PIN input
            this.Controls.Add(txtPin);

            this.btnSubmit = new Button();
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.Location = new System.Drawing.Point(100, 100);

            this.Controls.Add(btnSubmit);
        }
        private void PinButton_Click1(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string buttonText = button.Text;

            // Append the clicked button's text to the PIN textbox
            txtAccountNumber.Text += buttonText;
        }
        private void PinButton_Click2(object sender, EventArgs e)
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

        void submit(object sender)
        {
            // Check if either the account number or PIN fields are blank
            if (string.IsNullOrWhiteSpace(txtAccountNumber.Text) || string.IsNullOrWhiteSpace(txtPin.Text))
            {
                MessageBox.Show("Please enter both the account number and PIN.");
                clickedCount = 0;
                return; // Exit the method early
            }

            // Parse the account number and PIN
            int accountNumber, pin;
            if (!int.TryParse(txtAccountNumber.Text, out accountNumber) || !int.TryParse(txtPin.Text, out pin))
            {
                MessageBox.Show("Please enter valid numbers for the account number and PIN.");
                clickedCount = 0;
                return; // Exit the method early
            }

            Account activeAccount = null;

            // Find the active account
            foreach (Account acc in accounts)
            {
                if (acc.getAccountNum() == accountNumber && acc.checkPin(pin))
                {
                    activeAccount = acc;
                    break;
                }
            }

            // Show appropriate message based on login success
            if (activeAccount != null)
            {
                // If login successful, open the options form
                atmOptions optionsForm = new atmOptions(activeAccount, colour);
                optionsForm.Show(); // Show the options form
            }
            else
            {
                MessageBox.Show("Invalid account number or PIN. Please try again.");
                clearInputs();
                clickedCount = 0;
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                pinButtons[i].Click -= PinButton_Click1; // Assign a click event handler
                pinButtons[i].Click -= PinButton_Click2; // Assign a click event handler

            }
            clickedCount = 0;
            clearInputs();

            for (int i = 0; i < 9; i++)
            {
                pinButtons[i].Click += PinButton_Click1; // Assign a click event handler
            }
        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            switch (clickedCount)
            {
                case 0:
                    for (int i = 0; i < 9; i++)
                    {
                        pinButtons[i].Click -= PinButton_Click1; // Assign a click event handler
                        pinButtons[i].Click += PinButton_Click2; // Assign a click event handler

                    }
                    clickedCount = 1;
                    break;
                    
                case 1:
                    for (int i = 0; i < 9; i++)
                    {
                        pinButtons[i].Click += PinButton_Click1; // Assign a click event handler
                        pinButtons[i].Click -= PinButton_Click2; // Assign a click event handler

                    }
                    submit(sender);
                    clickedCount = 0;
                    clearInputs();
                    break;
            }
            
        }

        // Method to clear the text boxes
        private void clearInputs()
        {
            txtAccountNumber.Clear();
            txtPin.Clear();
        }
    }
}