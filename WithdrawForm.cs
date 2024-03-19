using ATM;
using System;
using System.Windows.Forms;

public partial class WithdrawForm : Form
{
    private Account account;

    public WithdrawForm(Account account)
    {
        InitializeComponent();
        this.account = account;
    }

    private void btnFixedAmount_Click(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button != null)
        {
            int amount = Convert.ToInt32(button.Text.Substring(1)); // Extract amount from button text
            Withdraw(amount);
        }
    }

    private void btnConfirmCustom_Click(object sender, EventArgs e)
    {
        if (int.TryParse(txtCustomAmount.Text, out int customAmount) && customAmount > 0)
        {
            Withdraw(customAmount);
        }
        else
        {
            MessageBox.Show("Please enter a valid amount.");
        }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        this.Close(); // Close the form without any action
    }

    private void Withdraw(int amount)
    {
        if (account.getBalance() >= amount)
        {
            account.decrementBalance(amount);
            MessageBox.Show($"Withdrawal successful. New balance: {account.getBalance()}");
            this.Close();
        }
        else
        {
            MessageBox.Show("Insufficient funds.");
        }
    }
}
