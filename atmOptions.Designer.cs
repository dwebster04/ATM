// atmOptions.Designer.cs
// this is displayed after log in
// here can select choices to withdraw or check balance

using System;
using System.Drawing;
using System.Windows.Forms;

namespace ATM
{
    partial class atmOptions
    {
        private System.Windows.Forms.Label lblOptions;
        private System.Windows.Forms.Button btnWithdraw;
        private System.Windows.Forms.Button btnBalance;
        private System.Windows.Forms.Button btnExit;
        private Account activeAccount;
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

        public atmOptions(Account account, int colourID)
        {
            this.colour = colourID;
            InitializeComponent();
            this.activeAccount = account;

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
                //pinButtons[i].Click += PinButton_Click; // Assign a click event handler
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

            // ~~~~~~~~~~~~~~~~~~~~~~

            this.lblOptions = new System.Windows.Forms.Label();
            this.btnWithdraw = new System.Windows.Forms.Button();
            this.btnBalance = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.BackColor = colours[colour];

            // lblOptions
            this.lblOptions.Location = new System.Drawing.Point(25, 20);
            this.lblOptions.Name = "lblOptions";
            this.lblOptions.Size = new System.Drawing.Size(150, 25);
            this.lblOptions.Text = "Select an option:";
            this.lblOptions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblOptions.BackColor = System.Drawing.Color.White; // Set the background color to red

            // btnWithdraw
            this.btnWithdraw.Location = new System.Drawing.Point(50, 55);
            this.btnWithdraw.Name = "btnWithdraw";
            this.btnWithdraw.Size = new System.Drawing.Size(100, 25);
            this.btnWithdraw.Text = "Withdraw Cash";

            // btnBalance
            this.btnBalance.Location = new System.Drawing.Point(50, 90);
            this.btnBalance.Name = "btnBalance";
            this.btnBalance.Size = new System.Drawing.Size(100, 25);
            this.btnBalance.Text = "Check Balance";

            // btnExit
            this.btnExit.Location = new System.Drawing.Point(50, 125);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(100, 25);
            this.btnExit.Text = "Exit";

            // atmOptions (Form)
            this.Size = new System.Drawing.Size(415, 475);
            this.Controls.Add(this.lblOptions);
            this.Controls.Add(this.btnWithdraw);
            this.Controls.Add(this.btnBalance);
            this.Controls.Add(this.btnExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "atmOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ATM Options";
            this.btnWithdraw.Click += btnWithdraw_Click;
            this.btnBalance.Click += btnBalance_Click;
            this.btnExit.Click += btnExit_Click;
            this.ResumeLayout(false);
        }
        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            WithdrawForm withdrawForm = new WithdrawForm(activeAccount, colour, true);
            withdrawForm.Show(); // Show the options form
        }

        private void btnBalance_Click(object sender, EventArgs e)
        {
            // Implement the logic for checking balance here
            MessageBox.Show($"Your current balance is: {activeAccount.getBalance()}", "Balance Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // Close the form when the Exit button is clicked
            this.Close();
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
    }
}
