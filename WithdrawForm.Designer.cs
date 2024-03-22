// WithdrawForm.Design.cs
// If withdraw is chosen on options form
// Can slect bettween 10, 20, 50, 100

using ATM;
using System;
using System.Drawing;
using System.Security.Principal;
using System.Windows.Forms;

partial class WithdrawForm
{
    private Label atmScreen;
    private Label lbl10; 
    private Label lbl20; 
    private Label lbl50; 
    private Label lbl100;
    private Label lblCancel;
    private Label lblDisplayWithdraw;

    private Account activeAccount;
    private Panel centerPanel;

    private Button btn10;
    private Button btn20;
    private Button btn50;
    private Button btn100;
    private Button btnCancel;
    private Button[] pinButtons = new Button[9];
    private Button confirm; 
    private Button clear;

    private int oldBalance;
    private int colour;
    Color[] colours = new Color[]{
            Color.LightSeaGreen,
            Color.Salmon,
            Color.DeepPink,
        };

    public WithdrawForm(Account account, int colourID, bool additionalParameter)
    {
        this.colour = colourID;
        InitializeComponent();
        this.activeAccount = account;
        oldBalance = activeAccount.getBalance();
    }

    private void InitializeComponent()
    {
        centerPanel = new Panel();
        btn10 = new Button();
        btn20 = new Button();
        btn50 = new Button();
        btn100 = new Button();
        btnCancel = new Button();

        InitializeButton(btn10, "$10", new System.Drawing.Point(0, 50));
        InitializeButton(btn20, "$20", new System.Drawing.Point(0, 100));
        InitializeButton(btn50, "$50", new System.Drawing.Point(350, 50));
        InitializeButton(btn100, "$100", new System.Drawing.Point(350, 100));

        InitializeCustomAmountComponents();

        // Define the label size
        System.Drawing.Size LabelSize = new System.Drawing.Size(50, 20);

        // £10 Label
        this.lbl10 = new Label();
        this.lbl10.Text = "£10";
        this.lbl10.Location = new System.Drawing.Point(btn10.Right + 10, btn10.Top + 15);
        this.lbl10.Size = LabelSize;
        this.lbl10.TextAlign = ContentAlignment.MiddleCenter; // Center text horizontally and vertically
        this.lbl10.ForeColor = Color.White;
        this.lbl10.BackColor = Color.DarkBlue;
        this.Controls.Add(lbl10);

        // £20 Label
        this.lbl20 = new Label();
        this.lbl20.Text = "£20";
        this.lbl20.Location = new System.Drawing.Point(btn20.Right + 10, btn20.Top + 15);
        this.lbl20.Size = LabelSize;
        this.lbl20.TextAlign = ContentAlignment.MiddleCenter; // Center text horizontally and vertically
        this.lbl20.ForeColor = Color.White;
        this.lbl20.BackColor = Color.DarkBlue;
        this.Controls.Add(lbl20);

        // £50 Label
        this.lbl50 = new Label();
        this.lbl50.Text = "£50";
        this.lbl50.Location = new System.Drawing.Point(btn50.Left - 10 - LabelSize.Width, btn50.Top + 15);
        this.lbl50.Size = LabelSize;
        this.lbl50.TextAlign = ContentAlignment.MiddleCenter; // Center text horizontally and vertically
        this.lbl50.ForeColor = Color.White;
        this.lbl50.BackColor = Color.DarkBlue;
        this.Controls.Add(lbl50);

        // £100 Label
        this.lbl100 = new Label();
        this.lbl100.Text = "£100";
        this.lbl100.Location = new System.Drawing.Point(btn100.Left - 10 - LabelSize.Width, btn100.Top + 15);
        this.lbl100.Size = LabelSize;
        this.lbl100.TextAlign = ContentAlignment.MiddleCenter; // Center text horizontally and vertically
        this.lbl100.ForeColor = Color.White;
        this.lbl100.BackColor = Color.DarkBlue;
        this.Controls.Add(lbl100);

        // Cancel Label
        this.lblCancel = new Label();
        this.lblCancel.Text = "Cancel";
        this.lblCancel.Location = new System.Drawing.Point(btnCancel.Right + 10, btnCancel.Top + 15);
        this.lblCancel.Size = LabelSize;
        this.lblCancel.TextAlign = ContentAlignment.MiddleCenter; // Center text horizontally and vertically
        this.lblCancel.ForeColor = Color.White;
        this.lblCancel.BackColor = Color.DarkBlue;
        this.Controls.Add(lblCancel);

        // Display Withdraw Label
        this.lblDisplayWithdraw = new Label();
        this.lblDisplayWithdraw.Text = "";
        this.lblDisplayWithdraw.Location = new System.Drawing.Point(100, 225);
        this.lblDisplayWithdraw.Size = new System.Drawing.Size(200, 75);
        this.lblDisplayWithdraw.Font = new Font(this.lblDisplayWithdraw.Font.FontFamily, 25, FontStyle.Bold);
        this.lblDisplayWithdraw.TextAlign = ContentAlignment.MiddleCenter; // Center text horizontally and vertically
        this.lblDisplayWithdraw.ForeColor = Color.LightBlue; // Light blue so it is hidden because same colour as ATM screen
        this.lblDisplayWithdraw.BackColor = Color.LightBlue;
        this.Controls.Add(lblDisplayWithdraw);

        // ATM Screen Label
        Label atmScreen = new Label();
        atmScreen.BackColor = Color.LightBlue;
        atmScreen.Size = new System.Drawing.Size(300, 300);
        atmScreen.Location = new System.Drawing.Point(50, 25);
        this.Controls.Add(atmScreen);

        // Panel settings
        centerPanel.AutoSize = true;
        centerPanel.Controls.Add(btn10);
        centerPanel.Controls.Add(btn20);
        centerPanel.Controls.Add(btn50);
        centerPanel.Controls.Add(btn100);
        centerPanel.Controls.Add(btnCancel);
        Controls.Add(centerPanel);

        // Withdraw Form
        this.Size = new System.Drawing.Size(415, 475);
        this.BackColor = colours[colour];
        Name = "WithdrawForm";

        // Create buttons for PIN input
        int buttonSize = 30;
        for (int i = 0; i < 9; i++)
        {
            pinButtons[i] = new Button();
            pinButtons[i].Text = (i + 1).ToString(); // Adjust button labels to start from 1
            pinButtons[i].Size = new Size(buttonSize, buttonSize);
            pinButtons[i].Location = new Point(50 + (i % 3) * (buttonSize + 0), 335 + (i / 3) * (buttonSize + 0));
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
        this.Controls.Add(confirm);


        // Clear Button
        this.clear = new Button();
        this.clear.Text = "X";
        this.clear.Location = new System.Drawing.Point(150, 380);
        this.clear.Size = new Size(buttonSize, buttonSize);
        this.clear.BackColor = Color.Crimson;
        this.clear.ForeColor = Color.White;
        this.Controls.Add(clear);
    }

    // Used to create and set values for buttons
    private void InitializeButton(Button button, string text, System.Drawing.Point location)
    {
        button.Text = text;
        button.Size = new System.Drawing.Size(50, 50); // Adjust button size as needed
        button.Location = location;
        button.Click += btnFixedAmount_Click;
        button.BackColor = Color.Gray;
        button.ForeColor = Color.Gray;
        centerPanel.Controls.Add(button);
    }

    // Used to create and set values for the cancel button
    private void InitializeCustomAmountComponents()
    {
        btnCancel.Text = "Cancel";
        btnCancel.Location = new System.Drawing.Point(0, 150);
        btnCancel.Size = new System.Drawing.Size(50, 50);
        btnCancel.BackColor = Color.Gray;
        btnCancel.ForeColor = Color.Gray;
        btnCancel.Click += btnCancel_Click;
    }

    // What happens when a button to select withdrawal amount is clicked
    private void btnFixedAmount_Click(object sender, EventArgs e)
    {
        DisableWithdrawButtons(); // Firstly disable all buttons so cannot withdraw two amounts at the same time
        lblDisplayWithdraw.BackColor = Color.LightBlue;
        lblDisplayWithdraw.ForeColor = Color.DarkBlue; 

        var button = sender as Button;
        if (button != null)
        {
            int amount = Convert.ToInt32(button.Text.Substring(1)); // Extract amount from button text
            Withdraw(amount);
        }
    }

    // Used to takeaway click event handlers from buttons
    private void DisableWithdrawButtons()
    {
        // Unsubscribe click event handlers for all buttons
        btn10.Click -= btnFixedAmount_Click;
        btn20.Click -= btnFixedAmount_Click;
        btn50.Click -= btnFixedAmount_Click;
        btn100.Click -= btnFixedAmount_Click;
        btnCancel.Click -= btnCancel_Click;
    }

    // When cancel button is clicked close this form
    private void btnCancel_Click(object sender, EventArgs e)
    {
        this.Close(); // Close the form without any action
    }

    private int countdownSeconds = 3; // Total countdown duration in seconds
    private Timer timer;
    private int dotCount = 1; 
    private int withdrawalAmount;
    private void Withdraw(int amount)
    {
        activeAccount.decrementBalance(amount); // Deduct amount from the account balance
        withdrawalAmount = amount;
        // Start the countdown timer
        StartCountdown();
    }

    private void StartCountdown()
    {
        timer = new Timer();
        timer.Interval = 1000; // Timer ticks every 1 second
        timer.Tick += Timer_Tick;
        timer.Start();
    }

    
    // Used to display new balance and addds artificial delay
    private void Timer_Tick(object sender, EventArgs e)
    {
        this.lblDisplayWithdraw.Font = new Font(this.lblDisplayWithdraw.Font.FontFamily, 25, FontStyle.Bold);
        if (dotCount <= 3) // Do when there is less than 3 dots
        {
            string dots = new string('.', dotCount);
            
            lblDisplayWithdraw.Text = dots; // Display dots ( . or .. or ...)
            dotCount++; // Increase the amount of dots for next time
        }
        else
        {
            timer.Stop(); // Stop the timer
            ShowWithdrawalResult(); // Display Withdrawal Result
        }
    }

    private void ShowWithdrawalResult()
    {
        // If withdrawal was successful
        if (activeAccount.getBalance() != oldBalance)
        {
            this.lblDisplayWithdraw.Font = new Font(this.lblDisplayWithdraw.Font.FontFamily, 12, FontStyle.Regular);
            lblDisplayWithdraw.Text = $"Withdrawal successful New balance: £{activeAccount.getBalance()}"; // Display success and new balnce
        
        }
        else // If withdrawal failed
        {
            this.lblDisplayWithdraw.Font = new Font(this.lblDisplayWithdraw.Font.FontFamily, 15, FontStyle.Regular);
            lblDisplayWithdraw.Text = ("Insufficient funds"); // Display fail
        }
        
        // Then close window after 3 seconds so there is time to read display message
        // Start a new timer for 3 seconds to close the form
        Timer closeTimer = new Timer();
        closeTimer.Interval = 3000; // 3 seconds
        closeTimer.Tick += (sender, e) =>
        {
            closeTimer.Stop();
            this.Close();
        };

        closeTimer.Start();
    }
}