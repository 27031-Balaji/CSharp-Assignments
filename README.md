# Assignment 3
# Inventory Management System
 
A console-based inventory management system handling name, price and quantity of stock available with many features.
 
## Features
 
### Add Products
- You can add products with name, price, and quantity.
- Product IDs are generated automatically. It is a 12-character ID extracted from GUID.
- Input validation for all product details is done.
 
### Display Products
- View all products in the inventory.
- Products are displayed in alphabetical order.
- Handles exceptions for empty product inventory.
 
### Search Products
- Search products using Product ID.
- Validates Product ID before searching.
- Handles exception for product not found in the repository.
 
### Edit Products
- Edit a product's name or price using Product ID.
- Supports multiple edits in a single session.
 
### Delete Products
- Delete products using Product ID.
- Displays product details before deletion.
- Asks for confirmation before deleting from the inventory.
 
### Stock Management
- Restock existing products.
- Reduce stock with exception handling for insufficient stock.
- View products with low stock.
 
## Techniques Used
 
- MVC Architecture
- List of Product objects for in-memory storage
- Helper class for input validation
- ConsoleTables for tabular output
 
## Challenges Faced
 
- Understanding and implementing the MVC architecture.
- Separating responsibilities across Controller, Service, Repository, Helper, and View.
- Trying to optimize the code by using private const strings and do-while loops.