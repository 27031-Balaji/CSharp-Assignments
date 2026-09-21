# Assignment 10 - Calculator Application

This assignment focuses on developing a simple Calculator Application using C# and .NET. The application demonstrates the implementation of basic arithmetic operations through a menu-driven console interface while applying concepts such as Object-Oriented Programming (OOP), class libraries, exception handling, input validation, and XML documentation.

## Features

- Addition of two numbers
- Subtraction of two numbers
- Multiplication of two numbers
- Division of two numbers
- Input validation for numeric values

---

## Feature Details

### Addition

The Addition feature allows users to find the sum of two integers.

#### Functionalities

1. Users enter two integer values.
2. The `Add()` method from the `MathUtils` class is invoked.
3. The sum of the numbers is returned and displayed.

---

### Subtraction

The Subtraction feature calculates the difference between two integers.

#### Functionalities

1. Users enter two integer values.
2. The `Subtract()` method is invoked.
3. The difference is calculated and displayed.

---

### Multiplication

The Multiplication feature calculates the product of two integers.

#### Functionalities

1. Users enter two integer values.
2. The `Multiply()` method is invoked.
3. The multiplication result is displayed.

---

### Division

The Division feature calculates the quotient of two integers.

#### Functionalities

1. Users enter two integer values.
2. The `Divide()` method is invoked.
3. The quotient is displayed.
4. Division by zero is prevented through exception handling.

---

### Input Validation

The application validates the user input before performing calculations.

#### Functionalities

1. Users are prompted to enter integer values.
2. Input is validated using `int.TryParse()`.
3. Invalid inputs are rejected.
4. Users are repeatedly prompted until a valid number is entered.

---

## Techniques Used

- Class libraries for MathUtils
- Exception handling for division operation.
- Helper for getting valid numeric value from the user.
- Menu-driven console interface for calculator operations.

---

## Recommended PR Review Order

For the best understanding of the implementation, review the project files in the following order:

```text
MathUtils.cs
     ↓
Program.cs
     ↓
XML Documentation Comments
```

This order helps understand how arithmetic operations are implemented first and then how they are consumed within the console application.

---

## How to Run the Application

### Prerequisites

- .NET 6 SDK or later
- Visual Studio 2022 / Visual Studio Code

### Steps

1. Clone or download the project.
2. Open the solution in Visual Studio.
3. Build the project.
4. Run the application.
5. Select an operation from the menu.
6. Enter two integer values.
7. View the result.
8. Continue performing calculations or exit the application.

---

## Approach

The Calculator Application was developed by separating the arithmetic logic from the user interface logic. A dedicated `MathUtils` class library was created to encapsulate all calculation-related operations. This approach improves code reusability and simplifies maintenance by keeping responsibilities clearly separated.

A menu-driven console interface was implemented to allow users to perform multiple calculations without restarting the application. User inputs are validated before any operation is executed to prevent invalid data from affecting the calculations.

For division operations, exception handling was implemented to safely manage divide-by-zero scenarios. XML documentation comments were also added throughout the codebase to improve readability and maintainability.

---

## Challenges Faced

### 1. Resolving Project Reference and Dependency Issues

While setting up the solution, the `CalculatorApplication` project was unable to recognize the `MathLibrary` project even after adding the project reference. The issue was resolved by rebuilding the solution and verifying that the library reference was correctly added. Once the solution was successfully built, the dependency was loaded properly and the classes became accessible.

### 2. Implementing Reliable Input Validation

One challenge was ensuring that the application handled incorrect user inputs. Users could enter alphabets, symbols, or empty values instead of integers. This was resolved by using `int.TryParse()` along with a validation loop that repeatedly prompts the user until a valid integer is entered.

---