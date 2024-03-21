// AC22005 Assessment 3 - 2535087 Drew Webster, 2527355 Daniyal Abdullah Alvi

// atm_Console.cs
// Contains program, account, and atm classes
// Entry point to program

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATM
{
    class Program
    {

        public static void Main(string[] args)
        {
            Account[] accounts = new Account[]
            {
                new Account(300, 1, 2),
                new Account(750, 2222, 222222),
                new Account(3000, 3333, 333333)
            };

            


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            atmThread[] atmThreads = new atmThread[3];
            for (int i = 0; i < atmThreads.Length; i++)
            {
                atmThreads[i] = new atmThread(accounts, i);
                atmThreads[i].Start();
            }
        }
    }
    public class Account
    {
        //the attributes for the account
        private int balance; // positive integer
        private int pin; // 4 digit num
        private int accountNum; // 6 digit num

        // a constructor that takes initial values for each of the attributes (balance, pin, accountNumber)
        public Account(int balance, int pin, int accountNum)
        {
            this.balance = balance;
            this.pin = pin;
            this.accountNum = accountNum;
        }

        //getter and setter functions for balance
        public int getBalance()
        {
            return balance;
        }

        public Boolean decrementBalance(int amount)
        {
            if (this.balance >= amount)
            {
                balance -= amount;
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean checkPin(int pinEntered)
        {
            if (pinEntered == pin)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int getAccountNum()
        {
            return accountNum;
        }

    }

    class atmThread
    {
        
        private Thread thread; // Define a Thread object

        // Constructor
        public atmThread(Account[] ac, int colour)
        {
            // Initialize the thread with a method to execute
            thread = new Thread(() =>
            {
                // Inside the thread, run the ATM login form
                Application.Run(new atmLogIn(ac, colour));
            });
        }

        // Method to start the thread
        public void Start()
        {
            // Start the thread
            thread.Start();
        }

        // Method to stop the thread
        public void Stop()
        {
            // Abort the thread
            thread.Abort();
        }
    }
}