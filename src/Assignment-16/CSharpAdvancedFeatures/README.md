# Assignment 16 - C# Advanced Concepts

This assignment focuses on exploring advanced features of C# that are commonly used to write flexible, maintainable, and expressive applications.
The assignment demonstrates the use of events and delegates, dynamic and var keywords, anonymous methods, lambda expressions and statements, advanced delegate-based sorting, records, and pattern matching. These concepts help developers build loosely coupled systems, simplify data processing, and write cleaner object-oriented code.

The seven different tasks are as follows.

---

# Task 1 – Understanding and Implementing Events and Delegates in C#

This task introduces the concepts of delegates and events in C#. A delegate is used to reference methods with a specific signature, while an event provides a mechanism for notifying subscribed methods when an action occurs.

The program defines a custom delegate named `Notify` and an event named `OnAction` inside a `Notifier` class. Multiple notification methods are subscribed to the event, and when the event is triggered, all subscribed methods are executed. The task demonstrates how events and delegates can be used to implement a simple notification system.

## Understanding from the task

- Defining custom delegates
- Creating and using events
- Subscribing methods to events
- Raising events using `Invoke`
- Multicast delegate behavior
- Event-driven programming concepts

## Key Learnings

- Learned how delegates can point to methods with the same signature.
- Understood how events are used to notify other parts of an application.
- Learned how multiple methods can subscribe to a single event.
- Understood how events help keep classes loosely coupled.
- Gained hands-on experience with event-driven programming.

---

# Task 2 – Understanding the Use of Dynamic and Var Keywords and Their Differences

This task demonstrates the differences between the `var` and `dynamic` keywords in C#. Both allow variables to be declared without explicitly specifying a type, but they behave differently when it comes to type handling.

The program declares a variable using `var` and shows that its type is determined during compilation and cannot be changed afterwards. It then declares a variable using `dynamic`, which resolves its type at runtime and allows the variable to hold values of different types during execution. The output illustrates the difference between compile-time type checking and runtime type resolution.

## Understanding from the task

- Type inference using `var`
- Runtime type resolution using `dynamic`
- Compile-time type safety
- Changing values of different types with `dynamic`
- Differences between static and dynamic typing
- Practical use cases of `var` and `dynamic`

## Key Learnings

- Learned the difference between `var` and `dynamic`.
- Understood that `var` determines its type at compile time.
- Learned that `dynamic` resolves its type at runtime.
- Observed how `dynamic` allows changing the data type of a variable.
- Understood the advantages and limitations of both approaches.

---

# Task 3 – Implementing Anonymous Methods

This task demonstrates the use of anonymous methods in C#. Anonymous methods allow delegate logic to be defined inline without creating a separate named method.

The program declares an array of integers and uses the `Array.Sort` method with an anonymous method to compare elements and sort them in ascending order. The array is displayed before and after sorting to show the effect of the custom comparison logic.

## Understanding from the task

- Anonymous Methods
- Delegates
- `Comparison<T>` Delegate
- Sorting using `Array.Sort`
- Custom Comparison Logic
- Inline Method Implementation

## Key Learnings

- Learned how to define anonymous methods using delegates.
- Understood when anonymous methods can be useful.
- Gained experience using delegates with sorting operations.
- Learned how custom comparison logic can be passed to methods.
- Understood the relationship between anonymous methods and delegates.

---

# Task 4 – Understanding and Using Lambda Expressions and Statements

This task demonstrates the use of lambda expressions and lambda statements in C#. Lambda expressions provide a concise way to define delegate logic and are commonly used with LINQ operations.

The program declares a list of integers and uses a lambda expression with the `Where` method to filter even numbers.
It then uses another lambda expression with the `Select` method to transform the filtered numbers by squaring them.
The resulting collection is displayed in the console.

## Understanding from the task

- Lambda Expressions
- Lambda Statements
- LINQ `Where`
- LINQ `Select`
- Collection Filtering
- Collection Transformation

## Key Learnings

- Learned how lambda expressions simplify delegate syntax.
- Gained experience using lambda expressions with LINQ.
- Understood how to filter collections using `Where`.
- Learned how to transform data using `Select`.
- Saw how multiple LINQ operations can be chained together.

---

# Task 5 – Advanced Use of Delegates for Sorting

This task demonstrates how delegates can be used to implement flexible sorting behavior in C#. A custom delegate is used to define different comparison strategies without changing the sorting logic itself.

The program defines a `Product` class containing product details and a custom `SortDelegate` that compares two products. 
Separate methods are implemented to sort products by name, category, and price.
Delegate instances are passed to a common `SortAndDisplay` method, which performs the sorting and displays the results using ConsoleTables. 
This demonstrates how delegates can be used to dynamically control application behavior.

## Understanding from the task

- Custom Delegates
- Delegate Instances
- Method References
- Reusable Sorting Logic

## Key Learnings

- Learned how delegates can make code more flexible.
- Understood how different sorting behaviors can be implemented using delegates.
- Gained experience passing methods as parameters.
- Learned how to reuse the same sorting logic with different comparison methods.
- Understood the benefits of separating behavior from implementation.

---

# Task 6 – Implementing and Manipulating Records in C# 9.0 and Above

This task demonstrates the features and benefits of records in C#. Records provide built-in support for value equality, immutability, deconstruction, and non-destructive mutation, making them well suited for representing data-centric objects.

The program defines a `Book` record and creates multiple instances to demonstrate record behavior. 
It verifies value equality by comparing two records with identical values, shows immutability by preventing direct modification of record properties, uses the `with` keyword to create a modified copy of an existing record, and applies deconstruction to extract and display record values. 
ConsoleTables is used to present book details in a structured format.

## Understanding from the task

- Records
- Immutable Reference Types
- Value Equality
- Non-Destructive Mutation
- `with` Expressions
- Deconstruction
- Record Constructors

## Key Learnings

- Learned how records differ from traditional classes.
- Understood value equality in records.
- Learned how records support immutability.
- Gained experience using the `with` keyword to create modified copies.
- Understood how deconstruction can simplify working with records.

---

# Task 7 – Implementing Advanced Pattern Matching in C# 7.0 and Above

This task demonstrates the use of pattern matching in C# to identify objects based on their runtime type and execute type-specific logic. Pattern matching simplifies working with object hierarchies by combining type checking and casting into a single expression.

The program defines a base `Shape` class and derived classes such as `Circle`, `Rectangle`, and `Triangle`, each implementing its own area calculation logic. 
A collection containing different shape objects is processed using a switch expression with type patterns. 
Based on the actual shape type, the program displays the shape details and calculates its area. 
The implementation also handles null values and unknown shape types.

## Understanding from the task

- Pattern Matching
- Type Patterns
- Switch Expressions
- Runtime Type Identification
- Method Overriding
- Null Pattern Handling
- Object-Oriented Design

## Key Learnings

- Learned how pattern matching simplifies type checking.
- Understood how switch expressions work with object types.
- Gained experience working with inheritance and polymorphism.
- Learned how to handle null values using pattern matching.
- Saw how pattern matching improves code readability and maintainability.

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
