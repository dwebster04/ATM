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
        private Account activeAccount;
        private int colour;
        Color[] colours = new Color[]{
            Color.LightSeaGreen,
            Color.Salmon,
            Color.DeepPink,
        };

        private Label atmScreen;
        private Label lblDisplayBalance;
        private Label lblWithdraw; 
        private Label lblBalance; 
        private Label lblExit;

        private Button[] buttons = new Button[5];
        private Button[] pinButtons = new Button[9];
        private Button confirm;
        private Button clear;

        private int displayedBalance;
        private string balanceString;
        
        private Timer timer;

        public atmOptions(Account account, int colourID)
        {
            this.colour = colourID;
            InitializeComponent();
            this.activeAccount = account;
        }

        private void StartTimer()
        {
            timer = new Timer();
            timer.Interval = 5000; // 5 seconds
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Change label colour to light blue (hide it as will be same colour as screen)
            lblDisplayBalance.ForeColor = Color.LightBlue;
            timer.Stop(); // Stop the timer
        }

        private void InitializeComponent()
        {
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

            this.buttons[0].Click += btnWithdraw_Click;
            this.buttons[1].Click += btnBalance_Click;
            this.buttons[4].Click += btnExit_Click;

            // Define the label size
            System.Drawing.Size LabelSize = new System.Drawing.Size(75, 20);

            // Withdraw Label
            this.lblWithdraw = new Label();
            this.lblWithdraw.Text = "Withdraw";
            this.lblWithdraw.Location = new System.Drawing.Point(buttons[0].Right + 10, buttons[0].Top + 15);
            this.lblWithdraw.Size = LabelSize;
            this.lblWithdraw.TextAlign = ContentAlignment.MiddleCenter; // Center text horizontally and vertically
            this.lblWithdraw.ForeColor = Color.White;
            this.lblWithdraw.BackColor = Color.DarkBlue;
            this.Controls.Add(lblWithdraw);

            // Balance Label
            this.lblBalance = new Label();
            this.lblBalance.Text = "Balance";
            this.lblBalance.Location = new System.Drawing.Point(buttons[1].Right + 10, buttons[1].Top + 15);
            this.lblBalance.Size = LabelSize;
            this.lblBalance.TextAlign = ContentAlignment.MiddleCenter; // Center text horizontally and vertically
            this.lblBalance.ForeColor = Color.White;
            this.lblBalance.BackColor = Color.DarkBlue;
            this.Controls.Add(lblBalance);

            // Exit Label
            this.lblExit = new Label();
            this.lblExit.Text = "Exit";
            this.lblExit.Location = new System.Drawing.Point(buttons[4].Right + 10, buttons[4].Top + 15);
            this.lblExit.Size = LabelSize;
            this.lblExit.TextAlign = ContentAlignment.MiddleCenter; // Center text horizontally and vertically
            this.lblExit.ForeColor = Color.White;
            this.lblExit.BackColor = Color.DarkBlue;
            this.Controls.Add(lblExit);

            // Display Balance Label
            this.lblDisplayBalance = new Label();
            this.lblDisplayBalance.Text = "£££";
            this.lblDisplayBalance.Location = new System.Drawing.Point(100, 225);
            this.lblDisplayBalance.Size = new System.Drawing.Size(200, 75);
            this.lblDisplayBalance.Font = new Font(this.lblDisplayBalance.Font.FontFamily, 25, FontStyle.Bold);
            this.lblDisplayBalance.TextAlign = ContentAlignment.MiddleCenter; // Center text horizontally and vertically
            this.lblDisplayBalance.ForeColor = Color.LightBlue;
            this.lblDisplayBalance.BackColor = Color.LightBlue;
            this.Controls.Add(lblDisplayBalance);

            // ATM Screen Label
            Label atmScreen = new Label();
            atmScreen.BackColor = Color.LightBlue;
            atmScreen.Size = new System.Drawing.Size(300, 300);
            atmScreen.Location = new System.Drawing.Point(50, 25);
            this.Controls.Add(atmScreen);
        

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

            // ATM Options Form
            this.BackColor = colours[colour];
            this.Size = new System.Drawing.Size(415, 475);   
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "atmOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ATM Options";

            this.ResumeLayout(false);
        }

        // When withdraw button clicked go to withdraw form
        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            WithdrawForm withdrawForm = new WithdrawForm(activeAccount, colour, true);
            withdrawForm.Show(); // Show the options form
        }

        // When balance button is clicked display balance on screen for 5 seconds
        private void btnBalance_Click(object sender, EventArgs e)
        {
            // Display the balance and set the colour to dark blue
            lblDisplayBalance.Text = "£" + activeAccount.getBalance();
            lblDisplayBalance.ForeColor = Color.DarkBlue;

            // Start the timer
            StartTimer();
        }
        
        // CLose this form when exit button is clicked
        private void btnExit_Click(object sender, EventArgs e)
        {
            // Close the form when the Exit button is clicked
            this.Close();
        }

        // Used to set values for the buttons
        private void InitializeButton(Button button, string text, System.Drawing.Point location)
        {
            button.Text = text;
            button.Size = new System.Drawing.Size(50, 50); // Adjust button size as needed
            button.Location = location;
            button.BackColor = Color.Gray;
            button.ForeColor = Color.Gray;
        }
    }
}
