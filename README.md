# 📝 Task Management Service (ASP.NET Core 8)

This is a simple **Task Management API** built using **.NET 8** that allows users to register, log in, and manage their tasks securely using **JWT token-based authentication**.

---

## 🚀 Features

- ✅ **User Registration & Login API**
- 🔐 **JWT Authentication & Authorization**
- 📋 **CRUD Operations on Tasks**
- 🗂️ Tasks are user-specific
- 📆 Tasks include title, description, due date, and status
- ⚙️ Built using clean architecture: Controllers, Services, Repositories, DTOs

---

## 📦 Tech Stack

- **.NET 8 Web API**
- **Entity Framework Core**
- **PostgreSQL (or SQL Server)**
- **JWT for Authentication**
- **FluentValidation for input validation**

---

## 🔐 Authentication

All protected endpoints require a **JWT token** in the `Authorization` header:

