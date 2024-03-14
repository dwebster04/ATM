using System;
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

        public atmLogIn(Account[] accounts)
        {
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
                // If login successful, open another form or perform actions
                // For example, you can open a new form like this:
                // ATMOptionsForm optionsForm = new ATMOptionsForm(activeAccount);
                // optionsForm.Show();

                // Or you can perform actions directly here
                MessageBox.Show("Login successful! Welcome to the ATM.");

                // Close the login form
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid account number or PIN. Please try again.");
            }
        }
    }
}