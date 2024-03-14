using System;
using System.Windows.Forms;

namespace ATM
{
    partial class atmOptions
    {
        // Declare labels, text boxes, and button for account number, PIN, and submit button
        private Account account;
        private Label lblOptions;
        private Button btnWithdraw;
        private Button btnBalance;
        private Button btnExit;

        public atmOptions(Account activeAccount)
        {
            InitializeComponent();
            this.account = activeAccount;
        }

        private void InitializeComponent()
        {
            this.lblOptions = new System.Windows.Forms.Label();
            this.btnWithdraw = new System.Windows.Forms.Button();
            this.btnBalance = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblOptions
            // 
            this.lblOptions.Location = new System.Drawing.Point(25, 20);
            this.lblOptions.Name = "lblOptions";
            this.lblOptions.Size = new System.Drawing.Size(150, 25);
            this.lblOptions.TabIndex = 0;
            this.lblOptions.Text = "Select an option:";
            this.lblOptions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblOptions.BackColor = System.Drawing.Color.Red;
            // 
            // btnWithdraw
            // 
            this.btnWithdraw.Location = new System.Drawing.Point(50, 55);
            this.btnWithdraw.Name = "btnWithdraw";
            this.btnWithdraw.Size = new System.Drawing.Size(100, 25);
            this.btnWithdraw.TabIndex = 1;
            this.btnWithdraw.Text = "Withdraw Cash";
            // 
            // btnBalance
            // 
            this.btnBalance.Location = new System.Drawing.Point(50, 90);
            this.btnBalance.Name = "btnBalance";
            this.btnBalance.Size = new System.Drawing.Size(100, 25);
            this.btnBalance.TabIndex = 2;
            this.btnBalance.Text = "Check Balance";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(50, 125);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(100, 25);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Exit";
            // 
            // atmOptions
            // 
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
            this.ResumeLayout(false);

        }

        private void BtnWithdraw_Click(object sender, EventArgs e)
        {
            // Add code to handle withdrawing cash here
            MessageBox.Show("Withdraw cash option selected.");
        }

        private void BtnBalance_Click(object sender, EventArgs e)
        {
            // Add code to handle checking balance here
            MessageBox.Show("Check balance option selected. Balance: " + account.getBalance());
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            // Close the form when the user chooses to exit
            this.Close();
        }
    }
}