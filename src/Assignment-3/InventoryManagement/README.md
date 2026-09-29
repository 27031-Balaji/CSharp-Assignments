# Inventory Management System
 
This is a basic Inventory Management System developed in C# using the MVC architecture. It allows users to manage products in an inventory with proper input validation and stock management.
 
## Features
 
- Add Products
- Edit Products
- Search Products
- View All Products
- Delete Products
- Restock Products
- Reduce Stock
- View Low Stock Products
 
---
 
# Feature Details
 
## Add Products
 
The Add Product feature allows users to add a new product to the inventory.
 
### Functionalities
 
1. Users can enter a product name, price and quantity.
2. A unique product ID is generated automatically. It is a 8-digit hex-charactered string extracted from GUID.
3. Product name, price and quantity are validated before the product is added.
 
---
 
## Edit Products
 
The Edit Product feature allows updating an existing product.
 
### Functionalities
 
1. Users can edit a product's name or price.
2. Products are searched using the product ID before editing.
3. Updated values are validated before saving.
4. Multiple edits can be performed until the user exits the edit menu.
 
---
 
## Search Products
 
The Search Product feature retrieves products either by product ID or product name.
 
### Functionalities
 
1. Users can search using a product ID.
2. Users can search using a product name with partial matching.
3. Product ID and product name are validated before searching.
4. Matching product details are displayed if found, otherwise an exception message is shown.
 
---
 
## View All Products
 
The View All Products feature displays every product in the inventory.
 
### Functionalities
 
1. Users can view the complete product list.
2. Products are displayed in a formatted table.
3. An appropriate exception message is displayed when the inventory is empty.
 
---
 
## Delete Products
 
The Delete Product feature removes a product from the inventory.
 
### Functionalities
 
1. Users can delete a product using its product ID.
2. The product ID is validated before deletion.
3. A confirmation prompt is displayed before removing the product.
4. A success message is displayed after deletion.
 
---
 
## Restock Products
 
The Restock Product feature increases the stock quantity of a product.
 
### Functionalities
 
1. Users can restock a product using its product ID.
2. The entered quantity is validated before updating.
3. The stock quantity is increased after successful validation.
 
---
 
## Reduce Stock
 
The Reduce Stock feature decreases the stock quantity of a product.
 
### Functionalities
 
1. Users can reduce stock using the product ID.
2. The entered quantity is validated before updating.
3. Stock cannot be reduced below zero.
4. An appropriate exception message is displayed if sufficient stock is unavailable.
 
---
 
## View Low Stock Products
 
The View Low Stock Products feature displays products whose stock is below the predefined threshold.
 
### Functionalities
 
1. Users can view all low-stock products.
2. Products are displayed in a formatted table.
3. An appropriate message is displayed when there are no low-stock products.
 
---
 
# Project Architecture
 
The application follows the MVC architecture.
 
**Model → Repository → Service → Controller → View**
 
## Model
 
Stores the product information.
 
## Repository
 
Stores the product list and performs CRUD operations.
 
## Service
 
Contains the business logic and communicates with the repository.
 
## Controller
 
Coordinates the application flow by receiving user input, validating data using helper methods, invoking service methods and directing the appropriate view.
 
## View
 
Handles all console input and output operations, including menus, prompts and displaying messages.
 
## Helper
 
Provides reusable validation methods for product IDs, names, prices and quantities.
 
---
 
# Techniques Used
 
- A list of objects is used to store product information.
- The Repository layer performs CRUD operations.
- The Service layer implements the business logic.
- The Controller layer coordinates the application flow.
- The View layer handles all console input and output operations.
- The Helper class performs reusable input validation.
- Custom exceptions are used for handling inventory-related errors.
- ConsoleTables is used to display products in a tabular format.
 
---

# Project Structure

```text
InventoryManagement
│
├── Controller
│   └── ProductController.cs
│
├── Exception
│   └── EmptyInventoryException.cs
│   └── InsufficientStockException.cs
│   └── ProductNotFoundException.cs
│
├── Helper
│   └── ConsoleMessages.cs
│   └── ProductHelper.cs
│
├── Model
│   └── Product.cs
│
├── Repository
│   └── ProductRepository.cs
│
├── Service
│   └── ProductService.cs
│
├── View
│   └── ConsoleOperation.cs
│
├── Program.cs
│
└── README.md
```

## File Overview

- **ProductController.cs**: Acts as an intermediate between the View and Service layers by validating user inputs and invoking the required business operations.
- **EmptyInventoryException.cs**: Custom exception thrown when operations are performed on an empty inventory.
- **InsufficientStockException.cs**: Custom exception thrown when stock reduction exceeds the available quantity.
- **ProductNotFoundException.cs**: Custom exception thrown when a requested product cannot be found.
- **ConsoleMessages.cs**: Stores reusable success, error, warning, and information messages displayed to the user.
- **ProductHelper.cs**: Provides reusable validation methods for Product ID, Product Name, Product Price, and Product Quantity.
- **Product.cs**: Represents the product model and stores Product ID, Name, Price, and Quantity details.
- **ProductRepository.cs**: Repository class used to store and retrieve products from an in-memory collection and perform CRUD operations.
- **ProductService.cs**: Communicates with the repository and contains business logic related to inventory management.
- **ConsoleOperation.cs**: Handles all console UI operations such as displaying menus, reading user input, displaying messages, and presenting products in tabular format.
- **Program.cs**: Creates object instances, injects dependencies, and starts the Inventory Management workflow.

---

# Recommended PR Review Order

For the best understanding of the implementation, review the project files in the following order:

```text
Model
↓
Helper
↓
Exception
↓
Repository
↓
Service
↓
View
↓
Controller
↓
Program.cs
```

This order follows the dependency flow of the application and helps to understand how products are created, validated, stored, processed, displayed, and managed throughout the application. Backtracking between layers can also be done to understand the complete implementation of a particular feature.

---
 
# How to Run the Application
 
## Prerequisites
 
- .NET 6 SDK or later
- Visual Studio 2022 or Visual Studio Code
 
## Steps
 
1. Clone or download the project.
2. Open the solution in Visual Studio.
3. Build the project.
4. Run the application.
 
---
 
# Challenges Faced
 
- Implementing the MVC architecture while maintaining separation of concerns.
- Designing reusable validation methods to reduce duplicate code.
- Handling inventory-specific exceptions using custom exception classes.
- Implementing partial product name search and stock management features.
 
---
 
# Future Enhancements
 
- Store products in a file or database instead of memory.
- Support editing additional product details.
- Generate inventory reports.
- Improve the console user interface.