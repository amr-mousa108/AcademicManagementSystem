using AcademicManagementSystem.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace AcademicManagementSystem.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public AppDbContext()
        {
            
        }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Trainee> Trainees { get; set; }
        public DbSet<CrsResult> crsResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Department Seed
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "Software Engineering", ManagerName = "Ahmed Ali" },
                new Department { Id = 2, Name = "Network", ManagerName = "Mahmoud Hassan" }
            );

            // Course Seed
            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Name = "C# Programming", Degree = 100, MinDegree = 60, DepartmentId = 1 },
                new Course { Id = 2, Name = "ASP.NET MVC", Degree = 100, MinDegree = 70, DepartmentId = 1 },
                new Course { Id = 3, Name = "CCNA Basics", Degree = 100, MinDegree = 65, DepartmentId = 2 }
            );

            // Instructor Seed
            modelBuilder.Entity<Instructor>().HasData(
                new Instructor { Id = 1, Name = "Ali Hassan", Salary = 8000, Address = "Cairo", ImgUrl = "ali.jpg", DepartmentId = 1, CrsId = 1 },
                new Instructor { Id = 2, Name = "Mohamed Salah", Salary = 9500, Address = "Giza", ImgUrl = "salah.jpg", DepartmentId = 1,CrsId = 2 },
                new Instructor { Id = 3, Name = "Omar Tarek", Salary = 7000, Address = "Alex", ImgUrl= "omar.jpg", DepartmentId = 2,CrsId = 3 }
            );

            // Trainee Seed
            modelBuilder.Entity<Trainee>().HasData(
                new Trainee { Id = 1, Name = "Mostafa Adel", Address = "Cairo", ImgUrl = "mostafa.jpg", AcademicLevel = 1, DepartmentId = 1 },
                new Trainee { Id = 2, Name = "Sara Ahmed", Address = "Alex", ImgUrl = "sara.jpg", AcademicLevel =2, DepartmentId = 1 },
                new Trainee { Id = 3, Name = "Nour Ali", Address = "Tanta", ImgUrl = "nour.jpg", AcademicLevel = 2, DepartmentId = 2 }
            );

            // crsResult Seed
            modelBuilder.Entity<CrsResult>().HasData(
                new CrsResult { Id = 1, Crs_Id = 1, Trainee_Id = 1, Degree = 88 },
                new CrsResult { Id = 2, Crs_Id = 2, Trainee_Id = 2, Degree = 97 },
                new CrsResult { Id = 3, Crs_Id = 3, Trainee_Id = 3, Degree = 80 }
            );


        modelBuilder.Entity<CrsResult>()
             .HasOne(cr => cr.Course)
             .WithMany(c => c.crsResults)
             .HasForeignKey(cr => cr.Crs_Id);

            modelBuilder.Entity<CrsResult>()
                .HasOne(t => t.Trainee)
                .WithMany(c => c.CrsResults)
                .HasForeignKey(cr => cr.Trainee_Id);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=D2;Trusted_Connection=True;TrustServerCertificate=True;");
        }


    }
}
