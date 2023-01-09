Contact Book

1.	ContactBook RESTful API: Automated API Tests 

Your task is to write automated tests (in C#, Java, JavaScript or other language) for the above provided API endpoints. You should implement the following automated tests:

•	List contacts and assert that the first contact is “Steve Jobs”.
•	Find contacts by keyword “albert” and assert that the first result holds “Albert Einstein”.
•	Find contacts by keyword “missing{randnum}” and assert that the results are empty.
•	Try to create a new contact, holding invalid data, and assert an error is returned.
•	Create a new contact, holding valid data, and assert the new contact is added and is properly listed in the contacts list.
You are free to use a testing framework of choice (e. g. NUnit or JUnit) and external libraries (e. g. RestSharp).

2.	ContactBook Web App: Automated Selenium UI Tests 

Write Selenium-based automated UI tests for the “ContactBook” app. You should implement the following automated UI tests:

•	List contacts and assert that the first contact is “Steve Jobs”.
•	Find contacts by keyword “albert” and assert that the first result holds “Albert Einstein”.
•	Find contacts by keyword “invalid2635” and assert that the results are empty.
•	Try to create a new contact, holding invalid data, and assert an error is shown.
•	Create a new contact, holding valid data, and assert the new contact is added and is properly listed at the end of the contacts page.

You are free to use a testing framework of choice (e. g. NUnit or JUnit), but your primary Web UI automation tool should be Selenium. You are free to use external libraries and tools.

3.	Appium Tests

Choose one of the next two problems: Android app UI tests or Windows app UI tests.

4.	ContactBook Desktop App: Automated Appium UI Tests 

Write Appium-based automated Windows UI tests for the “ContactBook” Windows desktop app. Implement the following automated testing scenario:

•	Start the app.
•	Connect to your backend API service.
•	Search for keyword “steve”.
•	Assert that “Steve Jobs” is returned as result.
