# Assignment 13 - Project References and Build Order

## Introduction
This assignment focuses on understanding project references, dependency management, and build order configuration in a multi-project C# solution. The solution demonstrates how different class library and console application projects can interact while maintaining a proper dependency hierarchy and avoiding circular references.

## Solution Overview
The solution consists of five projects:

- GreetingsApp
- AdditionalProject
- DisplayApp
- MathApp
- UtilityApp

The application implements a simple calculator that performs arithmetic operations using a layered architecture. Each project has a specific responsibility, promoting separation of concerns and maintainability.

---

## Project Responsibilities

### GreetingsApp
The entry point of the solution.

#### Responsibilities
- Starts the application.
- Creates an instance of `CalculatorStarter`.
- Initiates the calculator workflow.

---

### AdditionalProject
Acts as an intermediate project between the entry point and the user interface.

#### Responsibilities
- Creates an instance of `ConsoleUI`.
- Starts the calculator application.

---

### DisplayApp
Responsible for all user interaction.

#### Features
- Accepts user input.
- Validates user entries.
- Displays calculation results.
- Handles division-by-zero exceptions.
- Allows users to perform multiple calculations without restarting the application.

---

### MathApp
Contains the arithmetic logic.

#### Features
- Addition
- Subtraction
- Multiplication
- Division

#### Special Handling
- Throws a `DivideByZeroException` when division by zero is attempted.

---

### UtilityApp
Contains validation-related functionality.

#### Features
- Numeric input validation.
- Operator validation.
- Continue-choice validation (`Y` / `N`).

---

## Dependency Structure
The projects were organized into a one-directional dependency hierarchy to ensure proper build order and eliminate circular references.

### Dependency Diagram

```text
UtilityApp
▲
│
MathApp ───────► DisplayApp
                    ▲
                    │
           AdditionalProject
                    ▲
                    │
              GreetingsApp
```

### Project References

```text
DisplayApp

├── MathApp
└── UtilityApp

AdditionalProject
└── DisplayApp

GreetingsApp
└── AdditionalProject
```

---

## Build Order
Based on the project dependencies, Visual Studio determines the following build order:

```text
1. MathApp
2. UtilityApp
3. DisplayApp
4. AdditionalProject
5. GreetingsApp
```

This ensures that all dependent projects are built only after their referenced projects have been successfully compiled.

---

## Calculator Features

### Addition
Adds two integer values and displays the result.

### Subtraction
Subtracts the second number from the first number.

### Multiplication
Multiplies two integer values.

### Division
Divides the first number by the second number.

#### Validation
Division by zero is prevented through exception handling.

---

### Input Validation
The application validates user input before processing calculations.

#### Number Validation
- Accepts only valid integers.
- Rejects invalid input.
- Continues prompting until a valid value is entered.

#### Operator Validation
Supported operators:

```text
+
-
*
/
```

Invalid operators are rejected and the user is prompted again.

#### Continue Validation
Supported choices:

```text
Y
N
```

Any other input is rejected until a valid choice is entered.

---

## Project Structure

```text
Solution
│
├── GreetingsApp
│   └── Program.cs
│
├── AdditionalProject
│   └── CalculatorStarter.cs
│
├── DisplayApp
│   └── ConsoleUI.cs
│
├── MathApp
│   └── MathUtils.cs
│
├── UtilityApp
│   └── Validator.cs
│
└── README.md
```

---

## Recommended PR Review Order
For the best understanding of the implementation, review the files in the following order:

```text
MathUtils.cs
    ↓
Validator.cs
    ↓
ConsoleUI.cs
    ↓
CalculatorStarter.cs
    ↓
Program.cs
```

This order helps understand the core calculation and validation logic first, followed by the user interface and application startup flow.

---

## How to Run the Application

### Prerequisites

- .NET 6 SDK or later
- Visual Studio 2022 or Visual Studio Code

### Steps

1. Clone or download the solution.
2. Open the solution in Visual Studio.
3. Build the solution.
4. Set `GreetingsApp` as the startup project.
5. Run the application.
6. Enter two numbers and an operator.
7. View the calculated result.
8. Continue performing calculations or exit the application.

---

## Conclusion

This assignment provided hands-on experience with project references, dependency management, and build order configuration in a multi-project C# solution. By separating responsibilities across individual projects and maintaining a proper dependency hierarchy, the solution remains modular, maintainable, and easy to extend while demonstrating a practical implementation of a layered application architecture.