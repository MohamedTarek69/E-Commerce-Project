<h1 align="center">🛒 E-Commerce API</h1>

<p align="center">
  <b>ASP.NET Core Web API | Entity Framework Core | SQL Server</b>
</p>

<p align="center">
  A scalable E-Commerce RESTful API that provides authentication,
  product catalog management, and shopping basket functionality.
</p>

---

## 🏗️ Project Overview

The **E-Commerce API** is designed to provide the backend infrastructure for an online shopping platform.

### 🎯 Goals

- Provide a secure authentication system.
- Manage products, brands, and categories efficiently.
- Support shopping basket operations.
- Deliver scalable and maintainable APIs.
- Follow clean development practices and REST standards.

---

## ✨ Main Features

| Module | Description |
|----------|-------------|
| 🔐 Authentication | User registration, login, and current user retrieval |
| 🛍️ Product Catalog | Retrieve products with filtering and pagination |
| 🏷️ Brands | Retrieve available product brands |
| 📂 Product Types | Retrieve available product categories/types |
| 🧺 Shopping Basket | Create, retrieve, update, and delete customer baskets |
| 📑 API Documentation | Swagger integration for testing endpoints |

---

## 🧱 Architecture

The project follows a **Layered Architecture**:

```text
API Layer
│
├── Controllers
│
Business Layer
│
├── Services
│
Data Access Layer
│
├── Entity Framework Core
│
Database Layer
│
└── SQL Server
```

### Benefits

- Clean Separation of Concerns
- Scalability
- Maintainability
- Testability

---

## 🧰 Tech Stack

| Category | Technology |
|-----------|-------------|
| Backend | ASP.NET Core Web API |
| ORM | Entity Framework Core |
| Database | SQL Server |
| Authentication | JWT Authentication |
| API Documentation | Swagger |
| Mapping | AutoMapper |
| Design Patterns | Repository Pattern, Dependency Injection |
| Version Control | Git & GitHub |

---

# 🔐 Authentication Endpoints

### Register User

```http
POST /api/Authentication/Register
```

### Login

```http
POST /api/Authentication/Login
```

### Check Email Availability

```http
GET /api/Authentication/EmailExists
```

### Get Current User

```http
GET /api/Authentication/CurrentUser
```

---

# 🛍️ Product Endpoints

### Get All Products

```http
GET /api/Products
```

### Get Product By Id

```http
GET /api/Products/{id}
```

### Get Product Brands

```http
GET /api/Products/brands
```

### Get Product Types

```http
GET /api/Products/types
```

---

# 🧺 Basket Endpoints

### Get Basket

```http
GET /api/Baskets
```

### Create / Update Basket

```http
POST /api/Baskets
```

### Delete Basket

```http
DELETE /api/Baskets/{id}
```

---

## 📦 Core Models

### 👤 User

- Email
- Username
- Password
- JWT Token

### 🛒 Product

- Id
- Name
- Description
- Price
- PictureUrl
- Brand
- Type

### 🧺 Basket

- Basket Id
- Basket Items
- Quantity
- Product Information

### 🏷️ Brand

- Id
- Name

### 📂 Type

- Id
- Name

---

## 🗄️ Database Design

### Main Tables

```text
Products
ProductBrands
ProductTypes
AspNetUsers
```

### Basket Storage

```text
Basket
BasketItems
```

---

## 🧠 Key Concepts

- 🔐 JWT Authentication
- 📦 Repository Pattern
- 💉 Dependency Injection
- 🔄 AutoMapper
- 📑 Swagger Documentation
- 🗃️ Entity Framework Core
- 🚀 RESTful API Design

---

## 🚀 Getting Started

### Clone Repository

```bash
git clone https://github.com/MohamedTarek69/E-Commerce-Project.git
```

### Configure Database

Update the connection string inside:

```json
appsettings.json
```

### Apply Migrations

```powershell
Update-Database
```

### Run Application

```bash
dotnet run
```

---

## 📌 Future Enhancements

- 📦 Order Management
- 💳 Payment Integration
- ❤️ Wishlist Feature
- ⭐ Product Reviews & Ratings
- 🐳 Docker Support
- ☁️ Cloud Deployment
- 🔄 Microservices Migration

---

## 👨‍💻 Author

**Mohamed Tarek**

<p align="left">
  <a href="https://github.com/MohamedTarek69">GitHub</a> •
  <a href="https://www.linkedin.com/">LinkedIn</a>
</p>

---

<p align="center">
⭐ If you found this project useful, consider giving it a star.
</p>
