# YuuZone API

**YuuZone** is a modern backend API built with **ASP.NET Core**, using **Entity Framework Core (Code-First)** and **SQL Server**. It supports secure authentication with **JWT**, **Google OAuth**, and includes structured logging via **Serilog**, password hashing via **BCrypt**, and email delivery via **MailKit**.

---

## 🚀 Technologies Used

- **ASP.NET Core Web API**
- **Entity Framework Core (Code-First)**
- **SQL Server** (default)
- **JWT Authentication** (`Microsoft.AspNetCore.Authentication.JwtBearer`)
- **Google Authentication** (`Microsoft.AspNetCore.Authentication.Google`)
- **Serilog Logging** (`Serilog.AspNetCore`, `Serilog.Sinks.File`)
- **MailKit** for sending emails
- **BCrypt.Net** for password hashing

---

## 📦 Prerequisites

Make sure you have the following installed:

- [.NET 7+ SDK](https://dotnet.microsoft.com/en-us/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- Visual Studio / VS Code / Rider
- EF Core CLI tools (`dotnet tool install --global dotnet-ef`)

---

## 🔧 Configuration

### 🔑 `appsettings.json`

Create or update the `appsettings.json` file in the root of the project:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(local);Database=YuuZoneDb;User Id=your_user;Password=your_password;Encrypt=True;TrustServerCertificate=True;"
  },
  "JWT": {
    "Secret": "your-256-bit-secret",
    "Issuer": "YuuZone",
    "Audience": "YuuZone",
    "ExpirationTimeMinutes": 30,
    "RefreshTokenDay": 1
  },
  "SMTPSettings": {
    "Host": "smtp.gmail.com",
    "Port": 587,
    "SenderName": "YuuZone",
    "SenderEmail": "your-email@gmail.com",
    "Username": "your-email@gmail.com",
    "Password": "your-app-password",
    "EnableSsl": true
  },
  "Authentication": {
    "Google": {
      "ClientId": "your-google-client-id",
      "ClientSecret": "your-google-client-secret"
    }
  },
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft.AspNetCore": "Warning",
        "System": "Warning"
      }
    },
    "WriteTo": [
      { "Name": "Console" },
      {
        "Name": "File",
        "Args": {
          "path": "Logs/log.txt",
          "rollingInterval": "Day"
        }
      }
    ],
    "Enrich": [ "FromLogContext" ],
    "Properties": {
      "Application": "YuuZone"
    }
  }
}
````

---

## 🧪 How to Run

### 1. Clone the Repo

```bash
git clone https://github.com/TrNgQuangVinh/YuuZone.git
cd YuuZone
```

### 2. Apply EF Core Migrations

```bash
dotnet ef database update
```

Or via Visual Studio:

```powershell
Update-Database
```

### 3. Run the App

```bash
dotnet run
```

Navigate to `https://localhost:<port>/swagger` to access Swagger UI.

---

## 🔁 Using Other Databases

### ✅ MySQL

1. Install:

```bash
dotnet add package Pomelo.EntityFrameworkCore.MySql
```

2. Update your `DbContext` registration in `Program.cs`:

```csharp
options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
    ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection")));
```

3. Update the connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;port=3306;database=YuuZoneDb;user=root;password=your_password"
}
```

---

### ✅ PostgreSQL

1. Install:

```bash
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
```

2. In `Program.cs`:

```csharp
options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
```

3. Connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=YuuZoneDb;Username=postgres;Password=your_password"
}
```

Then re-run:

```bash
dotnet ef database update
```

---
## 🌱 Seeding the Database

This project supports basic data seeding (e.g., roles, admin user) using EF Core.

### 🔧 How to Seed

1. In `Program.cs`, ensure you call your seeding method after building the app:

```csharp
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DbInitializer.SeedAsync(services);
}
```

2. Create a static `DbInitializer` class (if not already present) and implement your seeding logic there:

```csharp
public static class DbInitializer
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var context = services.GetRequiredService<YuuZoneDbContext>();

        // Run pending migrations
        await context.Database.MigrateAsync();

        // Example: Add default roles
        if (!context.Roles.Any())
        {
            context.Roles.AddRange(
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "User" }
            );
            await context.SaveChangesAsync();
        }
    }
}
```

3. Then run:

```bash
dotnet run
```

> This will apply migrations and insert seed data if it doesn’t already exist.
---

Here's a clean **🗃️ Database Change Management** section for your `README.md`, incorporating all your EF Core CLI commands and notes:

---

## 🗃️ Database Change Management

This project uses **Entity Framework Core (Code-First)** for database schema generation and migration.

---

### 📦 Creating a New Migration

Run this in the **Repository** project:

```bash
dotnet ef migrations add <DescriptiveMigrationName> --project .\Repositories\ --startup-project .\YuuZone\
```

---

### 📥 Applying Migrations

To update the database with the latest migration:

```bash
dotnet ef database update
```

Or explicitly:

```bash
dotnet ef database update --project .\Repositories\ --startup-project .\YuuZone\
```

---

### 🧹 Removing the Last Migration

If you made a mistake or want to undo the latest migration (before it's applied):

```bash
dotnet ef migrations remove --project .\Repositories\ --startup-project .\YuuZone\
```

---

### 💣 Resetting the Database (If Needed)

> When migrations become corrupted or conflict with existing schema:

1. **Backup your data** (SQL Server Management Studio → Right-click DB → Tasks → Export Data).
2. Delete the database manually.
3. Delete the `Migrations/` folder in `Repositories/`.
4. Recreate a fresh migration:

```bash
dotnet ef migrations add InitialCreate --project .\Repositories\ --startup-project .\YuuZone\
dotnet ef database update --project .\Repositories\ --startup-project .\YuuZone\
```

---
## 🛡️ Auth Overview

* 🔐 **JWT**: Used for protected API endpoints
* 🧩 **Google Login**: OAuth2 with secure external login
* 🔄 **Refresh Tokens**: Stored in database for access token renewal
* 🔑 **BCrypt**: Secure password hashing and verification

---

## 📬 Email Features

SMTP is configured using **MailKit**, enabling:

* Email verification
* Password reset
* Notifications

Update your Gmail settings to enable [App Passwords](https://support.google.com/accounts/answer/185833).

---

## 📋 Logging

Serilog writes to:

* `Logs/log.txt` (rolling daily)
* Console output

You can adjust log level and sinks in `appsettings.json`.

---

## 🧑‍💻 Developer Notes

* Swagger UI is enabled at: `https://localhost:7184/swagger`
* CORS is configured for `http://localhost:3000` (React frontend)
* All service and repository layers are injected via DI in `Program.cs`

---

## 🙋‍♂️ Author

Made with ❤️ by [@TrNgQuangVinh](https://github.com/TrNgQuangVinh)

---

## 📝 License

MIT License — Free to use with attribution.

