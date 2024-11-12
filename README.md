# Banking System Console Application

This is a simple C# console-based banking application that simulates a basic banking system. It includes features for user registration, login, account creation, and basic account operations, such as deposits, withdrawals, balance checks, and transaction history generation. Additionally, it supports interest calculation for savings accounts.

## Features

### User Registration and Login
- **Register**: Users can register with a unique username (4-12 alphanumeric characters) and a secure password (minimum 6 characters, must include uppercase, lowercase, and a digit).
- **Login**: Registered users can log in to access their accounts.

### Account Management
- **Open Account**: Users can open an account by providing their first and last names, selecting an account type (savings or checking), and making an initial deposit. Users are also prompted to provide their PAN, address, contact number, and email.
- **Deposit & Withdraw**: Users can deposit and withdraw money from their accounts.
- **Check Balance**: Users can view the current balance of their accounts.
- **Generate Statement**: Users can view a history of all transactions made in their accounts.
- **Calculate Interest**: Only available for savings accounts; calculates monthly interest at a 5% annual rate, applicable once per month.

### Data Validation
The application validates:
- **PAN Number**: Should follow the format ""ABCDE1234F"".
- **Email Address**: Must be a valid email format.
- **Contact Number**: Must be a 10-digit number.

## Classes
- **Program**: The main program class that handles user registration, login, and navigation through account operations.
- **User**: Manages each user’s details and methods to open accounts, perform transactions, check balances, and calculate interest.
- **Account**: Represents a bank account, storing account information and transaction history.
- **Transaction**: Represents individual transactions, storing details such as date, type, and amount.

## Running the Application

1. **Registration**: Start the application and select the "Register" option from the main menu. 
   - Provide a unique username (4-12 alphanumeric characters).
   - Set a secure password (at least 6 characters, including uppercase, lowercase, and a digit).

2. **Login**: Once registered, return to the main menu and select the "Login" option.
   - Enter your registered username and password to access your account.

3. **Account Operations**: After logging in, you will be able to perform various banking operations:
   - **Open Account**: Provide your first and last names, choose an account type (savings or checking), make an initial deposit, and enter details like PAN, address, contact number, and email.
   - **Deposit & Withdraw**: Select either "Deposit" or "Withdraw" from the account menu and enter the amount to adjust your account balance.
   - **Check Balance**: View the current balance of your account.
   - **Generate Statement**: View a list of all transactions associated with your account.
   - **Calculate Interest** (Savings Accounts Only): Select this option to add monthly interest to your savings account balance, if it hasn’t been added for the current month.

4. **Logout**: When finished, select "Logout" from the account menu to safely end your session. This will return you to the main menu, where you can log in with another account or exit the application.
