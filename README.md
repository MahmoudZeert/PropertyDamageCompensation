# Property Damage Compensation Management
This project is a NET Core web application for managing property damage compensation. It provides a platform where users can sign up, 
submit personal information, create application files, and track the progress of their compensation claims.

# Features
User Registration: Users can sign up for an account to access the application.
User Login: Registered users can log in to their accounts.
Profile Management: Users can add and update their personal information.
Application Creation: Users can fill out a form to create an application file.
Application Submission: Users can submit their application files for evaluation.
Application Evaluation: Relevant authorities review the submitted applications for eligibility.
Payment Processing: Eligible applicants receive compensation for the approved claims.
Application Tracking: Users can track the status and progress of their compensation claims.

# Technology Stack
- Front-end:  .NET Core 6.0 MVC, HTML5, CSS, JavaScript
- Back-end: Net core 6.0 web API, services, and data access
- Database: Microsoft SQL Server
- Authentication and authorization: .NET Identity
- Dependency Injection: .NET Core Dependency Injection
- Exception Handling: Custom Middleware
- ORM: Entity Framework Core
- Architecture: I initially developed the app as a .NET Core 6.0 MVC web application. However, I have started refactoring it into a clean architecture solution. As part of this refactoring, I have implemented a small feature that involves the ability to add and update the entity "Floor" using the clean architecture pattern.

  In this pattern, the MVC front-end communicates with a typed HttpClient, which makes requests to a web API endpoint. The web API endpoint then calls a service, which in turn interacts with the repository to access the data store. This layered approach helps separate concerns and promotes modularity and maintainability in the codebase.
 
The solution Structure for the clean architecture is :
The project follows the principles of clean architecture and is structured into the following projects:
  * Domain: Contains the core domain models, interfaces, and business logic.
  * Contracts for modeling  web API.
  * Application: Implements application-specific logic and use cases as the application layer.
  * Infrastructure: Provides implementations for data access, external services integration, and other infrastructure concerns.
  * MVC Web: The MVC project is responsible for the web user interface.
  * Web API: The ASP.NET Core Web API project exposes functionalities for external integrations.
Getting Started
To run the project locally, follow these steps:

Clone the repository:

download code.

Open the solution in Visual Studio or your preferred development environment.

Update the connection string in the appsettings.json file to point to your Microsoft SQL Server database.

Build the solution to restore NuGet packages and compile the code.

Run the database migrations to create the required tables in the database.

Start the application.

Access the application in your web browser at http://localhost:port.






