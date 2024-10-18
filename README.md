1. Unzipping the Project
Download the ZIP file from the source you’ve provided.
Right-click the ZIP file and choose Extract All....
Select a destination folder where the project will be extracted.
Click "Extract" to unzip the project files.


2. Install Microsoft Visual Studio
If you don't have Visual Studio installed, follow these steps:

Go to the microsoft store and search:  Visual Studio.
Download the Community Edition .
Run the installer and:
Choose the ASP.NET and web development workload.
Include Entity Framework and SQL Server tools if prompted.
Click Install and wait for the installation to complete.

3. Open the Project in Visual Studio
Open Visual Studio.
Click Open a project or solution.
Navigate to the unzipped project folder.
Select the .sln (solution) file and click Open.

 open the Package Manager Console (from Tools > NuGet Package Manager) and run: dotnet restore

Login to Azure SQL Database:
Open SQL Server Management Studio (SSMS).
Click Connect > Database Engine.
Enter the Server Name and credentials from the appsettings.json.
Once connected, you can explore the database to see tables and data.

6. Run the Project
In Visual Studio, click Debug > Start Without Debugging or press Ctrl + F5.
Wait for the project to build and launch in your web browser.













ABOUT THE PROJECT - Once you have opened the website please register and select the role Lectuerer to. This will have its own set of navigation links differnt to that of other roles.
Logout when done. Create a new user with coordinator or manager role and you will see a new set of navigation links. now youll have more functionality, can add comments reject or approve claims as well as view all claims

I am using an Azure database so you do not need to create a database. 
If you need to have a look at the database in appsettings.json there is the logins and password
Please note my LOGIN was not configured properly so you will not be able to login to a profile you already created. The tests data and files have been provided
