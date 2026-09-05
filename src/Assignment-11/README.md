# Assignment 11 - Memory Management in C#

## Introduction

This assignment explores fundamental memory management concepts in C#. It demonstrates how data is stored and managed in memory using value types, reference types, stack and heap allocation, garbage collection, and resource cleanup through the `IDisposable` interface and the `using` statement.

The assignment consists of four tasks:

- Task 1 – Value Types and Reference Types
- Task 2 – Stack and Heap Memory
- Task 3 – Garbage Collection
- Task 4 – IDisposable and Using Statement

---

# Task 1 – Understanding Value Types and Reference Types

This task demonstrates the difference between value types and reference types in C#. A value type and a reference type are passed to a method and modified to observe their behavior after the method call.

## Concepts Covered

- Value Types
- Reference Types
- Stack Memory
- Heap Memory
- Method Parameter Passing
- Boxing and Unboxing
- System.Object

## Key Observation

- Value types are copied when passed to methods, so changes do not affect the original variable.
- Reference types share the same object reference, so modifications are reflected outside the method.
- Every type in C# ultimately derives from `System.Object`.
- Value types can be implicitly converted to `object` through **boxing**.
- Boxed values can be converted back to their original type through **unboxing**.

## Sample Example

```csharp
object obj = 10;     // Boxing
int number = (int)obj; // Unboxing
```

## Learning Outcome

After completing this task, I gained an understanding of:

- The difference between value types and reference types.
- How data is stored in stack and heap memory.
- How method calls affect different types.
- Why all C# types ultimately inherit from `System.Object`.
- How boxing and unboxing work and their impact on memory allocation.
- All value types are stored as struct and all the reference types are stored as classes.
- All value types are stored in `System.ValueType` class which tells the CLR that it is a value type.

---

# Task 2 – Working with the Stack and the Heap

This task demonstrates how value types and reference types are allocated in memory. The application creates a large array to observe heap allocation and uses multiple local variables to understand stack allocation and execution behavior.

## Concepts Covered

- Stack Memory
- Heap Memory
- Value Types
- Reference Types
- Memory Allocation
- Arrays
- Memory Profiling

## Key Observation

- Local variables such as integers are typically stored on the stack and are automatically cleaned up when the method execution completes.
- Arrays are reference types and their contents are allocated on the managed heap.
- Creating a large array significantly increases heap memory usage compared to using local value type variables.
- The stack is generally used for method calls and local variables, while the heap is used for dynamically allocated objects.

## Memory Analysis

### CreateArrayAndCalculateProduct()

```csharp
long[] numbers = new long[size];
```

- Allocates a large array containing 100,000 elements.
- The array object and its contents are stored on the heap.
- Heap memory usage increases noticeably during execution.

### CreateLocalValuesAndCalculateProduct()

```csharp
int value1 = 1, value2 = 2, value3 = 4;
...
```

- Uses multiple local value type variables.
- Variables are allocated within the method's execution context.
- Once the method completes, the array becomes eligible for garbage collection because no references to it remain.
- The memory may be reclaimed by the Garbage Collector during a future collection cycle.
- A decrease in heap memory usage can be observed after garbage collection occurs.
- Stack memory usage is not directly visible in the Diagnostic Tools memory graphs.
- Local variables can be inspected through the Locals window while debugging.
- Heap allocations are easier to observe using memory usage snapshots and profiling tools.

## Learning Outcome

After completing this task, I gained an understanding of:

- The difference between stack and heap memory.
- How value types and reference types are allocated.
- Why arrays consume heap memory.
- How to analyze memory usage using Visual Studio Diagnostic Tools.

---

# Task 3 – Using Garbage Collection and Understanding Its Impact on Performance

This task demonstrates how the .NET Garbage Collector (GC) manages memory by reclaiming unused objects. A large number of objects are created and later made eligible for garbage collection, allowing memory usage and performance impact to be observed.

## Concepts Covered

- Garbage Collection (GC)
- Managed Heap
- Object Lifecycle
- Large Object Allocation
- Performance Measurement
- Stopwatch

## Key Observation

- Creating a large number of objects increases heap memory usage.
- Objects are not immediately removed from memory when references are cleared.
- Objects become eligible for garbage collection only when they are no longer referenced.
- The Garbage Collector reclaims unused memory during a collection cycle.
- Triggering garbage collection manually can reduce memory usage but may temporarily impact application performance.

## Memory Analysis

### CreateAndDestroyObjects()

```csharp
List<LargeObject> objects = new List<LargeObject>();

for (int i = 0; i < 2000; i++)
{
    objects.Add(new LargeObject());
}
```

- Allocates a large number of objects on the managed heap.
- Each `LargeObject` contains an integer array of 100,000 elements.
- Heap memory usage increases significantly during object creation.

### Releasing Objects

```csharp
objects.Clear();
objects = null!;
```

- Removes references to the created objects.
- The objects become eligible for garbage collection.
- Memory is not reclaimed immediately after the references are removed.

### Manual Garbage Collection

```csharp
GC.Collect();
GC.WaitForPendingFinalizers();
GC.Collect();
```

- Forces the Garbage Collector to run.
- Reclaims memory occupied by unreachable objects.
- Results in lower memory consumption after collection.
- Execution time can be measured using `Stopwatch` to observe the performance cost.

## Learning Outcome

After completing this task, I gained an understanding of:

- How the .NET Garbage Collector manages memory.
- The difference between releasing references and freeing memory.
- How unmanaged memory pressure can affect application behavior.
- How the GC.GetTotalMemory() and how it differs from the heap size indicator in the diagnostics tool.
- Why explicitly calling GC.Collect() is not good in most cases.
- How memory allocations and object lifetimes influence application efficiency.

---

# Task 4 – Implementing and Understanding the IDisposable Interface and the Using Statement

This task demonstrates the importance of releasing unmanaged resources using the `IDisposable` interface and the `using` statement. The application performs file read and write operations both with and without disposing resources to observe the difference in behavior.

## Concepts Covered

- IDisposable Interface
- Using Statement
- Resource Management
- File Handling

## Key Observation

- Files that remain open can block other operations from accessing them.
- Objects implementing `IDisposable` should be properly disposed once they are no longer needed.
- The `using` statement automatically calls the `Dispose()` method when execution leaves its scope.
- Proper disposal ensures that file handles and other resources are released immediately.
- Failing to dispose resources can lead to file locks, memory overhead, and resource leaks.

## Resource Management Analysis

### Without Dispose

```csharp
FileWriter fileWriter = new FileWriter(filePath);
```

- A file is opened for writing.
- The file resource remains active because `Dispose()` is never called.
- Attempting to open the same file for reading results in an `IOException`.
- The file remains locked until the application exits or the object is garbage collected.

### With Dispose

```csharp
using (FileWriter fileWriter = new FileWriter(filePath))
{
    fileWriter.WriteIntoFile(contents);
}
```

- The file is opened within a `using` block.
- The `Dispose()` method is automatically invoked when the block completes.
- The file resource is released immediately.
- The file can be accessed safely for subsequent read operations.

### Dispose Implementation

```csharp
public void Dispose()
{
    this.streamWriter.Dispose();
}
```

- Releases the underlying file stream.
- Frees operating system resources associated with the file.
- Prevents resource leaks and file locking issues.

## Learning Outcome

After completing this task, I gained an understanding of:

- The purpose of the `IDisposable` interface.
- How the `Dispose()` method is used to release resources explicitly.
- How the `using` statement simplifies resource management.
- The difference between garbage collection and resource disposal.
- Why file streams should always be disposed after use.
- How improper resource management can cause file access exceptions.

---

# Project Structure

```text
Assignment11
│
├── Task1-ValueAndReferenceTypes
│   ├── College.cs
│   └── Program.cs
│   └── Student.cs
│
├── Task2-StackAndHeap
│   ├── College.cs
│   ├── Program.cs
│   └── Student.cs
│
├── Task3-GarbageCollection
│   ├── LargeObject.cs
│   └── Program.cs
│
├── Task4-IDisposableInterface
│   ├── FileWriter.cs
│   ├── FileReader.cs
│   └── QueryBuilder.cs
│
└── README.md
```

---