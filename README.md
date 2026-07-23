# Assignment 2
 
This assignment consists of three different programs implemented using C# by following Object-Oriented Programming concepts. It demonstrates the usage of the main OOPS concepts like Abstraction and Inheritance.
 
## Task 1 – Shape Hierarchy
 
This program is used to calculate the area of different shapes using inheritance and polymorphism.
 
### Functionalities
 
### Create Shapes
 
1. You can create either a Rectangle or a Circle.
2. You can enter the required dimensions and the color of the shape.
3. Multiple validations are performed to ensure only valid positive values are accepted.
 
### Calculate Area
 
1. The area is calculated based on the selected shape.
2. Different implementations of the "CalculateArea()" method are used for Rectangle and Circle.
 
### Print Details
 
1. Displays the shape type, color and calculated area.
2. Uses polymorphism to display the correct details for each shape.
 
### Techniques Used for Implementation
 
- I used an abstract "Shape" class with "Rectangle" and "Circle" as derived classes.
- Services class is used to create the required shape objects.
- Helper class is used to validate the user inputs.
- ConsoleOperation class is used for all input and output operations.
- Manual Dependency Injection is used to connect the different layers.
 
---
 
## Task 2 – Employee Hierarchy
 
This program is used to calculate employee bonuses using inheritance and polymorphism.
 
### Functionalities
 
### Create Employee
 
1. You can create either a Developer or a Manager.
2. You can enter the employee's name and salary.
3. Multiple validations are performed for the name and salary.
 
### Calculate Bonus
 
1. Calculates the bonus based on the employee type.
2. Different bonus calculations are implemented for Developer and Manager.
 
### Print Details
 
1. Displays the employee name, designation, salary and bonus amount.
2. Uses polymorphism to display the correct employee details.
 
### Techniques Used for Implementation
 
- I used an abstract "Employee" class with "Developer" and "Manager" as derived classes.
- Services class is used to create employee objects.
- Helper class is used for validating employee details.
- ConsoleOperation class handles all user interactions.
- Manual Dependency Injection is used throughout the application.
 
---
 
## Task 3 – Banking System
 
This is a basic banking system implemented using C# following the MVC architecture.
 
### Functionalities
 
### Create Account
 
1. You can create either a Savings Account or a Checking Account.
2. Account numbers are generated automatically.
3. Savings Accounts require a minimum initial deposit.
4. Multiple validations are performed for the initial deposit.
 
### Display Accounts
 
1. Displays all the available bank accounts.
2. Proper handling is done when no accounts are available.
 
### Deposit
 
1. You can deposit money using the account number.
2. Validations are performed for both account number and amount.
3. Invalid inputs are limited to a maximum number of attempts.
 
### Withdraw
 
1. You can withdraw money using the account number.
2. Savings Accounts maintain a minimum balance after withdrawal.
3. Checking Accounts allow withdrawals as long as sufficient balance is available.
4. Proper validations and exception handling are performed before every withdrawal.
 
### Techniques Used for Implementation
 
- I used an abstract "BankAccount" class with "SavingsAccount" and "CheckingAccount" as derived classes.
- Repository class is used to store and manage the bank accounts.
- Services class is used to perform banking operations.
- Helper class is used to validate account numbers and transaction amounts.
- ConsoleOperation class is used for all input and output operations.
- Manual Dependency Injection is used to connect the different layers.
 
# Challenges Faced
 
- Understanding and properly implementing abstraction and inheritance was initially challenging.
- Identifying and removing duplicate code while keeping the application simple was another challenge.
