# Basic Contact Manager

I have implemented a basic contact manager that allows us to store and manage contacts.
This application has the following features.

## Add Contacts
1. You can enter your name, phone number and email and optionally can add additional notes in the contact list.
2. Duplicate phone number entries are not allowed (Phone number acts as primary key).
3. Multiple if statements checking the right validation is done.

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
2. You can only edit either the name or email or notes and not all of them at the same time.
3. Multiple methods and exception handling techniques are used to ensure the correct entry of details by the user.

## Sort Contacts
1. You can sort contacts based on their names with this functionality.
2. After sorting, it displays the contact list sorted for ensuring righteousness of sorting.

# Techniques Used for Implementation
- I used a list of lists data structure to implement the contact list without using classes and objects.
- I used many methods to validate the input to check whether it is right or not.
- Phone number alone has 2 separate validations, one for the length and one for checking if there are any characters in between them.

# Challenges Faced
- Since this is the first time, I had difficulties adding the XML documentation for each of the methods and classes in the program.
- There are multiple conditions to check for each inputs, so I had a lot of time thinking about resolving them, than to code it in the program.

# Functionalities to be Implemented
1. I need to check on various tests and verify the correctness of the application.
2. I need to add some more exceptional case handling methods after checking on the tests.
