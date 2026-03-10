# Fintech Wallet API

![.NET](https://img.shields.io/badge/.NET-10.0-blue)
![License](https://img.shields.io/badge/license-MIT-green)
![Build](https://img.shields.io/badge/build-passing-brightgreen)

A backend API for a fintech wallet system built with **ASP.NET Core** and **Entity Framework Core**.
The system allows users to register, create wallets, deposit and withdraw funds, transfer money, and fund wallets via **Paystack** payment gateway.

---

## 🚀 Features

* User registration and login with secure password hashing
* JWT-based authentication
* Create and manage digital wallets
* Deposit and withdraw funds
* Transfer money between wallets
* Fund wallets via Paystack payment gateway
* Payment verification with automatic wallet crediting
* Transaction history
* Generic repository pattern
* RESTful API design with Swagger documentation
* Unit tests with xUnit and Moq

---

## 🧰 Tech Stack

| Layer | Technology |
|-------|-----------|
| **Framework** | ASP.NET Core (.NET 10) |
| **Language** | C# |
| **Database** | SQLite (via Entity Framework Core) |
| **Authentication** | JWT (JSON Web Token) |
| **Payment** | Paystack |
| **Testing** | xUnit, Moq |
| **Documentation** | Swagger / Swashbuckle |

---

## 🏗 Architecture

The project follows a **layered architecture** with a factory pattern for payment processing.

```
Controller → Service → Repository → Database
                 ↘
            PaymentFac → IPayment (Paystack)
```

* **Controllers** — handle HTTP requests and responses
* **Services** — contain business logic
* **Repositories** — generic data access via `IRepository<T>`
* **Payments** — payment gateway integration using factory pattern
* **Models** — domain entities and DTOs

---

## 📂 Project Structure

```text
FintechWallet/
│
├── Controllers/
│   ├── AuthController.cs          # Registration & login endpoints
│   └── WalletController.cs        # Wallet, transaction & payment endpoints
│
├── Services/
│   ├── IAuthService.cs            # Auth service interface
│   ├── AuthService.cs             # JWT token generation, password hashing
│   ├── IWalletService.cs          # Wallet service interface
│   └── WalletService.cs           # Deposit, withdraw, transfer logic
│
├── Repositories/
│   ├── IRepository.cs             # Generic repository interface
│   └── Repository.cs              # Generic repository implementation
│
├── Payments/
│   ├── IPayment.cs                # Payment provider interface
│   ├── PaymentFac.cs              # Payment factory
│   └── Paystack/
│       └── PaystackPayment.cs     # Paystack payment implementation
│
├── Models/
│   ├── User.cs                    # User entity
│   ├── Wallet.cs                  # Wallet entity
│   ├── Transaction.cs             # Transaction entity & enum
│   ├── Payment.cs                 # Payment request model
│   ├── PaymentLog.cs              # Payment audit log entity
│   └── Dtos.cs                    # Data transfer objects
│
├── Data/
│   └── WalletDbContext.cs         # EF Core database context
│
├── Tests/
│   └── WalletServiceTests.cs      # Unit tests for WalletService
│
├── Program.cs                     # App configuration & DI setup
├── appsettings.json               # App settings & connection strings
└── README.md
```

---

## ⚙️ Getting Started

### Prerequisites

* [.NET SDK 10](https://dotnet.microsoft.com/download) or later
* Visual Studio, VS Code, or Rider

### Installation

```bash
# Clone the repository
git clone https://github.com/muisoft/fintech-wallet-dotnet.git

# Navigate to the project folder
cd fintech-wallet-dotnet

# Restore dependencies
dotnet restore

# Run the application
dotnet run
```

The API will be available at `http://localhost:5000` with Swagger UI at `/swagger`.

---

## 📘 API Endpoints

### Auth

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/auth/register` | Register a new user |
| POST | `/api/auth/login` | Login and receive JWT token |

### Wallets

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/wallets` | Create a new wallet |
| GET | `/api/wallets` | List all user wallets |
| GET | `/api/wallets/{id}` | Get wallet details |
| POST | `/api/wallets/{id}/deposit` | Deposit funds |
| POST | `/api/wallets/withdraw` | Withdraw funds |
| POST | `/api/wallets/transfer` | Transfer between wallets |
| GET | `/api/wallets/{id}/transactions` | Get transaction history |

### Payments

| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/wallets/fund-with-payment` | Initialize Paystack payment |
| POST | `/api/wallets/verify-funding` | Verify payment & credit wallet |

---

## 🔐 Authentication

The API uses **JWT Bearer** authentication. Include the token in the `Authorization` header:

```
Authorization: Bearer <your_token>
```

---

## 🧪 Running Tests

```bash
dotnet test
```

---

## 📄 License

This project is licensed under the **MIT License**.

---

## 👤 Author

**Muideen Adewale** — Senior Software Engineer

[![LinkedIn](https://img.shields.io/badge/LinkedIn-muisoft-blue?logo=linkedin)](https://linkedin.com/in/muisoft)
[![Email](https://img.shields.io/badge/Email-olaprog2013%40gmail.com-red?logo=gmail)](mailto:olaprog2013@gmail.com)
