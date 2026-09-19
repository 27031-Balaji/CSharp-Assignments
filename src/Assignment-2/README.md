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

# Understanding of OOP Concepts
 
Through this assignment, I got a better understanding of how **Abstraction, Inheritance, and Polymorphism** actually work in C# and how they are connected to each other.
 
## Abstraction
 
An **abstract class** is used when we have a common base class, but we don't want to create an object of that class directly.
 
An abstract class can have both **normal methods** and **abstract methods**.
 
An **abstract method** only defines what the method should be, but does not contain the actual implementation. The derived classes are responsible for implementing it using override keyword.
 
For example, in the Banking System, BankAccount can have an abstract Withdraw() method because the withdrawal rules are different for SavingsAccount and CheckingAccount. At the same time, common functionality like Deposit() can be implemented directly in BankAccount.
 
## Inheritance
 
**Inheritance** allows a derived class to use the members of a base class and add its own functionality on top of it.
 
The different types of inheritance I learned are:
 
- **Single inheritance** – one derived class inherits from one base class.
- **Multilevel inheritance** – a class inherits from another derived class, creating a chain of inheritance.
- **Hierarchical inheritance** – multiple classes inherit from the same base class.
- **Multiple inheritance** – a class inherits from multiple classes. C# does not support this for classes.
 
For example, in the Banking System, SavingsAccount and CheckingAccount both inherit from BankAccount. They get the common functionality from BankAccount while implementing their own account-specific behavior.
 
## virtual, override, and new
 
I learned that these keywords are related to how inherited methods behave, but they are not the same.
 
- **virtual** allows a base class to provide a default implementation while giving derived classes the option to change it.
- **override** is used when a derived class wants to provide its own implementation of a virtual or abstract method.
- **`new`** is used to hide a member from the base class instead of overriding it. Because of this, new does not provide the same runtime polymorphic behavior as override.
 
The main difference I understood is:
 
```text
virtual
    ↓
Base class provides an implementation
    ↓
Derived class may override it
 
abstract
    ↓
Base class does not provide an implementation
    ↓
Derived class must override it
 
new
    ↓
Hides the base class member
    ↓
Does not behave like an override
```
 
## Base Class Reference and Derived Class Object
 
I also learned why we can do something like this:
 
```text
BankAccount account = new SavingsAccount();
```
 
Here, BankAccount is the **reference type**, while SavingsAccount is the **actual object type**.
 
The reference type decides which members I can access through the reference. However, when the method is overridden using virtual/override or abstract/override, the actual object type decides which implementation is executed at runtime.
 
This helped me understand how runtime polymorphism actually works.
 
## Polymorphism
 
**Polymorphism** means that the same method or operation can behave differently depending on the situation.
 
### Compile-time Polymorphism
 
Compile-time polymorphism is commonly achieved through **method overloading**.
 
For example, we can have multiple methods with the same name but different parameters:
 
```text
Add(int a, int b)
Add(int a, int b, int c)
Add(double a, double b)
```
 
The compiler can determine which method to call based on the arguments passed to the method.
 
### Runtime Polymorphism
 
Runtime polymorphism is achieved through **method overriding** using `virtual`/`override` or `abstract`/`override`.
 
For example:
 
```text
BankAccount account = new SavingsAccount();
account.Withdraw(500);
 
account = new CheckingAccount();
account.Withdraw(500);
```
 
The method call is the same. But the implementation that runs can be different depending on the actual object.
 
This helped me understand why a base class reference can point to different derived class objects and still execute the correct implementation at runtime.
 
## Overall Understanding
 
The main thing I understood from these concepts is how they all connect:
 
```text
Abstraction
    ↓
Create a common base and define what is required
 
Inheritance
    ↓
Reuse the common functionality in derived classes
 
Polymorphism
    ↓
Allow derived classes to behave differently
while using the same base class reference
```
 
Working on the Shape, Employee, and Banking System tasks helped me understand these concepts practically instead of just learning their definitions.

---

# Project Structure

```text
Assignment-2
│
├── Task1-ShapeHierarchy
│   ├── Shape.cs
│   ├── Rectangle.cs
│   ├── Circle.cs
│   └── Program.cs
│
├── Task2-EmployeeHierarchy
│   ├── Employee.cs
│   ├── Developer.cs
│   ├── Manager.cs
│   └── Program.cs
│
├── Task3-BankingSystem
│   ├── BankAccount.cs
│   ├── SavingsAccount.cs
│   ├── CheckingAccount.cs
│   └── Program.cs
│
└── README.md
```

## File Overview

### Task 1 – Shape Hierarchy

- **Shape.cs**: Abstract base class containing common shape properties and behaviors.
- **Rectangle.cs**: Implements rectangle-specific area calculation and printing details of the rectangle.
- **Circle.cs**: Implements circle-specific area calculation and printing details of the circle.
- **Program.cs**: Handles user interactions and application flow.

### Task 2 – Employee Hierarchy

- **Employee.cs**: Abstract base class containing employee details and bonus calculation contract.
- **Developer.cs**: Implements developer-specific bonus calculation and printing details of the developer.
- **Manager.cs**: Implements manager-specific bonus calculation and printing details of the manager.
- **Program.cs**: Handles employee creation, bonus calculation, and detail display.

### Task 3 – Banking System

- **BankAccount.cs**: Base class containing common account properties and operations.
- **SavingsAccount.cs**: Implements withdrawal rules with minimum balance validation and printing details in the savings account.
- **CheckingAccount.cs**: Implements standard withdrawal functionality and printing details in the checking account.
- **Program.cs**: Contains menu-driven console interactions and application workflow.

---

# Recommended PR Review Order

For the best understanding of the implementation, review the projects in the following order:

## 1. Task 1 – Shape Hierarchy

```text
Shape.cs
↓
Rectangle.cs
↓
Circle.cs
↓
Program.cs
```

---

## 2. Task 2 – Employee Hierarchy

```text
Employee.cs
↓
Developer.cs
↓
Manager.cs
↓
Program.cs
```

---

## 3. Task 3 – Banking System

```text
BankAccount.cs
↓
SavingsAccount.cs
↓
CheckingAccount.cs
↓
AccountService.cs
↓
Program.cs
```

On all 3 tasks, focus on:
- Abstract classes
- Inheritance
- Method overriding
- Account management
- Deposit and withdrawal operations

---

# How to Run the Applications

## Prerequisites

- .NET 6 SDK or later
- Visual Studio 2022 or Visual Studio Code

## Steps

1. Clone or download the project.
2. Open the solution in Visual Studio.
3. This solution consists of three different projects.
4. Build the solution and set one of the projects as the startup project.
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