# Fintech Wallet API (.NET)


![.NET](https://img.shields.io/badge/.NET-7.0-blue)
![License](https://img.shields.io/badge/license-MIT-green)
![Build](https://img.shields.io/badge/build-passing-brightgreen)
\

A backend API for a fintech wallet system built with **ASP.NET Core** and **SQL Server**.
The system allows users to create wallets, transfer funds, and manage transactions securely.

---

## 🚀 Features

* Create and manage digital wallets
* Deposit funds into wallets
* Withdraw funds from wallets
* Transfer money between users
* View transaction history
* Secure authentication using JWT
* RESTful API design
* API documentation with Swagger

---

## 🧰 Tech Stack

* **Backend:** C# / ASP.NET Core
* **Database:** SQL Server
* **ORM:** Entity Framework Core
* **Authentication:** JWT (JSON Web Token)
* **API Documentation:** Swagger

---

## 🏗 Architecture

The project follows a layered architecture for maintainability and scalability.

Controller → Service → Repository → Database

* **Controllers** handle HTTP requests and responses
* **Services** contain business logic
* **Repositories** interact with the database
* **Database** stores wallet and transaction data

---

## 📂 Project Structure

```text
fintech-wallet-dotnet
│
├── Controllers
│   └── WalletController.cs
│
├── Services
│   └── WalletService.cs
│
├── Repositories
│   └── WalletRepository.cs
│
├── Models
│   ├── Wallet.cs
│   └── Transaction.cs
│
├── Data
│   └── WalletDbContext.cs
│
├── Middleware
│   └── JwtMiddleware.cs
│
├── Tests
│   └── WalletServiceTests.cs
│
├── Program.cs
├── appsettings.json
└── README.md
```

---

## ⚙️ Getting Started

### Prerequisites

Make sure the following are installed:

* .NET SDK 7 or later
* SQL Server
* Visual Studio or VS Code

---

### Installation

Clone the repository:

```bash
git clone https://github.com/muisoft/fintech-wallet-dotnet.git
```

Navigate to the project folder:

```bash
cd fintech-wallet-dotnet
```

Restore dependencies:

```bash
dotnet restore
```

Update the database connection string in **appsettings.json**.

Run the application:

```bash
dotnet run
```

---

## 📘 API Endpoints

| Method | Endpoint                       | Description             |
| ------ | ------------------------------ | ----------------------- |
| POST   | /api/wallets                   | Create wallet           |
| GET    | /api/wallets/{id}              | Get wallet details      |
| POST   | /api/wallets/deposit           | Deposit funds           |
| POST   | /api/wallets/withdraw          | Withdraw funds          |
| POST   | /api/wallets/transfer          | Transfer funds          |
| GET    | /api/wallets/{id}/transactions | Get transaction history |

---

## 🔐 Authentication

The API uses **JWT authentication**.

Example request header:

```text
Authorization: Bearer <your_token>
```

---

## 🧪 Running Tests

Run the tests with:

```bash
dotnet test
```

---

## 📄 License

This project is licensed under the **MIT License**.

---

## 👤 Author

**Muideen Adewale**

Senior Software Engineer

LinkedIn: https://linkedin.com/in/muisoft
Email: [olaprog2013@gmail.com](mailto:olaprog2013@gmail.com)
