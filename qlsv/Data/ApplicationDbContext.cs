using Microsoft.AspNetCore.Identity;
using qlsv.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using qlsv.Data.Models;

namespace qlsv.Data
{
    public class ApplicationDbContext : IdentityDbContext<Users, AppRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Use Fluent API to configure  

            // Map entities to tables  
            modelBuilder.Entity<Users>().ToTable("Users");
  
            modelBuilder.Entity<Class>().ToTable("Class");
            modelBuilder.Entity<Departments>().ToTable("Departments");
            modelBuilder.Entity<Marks>().ToTable("Marks");
            modelBuilder.Entity<TestSchedule>().ToTable("TestSchedule");
            modelBuilder.Entity<AppRole>().ToTable("AppRole");

            // Configure Primary Users  
            modelBuilder.Entity<Users>().HasKey(ug => ug.Id).HasName("PK_StudentId");
            modelBuilder.Entity<TestSchedule>().HasKey(u => u.Id).HasName("PK_TestSchedule");
            modelBuilder.Entity<Marks>().HasKey(u => new { u.SubjectId, u.localId }).HasName("PK_Mark");
            modelBuilder.Entity<Departments>().HasKey(u => u.DepartmentId).HasName("PK_Department");
            modelBuilder.Entity<Class>().HasKey(u => u.ClassId).HasName("PK_Class");
            modelBuilder.Entity<ClassRoom>().HasKey(u => u.RoomId).HasName("PK_Class");

            // Configure indexes  
            modelBuilder.Entity<Users>().HasIndex(p => p.Id).IsUnique();
   
            modelBuilder.Entity<Marks>().HasIndex(u => new { u.SubjectId, u.localId });
            modelBuilder.Entity<Departments>().HasIndex(u =>  u.Name);
            modelBuilder.Entity<Class>().HasIndex(u =>  u.RoomId);

            // Configure columns  
            modelBuilder.Entity<AppRole>().Property(ug => ug.Description).HasColumnType("nvarchar(100)").HasMaxLength(100).IsRequired();

            modelBuilder.Entity<Users>().Property(ug => ug.LocalId).HasColumnType("nvarchar(20)").HasMaxLength(20).IsRequired();
            modelBuilder.Entity<Users>().Property(ug => ug.Name).HasColumnType("nvarchar(100)").HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Users>().Property(ug => ug.Age).HasColumnType("int").IsRequired();
            modelBuilder.Entity<Users>().Property(ug => ug.Address).HasColumnType("nvarchar(100)").HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Users>().Property(ug => ug.Gender).HasColumnType("int").IsRequired(true);
            modelBuilder.Entity<Users>().Property(ug => ug.Dob).HasColumnType("DATE").IsRequired(true);
            modelBuilder.Entity<Users>().Property(ug => ug.Role).HasColumnType("int").IsRequired(true);
            modelBuilder.Entity<Users>().Property(ug => ug.PhotoPath).HasColumnType("nvarchar(100").HasMaxLength(100);
                


            modelBuilder.Entity<TestSchedule>().Property(u => u.ClassId).HasColumnType("nvarchar(30)").HasMaxLength(30).IsRequired();
            modelBuilder.Entity<TestSchedule>().Property(u => u.ExamHour).HasColumnType("TIME");
            modelBuilder.Entity<TestSchedule>().Property(u => u.ExamTime).HasColumnType("DATE");
            modelBuilder.Entity<TestSchedule>().Property(u => u.SupervisorName).HasColumnType("nvarchar(100)").HasMaxLength(100);
            modelBuilder.Entity<TestSchedule>().Property(u => u.RoomId).HasColumnType("nvarchar(30)").HasMaxLength(30);
            modelBuilder.Entity<TestSchedule>().Property(u => u.Id).UseMySqlIdentityColumn();


            modelBuilder.Entity<Class>().Property(u => u.NumberLessons).HasColumnType("int").IsRequired();
            modelBuilder.Entity<Class>().Property(u => u.NumberCredits).HasColumnType("int").IsRequired();
            modelBuilder.Entity<Class>().Property(u => u.Year).HasColumnType("int").IsRequired();
            modelBuilder.Entity<Class>().Property(u => u.Semester).HasColumnType("int").IsRequired();
            modelBuilder.Entity<Class>().Property(u => u.DepartmentId).HasColumnType("nvarchar(30)").HasMaxLength(30);
           

            modelBuilder.Entity<Class>().Property(u => u.ClassId).HasColumnType("nvarchar(30)").HasMaxLength(30).IsRequired();
            modelBuilder.Entity<Class>().Property(u => u.ClassName).HasColumnType("nvarchar(100)").HasMaxLength(100).IsRequired();
            modelBuilder.Entity<Class>().Property(u => u.Capacity).HasColumnType("int").IsRequired();
            modelBuilder.Entity<Class>().Property(u => u.RoomId).HasColumnType("nvarchar(30)").HasMaxLength(30);

            modelBuilder.Entity<ClassRoom>().Property(u => u.RoomId).HasColumnType("nvarchar(30)").HasMaxLength(30).IsRequired();
            modelBuilder.Entity<ClassRoom>().Property(u => u.Seats).HasColumnType("int");
            modelBuilder.Entity<ClassRoom>().Property(u => u.Desks).HasColumnType("int");

            modelBuilder.Entity<Marks>().Property(u => u.SubjectId).HasColumnType("nvarchar(30)").HasMaxLength(30).IsRequired();
       
            modelBuilder.Entity<Marks>().Property(u => u.marks).HasColumnType("float");

            modelBuilder.Entity<Departments>().Property(u => u.DepartmentId).HasColumnType("nvarchar(30)").HasMaxLength(30).IsRequired();
            modelBuilder.Entity<Departments>().Property(u => u.Name).HasColumnType("nvarchar(100)").HasMaxLength(100).IsRequired();
            



            // Configure relationships  
            modelBuilder.Entity<Departments>().HasOne<Users>().WithMany().HasPrincipalKey(ug => ug.Id).HasForeignKey(u => u.LeaderId).OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_Department_User");

            modelBuilder.Entity<Marks>().HasOne<Class>().WithMany().HasPrincipalKey(ug => ug.ClassId).HasForeignKey(u => u.SubjectId).OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_Marks_Subject");
            modelBuilder.Entity<Marks>().HasOne<Users>().WithMany().HasPrincipalKey(ug => ug.LocalId).HasForeignKey(u => u.localId).OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_Marks_User");

            modelBuilder.Entity<Class>().HasOne<Users>().WithMany().HasPrincipalKey(ug => ug.LocalId).HasForeignKey(u => u.TeacherId).OnDelete(DeleteBehavior.NoAction).HasConstraintName("FK_class_Subject");
            modelBuilder.Entity<Class>().HasOne<ClassRoom>().WithMany().HasForeignKey(u => u.RoomId).HasConstraintName("FK_Class_RoomId");

            modelBuilder.Entity<Class>().HasOne<Departments>().WithMany().HasForeignKey(u => u.DepartmentId).HasConstraintName("FK_Subject_Deparment");

            modelBuilder.Entity<TestSchedule>().HasOne<Class>().WithMany().HasForeignKey(u => u.ClassId).HasConstraintName("Fk_TestSchedule_class");
            modelBuilder.Entity<TestSchedule>().HasOne<ClassRoom>().WithMany().HasForeignKey(u => u.RoomId).HasConstraintName("Fk_TestSchedule_Room");

            base.OnModelCreating(modelBuilder);
        }

        //public DbSet<Users> Users { get; set; }
        public DbSet<Class> Class { get; set; }
        public DbSet<ClassRoom> classRoom { get; set; }
        public DbSet<Departments> Departments { get; set; }
  
        public DbSet<Marks> Marks { get; set; }
        public DbSet<TestSchedule> TestSchedule { get; set; }
    }

}
