# Assignment 17 - Reflection

This assignment consists of different tasks that help understand the core concepts of Reflection and Reflection.Emit in C#.

The assignment demonstrates inspecting assembly metadata, dynamically inspecting and modifying objects, invoking methods at runtime, 
creating types dynamically, generating mock implementations, and comparing Reflection-based serialization with Reflection.Emit.

The different tasks are as follows.

---

# Task 1 – Inspect Assembly Metadata

This task demonstrates the fundamentals of Reflection by inspecting the metadata of a .NET assembly.

The program accepts an assembly file path, loads the assembly using Assembly.LoadFile(), retrieves all types using GetTypes(), and displays the methods, properties, fields, and events associated with each type.

If a type does not contain a particular member category, an appropriate message is displayed.

---

# Task 2 – Dynamic Object Inspector

This task demonstrates how Reflection can be used to inspect and modify an object's properties at runtime.

The program accepts an object, retrieves its type using GetType(), and displays all of its properties and their current values using GetProperties() and GetValue().

The user is then allowed to update a property by providing the property name and a new value. The property is located using GetProperty() and updated dynamically using SetValue().

A Person object is used to demonstrate the inspector functionality, allowing the user to view and modify the Name, Age, and City properties during runtime.

---

# Task 3 – Dynamic Method Invoker

This task demonstrates how Reflection can be used to dynamically invoke methods at runtime based on a method name provided as a string.

The program accepts an object and a method name as input. Using GetType(), it retrieves the type information of the object and locates the specified method using GetMethod(). 
The method is then executed dynamically using `Invoke()`.

A `Calculator` class is used to demonstrate the functionality, allowing the user to invoke methods such as `Add`, `Subtract`, and `Multiply` by simply providing the method name at runtime.

---

# Task 4 – Dynamic Type Builder

This task demonstrates the use of System.Reflection.Emit to create types dynamically at runtime.

The program creates a dynamic assembly and module using AssemblyBuilder and ModuleBuilder. 
A new type named Employee is then created using TypeBuilder. The type includes a dynamically generated property named Name and a method named Display.

Getter and setter methods for the property are defined using MethodBuilder and implemented through ILGenerator. 
The Display method is also generated dynamically and prints a message to the console.

Once the type definition is complete, CreateType() is used to create the runtime type. 
An instance of the generated type is then created using Activator.CreateInstance(), and Reflection is used to set the property value and invoke the dynamically generated method.

---

# Task 5 – Plugin System

This task demonstrates how Reflection can be used to build a plugin-based application that discovers and loads external assemblies at runtime.

A common interface named `IPlugin` is defined to establish a contract that all plugins must follow. Three plugin implementations were created:

- Atbash Cipher Plugin
- Base64 Plugin
- Caesar Cipher Plugin

The main application scans a plugin directory, loads all available plugin assemblies using `Assembly.LoadFrom()`, and uses Reflection to identify classes that implement the `IPlugin` interface.

Instances of the discovered plugins are created dynamically using `Activator.CreateInstance()`. 
The application then interacts with the plugins through the common interface, allowing users to select a plugin and perform encryption or decryption operations without requiring compile-time knowledge of the plugin implementation.

This demonstrates extensibility, as new plugins can be added to the application simply by placing their DLL files in the plugin folder without modifying the main application.

---

# Task 6 – Mocking Framework

This task demonstrates how `Reflection.Emit` can be used to dynamically generate mock implementations of interfaces for testing purposes.

The existing `IPlugin` interface from the Plugin System task was used as the interface to be mocked. 
A dynamic type named `MockPlugin` is created at runtime using `TypeBuilder` and configured to implement the `IPlugin` interface.

The mock type provides implementations for the `PluginName`, `Encrypt`, and `Decrypt` members using `PropertyBuilder`, `MethodBuilder`, and `ILGenerator`. 
Instead of performing actual encryption or decryption, the methods return predefined values suitable for testing.

Once the type is created using `CreateType()`, an instance is generated using `Activator.CreateInstance()` and used just like a normal plugin implementation. 
This allows unit tests to verify application behavior without relying on real plugin implementations.

---

# Task 7 – Serialization API

This task demonstrates how Reflection and `Reflection.Emit` can be used to serialize objects into a string representation and compares their performance.

A simple serializer was first implemented using Reflection. The serializer accepts an object, retrieves its properties using `GetProperties()`, 
obtains their values using `GetValue()`, and constructs a serialized string containing the property names and values.

The serializer was tested using a `Person` object and successfully converted the object's state into a readable string format.

After testing, several limitations were identified, including the performance overhead of repeatedly using Reflection APIs and the inability to efficiently handle complex object graphs. 
To address these limitations, a second serializer was implemented using `Reflection.Emit`.

The `Reflection.Emit` implementation generates a dynamic serialization method at runtime using `DynamicMethod` and `ILGenerator`. 
This dynamically generated method directly accesses the object's properties and builds the serialized string without repeatedly invoking Reflection APIs.

Finally, both implementations were executed repeatedly using a `Stopwatch` and their execution times were compared to evaluate performance and correctness.

---

# Challenges Faced

- Understanding how Reflection retrieves metadata at runtime.
- Working with assemblies, types, properties, methods, and events dynamically.
- Using `BindingFlags` to retrieve specific members.
- Generating types and methods dynamically using `Reflection.Emit`.
- Writing IL instructions using `ILGenerator`.
- Implementing dynamic plugin discovery and loading.
- Creating mock implementations of interfaces at runtime.
- Comparing the performance of Reflection and `Reflection.Emit`.
- Designing a reusable serialization mechanism.
- Handling runtime type creation and object instantiation.