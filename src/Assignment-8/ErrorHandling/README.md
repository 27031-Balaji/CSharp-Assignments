# Assignment 8 - Error Handling

This assignment consists of five different tasks which helps to understand the core concepts of exceptions and using try-catch-finally blocks to manipulate the exceptions. This assignment implements various strategies like try/catch/finally blocks, exceptions, custom exception classes, and global unhandled exception handling.

The five different tasks are as follows.

---

# Task 1 – Understanding and using try/catch/finally blocks

This task is to make a simple C# program that does the division operation that is handled by try/catch block to handle the DivideByZeroException and print the appropriate message.

The program accepts two integer values from the user and performs a division operation. It handles dividing by zero error and handles the exception in the catch block.

## Understanding from the task

- Exception handling in C#
- Using try/catch/finally blocks
- Handling the DivideByZeroException

# Task 2 – Catching and Throwing Different Types of Exceptions

This task extends the program from Task 1 by introducing an array of integers and handling multiple exception types.

The program allows the user to create an integer array, select two elements using their positions, and perform a division operation. It handles both DivideByZeroException and IndexOutOfRangeException. When an IndexOutOfRangeException occurs, a new exception containing a custom message is thrown and then handled by an outer catch block.

## Understanding from the task

- Nested try/catch blocks
- Throwing exceptions
- Custom exception messages
- Handling multiple exception using catch blocks 

# Task 3 – Defining and Using Custom Exception Classes

This task extends the program from Task 2 by introducing a custom exception class called InvalidUserInputException.

The program allows the user to create an integer array, access elements using their positions, and perform a division operation. In addition to handling DivideByZeroException and IndexOutOfRangeException, it now validates user input by throwing a custom exception whenever an invalid value is entered.

## Understanding from the task

- Creating custom exception classes
- Input validation using exceptions
- Managing multiple exception types
- Catching custom exceptions

# Task 4 – Handling Global Unhandled Exceptions

This task extends the program from Task 3 by introducing global exception handling using the AppDomain.UnhandledException event.

The program continues to use custom exception handling, array access validation, and division operations. In addition, it now includes a universal exception handler capable of catching exceptions that are not handled anywhere else in the application. This helps prevent unexpected application crashes and provides useful debugging information.

## Understanding from the task

- AppDomain.UnhandledException event and its arguments
- Global exception handling

# Task 5 – Understanding and Interpreting Exception Stack Traces

This task extends the program from Task 4 by focusing on exception stack traces. The objective is to understand how exceptions travel through the application and how stack traces can be used to identify the exact location where an error occurred.

The program continues to use custom exception handling, array validation, and global exception handling. When an exception occurs, the stack trace is displayed, allowing developers to trace the execution path that led to the error.

## Interpretation of Stack Trace

During the execution, I reviewed the following stack traces:

```text
at ErrorHandling.Program.Main(String[] args)
in C:\CSharp-Assignments\CSharp-Assignments\src\Assignment-8\ErrorHandling\Task5-StackTrace\Program.cs:line 44
```
in the main exception.

```text
at ErrorHandling.Program.Main(String[] args)
in C:\CSharp-Assignments\CSharp-Assignments\src\Assignment-8\ErrorHandling\Task5-StackTrace\Program.cs:line 27
```
in the inner exception. 

From this I came to an interpretation that,

- The exception occurred inside the Main() method of the Program class.
- The source file where the exception originated is Program.cs.
- The runtime identified the exact locations of the exception at **line 27** and **line 44**.
- These line numbers helped me quickly locate the statements responsible for the error without manually searching through the entire code.
- The stack trace provides valuable debugging information by showing where the exception occurred and the path taken during program execution.

## Understanding from the task

This task helped me understand that a **stack trace** is one of the most useful debugging tools in C#. It helps identify: 
- The method where the exception occurred.
- The source file containing the error.
- The exact line number that caused the exception.
- The execution flow leading to the exception.

By analyzing the stack trace, I was able to quickly determine the source of the problem and understand how the exception propagated through the application.

---

# Project Structure

```text
Assignment-8
│
├── Task1
│   └── Program.cs
│
├── Task2
│   └── Program.cs
│
├── Task3
│   └── Program.cs
│   └── InvalidUserInputException.cs
│
├── Task4
│   └── Program.cs
│   └── InvalidUserInputException.cs
│
├── Task5
│   └── Program.cs
│   └── InvalidUserInputException.cs
│
└── README.md
```

## File Overview

### Task 1
- **Program.cs**: Performs a division operation and demonstrates exception handling using try, catch, and finally blocks.

### Task 2
- **Program.cs**: Extends Task 1 by introducing array operations and handling both DivideByZeroException and IndexOutOfRangeException. Demonstrates re-throwing exceptions with custom messages.

### Task 3
- **Program.cs**: Validates user input and demonstrates how custom exceptions are thrown and handled.
- **InvalidUserInputException.cs**: Defines the custom exception used when invalid input is entered.

### Task 4
- **Program.cs**: Registers a global exception handler using AppDomain.UnhandledException and demonstrates handling unhandled exceptions at the application level.
- **InvalidUserInputException.cs**: Defines the custom exception used when invalid input is entered.

### Task 5
- **Program.cs**: Demonstrates how to obtain, view, and analyze exception stack traces for debugging purposes.
- **InvalidUserInputException.cs**: Defines the custom exception used when invalid input is entered.

---

# Recommended PR Review Order

For the best understanding of the implementation, review the projects in the following order:

## 1. Task 1 – Understanding and Using try/catch/finally Blocks

Focus on:
- Basic exception handling
- try block
- catch block
- finally block
- DivideByZeroException
---

## 2. Task 2 – Catching and Throwing Different Types of Exceptions

Focus on:
- Nested try/catch blocks
- IndexOutOfRangeException
- Throw keyword
- Exception propagation
- Custom exception messages
---

## 3. Task 3 – Defining and Using Custom Exception Classes

Focus on:
- Custom exception classes
- Input validation
- Throwing and catching custom exceptions.
---
## 4. Task 4 – Handling Global Unhandled Exceptions

Focus on:

- AppDomain.UnhandledException
- Unhandled exception flow

## 5. Task 5 – Understanding and Interpreting Exception Stack Traces

Focus on:

- Stack traces and how it is generated.
- Stack traces for the main and inner exceptions and how they can be used for debugging.
---

# How to Run the Application

## Prerequisites

- .NET 6 SDK or later
- Visual Studio 2022 or Visual Studio Code

## Steps

1. Clone or download the project.
2. Open the solution in Visual Studio.
3. Build the solution and set one of the projects as the startup project.
4. Run the project.

---

# Challenges Faced

- Understanding how exceptions propagate between nested catch blocks.
- Creating and using custom exception classes effectively.
- Understanding the behavior of global exception handlers.
- Interpreting stack traces and identifying the source of runtime errors.