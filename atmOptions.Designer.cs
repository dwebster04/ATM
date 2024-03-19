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
            Color.Red,
            Color.Blue,
            Color.Green,
        };

        public atmOptions(Account account, int colourID)
        {
            this.colour = colourID;
            InitializeComponent();
            this.activeAccount = account;

        }

        private void InitializeComponent()
        {
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
            this.ClientSize = new System.Drawing.Size(200, 170);
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
    }
}
