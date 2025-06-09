# 👗 FitVerse – E-Commerce Clothing Website

**FitVerse** is a responsive e-commerce web application for clothing and accessories (Men, Women, Kids). Built with **ASP.NET Core MVC**, it offers a seamless online shopping experience with modern features and a clean UI.

---

## 🛙️ Live Features

* 🏠 Homepage with hero slider, category highlights, new arrivals, sales, and best sellers
* 👔 Product listing by category (Men, Women, Kids, Accessories)
* 🛒 Shopping cart with add/remove quantity
* 🔐 User authentication (Register/Login/Logout)
* 📦 Order checkout system
* 📋 Admin dashboard to manage products, categories, and orders
* 💬 Responsive design with Bootstrap 5
* 📩 Newsletter subscription (UI only)
* ⚙️ Code-first Entity Framework with SQL Server

---

## 🧑‍💻 Tech Stack

* **Frontend:** HTML, CSS, Bootstrap, JavaScript, jQuery, AJAX
* **Backend:** ASP.NET Core MVC (.NET 6/7), Entity Framework Core (Code First)
* **Database:** Microsoft SQL Server
* **Architecture:** MVC Pattern + SOLID Principles + IoC (Dependency Injection)

---

## 📂 Folder Structure

```
FitVerse/
🔝 Controllers/
🔝 Models/
🔝 ViewModels/
   └️ [Entity]ViewModels/
🔝 Views/
   └️ Shared/
   └️ Home/
   └️ Product/
   └️ Cart/
   └️ Admin/
🔝 Data/
   └️ ApplicationDbContext.cs
🔝 Repositories/
   └️ Interfaces/
   └️ Implementations/
🔝 Services/
🔝 wwwroot/
   └️ css/
   └️ js/
   └️ images/
🔝 appsettings.json
🔝 Program.cs / Startup.cs
```

---

## 🚀 Getting Started (Development)

### Prerequisites

* [.NET SDK](https://dotnet.microsoft.com/en-us/download)
* [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
* [Visual Studio 2022+](https://visualstudio.microsoft.com/) or VS Code

### Setup

```bash
# Clone the repository
git clone https://github.com/yourusername/FitVerse.git
cd FitVerse

# Restore packages
dotnet restore

# Apply migrations and update database
dotnet ef database update

# Run the project
dotnet run
```

---

## 👥 Team Members

This project is developed by a 6-member team for a one-week challenge.

* **محمد الصباغ** (Team Leader)
* \[Member 2]
* \[Member 3]
* \[Member 4]
* \[Member 5]
* \[Member 6]

*(You can list GitHub profiles or responsibilities here)*

---

## 📸 Screenshots

> *(Add homepage, product details, cart, admin panel, etc. once available)*

---

## 📄 License

This project is licensed under the **MIT License** – see the [LICENSE](./LICENSE) file for details.

---

## 🌐 Brand Info

**FitVerse** – *A Universe of Style that Fits Everyone.*

Arabic Name: **فيتفِرس للأزياء**
Slogan: **"عالم من الأناقة يناسبك"**

---

## 🤝 Contributions

Feel free to fork and enhance — this project is open for learning and collaboration.

---

## 🔗 Useful Links

* [ASP.NET Core Docs](https://learn.microsoft.com/en-us/aspnet/core/)
* [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
* [Bootstrap 5](https://getbootstrap.com/)

---
