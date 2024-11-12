using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;



public class Program{


    private static List<User> users = new List<User>();
    private static User currentUser = null;

    public static void Main()
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine("\n1. Register\n2. Login\n3. Exit");
            Console.Write("Select an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Register();
                    break;
                case "2":
                    Login();
                    break;
                case "3":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }

    public static void Register()
    {
        string username;
        while (true)
        {
            Console.Write("Enter username (alphanumeric, 4-12 characters): ");
            username = Console.ReadLine();

            if (!IsValidUsername(username))
            {
                Console.WriteLine("Invalid username. It must be 4-12 characters long and contain only alphanumeric characters.");
            }
            else if (users.Exists(u => u.Username == username))
            {
                Console.WriteLine("Username is already taken. Please try a different username.");
            }
            else
            {
                break;
            }
        }

        string password;
        while (true)
        {
            Console.Write("Enter password (at least 6 characters, including uppercase, lowercase, and a number): ");
            password = Console.ReadLine();

            if (IsStrongPassword(password))
                break;

            Console.WriteLine("Password is not strong enough. Please try again.");
        }

        users.Add(new User(username, password));


        Console.WriteLine();
        Console.WriteLine("---------------------------------------------------------------------");
        Console.WriteLine();
        Console.WriteLine("Registration successful.");
        Console.WriteLine();
        Console.WriteLine("---------------------------------------------------------------------");
    }

    public static bool IsValidUsername(string username)
    {
        return username.Length >= 4 && username.Length <= 12 && Regex.IsMatch(username, @"^[a-zA-Z0-9]+$");
    }

    public static bool IsStrongPassword(string password)
    {
        return password.Length >= 6 &&
               Regex.IsMatch(password, @"[A-Z]") && // At least one uppercase letter
               Regex.IsMatch(password, @"[a-z]") && // At least one lowercase letter
               Regex.IsMatch(password, @"\d");      // At least one digit
    }

    public static void Login()
    {
        Console.Write("Enter username: ");
        string username = Console.ReadLine();
        Console.Write("Enter password: ");
        string password = Console.ReadLine();

        currentUser = users.Find(u => u.Username == username && u.Password == password);
        if (currentUser == null)
        {
            Console.WriteLine("Invalid credentials.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("---------------------------------------------------------------------");
        Console.WriteLine();
        Console.WriteLine("Login successful.");
        Console.WriteLine();
        Console.WriteLine("---------------------------------------------------------------------"); AccountMenu();
    }

    public static void AccountMenu()
    {
        bool inAccountMenu = true;
        while (inAccountMenu)
        {
            Console.WriteLine("\n1. Open Account\n2. Deposit\n3. Withdraw\n4. Check Balance\n5. Generate Statement\n6. Calculate Interest (savings only)\n7. Logout");
            Console.Write("Select an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    currentUser.OpenAccount();
                    break;
                case "2":
                    currentUser.MakeTransaction("deposit");
                    break;
                case "3":
                    currentUser.MakeTransaction("withdraw");
                    break;
                case "4":
                    currentUser.CheckBalance();
                    break;
                case "5":
                    currentUser.GenerateStatement();
                    break;
                case "6":
                    currentUser.CalculateInterest();
                    break;
                case "7":
                    inAccountMenu = false;
                    currentUser = null;
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }
}




public class User
{
    public string Username { get; }
    public string Password { get; }
    private List<Account> accounts = new List<Account>();

    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public void OpenAccount()
    {
        // Collecting first and last name separately
        Console.Write("Enter account holder's first name: ");
        string firstName = Console.ReadLine();

        Console.Write("Enter account holder's last name: ");
        string lastName = Console.ReadLine();

        // Providing options for account type
        string accType;
        while (true)
        {
            Console.Write("Select type of account (a. Savings, b. Checking): ");
            string choice = Console.ReadLine().ToLower();

            if (choice == "a")
            {
                accType = "savings";
                break;
            }
            else if (choice == "b")
            {
                accType = "checking";
                break;
            }
            else
            {
                Console.WriteLine("Invalid selection. Please choose 'a' for Savings or 'b' for Checking.");
            }
        }

        // Initial deposit input
        decimal initialDeposit;
        while (true)
        {
            Console.Write("Enter initial deposit amount: ");
            if (decimal.TryParse(Console.ReadLine(), out initialDeposit) && initialDeposit > 0)
            {
                break;
            }
            Console.WriteLine("Invalid amount. Please enter a positive decimal value.");
        }

        // PAN number validation
        string panNumber;
        while (true)
        {
            Console.Write("Enter PAN number: ");
            panNumber = Console.ReadLine();

            if (IsValidPAN(panNumber))
            {
                break;
            }
            Console.WriteLine("Invalid PAN number. It should follow the format (e.g., ABCDE1234F).");
        }

        // Address input
        Console.Write("Enter address: ");
        string address = Console.ReadLine();

        // Contact number validation
        string contactNumber;
        while (true)
        {
            Console.Write("Enter contact number: ");
            contactNumber = Console.ReadLine();

            if (IsValidContactNumber(contactNumber))
            {
                break;
            }
            Console.WriteLine("Invalid contact number. Please enter a valid 10-digit number.");
        }

        // Email validation
        string email;
        while (true)
        {
            Console.Write("Enter email address: ");
            email = Console.ReadLine();

            if (IsValidEmail(email))
            {
                break;
            }
            Console.WriteLine("Invalid email format. Please enter a valid email address.");
        }

        // Create account and add to list
        var newAccount = new Account($"{firstName} {lastName}", accType, initialDeposit, panNumber, address, contactNumber, email);
        accounts.Add(newAccount);

        // Print success message and account details
        Console.WriteLine();
        Console.WriteLine("---------------------------------------------------------------------");
        Console.WriteLine();
        Console.WriteLine("Account opened successfully!");

        Console.WriteLine($"Account Holder: {newAccount.HolderName}");
        Console.WriteLine($"Account Type: {newAccount.AccountType}");
        Console.WriteLine($"Initial Deposit: {newAccount.Balance:C}");
        Console.WriteLine($"PAN Number: {panNumber}");
        Console.WriteLine($"Address: {newAccount.Address}");
        Console.WriteLine($"Contact Number: {newAccount.ContactNumber}");
        Console.WriteLine($"Email: {newAccount.Email}");
        Console.WriteLine();

        // Highlighting the account number
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow; // Change text color to highlight
        Console.WriteLine($"*** IMPORTANT: Your Account Number is: {newAccount.AccountNumber} ***");
        Console.ResetColor(); // Reset text color to default

        Console.WriteLine();
        Console.WriteLine("Please make a note of this number as it will be required for all future transactions.");
        Console.WriteLine();

        Console.WriteLine("---------------------------------------------------------------------");

    }


    // Helper method to validate PAN number format (e.g., "ABCDE1234F")
    private bool IsValidPAN(string pan)
    {
        return Regex.IsMatch(pan, @"^[A-Z]{5}[0-9]{4}[A-Z]{1}$");
    }

    // Helper method to validate email format
    private bool IsValidEmail(string email)
    {
        return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }

    // Helper method to validate contact number (10 digits)
    private bool IsValidContactNumber(string contactNumber)
    {
        return Regex.IsMatch(contactNumber, @"^\d{10}$");
    }


    public void MakeTransaction(string type)
    {
        Console.Write("Enter account number: ");
        string accNum = Console.ReadLine();
        Account account = accounts.Find(a => a.AccountNumber == accNum);

        if (account == null)
        {
            Console.WriteLine("Account not found.");
            return;
        }

        Console.Write("Enter amount: ");
        decimal amount = decimal.Parse(Console.ReadLine());

        if (type == "deposit")
        {
            account.Deposit(amount);
        }
        else if (type == "withdraw")
        {
            account.Withdraw(amount);
        }
    }

    public void CheckBalance()
    {
        Console.Write("Enter account number: ");
        string accNum = Console.ReadLine();
        Account account = accounts.Find(a => a.AccountNumber == accNum);

        if (account != null)
        {
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine($"Current Balance: {account.Balance:C}");
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------");
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Account not found.");
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------");
        }
    }

    public void GenerateStatement()
    {
        Console.Write("Enter account number: ");
        string accNum = Console.ReadLine();
        Account account = accounts.Find(a => a.AccountNumber == accNum);

        if (account != null)
        {
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Transaction History:");
            foreach (var t in account.Transactions)
            {
                Console.WriteLine($"{t.Date} | {t.Type} | {t.Amount:C}");
            }
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------");
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Account not found.");
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------");
        }
    }

    public void CalculateInterest()
    {
        Console.Write("Enter account number: ");
        string accNum = Console.ReadLine();
        Account account = accounts.Find(a => a.AccountNumber == accNum);

        if (account != null && account.AccountType == "savings")
        {
            // Check if interest has been added for the current month
            if (account.LastInterestAdded.Month != DateTime.Now.Month || account.LastInterestAdded.Year != DateTime.Now.Year)
            {
                decimal interestRate = 0.05m; // 5% annual interest rate for example
                decimal monthlyInterest = account.Balance * (interestRate / 12); // Monthly interest calculation

                // Adding interest to the account balance
                account.Balance += monthlyInterest;


                // Update the last interest added date
                account.LastInterestAdded = DateTime.Now;

                Console.WriteLine();
                Console.WriteLine("---------------------------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine($"Monthly interest of {monthlyInterest:C} has been added to the account.");
                Console.WriteLine($"New balance: {account.Balance:C}");
                Console.WriteLine("---------------------------------------------------------------------");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("---------------------------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine("Interest has already been added for this month.");
                Console.WriteLine();
                Console.WriteLine("---------------------------------------------------------------------");
            }
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine("Interest calculation is only applicable to savings accounts.");
            Console.WriteLine("---------------------------------------------------------------------");
        }
    }

}




public class Account
{
    private static int nextAccNum = 1000;
    public string AccountNumber { get; }
    public string HolderName { get; }
    public string AccountType { get; }
    public decimal Balance { get; set; }

    public string IDVerification { get; }
    public string Address { get; }
    public string ContactNumber { get; }
    public string Email { get; }
    public DateTime LastInterestAdded { get; set; }

    public List<Transaction> Transactions { get; }

    public Account(string holderName, string accountType, decimal initialDeposit, string panNumber, string address, string contactNumber, string email)
    {
        AccountNumber = nextAccNum.ToString();
        nextAccNum++;
        HolderName = holderName;
        AccountType = accountType;
        Balance = initialDeposit;

        IDVerification = panNumber;
        Address = address;
        ContactNumber = contactNumber;
        Email = email;
        LastInterestAdded = DateTime.MinValue;

        Transactions = new List<Transaction>
        {
            new Transaction("Initial Deposit", initialDeposit)
        };
    }



    public void Deposit(decimal amount)
    {
        Balance += amount;
        Transactions.Add(new Transaction("Deposit", amount));
        Console.WriteLine();
        Console.WriteLine("---------------------------------------------------------------------");
        Console.WriteLine();
        Console.WriteLine($"Deposited {amount:C}");
        Console.WriteLine($"New Balance: {Balance:C}");
        Console.WriteLine();
        Console.WriteLine("---------------------------------------------------------------------");
    }

    public void Withdraw(decimal amount)
    {
        if (Balance >= amount)
        {
            Balance -= amount;
            Transactions.Add(new Transaction("Withdraw", amount));

            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine($"Withdrew {amount:C}.");
            Console.WriteLine($"New Balance: {Balance:C}");
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------");
        }
        else
        {
            Console.WriteLine("Insufficient funds.");
        }
    }

    

    public void AddMonthlyInterest()
    {
        decimal interestRate = 0.05m; 
        decimal monthlyInterest = Balance * (interestRate / 12); 
        Balance += monthlyInterest;
        LastInterestAdded = DateTime.Now; 
    }
}

public class Transaction
{
    public DateTime Date { get; }
    public string Type { get; }
    public decimal Amount { get; }

    public Transaction(string type, decimal amount)
    {
        Date = DateTime.Now;
        Type = type;
        Amount = amount;
    }
}

