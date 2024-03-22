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
        private Label lblWelcome;
        private Label atmScreen;
        private Label lblDisplayError;

        private TextBox txtAccountNumber;
        private TextBox txtPin;

        private Button btnSubmit;
        private Button[] buttons = new Button[5];
        private Button[] pinButtons = new Button[9];
        private Button confirm; 
        private Button clear;

        private Account[] accounts;

        int clickedCount = 0;
        private int colour;

        Color[] colours = new Color[]{
            Color.LightSeaGreen,
            Color.Salmon,
            Color.DeepPink,
        };

        private Timer timer;

        public atmLogIn(Account[] accounts, int colourID)
        {
            this.colour = colourID;
            InitializeComponent();
            this.accounts = accounts;
        }
        private void InitializeComponent()
        {
            // Welcome Label
            this.lblWelcome = new Label();
            this.lblWelcome.Text = "BANK OF LIDL";
            this.lblWelcome.Size = new System.Drawing.Size(200, 50);
            this.lblWelcome.Font = new Font(this.lblWelcome.Font.FontFamily, 15, FontStyle.Bold);
            this.lblWelcome.TextAlign = ContentAlignment.MiddleCenter; // Center text horizontally and vertically
            this.lblWelcome.Location = new System.Drawing.Point(100, 50);
            this.lblWelcome.BackColor = Color.LightBlue;
            this.lblWelcome.ForeColor = Color.DarkBlue;
            this.Controls.Add(lblWelcome);

            // Display Error Label
            this.lblDisplayError = new Label();
            this.lblDisplayError.Text = "£££";
            this.lblDisplayError.Location = new System.Drawing.Point(100, 250);
            this.lblDisplayError.Size = new System.Drawing.Size(200, 75);
            this.lblDisplayError.Font = new Font(this.lblDisplayError.Font.FontFamily, 7, FontStyle.Regular);
            this.lblDisplayError.TextAlign = ContentAlignment.MiddleCenter; // Center text horizontally and vertically
            this.lblDisplayError.ForeColor = Color.LightBlue;
            this.lblDisplayError.BackColor = Color.LightBlue;
            this.Controls.Add(lblDisplayError);

            // Account Number Label
            this.lblAccountNumber = new Label();
            this.lblAccountNumber.Text = "Account Number:";
            this.lblAccountNumber.Location = new System.Drawing.Point(140, 150);
            this.lblAccountNumber.Size = new System.Drawing.Size(120, 20);
            this.lblAccountNumber.ForeColor = Color.White;
            this.lblAccountNumber.BackColor = Color.DarkBlue;
            this.Controls.Add(lblAccountNumber);

            // Account Number Text
            this.txtAccountNumber = new TextBox();
            this.txtAccountNumber.Location = new System.Drawing.Point(140, 175);
            this.txtAccountNumber.Size = new System.Drawing.Size(120, 20);
            this.Controls.Add(txtAccountNumber);

            // Pin Label
            this.lblPin = new Label();
            this.lblPin.Text = "PIN:";
            this.lblPin.Location = new System.Drawing.Point(140, 200);
            this.lblPin.Size = new System.Drawing.Size(120, 20);
            this.lblPin.ForeColor = Color.White;
            this.lblPin.BackColor = Color.DarkBlue;
            this.Controls.Add(lblPin);

            // Pin Text
            this.txtPin = new TextBox();
            this.txtPin.Location = new System.Drawing.Point(140, 225);
            this.txtPin.Size = new System.Drawing.Size(120, 20);
            this.txtPin.PasswordChar = '*'; // Mask the PIN input
            this.Controls.Add(txtPin);

            // ATM Screen Label
            Label atmScreen = new Label();
            atmScreen.BackColor = Color.LightBlue;
            atmScreen.Size = new System.Drawing.Size(300, 300);
            atmScreen.Location = new System.Drawing.Point(50, 25);
            this.Controls.Add(atmScreen);

            // Buttons on the side of the ATM
            buttons = new Button[5];

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i] = new Button(); // Initialize each button in the array
            }

            // Initialize buttons
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
                pinButtons[i].Text = (i + 1).ToString(); 
                pinButtons[i].Size = new Size(buttonSize, buttonSize); 
                pinButtons[i].Location = new Point(50 + (i % 3) * (buttonSize + 0), 335 + (i / 3) * (buttonSize + 0));
                pinButtons[i].Click += PinButton_Click1; // Assign a click event handler
                pinButtons[i].BackColor = Color.Gray;
                pinButtons[i].ForeColor = Color.White;
                this.Controls.Add(pinButtons[i]); // Add the button to the form's controls
            }

            // Confirm Button
            this.confirm = new Button();
            this.confirm.Text = "Y";
            this.confirm.Location = new System.Drawing.Point(150, 350);
            this.confirm.Size = new Size(buttonSize, buttonSize);
            this.confirm.BackColor = Color.ForestGreen;
            this.confirm.ForeColor = Color.White;
            this.confirm.Click += btnConfirm_Click;
            this.Controls.Add(confirm);


            // Clear Button
            this.clear = new Button();
            this.clear.Text = "X";
            this.clear.Location = new System.Drawing.Point(150, 380);
            this.clear.Size = new Size(buttonSize, buttonSize);
            this.clear.BackColor = Color.Crimson;
            this.clear.ForeColor = Color.White;
            this.clear.Click += btnClear_Click;
            this.Controls.Add(clear);

            // Initialize Form Properties
            this.Text = "ATM Login";
            this.Size = new System.Drawing.Size(415, 475);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = colours[colour];

            // Submit Button
            this.btnSubmit = new Button();
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.Location = new System.Drawing.Point(100, 100);
            this.Controls.Add(btnSubmit);
        }

        // When 'KeyPad' buttons are clicked type in the account number text box
        private void PinButton_Click1(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string buttonText = button.Text;

            // Append the clicked button's text to the account number textbox
            txtAccountNumber.Text += buttonText;
        }

        // When 'KeyPad' buttons are clicked type in the PIN text box
        private void PinButton_Click2(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string buttonText = button.Text;

            // Append the clicked button's text to the PIN textbox
            txtPin.Text += buttonText;
        }

        // Used to create buttons
        private void InitializeButton(Button button, string text, System.Drawing.Point location)
        {
            button.Text = text;
            button.Size = new System.Drawing.Size(50, 50);
            button.Location = location;
            button.BackColor = Color.Gray;
            button.ForeColor = Color.Gray;
        }

        private void StartTimer()
        {
            timer = new Timer();
            timer.Interval = 3000; // 3 seconds
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Change label colour to light blue
            lblDisplayError.ForeColor = Color.LightBlue;

            // Reset the label text color to its original color after 3 seconds
            Timer timer = (Timer)sender;
            timer.Stop(); // Stop the timer
            timer.Dispose(); // Dispose the timer to release resources
        }

        // On the 2nd press of confirm button submit account number and PIN to be checked then go to options form
        void submit(object sender)
        {
            // Check if either the account number or PIN fields are blank
            if (string.IsNullOrWhiteSpace(txtAccountNumber.Text) || string.IsNullOrWhiteSpace(txtPin.Text))
            {
                lblDisplayError.ForeColor = Color.DarkBlue; // Set text color to dark blue
                lblDisplayError.Text = "Please enter both the account number and PIN.";
                StartTimer(); // Start the timer to change label color
                clickedCount = 0;
                return; // Exit the method early
            }

            // Parse the account number and PIN
            int accountNumber, pin;
            if (!int.TryParse(txtAccountNumber.Text, out accountNumber) || !int.TryParse(txtPin.Text, out pin))
            {
                lblDisplayError.ForeColor = Color.DarkBlue; // Set text color to dark blue
                lblDisplayError.Text = "Please enter valid numbers for the account number and PIN.";
                StartTimer(); // Start the timer to change label color
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
                lblDisplayError.ForeColor = Color.DarkBlue; // Set text color to dark blue
                lblDisplayError.Text = "Please enter valid numbers for the account number and PIN.";
                StartTimer(); // Start the timer to change label color
                clearInputs();
                clickedCount = 0;
            }
        }

        // Clear account number and PIN text boxes
        private void btnClear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                // remove click functions
                pinButtons[i].Click -= PinButton_Click1;
                pinButtons[i].Click -= PinButton_Click2; 
            }

            // set the times the confrim button has been clicked to 0 and clear text boxes
            clickedCount = 0;
            clearInputs();

            for (int i = 0; i < 9; i++) // loop through all pin buttons and give them the first click handler
            {
                pinButtons[i].Click += PinButton_Click1; // Assign a click event handler
            }
        }

        // first click is after entering account number then second click is after entering PIN to submit detials
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            switch (clickedCount)
            {
                case 0: // if first time being clicked
                    for (int i = 0; i < 9; i++)
                    {
                        pinButtons[i].Click -= PinButton_Click1; // takeaway click handler 1
                        pinButtons[i].Click += PinButton_Click2; // add click handler 2

                    }
                    clickedCount = 1;
                    break;
                    
                case 1:
                    for (int i = 0; i < 9; i++)
                    {
                        pinButtons[i].Click += PinButton_Click1; // add click handler 1
                        pinButtons[i].Click -= PinButton_Click2; // takeaway click handler 2

                    }

                    submit(sender); // submit
                    clickedCount = 0; // rest clicked count
                    clearInputs(); // clear text boxes
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