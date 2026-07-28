# Assignment 3
# Inventory Management System
 
A console-based inventory management system is done using C# and MVC Architecture.
 
## Features
 
### Add Products
- You can add products with name, price, and quantity.
- Product IDs are generated automatically.
- Input validation for all product details is done.
 
### Display Products
- View all products in the inventory.
- Products are displayed in alphabetical order.
- Handles conditions for empty product inventory.
 
### Search Products
- Search products using Product ID.
- Validates Product ID before searching.
 
### Edit Products
- Edit a product's name or price.
- Supports multiple edits in a single session.
 
### Delete Products
- Delete products using Product ID.
- Displays product details before deletion.
- Asks for confirmation before deleting from the inventory.
 
### Stock Management
- Restock existing products.
- Reduce stock with custom exception handling for insufficient stock.
- View products with low stock.
 
## Techniques Used
 
- MVC Architecture
- Repository Pattern
- List of Product objects for in-memory storage
- Helper class for input validation
- ConsoleTables for tabular output
 
## Challenges Faced
 
- Understanding and implementing the MVC architecture.
- Separating responsibilities across Controller, Service, Repository, Helper, and View.
- Trying to optimize the code by using private const strings and do-while loops with constructor injection.

## Future Implementations

- Upgrade the storage by using a database or JSON file.