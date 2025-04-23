# 📝 ToDo Web API - ASP.NET Core 8

This is a RESTful Web API project built with **ASP.NET Core 8**, following the **Clean Architecture** pattern. It allows full CRUD operations on user entities, task lists, and individual tasks. It uses **Entity Framework Core** for SQL Server or In-Memory database integration and includes automatic API documentation via Swagger.

---

## 🚀 Features

✅ Full CRUD operations for users  
✅ Full CRUD operations for task lists  
✅ Full CRUD operations for individual tasks  
✅ Integration with SQL Server or In-Memory DB  
✅ Clean Architecture: layered separation (Domain, Application, Infrastructure, WebAPI)  
✅ Auto-generated Swagger documentation  
✅ Seed data generation with In-Memory DB  

---

## 🛠️ Technologies Used

- ASP.NET Core 8
- C#
- Entity Framework Core 8
- SQL Server / In-Memory Database
- Swagger / OpenAPI (Swashbuckle)
- AutoMapper
- Clean Architecture principles

---

## 📬 Main API Endpoints

**Users**
- `GET /api/User/GetAll` → Get all users  
- `GET /api/User/{id}` → Get a user by ID  
- `POST /api/User` → Create a new user  
- `PUT /api/User/{id}` → Update an existing user  
- `DELETE /api/User/{id}` → Delete a user  

**Task Lists**
- `GET /api/TaskList/GetAll` → Get all task lists  
- `GET /api/TaskList/{id}` → Get a task list by ID  
- `POST /api/TaskList` → Create a new task list  
- `PUT /api/TaskList/{id}` → Update a task list  
- `DELETE /api/TaskList/{id}` → Delete a task list  

**Task Items**
- `GET /api/TaskItem/GetAll` → Get all tasks  
- `GET /api/TaskItem/{id}` → Get a task by ID  
- `POST /api/TaskItem` → Create a new task  
- `PUT /api/TaskItem/{id}` → Update a task  
- `DELETE /api/TaskItem/{id}` → Delete a task  

---

## 🧪 Seed Data

When `UseInMemoryDB` is enabled in `appsettings.json`, the application auto-generates:
- 2 users
- 1 task list per user
- 2 task items per list

---
## 📄 Future Improvements
- Input validation with FluentValidation

- Authentication using JWT

- Global exception handling

- Logging with Serilog

- Unit and integration tests
