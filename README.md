# Telecom Client Billing & Management Web Application

<p align="center">
    <!-- Backend -->
  <img src="https://img.shields.io/badge/.NET%208%20LTS-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/ASP.NET%20Core%20MVC-5C2D91?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/C%23-239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white" />

  <!-- ORM / Database -->
  <img src="https://img.shields.io/badge/EF%20Core-9.0-68217A?style=for-the-badge" />
  <img src="https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white" />

  <!-- Frontend -->
  <img src="https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white" />
  <img src="https://img.shields.io/badge/HTML5-%23E34F26.svg?style=for-the-badge&logo=html5&logoColor=white" />
  <img src="https://img.shields.io/badge/CSS3-%231572B6.svg?style=for-the-badge&logo=css3&logoColor=white" />

  <!-- Other  -->
  <img src="https://img.shields.io/badge/License-MIT-yellow.svg?style=for-the-badge" />
  <img src="https://img.shields.io/badge/Project%20Type-University%20Project-orange?style=for-the-badge" />
</p>


## Overview

This is a university project developed using **ASP.NET Core MVC**, **SQL Server**, and **Entity Framework Core**.  

It simulates a complete telecom billing service ecosystem with:

- Role-based interfaces (Admin, Seller, Client)

- Client management workflows

- Billing functionalities

- Program/Plan assignment and modification

- Session-based role handling

- Database-driven design using EF Core 9

Although designed for educational purposes, the project follows real-world application architecture practices (MVC, layered logic, DB normalization).

## Features

### 🔐 Authentication & Session
- Simple login system (username/password)
- Session-based role selection
- Three distinct user flows

### 👤 Client Features
- View billing history  
- View call history  
- Pay bills  

### 🧑‍💼 Seller Features
- Register new clients  
- Issue bills to clients  
- Assign/change client plans  
- Validate unique usernames & phone numbers  

### 👑 Administrator Features
- Create new sellers  
- Create & edit billing plans  
- Modify program rates, names, or assign permissions  

---

## Architecture

The project follows the **Model-View-Controller (MVC)** design pattern:

- **Models:** Represent entities like `User`, `Client`, `Seller`, `Bill`, `Call`, `Plan`, etc.
- **Views:** Razor pages organized by user role (Admin, Seller, Client)
- **Controllers:** Handle user input and business logic.
- **Database:** A structured relational schema handled via `MVCDBContext` using **Entity Framework Core**.

---

## ⚙️ Internal Architecture & Dependency Injection

The application uses ASP.NET Core’s **built‑in Dependency Injection (DI)** container.  
Key registered services in `Program.cs` include:

- `AddControllersWithViews()` — Enables MVC and Razor views  
- `AddDbContext<MVCDBContext>()` — Registers EF Core DbContext (SQL Server)  
- `AddSession()` — Enables session-based role and state handling  

These services are injected into controllers using constructor injection, keeping the architecture modular and testable.

---

## 🔧 ASP.NET Core Middleware Pipeline

The application uses the standard ASP.NET Core middleware pipeline, including:

- `UseHttpsRedirection()` — Enforces HTTPS  
- `UseStaticFiles()` — Serves static assets (CSS, JS, images)  
- `UseRouting()` — Enables endpoint routing  
- `UseAuthorization()` — Handles authorization middleware  
- `UseSession()` — Activates session state for logged‑in users  

---

## Technologies Used

- **ASP.NET Core MVC (.NET 8)**
- **C#**
- **Entity Framework Core 9**
- **Microsoft SQL Server 2022**
- **Razor Views**
- **Bootstrap 5**
- **HTML5 / CSS3**
- **Visual Studio 2022**

---

## Getting Started

1. Clone the repository:
   ```bash
   git clone https://github.com/kon-f/MVC_Telecom_Billing_Project
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

---

## 📸 Screenshots

### 📊 Database Schema
<p align="center">
  <img src="screenshots/DB_Schema.png" width="600"/>
</p>

### 🔐 Authentication

<table>
<tr>
<td><img src="screenshots/5_Login.png" width="100%"></td>
<td><img src="screenshots/6_Fail_Login.png" width="100%"></td>
</tr>
</table>

### 👑 Admin Dashboard & Seller Management

<p align="center">
  <img src="screenshots/7_Admin_Menu.png" width="45%" />
</p>

<table>
<tr>
<td><img src="screenshots/8_List_Existing_Sellers.png" width="100%"></td>
<td><img src="screenshots/10_Seller_Created.png" width="100%"></td>
</tr>
</table>

<table>
<tr>
<td><img src="screenshots/9_Create_Seller.png" width="100%"></td>
<td><img src="screenshots/9_FaiI.png" width="100%"></td>
</tr>
</table>

### 📝 Program Management (Admin)

<table>
<tr>
<td><img src="screenshots/11_Modify_Programs.png" width="100%"></td>
<td><img src="screenshots/12_Modified_Programs.png" width="100%"></td>
</tr>
</table>

<table>
<tr>
<td><img src="screenshots/13_Create_Program.png" width="100%"></td>
<td><img src="screenshots/13_Create_Program_Fail.png" width="100%"></td>
</tr>
</table>

<p align="center">
  <img src="screenshots/14_New_Programs.png" width="45%" />
</p>

### 👤 Client Interface

<table>
<tr>
<td><img src="screenshots/15_Client_Menu.png" width="100%"></td>
<td><img src="screenshots/16_Call_History.png" width="100%"></td>
</tr>
</table>

<table>
<tr>
<td><img src="screenshots/17_View_Bills.png" width="100%"></td>
<td><img src="screenshots/18_Pay_Bill.png" width="100%"></td>
</tr>
</table>

### 🧾 Seller Tools

<p align="center">
  <img src="screenshots/19_Seller_Menu.png" width="45%" />
</p>

<table>
<tr>
<td><img src="screenshots/20_Change_Client_Program.png" width="100%"></td>
<td><img src="screenshots/21_Changed_Client_Program.png" width="100%"></td>
</tr>
</table>

<table>
<tr>
<td><img src="screenshots/22_Add_Client.png" width="100%"></td>
<td><img src="screenshots/22_Add_Client_Fail.png" width="100%"></td>
</tr>
</table>

<table>
<tr>
<td><img src="screenshots/23_Client_Added.png" width="100%"></td>
<td><img src="screenshots/24_Issue_Bill.png" width="100%"></td>
</tr>
</table>

<table>
<tr>
<td><img src="screenshots/25_No_Bills.png" width="100%"></td>
<td><img src="screenshots/25_Yes_Bills.png" width="100%"></td>
</tr>
</table>
  
---

## Notes

- `appsettings.json` is excluded from version control.
- Project is for **educational purposes**; authentication is simplified.

---

## 📄 License

This project is licensed under the MIT License.  
You are free to use, modify, and distribute this software, provided that the original copyright notice and this permission notice are included.

See the **LICENSE** file for details.

