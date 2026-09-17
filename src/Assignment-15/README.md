# Assignment 15 - Working with Files and Streams in C#

## Introduction

This assignment focuses on developing a practical understanding of file handling and stream-based operations in C#.

C# provides several stream implementations, each designed for different use cases. `FileStream` enables direct interaction with files stored on disk, `MemoryStream` allows data to be processed in memory, and `BufferedStream` improves I/O performance by reducing the number of read and write operations performed against the underlying stream.

The assignment consists of four major tasks:

- Task 1 – Implement a File Data Processor
- Task 2 – Implement a File Data Processor with Asynchronous Methods
- Task 3 – Investigate Issues in Basic File Usage
- Task 4 – Analyze and Resolve Performance Issues with Logging System for Multiple Users

---

# Task 1 – Implement a File Data Processor

This task focuses on processing large files efficiently using different stream implementations available in C#. The application creates a large text file, reads it in chunks using a buffer, processes the data, compares the performance of `FileStream` and `BufferedStream`, and finally writes the processed data to a new file using a `MemoryStream`.

## Understanding from the Task

- Creating a large file programmatically using file writing techniques
- Reading large files using `FileStream`
- Reading files in chunks using a buffer
- Measuring execution time using `Stopwatch`
- Comparing `FileStream` and `BufferedStream` performance
- Processing data while reading from a file
- Using `MemoryStream` as an intermediate in-memory buffer
- Writing processed data to a new file

---

## Implementation Overview

The application begins by checking whether a 1 GB text file exists. If the file does not exist, it is generated programmatically using the `LargeFileMaker` class.

Once the file is available, the application performs three major operations:

1. Reads the file using a `FileStream` and measures the time taken.
2. Reads the same file using a `BufferedStream` and measures the time taken.
3. Processes the file contents by converting all text to uppercase and writes the processed output to a new file using a `MemoryStream`.

A `Stopwatch` is used to record the execution time of each operation and display the results to the user.

---

## Key Observations

- Reading large files in chunks prevents excessive memory usage.
- `FileStream` provides direct access to file data.
- `BufferedStream` can improve performance by reducing file system interactions.
- Processing can be performed incrementally while reading.
- `MemoryStream` provides an efficient temporary buffer before writing data.
- Performance comparisons can be measured objectively using `Stopwatch`.
- Stream-based processing allows files larger than available memory to be handled efficiently.

---

# Task 2 – Implement a File Data Processor with Asynchronous Methods

This task extends the file processing application developed in Task 1 by introducing asynchronous programming techniques. The objective is to improve responsiveness and scalability by using the asynchronous capabilities provided by `FileStream`, `BufferedStream`, and `MemoryStream`.

The application processes multiple large files concurrently while performing the same operations as the synchronous implementation: reading, processing, and writing data.

## Understanding from the Task

- Understanding asynchronous file operations
- Using `async` and `await`
- Reading files asynchronously using `ReadAsync()`
- Writing files asynchronously using `CopyToAsync()`
- Processing multiple files concurrently
- Coordinating multiple asynchronous operations using `Task.WhenAll()`
- Comparing synchronous and asynchronous performance
- Understanding non-blocking I/O operations

---

## Implementation Overview

The application processes two large files and compares execution times between:

1. Synchronous file processing
2. Asynchronous file processing

A `Stopwatch` is used to measure the execution time of both approaches.

The asynchronous implementation uses:

- Asynchronous `FileStream`
- Asynchronous `BufferedStream`
- Asynchronous `MemoryStream`
- Concurrent file processing with `Task.WhenAll()`

This allows multiple files to be processed simultaneously without blocking the executing thread while waiting for I/O operations to complete.


## Synchronous File Processing

The first implementation processes files one after another.

### Implementation Details

The application:

1. Reads data from the input file.
2. Processes the data.
3. Writes the processed data to an output file.
4. Repeats the process for the second file.

Only after the first file completes does the application begin processing the second file.

---

## Asynchronous File Processing

The second implementation performs the same operations asynchronously.

### Implementation Details

The asynchronous `FileStream` is created using:

```csharp
useAsync: true
```

This enables true asynchronous file I/O operations.
Instead of blocking while waiting for disk operations to complete, control is returned to the runtime, allowing other work to proceed.

### Advantages

- Improved scalability
- Reduced thread blocking
- Better utilization of system resources
- More efficient handling of large files

---

## Key Observations

- Asynchronous file operations reduce thread blocking during I/O operations.
- `async` and `await` simplify asynchronous programming while maintaining readability.
- `ReadAsync()` allows large files to be processed efficiently without blocking.
- `CopyToAsync()` provides an asynchronous mechanism for writing data to disk.
- `Task.WhenAll()` enables multiple files to be processed concurrently.
- Concurrent processing can significantly reduce overall execution time.
- The asynchronous implementation scales better as the number of files increases.
- Memory usage remains controlled because data is processed in chunks rather than loading complete files into memory.

---

# Task 3 – Investigate Issues in Basic File Usage

This task focuses on identifying and resolving memory inefficiencies present in the provided file processing code.

## Understanding from the Task

- Identifying unnecessary memory allocations
- Detecting redundant data copying
- Improving file writing efficiency
- Reducing output overhead
- Comparing optimized and unoptimized implementations

---

## Implementation Overview

The starter code was analyzed to identify operations that caused unnecessary memory usage and reduced performance. 
An optimized version was then implemented to remove these inefficiencies and improve overall execution speed.

---

## Issues Identified

### 1. Unnecessary MemoryStream Usage
The original code writes data to a `MemoryStream`, converts it back to a byte array using `ToArray()`, and then writes it to a file.
This creates unnecessary memory allocations and data copying.

### 2. Additional Memory Allocation
The call to `MemoryStream.ToArray()` creates a completely new byte array, duplicating data already present in memory.

### 3. Inefficient Console Output
The file contents are displayed one character at a time.
This results in many console write operations.

---

## Improvements Made

### 1. Direct File Writing
The unnecessary `MemoryStream` was removed and the byte array is written directly to the file using `FileStream`.

### 2. Removed Data Duplication
The extra memory allocation caused by `ToArray()` was eliminated.

### 3. Efficient Output
Instead of printing character-by-character, the buffer is converted to a string and written once.

---

## Key Learnings

- Avoid using `MemoryStream` when data can be written directly to a file.
- Minimize unnecessary memory allocations and data copies.
- Excessive console output can impact performance.
- Simpler file operations often result in better efficiency and maintainability.

---

# Task 4 – Analyze and Resolve Performance Issues with Logging System for Multiple Users

This task focuses on identifying and resolving memory, performance, and concurrency issues in a multi-user logging system.

## Understanding from the Task

- Identifying performance bottlenecks
- Reducing unnecessary memory usage
- Implementing thread-safe logging
- Reducing file access contention
- Comparing logging performance under load

---

## Implementation Overview

The starter logging system was analyzed to identify inefficiencies. Improvements were then introduced incrementally, including direct file writing, thread synchronization, user-specific log files, and performance testing.

---

## Subtask 1 – Identifying Issues

### Issues Identified

- A new `MemoryStream` is created for every log operation.
- Error messages are copied multiple times before reaching the file.
- All users write to the same log file.
- Multiple threads can attempt to write simultaneously.
- No thread-safety mechanism is implemented.

These issues increase memory usage and create file access contention under heavy load.

---

## Subtask 2 – Improving File Writing

### Changes Made

- Removed the unnecessary `MemoryStream`.
- Converted the error message directly into a byte array.
- Wrote the byte array directly to the file using `FileStream`.

---

## Subtask 3 – Thread-Safe Logging

### Changes Made

A locking mechanism was added around the file write operation to ensure that only one thread writes to the log file at a time.

### Benefits

- Prevents concurrent file access issues.
- Ensures log entries are written correctly.
- Improves reliability in multi-user scenarios.

### Limitation

Although thread-safe, all users still write to the same file, causing contention under high load.

---

## Subtask 4 – Independent Error Files

### Changes Made

Each user is assigned a dedicated log file.
A dictionary of lock objects is maintained so that each file has its own lock.

### Benefits

- Reduces file access contention.
- Allows different users to write simultaneously.
- Keeps logs organized by user.
- Maintains thread safety for each file.

---

## Subtask 5 – Performance Testing

### Test Setup

- 20 User IDs
- 10 Error Messages
- 500 Concurrent Tasks

Each task randomly selects a user and an error message before performing a logging operation.
The user-specific logger performs better because logging operations are distributed across multiple files, significantly reducing contention.

---

## Key Learnings

- Avoid unnecessary `MemoryStream` usage when direct file writing is sufficient.
- Shared resources can become bottlenecks in concurrent applications.
- Locks are essential when multiple threads access the same file.
- User-specific files improve scalability and organization.
- Performance testing helps validate optimization efforts under realistic workloads.

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