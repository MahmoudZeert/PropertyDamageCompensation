# Property Damage Compensation Management
This project is a web application for managing property damage compensation. It provides a platform where users can sign up, 
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
Front-end:  .NET Core 6.0 MVC, HTML5, CSS, JavaScript
Back-end: Net core 6.0 web API,services and data access
Database: Microsoft SQL Server
Architecture: Clean Architecture

Authentication: .NET Identity
Dependency Injection: .NET Core Dependency Injection
Exception Handling: Custom Middleware
ORM: Entity Framework Core
Solution Structure
The project follows the principles of clean architecture and is structured into the following projects:

Domain: Contains the core domain models, interfaces, and business logic.
Application: Implements application-specific logic and use cases, acting as the application layer.
Infrastructure: Provides implementations for data access, external services integration, and other infrastructure concerns.
MVC: The MVC project responsible for the web user interface.
API: The ASP.NET Core Web API project to expose functionalities for external integrations.
Getting Started
To run the project locally, follow these steps:

Clone the repository:

bash
Copy code
git clone https://github.com/your-username/property-damage-compensation.git
Open the solution in Visual Studio or your preferred development environment.

Update the connection string in the appsettings.json file to point to your Microsoft SQL Server database.

Build the solution to restore NuGet packages and compile the code.

Run the database migrations to create the required tables in the database.

Start the application.

Access the application in your web browser at http://localhost:port.

Contribution Guidelines
If you want to contribute to this project, please follow these steps:

Fork the repository on GitHub.
Create a new branch for your feature or bug fix.
Make your changes and commit them with descriptive messages.
Push your changes to your forked repository.
Submit a pull request to the original repository, describing the changes you made.
License
This project is licensed under the MIT License.

Acknowledgements
.NET Core Documentation
Entity Framework Core Documentation
ASP.NET Core Documentation
Bootstrap Framework
Feel free to modify and expand upon the provided content to accurately represent your project's structure, technologies, and guidelines. Remember to include any additional dependencies, installation instructions, and relevant details specific to your implementation.
