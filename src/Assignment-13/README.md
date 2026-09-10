# Assignment 13 - Understanding Collections and Generics

## Introduction

This assignment explores the fundamental concepts of Collections and Generics in C#. 
Collections provide efficient ways to store, organize, and manage groups of data, while Generics enhance type safety, code reusability, and maintainability by allowing data structures and methods to work with different data types without sacrificing compile-time type checking.

The assignment consists of six tasks:

- Task 1 – Working with Lists
- Task 2 – Using Stacks
- Task 3 – Working with Queues
- Task 4 – Understanding Dictionaries
- Task 5 – Applying Generic Collections
- Task 6 – Understanding IEnumerable and IReadOnlyDictionary

---

# Task 1 – Working with Lists

This task demonstrates the use of the List collection to store and manage a collection of book titles. 
Common list operations such as adding, removing, searching, and displaying elements are performed.

## Understanding from the Task

- Creating a `List<string>`
- Adding elements using `Add()`
- Removing elements using `Remove()`
- Checking element existence using `Contains()`
- Iterating through collections using `foreach`

---

## Implementation Overview

The program creates a `List<string>` to store book titles and adds five books using the `Add()` method. It then demonstrates the `Remove()` method by attempting to remove a non-existing book and subsequently removing an existing book.

The `Contains()` method is used to verify whether a specific book exists in the collection. Finally, all remaining books are displayed using a `foreach` loop.

---

# Task 2 – Using Stacks

This task demonstrates the use of the Stack collection to reverse a string. 
The stack follows the Last In, First Out (LIFO) principle, making it ideal for reversing data.

## Understanding from the Task

- Creating a `Stack<char>`
- Adding elements using `Push()`
- Removing elements using `Pop()`
- Using the `Count` property
- Reversing a string using a stack

---

## Implementation Overview

The program accepts a string from the user and creates a `Stack<char>` to store its characters. Each character of the input string is pushed onto the stack using the `Push()` method.

The characters are then popped one by one using the `Pop()` method and stored in a character array. Since a stack removes elements in reverse order of insertion, the resulting array forms the reversed string.

Finally, the program displays both the original and the reversed string.

---

# Task 3 – Working with Queues

This task demonstrates the use of the Queue collection to simulate a queue of people waiting in line. 
The queue follows the First In, First Out (FIFO) principle, where the first person added is the first person removed.

## Understanding from the Task

- Creating a `Queue<string>`
- Adding elements using `Enqueue()`
- Removing elements using `Dequeue()`
- Handling queue operations safely
- Iterating through collections using `foreach`

---

## Implementation Overview

The program creates a `Queue<string>` to represent a line of people and adds five names using the `Enqueue()` method.

A person is then removed from the front of the queue using the `Dequeue()` method. Since attempting to dequeue from an empty queue throws an exception, the operation is performed inside a `try-catch` block.

Finally, the remaining people in the queue are displayed using a `foreach` loop.

---

# Task 4 – Understanding Dictionaries

This task demonstrates the use of the Dictionary collection to map student names with their corresponding grades. 
Dictionaries store data as key-value pairs and provide efficient access to values using unique keys.

## Understanding from the Task

- Creating a `Dictionary<string, int>`
- Adding key-value pairs using `Add()`
- Removing elements using `Remove()`
- Accessing and displaying dictionary entries
- Iterating through a dictionary using `foreach`

---

## Implementation Overview

The program creates a `Dictionary<string, int>` where the student's name serves as the key and the student's grade serves as the value. Five students and their grades are added using the `Add()` method.

A student is then removed from the dictionary using the `Remove()` method. Finally, the remaining student records are displayed by iterating through the dictionary using a `foreach` loop and accessing each `KeyValuePair`.

---

# Task 5 – Applying Generic Collections

This task demonstrates the implementation of custom generic collection classes that encapsulate the functionality of List, Stack, Queue, Dictionary. 
The application provides a menu-driven interface to showcase how generics improve type safety, reusability, and maintainability.

## Understanding from the Task

- Creating custom generic classes
- Using List, Stack, Queue, Dictionary
- Building a menu-driven console application
- Improving type safety through generics

---

## Implementation Overview

The program combines the functionality of the previous tasks into a single application using custom generic collection classes.

A menu-driven interface allows the user to choose between demonstrations for Lists, Stacks, Queues, and Dictionaries. Each custom collection class internally uses the corresponding .NET generic collection while exposing commonly used operations such as adding, removing, searching, and displaying data.

This approach promotes code reusability and provides a consistent interface for working with different collection types.

---

## Generic Collections Implemented

### GenericList<T>

A custom wrapper around List that supports:

- Adding elements
- Removing elements
- Checking element existence
- Displaying items
- Retrieving collection count

### GenericStack<T>

A custom wrapper around Stack that supports:

- Pushing elements
- Popping elements
- Checking element existence
- Displaying items
- Retrieving collection count

### GenericQueue<T>

A custom wrapper around Queue that supports:

- Enqueuing elements
- Dequeuing elements
- Checking element existence
- Displaying items
- Retrieving collection count

### GenericDictionary<TKey, TValue>

A custom wrapper around Dictionary that supports:

- Adding key-value pairs
- Removing elements
- Checking key existence
- Displaying key-value pairs
- Retrieving collection count

---

# Task 6 – Understanding IEnumerable and IReadOnlyDictionary

This task demonstrates the use of collection abstractions through IEnumerable and IReadOnlyDictionary.
It highlights how programming against interfaces improves reusability, flexibility, and data protection.

## Understanding from the Task

- Using `IEnumerable<int>` as a method parameter
- Working with multiple concrete collection types
- Calculating values using LINQ
- Creating and returning an `IReadOnlyDictionary`
- Reading dictionary data through a read-only interface
- Enforcing immutability using `IReadOnlyDictionary`

---

## Implementation Overview

The program implements a `SumOfElements()` method that accepts an `IEnumerable<int>` and calculates the sum of all elements. Since multiple collection types implement `IEnumerable<int>`, the same method can be reused with a `List<int>`, array, and `Queue<int>` without modification.

The program also demonstrates the use of `IReadOnlyDictionary<string, int>`. A dictionary containing student names and grades is created and returned through a read-only interface. Another method accepts the read-only dictionary and displays its contents.

Finally, the task illustrates immutability by showing that elements cannot be modified through an `IReadOnlyDictionary` reference.

---

# Project Structure

```text
Assignment-13
│
├── Task1-List
│   └── Program.cs
│
├── Task2-Stack
│   └── Program.cs
│
├── Task3-Queue
│   └── Program.cs
│
├── Task4-Dictionaries
│   └── Program.cs
│
├── Task5-Generics
│   └── Collections
│		└── GenericList.cs
│		└── GenericDictionary.cs
│		└── GenericQueue.cs
│		└── GenericStack.cs
│   └── Program.cs
│
├── Task6-IEnumerableAndIReadOnlyDictionary
│   └── Program.cs
│
└── README.md
```

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

# Key Learnings

- Learned how to use the most commonly used generic collections in C#, including List, Stack, Queue and Dictionary.
- Learned how generics improve type safety by eliminating the need for type casting.
- Discovered that collection methods such as `Remove()` often return a boolean value indicating whether the operation was successful.
- Learned that `IEnumerable<T>` promotes reusability by allowing methods to work with multiple collection types such as lists, arrays, and queues through a common interface.
- Gained experience using LINQ extension methods such as `Sum()` and learned that it can throw exceptions like `ArgumentNullException` when the source collection is null and `OverflowException` when the calculated sum exceeds the range of the numeric type.
- Learned about the `KeyValuePair<TKey, TValue>` structure, which represents individual entries in a dictionary and is commonly used when iterating through key-value collections.