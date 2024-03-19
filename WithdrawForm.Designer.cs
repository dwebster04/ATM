﻿// WithdrawForm.Design.cs
// If withdraw is chosen on options form
// Can slect bettween 10, 20, 50, 100 and custom amount to withdraw.

using ATM;
using System;
using System.Drawing;
using System.Security.Principal;
using System.Windows.Forms;

partial class WithdrawForm
{
    private Account activeAccount;
    private Panel centerPanel;
    private Button btn10;
    private Button btn20;
    private Button btn50;
    private Button btn100;
    private TextBox txtCustomAmount;
    private Button btnConfirmCustom;
    private Button btnCancel;
    private int colour;
    Color[] colours = new Color[]{
            Color.Red,
            Color.Blue,
            Color.Green,
    };

    public WithdrawForm(Account account, int colourID, bool additionalParameter)
    {
        this.colour = colourID;
        InitializeComponent();
        this.activeAccount = account;  
    }

    private void InitializeComponent()
    {
        centerPanel = new Panel();
        btn10 = new Button();
        btn20 = new Button();
        btn50 = new Button();
        btn100 = new Button();
        txtCustomAmount = new TextBox();
        btnConfirmCustom = new Button();
        btnCancel = new Button();

        // Initialize and configure the buttons
        InitializeButton(btn10, "$10", new System.Drawing.Point(10, 20));
        InitializeButton(btn20, "$20", new System.Drawing.Point(75, 20));
        InitializeButton(btn50, "$50", new System.Drawing.Point(140, 20));
        InitializeButton(btn100, "$100", new System.Drawing.Point(205, 20));

        InitializeCustomAmountComponents();

        // Panel settings
        centerPanel.AutoSize = true;
        centerPanel.Controls.Add(btn10);
        centerPanel.Controls.Add(btn20);
        centerPanel.Controls.Add(btn50);
        centerPanel.Controls.Add(btn100);
        centerPanel.Controls.Add(txtCustomAmount);
        centerPanel.Controls.Add(btnConfirmCustom);
        centerPanel.Controls.Add(btnCancel);
        Controls.Add(centerPanel);

        // Form Properties
        AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(294, 120);
        this.BackColor = colours[colour];
        Name = "WithdrawForm";
        Text = "Withdraw Money";

        // Center the panel within the form
        centerPanel.Location = new System.Drawing.Point((ClientSize.Width - centerPanel.Width) / 2,
                                                       (ClientSize.Height - centerPanel.Height) / 2);
        centerPanel.Anchor = AnchorStyles.None;

        // Adjusting the form's layout event to re-center the panel on resize
        this.Resize += (sender, e) =>
        {
            centerPanel.Location = new System.Drawing.Point((ClientSize.Width - centerPanel.Width) / 2,
                                                           (ClientSize.Height - centerPanel.Height) / 2);
        };
    }

    private void InitializeButton(Button button, string text, System.Drawing.Point location)
    {
        button.Text = text;
        button.Size = new System.Drawing.Size(60, 40);
        button.Location = location;
        button.Click += btnFixedAmount_Click;
        centerPanel.Controls.Add(button);
    }

    private void InitializeCustomAmountComponents()
    {
        txtCustomAmount.Location = new System.Drawing.Point(10, 70);
        txtCustomAmount.Size = new System.Drawing.Size(100, 20);

        btnConfirmCustom.Text = "Confirm";
        btnConfirmCustom.Location = new System.Drawing.Point(120, 70);
        btnConfirmCustom.Size = new System.Drawing.Size(75, 23);
        btnConfirmCustom.Click += btnConfirmCustom_Click;

        btnCancel.Text = "Cancel";
        btnCancel.Location = new System.Drawing.Point(205, 70);
        btnCancel.Size = new System.Drawing.Size(75, 23);
        btnCancel.Click += btnCancel_Click;
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
        if (activeAccount.getBalance() >= amount)
        {
            activeAccount.decrementBalance(amount);
            MessageBox.Show($"Withdrawal successful. New balance: {activeAccount.getBalance()}");
            this.Close();
        }
        else
        {
            MessageBox.Show("Insufficient funds.");
        }
    }
}