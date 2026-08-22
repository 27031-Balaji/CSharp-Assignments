# Expense Tracker Application
 
This is an Expense Tracker Console-based application developed in C# using the MVC architecture. It allows users to manage their financial records such as incomes and expenses and make financial summaries.
 
## Features
 
- Add Record
- View Records
- Search Records
- Delete Record
- Edit Record
- Financial Summary
 
---
 
# Feature Details
 
## Add Record
 
The "Add Record" feature is used to add an income or an expense record to the repository according to the user's choice.
 
### Functionalities
 
1. Users can enter date, amount spent or gained, source of income (or) category of expense and an optional description.
2. A unique product ID is generated automatically when making a financial record. It is a 12-digit alphanumeric string extracted from GUID.
3. Record date, amount and classification of income/expense are validated before the product is added.
4. A menu is built to add records according to the user's choice.
 
---

## View Records
 
The "View Records" feature displays all incomes or expenses or all the records in the repository according to the user's choice.
 
### Functionalities
 
1. Users can view the entire record list or the entire income list or the entire expense list.
2. Records are displayed in a formatted table.
3. A menu is built to display the records according to user's preference.
 
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
 
# Project Architecture
 
The application follows the MVC architecture.
 
**Model → Repository → Service → Controller → View**
 
## Model
 
Stores the record information and serves as a template for storage.
 
## Repository
 
Stores the records in either a record list in memory or a CSV file.
 
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

# Project Structure

```text
ExpenseTracker
│
├── Constant
│   └── CsvConstant.cs
|
├── Controller
│   └── ExpenseController.cs
│
├── Enums
│   └── AddMenuOption.cs
|   └── EditMenuOption.cs
│   └── ExpenseCategoryType.cs
|   └── IncomeSourceType.cs
│   └── MainMenuOption.cs
|   └── MessageType.cs
│   └── SearchType.cs
|   └── ViewOptionMenu.cs
│
├── Helper
│   └── ConsoleMessages.cs
|   └── ExpenseHelper.cs
│
├── Model
│   └── Expense.cs
|   └── FinancialRecord.cs
│   └── Income.cs
│
├── Repository
│   └── CsvExpenseRepository.cs
|   └── IExpenseRepository.cs
│   └── InMemoryExpenseRepository.cs
│
├── View
│   └── ConsoleOperation.cs
│
├── Program.cs
│
└── README.md
```

## File Overview

- **CsvConstant.cs**: Used to store the CSV file header and the file path.
- **ExpenseController.cs**: Acts as an intermediate between view and services by validating the input given by the user and passing to service layer.
- **MainMenuOption.cs**: Used for segregating the input choice given in the main menu.
- **AddMenuOption.cs**: Used for segregating the input choice given for the add operation.
- **EditMenuOption.cs**: Used for segregating the input choice given for the edit operation.
- **ViewOptionMenu.cs**: Used for segregating the input choice given for the view operation.
- **ExpenseCategory.cs**: Used to store the different categories of expense.
- **IncomeSource.cs**: Used to store the different sources of income.
- **MessageType.cs**: Used to separate the different types of messages coming from the controller.
- **SearchType.cs**: Used to find the search type of the user according to the user's input.
- **ConsoleMessages.cs**: Used to store multiple console messages to be displayed to the user.
- **ExpenseHelper.cs**: Helps to validate the inputs given by the user.
- **Expense.cs**: A template of the user's expense record, inherited from the financial record class.
- **FinancialRecord.cs**: A template of the user's record, serves as a base class.
- **Income.cs**: A template of the user's income record, inherited from the financial record class.
- **CsvExpenseRepository.cs**: Repository class mainly used to store and retrieve the records from a CSV file.
- **IExpenseRepository.cs**: The base interface used as a contract for all the repository classes.
- **InMemoryExpenseRepository.cs**: Repository class mainly used to store and retrieve from a in-memory list of records.
- **ExpenseService.cs**: Communicates with the repository for storing and retrieving details.
- **ConsoleOperation.cs**: Handles all the UI operations and interacts with the user.
- **Program.cs**: Creates objects and passes on to the constructors and runs the controller.

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
 
- Implementing the MVC architecture while maintaining proper separation of concerns between Model, Repository, Service, Controller, and View layers.
- Implementing flexible search functionality based on date, amount and classification.
- Supporting in memory and CSV based storage while maintaining constant CRUD operations.
- Managing two different record types into a common base class.
 
---
 
# Future Enhancements
 
- Store records in a database instead of CSV.
- Implement yearly financial reports.
- Enhance the console UI.