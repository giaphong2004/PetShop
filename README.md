<!-- README.md for PetShop - HR-friendly, copy/paste into repo root -->

<p align="center">

  <img width="900" alt="Screenshot 2025-10-24 144351" src="https://github.com/user-attachments/assets/b3ba590e-3b89-4155-8736-0b8afd7c9e96" />

</p>

# PETSHOP — E-commerce demo (ASP.NET MVC) 🐾

![ASP.NET MVC](https://img.shields.io/badge/ASP.NET_MVC-5-blueviolet.svg)
![C#](https://img.shields.io/badge/C%23-11.0-512BD4?logo=c-sharp&logoColor=white)
![Entity Framework](https://img.shields.io/badge/Entity_Framework-6-green.svg)
![SQL Server](https://img.shields.io/badge/SQL_Server-2012%2B-CC2927?logo=microsoft-sql-server&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5-7952B3?logo=bootstrap&logoColor=white)

> **Mục tiêu:** Demo hệ thống e-commerce hoàn chỉnh để chứng minh tư duy sản phẩm + kỹ năng backend (C#, ASP.NET MVC, EF).

---

## 🔎 TL;DR 
- **Project:** Website bán hàng thú cưng (PetShop) — frontend + backend + admin.  
- **Vai trò bạn thấy ở đây:** Thiết kế kiến trúc 3-layer, triển khai backend C# ASP.NET MVC, thiết kế DB, tích hợp EF, làm UX cơ bản.  
- **Impression:** code có cấu trúc, deployable, có quy trình nghiệp vụ (checkout, order history, admin CRUD).

---

## ⭐ Key features (tóm tắt)
- Product listing, filtering, responsive UI  
- Cart (Session-based) + Checkout flow  
- Auth (Forms Auth) & password hashed (BCrypt.Net)  
- Admin: Product CRUD, Order management, Dashboard  
- EF6 (Database First), SQL Server

---

## 📸 Visual 
| Admin | Cart | Checkout |
|---|---|---|
|<img width="300" height="1216" alt="image" src="https://github.com/user-attachments/assets/b0554ed0-d988-4e0e-80a0-c39c9fc8e1f4" />|<img width="300" height="979" alt="image" src="https://github.com/user-attachments/assets/146ce5d6-0342-454c-b209-18fe30ae8379" />| <img width="300" height="1513" alt="image" src="https://github.com/user-attachments/assets/7c74a37c-b162-457c-ba43-86d04189fc4b" />

---
## 🗃️ Cơ sở dữ liệu (Database)

📥 Link tải database:
👉 [Download Database PetShop (Google Drive)](https://drive.google.com/drive/folders/1zucyrLN_a0b2ktKZsobLg5E4dT_Acymn?usp=sharing)

Sau khi tải về, import file .bak hoặc .sql vào SQL Server Management Studio.

---
## 🛠 Quick start 
**Requirements:** Windows, Visual Studio 2022, .NET Framework 4.8, SQL Server 2012+

```bash
git clone https://github.com/giaphong2004/PetShop.git
# Mở PetShop.sln trong Visual Studio
# Restore DB từ .bak (link trong thư mục /database hoặc README)
# Cập nhật connection string trong Web.config & App.config
# Build -> Run (F5)


