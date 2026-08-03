# Assignment 2
 
This assignment consists of three different console-based applications developed in C#. It demonstrates the implementation of Object-Oriented Programming concepts such as Abstraction, Inheritance, and Polymorphism.
 
---
 
# Task 1 – Shape Hierarchy
 
This application calculates the area of different shapes using inheritance and polymorphism.
 
## Features
 
- Create Rectangle
- Create Circle
- Calculate Area
- Print Shape Details
 
---
 
## Feature Details
 
### Create Shapes
 
The Create Shape feature allows users to create a Rectangle or Circle.
 
#### Functionalities
 
1. Users can choose between Rectangle and Circle.
2. Users can enter the color and dimensions of the selected shape.
3. The entered color and dimensions are validated before creating the object.
 
---
 
### Calculate Area
 
The Calculate Area feature calculates the area of the selected shape.
 
#### Functionalities
 
1. Calculates the area based on the selected shape.
2. Different implementations of the `CalculateArea()` method are used for Rectangle and Circle.
 
---
 
### Print Details
 
The Print Details feature displays the complete information of the created shape.
 
#### Functionalities
 
1. Displays the shape type.
2. Displays the color and dimensions.
3. Displays the calculated area.
4. Uses polymorphism to display the appropriate details.
 
---
 
# Techniques Used
 
- Abstract classes
- Inheritance
- Method overriding
- Polymorphism
- Console-based user interaction
- Input validation
 
---
 
# Task 2 – Employee Hierarchy
 
This application calculates employee bonuses using inheritance and polymorphism.
 
## Features
 
- Create Developer
- Create Manager
- Calculate Bonus
- Print Employee Details
 
---
 
## Feature Details
 
### Create Employee
 
The Create Employee feature allows users to create an employee.
 
#### Functionalities
 
1. Users can choose between Developer and Manager.
2. Users can enter the employee's name and salary.
3. The entered details are validated before creating the object.
 
---
 
### Calculate Bonus
 
The Calculate Bonus feature calculates the employee bonus.
 
#### Functionalities
 
1. Calculates the bonus based on the employee type.
2. Different implementations of the `CalculateBonus()` method are used for Developer and Manager.
 
---
 
### Print Details
 
The Print Details feature displays complete employee information.
 
#### Functionalities
 
1. Displays the employee name.
2. Displays the employee designation.
3. Displays the salary and calculated bonus.
4. Uses polymorphism to display the appropriate details.
 
---
 
# Techniques Used
 
- Abstract classes
- Inheritance
- Method overriding
- Polymorphism
- Console-based user interaction
- Input validation
 
---
 
# Task 3 – Banking System
 
This application is a basic banking system implemented using Object-Oriented Programming concepts.
 
## Features
 
- Create Savings Account
- Create Checking Account
- Display Accounts
- Deposit Money
- Withdraw Money
 
---
 
## Feature Details
 
### Create Account
 
The Create Account feature allows users to create a bank account.
 
#### Functionalities
 
1. Users can create either a Savings Account or a Checking Account.
2. A unique 10-digit account number is generated automatically.
3. Savings Accounts require a minimum initial deposit of Rs. 1000.
4. The entered amount is validated before account creation.
 
---
 
### Display Accounts
 
The Display Accounts feature displays all created bank accounts.
 
#### Functionalities
 
1. Displays all available bank accounts.
2. Displays an appropriate message if no accounts are available.
 
---
 
### Deposit
 
The Deposit feature allows users to deposit money into an account.
 
#### Functionalities
 
1. Users can deposit money using the account number.
2. The account number and amount are validated.
3. Invalid inputs are limited to a maximum of three attempts.
4. The updated balance is displayed after a successful deposit.
 
---
 
### Withdraw
 
The Withdraw feature allows users to withdraw money from an account.
 
#### Functionalities
 
1. Users can withdraw money using the account number.
2. Savings Accounts maintain a minimum balance of Rs. 1000 after withdrawal.
3. Checking Accounts allow withdrawals based on the available balance.
4. The updated balance is displayed after a successful withdrawal.
 
---
 
# Techniques Used
 
- Abstract classes
- Inheritance
- Method overriding
- Polymorphism
- List of objects for storing bank accounts
- Random generation of unique account numbers
- Console-based user interaction
- Input validation
 
---
 
# How to Run the Applications
 
## Prerequisites
 
- .NET 6 SDK or later
- Visual Studio 2022 or Visual Studio Code
 
## Steps
 
1. Clone or download the project.
2. Open the solution in Visual Studio.
3. This solution consists of 3 different projects.
4. Build solution and set 1 of the 3 projects as the startup project.
5. Run the project.
 
---
 
# Challenges Faced
 
- Understanding and implementing abstraction, inheritance, and polymorphism.
- Designing reusable methods to reduce duplicate code.
- Implementing input validation while keeping the code clean and readable.
 
---
 
# Future Enhancements
 
- Add file or database storage for the Banking System.
- Add more shapes, employee types, and bank account types.
- Improve the console user interface.