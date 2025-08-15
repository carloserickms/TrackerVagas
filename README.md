# 📌 TrackerVagas - Backend ![Build Status](https://img.shields.io/badge/build-passing-brightgreen)

API developed in **ASP.NET Core** to manage job applications. This backend provides all the necessary endpoints to create, update, and retrieve job postings, track recruitment stages, and generate statistics related to your job search journey.

---

## 🔍 TrackerVagas – Take control of your job hunt

Job searching often means applying through multiple platforms, each with its own system. Keeping track becomes messy, and spreadsheets, while helpful, can quickly become overwhelming.

**TrackerVagas** was created to simplify this process. It allows you to register and monitor all your job applications in one place. You'll see essential details like company information, application dates, and current status — all with a clean and interactive interface.

Like a spreadsheet, **you’re in control**: no unnecessary fields, no forced input. Just the essential tools to keep your job search organized and stress-free.

**TrackerVagas** — everything you need, in one clear and functional space.

<p align="center">
  <img src="https://github.com/user-attachments/assets/f26877a0-4f5b-4423-b37b-85c1e6ccc428" alt="API Demonstration" width="600"/>
</p>

---

## ⚙️ Features

- 📋 Full CRUD for job applications  
- 🧭 Track progress through stages (applied, interview, offer, etc.)  
- 🔍 Filter and sort by status, date, or company  
- 📊 Endpoints for statistics and application insights  
- 🔐 JWT authentication and Google login support  
- 🧪 Automated testing *(COMING SOON)*  

---

## 🛠️ Technologies & Tools

- [.NET 9](https://dotnet.microsoft.com/)
- [ASP.NET Core Web API](https://learn.microsoft.com/aspnet/core/web-api/)
- [Entity Framework Core](https://docs.microsoft.com/ef/core/)
- [MySQL](https://dev.mysql.com/doc/)
- [Swagger](https://swagger.io/tools/swagger-ui/) – for interactive API documentation  
- [xUnit](https://xunit.net/) *(COMING SOON)*

---

## 🌐 Frontend
Repository: https://github.com/carloserickms/TrackerVagas-frontend

## 🚀 How to Run the Project

1. **Clone the repository**
```bash
git clone https://github.com/your-username/TrackerVagas-Backend.git

```
2. **Navigate to the project folder**
```bash
cd TrackerVagas/App
```
3. **Create a .env file in the project root and add the following keys**
```bash
DATABASECONNECTION
MYSQL_ROOT_PASSWORD
MYSQL_DATABASE
SECRETKEY
```
4. **Run docker compose**
```bash
docker compose up --build
```
---
## 👤 Author
Developed by: [Carlos Erick](https://github.com/carloserickms)
📫 Contact: [LinkedIn](https://www.linkedin.com/in/carlos-erick/) | carloserick71@gmail.com
