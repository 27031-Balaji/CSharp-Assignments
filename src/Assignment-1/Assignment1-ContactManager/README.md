# Basic Contact Manager
 
This is a basic Contact Manager application developed in C# using the MVC architecture. It allows users to perform CRUD operations on contacts while validating user inputs.
 
## Features
 
- Add Contacts
- Display Contacts
- Search Contacts
- Delete Contacts
- Edit Contacts
 
---
 
# Feature Details
 
## Add Contacts
 
The Add Contact feature allows users to create a new contact.
 
### Functionalities
 
1. Users can enter a contact's name, email address, phone number and optionally add notes.
2. Duplicate phone numbers are not allowed.
3. The entered name, email and phone number are validated before the contact is added.
 
---
 
## Display Contacts
 
The Display Contacts feature shows all available contacts.
 
### Functionalities
 
1. Users can view the complete contact list.
2. An appropriate message is displayed when the contact list is empty.
3. The sorted version of the contact list is displayed.
 
---
 
## Search Contacts
 
The Search Contact feature retrieves a contact using its phone number.
 
### Functionalities
 
1. Users can search for a contact using the registered phone number.
2. The entered phone number is validated before searching.
3. The matching contact details are displayed if the contact exists, else an error is shown.
 
---
 
## Delete Contacts
 
The Delete Contact feature removes a contact from the contact list.
 
### Functionalities
 
1. Users can delete a contact using its phone number.
2. The entered phone number is validated before deletion.
3. A confirmation message is displayed after the operation.
 
---
 
## Edit Contacts
 
The Edit Contact feature allows updating an existing contact.
 
### Functionalities
 
1. Users can edit a contact's name, email, phone number or notes.
3. Updated values are validated before saving.
 
---
 
# Project Architecture
 
The application follows the MVC architecture.
 
**Model → Repository → Service → Controller → View**
 
## Model
 
Stores the contact information.
 
## Repository
 
Stores the contact list and performs CRUD operations.
 
## Service
 
Contains the business logic and communicates with the repository.
 
## Controller
 
Coordinates the application flow by receiving user input, validating data using helper methods, invoking service methods and directing the appropriate view.
 
## View
 
Handles all console input and output operations, including menus, prompts and displaying messages.
 
## Helper
 
Provides reusable validation methods for names, email addresses and phone numbers.
 
---
 
# Techniques Used
 
- A list of objects is used to store the contact list.
- The Repository layer performs CRUD operations.
- The Service layer implements the business logic.
- The Controller layer coordinates the application flow.
- The View layer handles all console input and output operations.
- The Helper class performs input validation and reusable checks.
 
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
 
- Understanding and implementing the MVC architecture.
- Separating the application into Model, Repository, Service, Controller and View layers.
- Organizing validation logic into reusable methods while keeping the controller clean.
 
---
 
# Future Enhancements
 
- Store contacts in a file or database instead of memory.
- Support searching, editing and deleting contacts using fields other than the phone number.
- Improve the user interface.