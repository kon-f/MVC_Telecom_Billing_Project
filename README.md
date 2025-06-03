# Mobile Client Billing & Management Web Application

## Overview

This is a university project developed using **ASP.NET Core MVC** and **SQL Server**. The application simulates a complete billing and client management system for a mobile phone service provider. It features role-based functionality for different types of users.

## Features

### 🔐 Login
- Basic login form for all users.
- Session/role management not implemented.

### 👤 Client
- View personal bills
- View call history
- Pay bills (logic can be extended)

### 🧑‍💼 Seller
- Register new clients
- Issue client bills
- Assign clients to billing plans

### 🛠️ Administrator
- Create new sellers
- Create billing plans
- Modify billing plan details (e.g., flat rates)

## Architecture

The project follows the **Model-View-Controller (MVC)** design pattern:

- **Models:** Represent entities like `Client`, `Seller`, `Bill`, `Call`, `Plan`, etc.
- **Views:** Razor pages separated by user roles.
- **Controllers:** Handle user input and business logic.
- **Database:** Handled via `MVCDBContext` using **Entity Framework Core**.

## Technologies Used

- ASP.NET Core MVC
- C# (.NET 6 or later)
- Entity Framework Core
- SQL Server 2022
- Visual Studio 2022

## Getting Started

1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/your-repo-name.git
   ```

2. Open the solution in **Visual Studio 2022**.

3. Copy the configuration template:
   ```bash
   cp appsettings.Template.json appsettings.json
   ```

4. Fill in your connection string in `appsettings.json`.

5. (Optional) Apply EF Core migrations:
   ```bash
   dotnet ef database update
   ```

6. Run the app using IIS Express or Kestrel.

## Screenshots

(Add interface images here)

## Notes

- `appsettings.json` is excluded from version control.
- The system is for educational purposes only and lacks full authentication/security handling.