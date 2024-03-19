using System;
using System.Windows.Forms;

namespace ATM
{
    public partial class atmOptions : Form
    {
        private Account account;

        public atmOptions(Account activeAccount)
        {
            InitializeComponent();
            this.account = activeAccount;
            this.btnWithdraw.Click += BtnWithdraw_Click;
            this.btnBalance.Click += BtnBalance_Click;
            this.btnExit.Click += BtnExit_Click;
        }

        private void BtnWithdraw_Click(object sender, EventArgs e)
        {
            // Assuming 'account' is the Account object that's already been authenticated
            var withdrawForm = new WithdrawForm(account);
            withdrawForm.ShowDialog();
        }



        private void BtnBalance_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Your current balance is: {account.getBalance()}", "Balance Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close(); // Closes the options form
        }

    }
}
