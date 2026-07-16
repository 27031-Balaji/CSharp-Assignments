# Basic Contact Manager

This is a basic contact manager using C# using the MVC architecture. This has the following functionalities.

## Add Contacts
1. You can enter your name, phone number and email and optionally can add additional notes in the contact list.
2. Duplicate phone number entries are not allowed.
3. Multiple validations are checked for email and phone number.
4. After adding contacts, it automatically sorts the contact list with the ascending order of name.

## Display Contacts
1. You can view the contact list entirely.
2. Exception handling is handled for empty contact list.

## Search Contacts
1. You can search specific contact from the contact list using the phone number.
2. Exception handling is done for null phone number and incorrect phone number properly.

## Delete Contacts
1. You can delete a specific contact from the contact list using the phone number as the input.
2. Exception handling is handled the same way as in searching contacts.

## Edit Contacts
1. You can edit a specific contact's name, email or notes using the phone number as the input.
2. You can edit name, email or notes and this can loop multiple times ensuring multiple changes.
3. Multiple methods and exception handling techniques are used to ensure the correct entry of details by the user.
4. After editing contacts, it automatically sorts the contact list with the ascending order of name.

# Techniques Used for Implementation
- I used a list of contact objects to store the contact list.
- Repository class is used to perform the CRUD operations of the contact list.
- Services class is used to do different services such as Add, Delete, Search, Display, Edit.
- Helper class is made to validate the data entered by the user and check conditions like contact emptiness and whether the contact is already present or not.
- ConsoleOperation class is used for I/O operations.

# Challenges Faced
- The introduction of MVC architecture was new to me and learning and adapting to it has been a challenge.
- Learning to segregate the methods to different classes was difficult to me.

# Future Implementations
- Can upgrade the database functionalities by using either a file or using a database.
- Can use other means to search, edit or delete contact instead of just using the phone number.