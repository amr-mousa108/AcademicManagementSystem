# Academic Management System

A comprehensive ASP.NET MVC web application for managing educational institutions, built with a clean layered architecture.

## 📋 Overview

**Academic Management System** is a robust web application designed to streamline the management of educational centers. It handles trainees, instructors, courses, departments, and academic results through an intuitive interface and maintainable codebase.

## 🏗️ Architecture

The project follows a **layered architecture** pattern for better separation of concerns, maintainability, and scalability:

```
┌─────────────────────────────────────┐
│          Views (UI Layer)           │  ← Razor Pages & User Interface
├─────────────────────────────────────┤
│    Controllers (Presentation)       │  ← Request Handling & Routing
├─────────────────────────────────────┤
│      Models (Domain Layer)          │  ← Business Entities & Logic
├─────────────────────────────────────┤
│   Data (Persistence Layer)          │  ← DbContext & EF Core
└─────────────────────────────────────┘
```

### Layer Responsibilities

- **Views (UI Layer)**: Razor pages for rendering data and capturing user input
- **Controllers (Presentation Layer)**: Handles HTTP requests, coordinates between views and models
- **Models (Domain Layer)**: Defines core entities (Trainee, Instructor, Course, Department, CourseResult) with validation rules
- **Data (Persistence Layer)**: Contains `AppDbContext`, migrations, and database operations using Entity Framework Core

## ✨ Features

- ✅ **Trainee Management**: Add, edit, view, and delete trainee records
- ✅ **Instructor Management**: Manage instructor profiles and assignments
- ✅ **Course Management**: Create and organize courses
- ✅ **Department Management**: Structure departments and their relationships
- ✅ **Results Tracking**: Monitor and record course results for each trainee
- ✅ **Responsive Design**: Bootstrap-powered UI that works on all devices
- ✅ **Clean Architecture**: Layered design for easy maintenance and future expansion

## 🛠️ Technologies Used

| Technology | Purpose |
|------------|---------|
| **ASP.NET MVC** | Web framework |
| **Entity Framework Core** | ORM for database operations |
| **SQL Server** | Database management system |
| **Bootstrap 5** | Responsive UI framework |
| **jQuery** | Client-side interactivity |
| **C# (.NET)** | Primary programming language |

## 🚀 Getting Started

### Prerequisites

- [Visual Studio 2022](https://visualstudio.microsoft.com/) or later
- [.NET 6.0 SDK](https://dotnet.microsoft.com/download) or later
- [SQL Server](https://www.microsoft.com/sql-server) (Express or higher)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/YourUsername/academic-management-system.git
   cd academic-management-system
   ```

2. **Open the solution**
   - Launch Visual Studio
   - Open `AcademicManagementSystem.sln`

3. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

4. **Update database connection string**
   - Open `appsettings.json`
   - Update the connection string to match your SQL Server instance:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=AcademicDB;Trusted_Connection=True;"
     }
   }
   ```

5. **Apply database migrations**
   ```bash
   dotnet ef database update
   ```

6. **Run the application**
   ```bash
   dotnet run
   ```
   Or press `F5` in Visual Studio

7. **Access the application**
   - Open your browser and navigate to `https://localhost:5001`

## 📁 Project Structure

```
AcademicManagementSystem/
├── Controllers/          # MVC Controllers
│   ├── TraineeController.cs
│   ├── InstructorController.cs
│   ├── CourseController.cs
│   └── DepartmentController.cs
├── Models/              # Domain entities
│   ├── Trainee.cs
│   ├── Instructor.cs
│   ├── Course.cs
│   ├── Department.cs
│   └── CourseResult.cs
├── Data/                # Database context & migrations
│   ├── AppDbContext.cs
│   └── Migrations/
├── Views/               # Razor views
│   ├── Trainee/
│   ├── Instructor/
│   ├── Course/
│   ├── Department/
│   └── Shared/
├── wwwroot/             # Static files
│   ├── css/
│   ├── js/
│   └── lib/
└── appsettings.json     # Configuration
```

## 📊 Database Schema

The system uses the following core entities:

- **Trainee**: Student information and enrollment details
- **Instructor**: Teacher profiles and specializations
- **Course**: Course catalog and descriptions
- **Department**: Organizational units
- **CourseResult**: Academic performance records

## 🔄 Future Enhancements

- [ ] Authentication & Authorization (ASP.NET Identity)
- [ ] Role-based access control (Admin, Instructor, Trainee)
- [ ] Advanced reporting and analytics
- [ ] Email notifications
- [ ] API layer for mobile applications
- [ ] File upload for documents and certificates

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👨‍💻 Author

**Your Name**
- GitHub: [amr-mousa108](https://github.com/amr-mousa108)
- LinkedIn: [Amr Mousa](https://www.linkedin.com/in/amr-mousa-0bab79371/)

## 📧 Contact

For questions or support, please open an issue or contact(mailto:amarmousa1223@gmail.com)

---

⭐ **If you find this project helpful, please consider giving it a star!** ⭐
