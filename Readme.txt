# ZeissAPIApp - ASP.NET Core Web API

This is a **.NET 5.0 Web API project** for managing products. It includes:
- Entity Framework Core for database management
- xUnit & Moq for unit testing
- Swagger for API documentation

##Getting Started

### Prerequisites
Ensure you have the following installed:
- [.NET 5.0 SDK](https://dotnet.microsoft.com/download/dotnet/5.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Git](https://git-scm.com/downloads)

###Clone the Repository
Run:
```sh
git clone https://github.com/sarveshbs87/zeissapiapp.git
cd zeissapiapp

###Install Dependencies
dotnet restore

###Set Up the Database

Modify appsettings.json with your database connection string:
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=ZeissDB;Trusted_Connection=True;"
}

Run migrations:
dotnet ef database update

###Run the API
dotnet run --project ZeissAPIApp

###Run Tests
dotnet test