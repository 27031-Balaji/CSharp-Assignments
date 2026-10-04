# Assignment 10 - Exploration Documentation

This exploration focuses on understanding the core concepts and architecture of the .NET platform.

## 1. Explain what the .NET platform is and its primary purpose. 

### Answer:
.NET is a software development platform created by Microsoft that is used to build, run, and manage various types of applications. It supports languages like C#, F#, and Visual Basic. 

.NET's main purpose is to provide:

- A common runtime to execute applications using the CLR (Common Language Runtime)
- Reusable libraries for file handling, JSON, LINQ, etc.,
- Frameworks for different types of applications like console application, desktop application, web application, etc., 

## 2. What are the key components of the .NET platform? 

### Answer:
These are the important key components of the .NET platform:

**CLR (Common Language Runtime)**: The CLR is used to execute the .NET applications and provides services such as memory management, garbage collection, exception handling. 

**CIL/IL (Common Intermediate Language)**: When a .NET application is compiled by Roslyn, the code is converted to intermediate language, so that, the CLR then converts the IL to machine code for that specific OS. 

**JIT (Just-In-Time Compiler)**: JIT converts the intermediate language to native machine code so that the CPU can execute.

**GC (Garbage Collector)**: The GC automatically manages the unused memory to eliminate memory fragmentation. The objects which are no longer reachable will be collected by the GC for more objects to occupy.

**Frameworks**: .NET supports many frameworks like ASP.NET Core, Windows Forms, WPF, .NET MAUC.

## 3. Differentiate between the Common Language Runtime (CLR) and Common Type System (CTS) in .NET.

### Answer:
| Common Language Runtime (CLR) | Common Type System (CTS) |
|-------------------------------|---------------------------|
| It is the runtime environment of .NET. | It is the set of rules and specifications for types in .NET. |
| Responsible for executing .NET code. | Defines how data types are represented and behave. |
| Handles JIT compilation, garbage collection, memory management, threading, and exception handling. | Defines value types, reference types, classes, structs, enums, interfaces, and other type definitions. |
| Provides services required during application execution. | Ensures type compatibility and language interoperability across .NET languages. |
| Example: JIT converts IL into native machine code. | Example: `int` in C# corresponds to `System.Int32`. |

## 4. What is the role of the Global Assembly Cache (GAC) in .NET? 

### Answer:
- The Global Assembly Cache (GAC) stores assemblies specifically designed to be shared by many applications on the computer.

- For example, when three applications use the same library (.dll file), then loading these libraries separately is inefficient, and all three applications should maintain their own copy of the library. 

- Instead, when we install the library to the GAC, a compatible shared assembly is created, so that the library can be used in many applications easily.

- GAC works as a centralized assembly storage for many applications to use.

## 5. Explain the difference between value types and reference types in C#. 

### Answer:
| Value Types | Reference Types |
|-------------|-----------------|
| Stores the actual value directly. | Stores a reference to an object. |
| Typically allocated inline (commonly on the stack for local variables or within containing objects). | The reference is stored in the variable, while the object is typically allocated on the heap. |
| When assigned to another variable, a copy of the value is created. | When assigned to another variable, both variables refer to the same object. |
| Changes made to one copy do not affect the other. | Changes made through one reference are visible through all references to the same object. |
| Examples: `int`, `float`, `bool`, `DateTime`, `struct`, `enum`. | Examples: `class`, `interface`, `string`, `array`, `List<T>`, `object`. |

## 6. Describe the concept of garbage collection on .NET and its advantages. 

### Answer:
Garbage collection (GC) is a mechanism in .NET which automatically manages the unused memory, and it is provided by the .NET CLR. Its main purpose is to automatically find objects which are no longer reachable and clean them from the memory. So instead of manually freeing the memory, GC does it in the background.

- The GC determines whether the object is still reachable through GC roots.

- GC roots are objects or references that serve as starting points for the GC to determine which objects in memory are reachable and should not be collected.

- As long as the object is reachable, the object is considered alive. 

- There are mainly 3 generations in a GC. 

     - **Generation 0**: Contains newly allocated objects. Here, most objects are short-lived, so many objects can be collected here. 

     - **Generation 1**: Objects that survive a Gen 0 collection are promoted to Gen 1. 

     - **Generation 2**: Objects that survive longer collections will be promoted to Gen 2. Gen 2 collections occur less frequently because they are more expensive.

#### Advantages of Garbage Collector: 

- Automatic Memory Management: The GC automatically identifies and removes objects that are no longer referenced. 

- Prevents Memory Leaks: Objects that are longer reachable will be removed and reclaimed by the GC, reducing the risk of forgetting to release the memory. 

- Automatic Heap Collection: The GC moves objects to free space for new objects to occupy, eliminating memory fragmentation.

## 7. What is the purpose of Globalization and Localization features in .NET? 

### Answer:
#### Globalization:

Globalization means designing the application in such a way that it can work with different cultures and regions without requiring major code changes. Example: Using CultureInfo class in C# for dates and currencies.

#### Localization:

Localization means designing the application for a particular language, region, or culture. This is commonly done by using resource files (.resx) for translated UI text. Example: Printing the welcome message as "Welcome" for English and "Bienvenue" for French.

## 8. Explain the role of the Common Intermediate Language (CIL) and Just-In-Time (JIT) compilation in the .NET framework. 

### Answer:
#### Common Intermediate Language (CIL):  

- It is the intermediate code produced when a .NET language such as C# is compiled.  

- It is mainly CPU-independent. 

- It is produced by the language compiler called Roslyn. 

- This allows the .NET runtime to work with code produced by different .NET languages.

#### Just-In-Time Compiler (JIT):  

- The JIT takes the CIL and compiles it into native machine code that can be executed on the current OS and processor.

- This happens while the application is running, thus the name Just-In-Time is given.

- JIT is a feature in Common Language Runtime (CLR) responsible for compiling CIL to native machine code.