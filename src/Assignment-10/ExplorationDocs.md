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

**CLR (Common Language Runtime)**: The CLR is the execution engine of .NET. It manages application execution and provides services such as memory management, garbage collection, exception handling, type safety, and security. 

**CIL/IL (Common Intermediate Language)**: When .NET language code is compiled, the source code is converted into Intermediate Language (IL). IL is CPU-independent code that can run on any platform supported by .NET. 

**JIT (Just-In-Time Compiler)**: The JIT compiler translates IL into native machine code for the current CPU architecture and operating system at runtime.

**GC (Garbage Collector)**: The Garbage Collector automatically reclaims memory occupied by objects that are no longer reachable by the application. Its primary purpose is memory management, and memory compaction is a secondary benefit.

**BCL (Base Class Library)**: The Base Class Library provides a set of reusable classes and APIs for common programming tasks such as file handling, collections, networking, etc.

**.NET SDK and CLI**: The .NET SDK contains the tools required to build, run, test, and publish .NET applications. The .NET CLI (dotnet) provides command-line commands for these development tasks.

**NuGet**: NuGet is the package management system for .NET. It allows developers to install, update, and manage third-party packages within their applications.

**CTS (Common Type System)**: The Common Type System defines how data types are declared, used, and managed within .NET.

**CLS (Common Language Specification)**: The Common Language Specification is a set of rules that compilers must follow to ensure interoperability between different .NET languages.

**Frameworks**: .NET supports many frameworks like ASP.NET Core, Windows Forms, WPF, .NET MAUI.

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
- The Global Assembly Cache (GAC) is a storage location in .NET Framework for assemblies that are shared by multiple applications.
- Assemblies must be strong-named before they can be installed into the GAC.
- The GAC supports side-by-side versioning, allowing multiple versions of the same assembly to exist on the same machine.
- Instead of each application having its own copy of a shared assembly, the existing assembly can be installed into the GAC and used by multiple applications.
- The GAC is available only in .NET Framework. It is not available in .NET Core or .NET 5+.
- In modern .NET, shared libraries are typically distributed through NuGet packages and the shared framework rather than the GAC.

## 5. Explain the difference between value types and reference types in C#. 

### Answer:
| Value Types | Reference Types |
|-------------|-----------------|
| Stores the actual value directly. | Stores a reference to an object. |
| Typically allocated inline (commonly on the stack for local variables or within containing objects). | The reference is stored in the variable, while the object is typically allocated on the heap. |
| When assigned to another variable, a copy of the value is created. | When assigned to another variable, both variables refer to the same object. |
| Changes made to one copy do not affect the other. | Changes made through one reference are visible through all references to the same object. |
| Cannot be null by default. To allow null, use nullable value types such as int? or DateTime?. | Can be null by default. |
| Default value is the zero-initialized value of the type. | Default value is null. |
| Can be boxed into an object or interface type and later unboxed back to the original value type. | No boxing or unboxing is required. |
| Examples: `int`, `bool`, `DateTime`, `DayOfWeek`, `MyStruct`. | Examples: `string`, `object`, `int[]`, `List<int>`, `MyClass`. |

## 6. Describe the concept of garbage collection on .NET and its advantages. 

### Answer:
Garbage collection (GC) is a mechanism in .NET which automatically manages the unused memory, and it is provided by the .NET CLR. Its primary purpose is to automatically identify objects that are no longer reachable and reclaim the memory occupied by them. This removes the need for developers to manually allocate and free memory.

- The GC determines whether the object is still reachable through GC roots.

- GC roots are references that serve as starting points for the GC to determine which objects in memory are reachable and should not be collected.

- As long as an object is reachable from a GC root, it is considered alive and will not be collected. 

- There are mainly 3 generations in a GC. 

     - **Generation 0**: Contains newly allocated objects. Here, most objects are short-lived, so many objects can be collected here. 

     - **Generation 1**: Objects that survive a Gen 0 collection are promoted to Gen 1. 

     - **Generation 2**: Objects that survive longer collections will be promoted to Gen 2. Gen 2 collections occur less frequently because they are more expensive.

- Large objects (85 KB or larger) are allocated on the Large Object Heap (LOH). The LOH is collected during Generation 2 collections and is handled differently from the small object heap.

#### Advantages of Garbage Collector: 

- Automatic Memory Management: The GC automatically identifies and removes objects that are no longer referenced. 

- Reduced Risk of Memory Leaks: The GC helps prevent many memory leaks caused by forgetting to release managed objects. However, managed memory leaks can still occur, such as static references, long-lived collections, event handlers that are never unsubscribed, etc. 

- Heap Compaction: The GC moves objects to free space for new objects to occupy, eliminating memory fragmentation.

#### Important Note on GC:
Garbage collection only manages managed memory. Resources such as file handles, database connections and unmanaged memory are not automatically released by the GC. Such resources should be cleaned up using IDisposable and the using statement.

## 7. What is the purpose of Globalization and Localization features in .NET? 

### Answer:
#### Globalization:

Globalization is the process of designing and developing an application so that it can support multiple cultures and regions without requiring major code changes. It ensures that culture-specific information, such as dates, times, numbers, and currencies, can be displayed and processed correctly for different users.

Example: Using the CultureInfo class to format dates and currency values according to the user's culture. 

- CurrentCulture determines culture-specific formatting, such as dates, times, numbers, and currencies.
Eg: $1,234.56 vs €1.234,56.

#### Localization:

Localization is the process of adapting an already globalized application to a specific language, region, or culture. This is typically done by translating resources and providing culture-specific content.

Example: Displaying "Welcome" in English and "Bienvenue" in French using .resx resource files.

- Localized resources are commonly stored in resource files and compiled into satellite assemblies.
- CurrentUICulture determines which localized resources (such as UI text) are loaded.

## 8. Explain the role of the Common Intermediate Language (CIL) and Just-In-Time (JIT) compilation in the .NET framework. 

### Answer:
#### Common Intermediate Language (CIL):  

- Common Intermediate Language (CIL), also known as IL, is the intermediate code generated when .NET languages.
- CIL is CPU-independent, which allows the same assembly to run on different platforms supported by .NET. 
- Language compilers convert source code into CIL. For example, Roslyn compiles C# and VB.NET code into CIL, while F# uses its own compiler. 
- CIL is stored in an assembly together with metadata, which contains information about types, methods, references, and assembly details.
- Because all .NET languages compile to the same intermediate language, the CLR can execute code from different .NET languages consistently.

#### Just-In-Time Compiler (JIT):  

- The JIT compiler converts CIL into native machine code for the current operating system and processor architecture.
- JIT compilation occurs at runtime when a method is called for the first time.
- Once a method has been compiled, the generated native code is reused, so it does not need to be compiled again.
- This approach allows .NET applications to remain platform-independent while still achieving good runtime performance.

#### Other Compilation Approaches:

- **Tiered Compilation**: The runtime can initially generate quickly compiled code and later recompile frequently used methods.
- **ReadyToRun (R2R)**: Assemblies can be precompiled ahead of time to reduce JIT compilation work.
- **Native AOT (Ahead-of-Time Compilation)**: The application is compiled directly to native machine code before deployment.