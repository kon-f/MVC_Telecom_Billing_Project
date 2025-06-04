# Telecom Client Billing & Management Web Application

## Overview

This is a mid-course small university project developed using **ASP.NET Core MVC** and **SQL Server**. The application simulates a complete billing and client management system for a telecom service provider. It features role-based functionality for different types of users.

## Features

### 🔐 Login
- Basic login form for all users.
- Session/role management not fully implemented.

### 👤 Client
- View personal bills
- View call history
- Pay bills 

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
- Microsoft SQL Server 2022
- Visual Studio 2022

## Getting Started

1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/your-repo-name.git
   ```

2. Open the solution in **Visual Studio 2022**.

3. Rename the configuration template:

   Rename `appsettings.Template.json` to `appsettings.json`.

   > On Linux/macOS, you can use:
   > ```bash
   > mv appsettings.Template.json appsettings.json
   > ```

   > On Windows, just rename the file manually or use PowerShell:
   > ```powershell
   > Rename-Item "appsettings.Template.json" "appsettings.json"
   > ```

4. Fill in your connection string in `appsettings.json`.

5. (Optional) Apply EF Core migrations:
   ```bash
   dotnet ef database update
   ```

6. Run the app.

## Screenshots

### Database Schema (Designed in Microsoft SQL Management Studio)
![Database Schema](screenshots/DB_Schema.png)

### 🔐 Login Page
![LoginPage](screenshots/5_Login.png)

### ❌ Failed Login
![FailedLogin](screenshots/6_Fail_Login.png)

### 👑 Admin Logged In
![AdminLogin](screenshots/7_Admin_Menu.png)

### 📃 List Existing Sellers
![Sellers](screenshots/8_List_Existing_Sellers.png)

### ➕ Create Seller
![CreateSeller](screenshots/9_Create_Seller.png)

### ⚠️ Create Seller Fail
![CreateSellerFail](screenshots/9_FaiI.png)

### ✅ Seller Created
![SellerCreated](screenshots/10_Seller_Created.png)

### ⚙️ Modify Programs
![ModifyPrograms](screenshots/11_Modify_Programs.png)

### 📝 Modified Programs
![ModifiedPrograms](screenshots/12_Modified_Programs.png)

### ➕ Create Program
![CreateProgram](screenshots/13_Create_Program.png)

### ❌ Create Program Fail
![CreateProgramFail](screenshots/13_Create_Program_Fail.png)

### 📋 View New Programs
![NewPrograms](screenshots/14_New_Programs.png)

### 👤 Client Menu
![ClientMenu](screenshots/15_Client_Menu.png)

### 📞 Call History
![CallHistory](screenshots/16_Call_History.png)

### 📄 View Bills
![ViewBills](screenshots/17_View_Bills.png)

### 💳 Pay Bill
![PayBill](screenshots/18_Pay_Bill.png)

### 🧾 Seller Menu
![SellerMenu](screenshots/19_Seller_Menu.png)

### 🔄 Change Client Program
![ChangeClientProgram](screenshots/20_Change_Client_Program.png)

### ✅ Client Program Changed
![ClientProgramChanged](screenshots/21_Changed_Client_Program.png)

### ➕ Add Client
![AddClient](screenshots/22_Add_Client.png)

### ❌ Add Client Fail
![AddClientFail](screenshots/22_Add_Client_Fail.png)

### ✅ Client Added
![ClientAdded](screenshots/23_Client_Added.png)

### 🧾 Issue Bill
![IssueBill](screenshots/24_Issue_Bill.png)

### 📭 No Bills Available
![NoBills](screenshots/25_No_Bills.png)

### 📬 Bills Available
![YesBills](screenshots/25_Yes_Bills.png)

## Notes

- `appsettings.json` is excluded from version control.
- The system is for educational purposes only and lacks full authentication/security handling.
