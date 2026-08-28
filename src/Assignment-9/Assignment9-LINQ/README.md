# Assignment 9 - LINQ

This assignment consists of five different tasks that help understand the core concepts of LINQ in C#. 
The assignment demonstrates querying collections using LINQ operators, working with projections, grouping, joining data, analyzing query performance, and implementing a Fluent API Query Builder.

The five different tasks are as follows.

---

# Task 1 – Basic LINQ Queries

This task introduces fundamental LINQ operations such as filtering, projection, sorting, and aggregation.
The program filters products belonging to the **Electronics** category with a price greater than **$500**, projects the required fields, sorts the filtered results in descending order of price, and calculates the average price of the selected products.

## Understanding from the task

- Filtering using `Where`
- Projection using `Select`
- Sorting using `OrderByDescending`
- Aggregation using `Average`
- Working with tuples

---

# Task 2 – Complex LINQ Queries

This task demonstrates more advanced LINQ operations including grouping and joining data from multiple collections.
The program groups products by category and identifies the most expensive product within each category. It also joins products and suppliers using a common key and displays the combined information.

## Understanding from the task

- Grouping using `GroupBy`
- Aggregations within groups
- Joining collections using `Join`
- Selecting related data from multiple collections
- Working with grouped data

---

# Task 3 – LINQ to Objects

This task focuses on applying LINQ operations to in-memory collections.
The program works with an integer array to determine the second highest distinct number and find all pairs of numbers whose sum equals a specified target value.

## Understanding from the task

- LINQ to Objects
- Removing duplicates using `Distinct`
- Ordering data using `OrderByDescending`
- Skipping elements using `Skip`
- Generating combinations using `SelectMany`
- Finding custom conditions using LINQ

---

# Task 4 – LINQ Performance Analysis

This task demonstrates how query structure affects performance.
Two similar queries are executed:

1. Sorting all products first and then filtering books.
2. Filtering books first and then sorting the smaller result set.

Execution times are measured using a `Stopwatch` to compare the approaches.

## Understanding from the task

- Deferred execution
- Query optimization
- Filtering before sorting
- Measuring performance using `Stopwatch`
- Efficient LINQ query design

---

# Task 5 – Fluent API Query Builder

This task implements a custom Fluent API Query Builder that enables dynamic query composition on collections.
The QueryBuilder utility supports:

- Dynamic filtering using Expression Trees
- Multiple filter conditions
- Sorting
- Joining collections
- Query execution

The utility allows methods to be chained together fluently, making query construction more readable and maintainable.
Expression Trees are used to dynamically generate LINQ filter expressions at runtime based on the property name, filter value, and selected filter condition. 
This allows the QueryBuilder to support flexible filtering without requiring hardcoded predicates.

##### Filter Condition Examples

Contains:

```csharp
.Filter("ProductName", "Phone", FilterCondition.Contains)
```

Starts With:
```csharp
.Filter("ProductName", "S", FilterCondition.StartsWith)
```

Ends With:
```csharp
.Filter("ProductName", "r", FilterCondition.EndsWith)
```

Greater Than Or Equal To:
```csharp
.Filter("Price", 200, FilterCondition.GreaterThanOrEqualTo)
```

Less Than Or Equal To:
```csharp
.Filter("Price", 200, FilterCondition.LessThanOrEqualTo)
```

The task also demonstrates:

1. QueryBuilder with filtering, sorting, and execution.
2. QueryBuilder with filtering, sorting, joining, and execution.

## Understanding from the task

- Fluent API Pattern
- Method Chaining
- LINQ Query Composition
- Custom Utility Design
- Reusability and Extensibility

---

# QueryBuilder Design

The `QueryBuilder<T>` class provides a fluent interface for constructing LINQ queries.

### Supported Methods

#### Filter

Filters the collection dynamically using a property name, filter value, and filter condition. 
Supported filter conditions: 
- Contains
- StartsWith 
- EndsWith
- GreaterThanOrEqualTo
- LessThanOrEqualTo

```csharp
.Filter("Price", 200, FilterCondition.GreaterThanOrEqualTo)
```

#### SortBy

Sorts the collection based on a specified property.

```csharp
.SortBy(product => product.Price)
```

#### Join

Joins two collections using matching keys.

```csharp
.Join(
    context.Suppliers,
    product => product.ProductId,
    supplier => supplier.ProductId,
    (product, supplier) => new
    {
        product.ProductName,
        supplier.SupplierName
    })
```

#### Execute

Materializes the query and returns the result as a list.

```csharp
.Execute();
```

---

# Project Structure

```text
Assignment9-LINQ
│
├── Data
│   ├── SampleDatabaseContext.cs
│   └── SampleDataLoader.cs
│
├── Model
│   ├── Order.cs
│   ├── Product.cs
│   └── Supplier.cs
│
├── Tasks
│   ├── Task1.cs
│   ├── Task2.cs
│   ├── Task3.cs
│   ├── Task4.cs
│   └── Task5.cs
│
├── Utils
│   └── QueryBuilder.cs
│   └── FilterCondition.cs
│
├── Program.cs
│
└── README.md
```

---

# File Overview

## Data

### SampleDatabaseContext.cs
Contains the collections used throughout all tasks.

### SampleDataLoader.cs
Loads sample data into the database context.

---

## Model

### Product.cs
Represents a product entity.

### Supplier.cs
Represents a supplier entity.

### Order.cs
Represents an order entity.

---

## Tasks

### Task1.cs
Demonstrates basic LINQ queries involving filtering, sorting, projection, and aggregation.

### Task2.cs
Demonstrates grouping and joining operations using LINQ.

### Task3.cs
Demonstrates LINQ to Objects using arrays and numerical operations.

### Task4.cs
Analyzes and compares the performance of different LINQ query structures.

### Task5.cs
Demonstrates the implementation and usage of the custom Fluent API QueryBuilder.

---

## Utils

### QueryBuilder.cs
Defines the custom generic QueryBuilder class used for fluent query construction, dynamic filtering using Expression Trees, sorting, joining, and query execution.

### FilterCondition.cs
Defines the enum for the different filter conditions given by the user.

---

## Program.cs
Provides a menu-driven interface for executing each task individually.

---

# Recommended PR Review Order

For the best understanding of the implementation, review the project in the following order:

## 1. Task 1 – Basic LINQ Queries

Focus on:
- Where
- Select
- OrderByDescending
- Average
- Tuple projections

---

## 2. Task 2 – Complex LINQ Queries

Focus on:
- GroupBy
- Join
- Aggregations
- Group processing

---

## 3. Task 3 – LINQ to Objects

Focus on:
- Distinct
- Skip
- SelectMany
- In-memory querying

---

## 4. Task 4 – LINQ Performance Analysis

Focus on:
- Deferred execution
- Query optimization
- Stopwatch
- Filtering before sorting
- Performance comparison

---

## 5. Task 5 – Fluent API Query Builder

Focus on:
- Fluent API Pattern
- Method Chaining
- Expression Trees
- Dynamic Query Generation
- Multiple Filter Conditions

---

# Challenges Faced

- Understanding how deferred execution works in LINQ.
- Working with grouping and joining operations.
- Finding efficient ways to query and transform collections.
- Using right measurements for query performance using Stopwatch.
- Designing a reusable QueryBuilder using the Fluent API pattern.
- Implementing generic methods that support filtering, sorting, and joining operations.
- Understanding and implementing Expression Trees.
- Dynamically generating LINQ expressions at runtime.
- Handling invalid property names and unsupported filter operations.