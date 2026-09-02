# Expense Tracker Application
 
This is an Expense Tracker Console-based application developed in C# using the MVC architecture. It allows users to manage their financial records such as incomes and expenses and make financial summaries.
 
## Features

- Multi User Support
- Add Record
- View Records
- Search Records
- Delete Record
- Edit Record
- Financial Summary
 
---
 
# Feature Details

## Multi User Support

The application supports multiple users with isolated financial records.

### Functionalities

1. Multiple users can use the application independently.
2. Each user's transactions are stored in a separate CSV file.
3. User data is isolated and cannot be accessed by other users.
4. Only the currently logged-in user's records are loaded into memory.

---
 
## Add Record
 
The "Add Record" feature is used to add an income or an expense record to the repository according to the user's choice.
 
### Functionalities
 
1. Users can enter date, amount spent or gained, source of income (or) category of expense and an optional description.
2. A unique product ID is generated automatically when making a financial record. It is a 12-digit alphanumeric string extracted from GUID.
3. Record date, amount and classification of income/expense are validated before the product is added.
4. A menu is built to add records according to the user's choice.
 
---

## View Records
 
The "View Records" feature displays income records, expense records, or all records within an optional date range.
 
### Functionalities

1. Users can view the entire record list or the entire income list or the entire expense list.
2. Records can be filtered using an optional start date and end date.
3. Users can:
   - View records between a start date and end date.
   - View records from a specific date until today.
   - View records until a specified end date.
   - View all records by skipping both dates.
4. Records are displayed in a formatted table.
5. A menu is built to display the records according to user's preference.
 
---

## Search Records
 
The "Search Records" feature retrieves records with the date or amount or classification (Source of Income / Category of Expense) of the record.
 
### Functionalities
 
1. Users can search for a specific date or a specific amount they spent/gained or specific classification of income/expense.
2. The inputs are validated before searching.
3. The matching records will be shown to the user in a formatted table.
 
---

## Delete Record
 
The "Delete Record" feature removes a record from the repository according to the user's search input.
 
### Functionalities
 
1. Users can delete a specific record using the date or amount or classification of the record.
2. The input is validated before searching and deleting a specific record.
3. If multiple record exists, then the search results are displayed and the user has to enter the specific ID of the record.
4. The input ID is again validated before deleting.
5. The separate record is printed and asked for confirmation before deleting.
 
---
 
## Edit Record
 
The "Edit Record"" feature allows updating an existing record in the repository.
 
### Functionalities
 
1. Users can edit a record's date, amount, classification (income/expense) and the description of the record.
2. The record used for editing can be searched using the date, amount or classification of the record.
3. A menu is built to ask for specific edit choices from the user.
4. Multiple edits can be performed until the user exits the edit menu.

---

## Financial Summary

The "Financial Summary" feature provides an overview of the user's financial activity within an optional date range.

### Functionalities

1. Users can generate a financial summary for a specific date range.
2. Both start date and end date are optional.
3. Users can:
   - View summary between two dates.
   - View summary from a selected date until today.
   - View summary until a selected end date.
   - View an overall financial summary.
4. Displays:
   - Net Income
   - Net Expense
   - Net Balance
   - Savings Rate
   - Highest Expense and its Category

---

# Project Architecture
 
The application follows the MVC architecture.
 
**Model → Repository → Service → Controller → View**
 
## Model
 
Stores the record information and serves as a template for storage.
 
## Repository
 
The application uses CSV files for persistent storage of user and transaction data.

## Data Storage Structure

```text
Data
├── Users.csv
└── Transactions
    ├── UserId1.csv
    ├── UserId2.csv
    └── UserId3.csv
    └── ...
```
 
## Service
 
Communicates with the repository and coordinates application level operations for the record instances.
 
## Controller
 
Coordinates the application flow by receiving user input, validating data using helper methods, invoking service methods and directing the appropriate view.
 
## View
 
Handles all console input and output operations, including menus, prompts and displaying messages.
 
## Helper
 
Provides reusable validation methods for date, amount, classification of income or expense, etc.,
 
---
 
# Techniques Used
 
- The application uses either an in-memory list of records, or a CSV file to store the records.
- The Repository layer performs CRUD operations.
- The Service layer communicates with the repository.
- The Controller layer coordinates the application flow.
- The View layer handles all console input and output operations.
- The Helper class performs reusable input validation.
- ConsoleTables is used to display records in a tabular format.
 
---

# Recent Enhancements

- Date range filtering is supported for viewing records and generating financial summaries.
- Generic methods are used to reduce duplicate logic when retrieving records by type and date range.
- Exception handling is implemented at the application entry point to gracefully handle unexpected errors.
- User-specific CSV files are maintained for optimized data loading.
- Only the active user's records are loaded into memory during login.
- Multi-user support is implemented with isolated transaction storage.

---

# Project Structure

```text
ExpenseTracker
│
├── ConstantLiteral
│   └── Constant.cs
|
├── Controller
│   └── ApplicationController.cs
│   └── AuthenticationController.cs
│
├── CsvUtils
│   └── CsvHandler.cs
|
├── Enums
│   └── AddMenuOption.cs
│   └── AuthenticationOptionMenu.cs
|   └── EditMenuOption.cs
│   └── ExpenseCategory.cs
|   └── IncomeSource.cs
│   └── MainMenuOption.cs
|   └── MessageType.cs
│   └── RecordType.cs
│   └── SearchType.cs
|   └── ViewMenuOption.cs
│
├── Helper
│   └── AuthenticationHelper.cs
│   └── ConsoleMessages.cs
|   └── FinanceHelper.cs
│
├── Model
│   └── AuthenticatedUser.cs
│   └── Expense.cs
|   └── FinancialRecord.cs
│   └── Income.cs
│   └── User.cs
│
├── Repository
│   └── CsvFinanceRepository.cs
|   └── CsvUserRepository.cs
│   └── IFinanceRepository.cs
│   └── IUserRepository.cs
│
├── Service
│   └── AuthenticationService.cs
│   └── FinanceService.cs
│   └── UserService.cs
│
├── View
│   └── ApplicationView.cs
│   └── AuthenticationView.cs
│
├── Program.cs
│
└── README.md
```

## File Overview

- **Constant.cs**: Stores application-wide constants such as file paths, CSV headers, validation limits, and reusable constant values.

- **ApplicationController.cs**: Coordinates all finance-related operations such as adding, viewing, editing, deleting, searching records, and generating financial summaries.

- **AuthenticationController.cs**: Manages user authentication workflows including user registration, login, logout, and account deletion.

- **CsvHandler.cs**: Provides reusable CSV file operations such as reading, writing, updating, and managing user-specific CSV files.

- **AddMenuOption.cs**: Represents the available options for adding financial records.

- **AuthenticationOptionMenu.cs**: Represents the available options in the authentication menu such as login and registration.

- **EditMenuOption.cs**: Represents the available options for editing financial records.

- **ExpenseCategory.cs**: Contains the supported expense categories used when creating expense records.

- **IncomeSource.cs**: Contains the supported income sources used when creating income records.

- **MainMenuOption.cs**: Represents the available options in the application's main menu.

- **MessageType.cs**: Defines the different message categories displayed to the user such as information, success, warning, and error messages.

- **RecordType.cs**: Represents the different financial record types available in the application.

- **SearchType.cs**: Represents the available search criteria for locating records.

- **ViewMenuOption.cs**: Represents the available options in the record viewing menu.

- **AuthenticationHelper.cs**: Provides reusable validation logic for authentication-related operations such as username, password, and account validation.

- **ConsoleMessages.cs**: Stores reusable application messages displayed throughout the console interface.

- **FinanceHelper.cs**: Provides reusable validation methods for financial record inputs such as dates, amounts, classifications, and identifiers.

- **AuthenticatedUser.cs**: Stores information about the currently logged-in user and maintains the active user session.

- **Expense.cs**: Represents an expense record and extends the FinancialRecord base class.

- **FinancialRecord.cs**: Serves as the base model for all financial records and contains common properties shared by incomes and expenses.

- **Income.cs**: Represents an income record and extends the FinancialRecord base class.

- **User.cs**: Represents a user account and stores user-related information.

- **CsvFinanceRepository.cs**: Handles storage and retrieval of financial records from user-specific CSV files.

- **CsvUserRepository.cs**: Handles storage and retrieval of user account information from CSV files.

- **IFinanceRepository.cs**: Defines the contract for finance-related repository operations.

- **IUserRepository.cs**: Defines the contract for user-related repository operations.

- **AuthenticationService.cs**: Contains business logic for user authentication, registration, login, logout, and account management.

- **FinanceService.cs**: Contains business logic for financial record management, date range filtering, searching, editing, deleting, and financial summaries.

- **UserService.cs**: Provides user-related operations and coordinates interactions between user repositories and authentication services.

- **ApplicationView.cs**: Handles all console interactions related to financial record management, menus, prompts, and data display.

- **AuthenticationView.cs**: Handles all console interactions related to authentication such as login, registration, and account management.

- **Program.cs**: Serves as the application's entry point, initializes dependencies, configures repositories, services, controllers, handles application-level exceptions, and starts the application workflow.

---

# Recommended PR Review Order

For the best understanding of the implementation, review the projects in the following order:

```text
Enums & Constants
↓
Model
↓
Repository
↓
Service
↓
Helper
↓
View
↓
Controller
↓
Program.cs
```

This order follows the dependency flow of the application and helps to understand how each functionality works. Backtracking between layers can also be done to see the full picture of the functionality of the code.

---
 
# How to Run the Application
 
## Prerequisites
 
- .NET 6 SDK or later
- Visual Studio 2022 or Visual Studio Code
 
## Steps
 
1. Clone or download the project.
2. Open the solution in Visual Studio.
3. Build the project.
4. Run the application.
 
---
 
# Challenges Faced

- Implementing the MVC architecture while maintaining proper separation of concerns between Model, Repository, Service, Controller and View layers.
- Implementing flexible search functionality based on date, amount and classification.
- Supporting CSV-based storage while maintaining consistent CRUD operations.
- Managing income and expense records using a common base class.
- Designing optional date-range filtering across multiple features.
- Implementing multi-user support while maintaining data isolation.
- Optimizing CSV loading to improve application performance.
- Reducing code duplication through generic methods.
 
---