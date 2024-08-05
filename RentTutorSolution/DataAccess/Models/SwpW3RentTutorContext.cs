using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Models;

public partial class SwpW3RentTutorContext : DbContext
{
    public SwpW3RentTutorContext()
    {
    }

    public SwpW3RentTutorContext(DbContextOptions<SwpW3RentTutorContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderHistory> OrderHistories { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<TutorSubject> TutorSubjects { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(GetConnectionString());
    public string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();
        var strConn = config["ConnectionStrings:DefaultConnection"];
        return strConn;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Courses__C92D71875449643C");

            entity.HasIndex(e => e.CourseName, "IX_Courses_CourseName");

            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.CourseName)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BAF05B8389B");

            entity.HasIndex(e => e.StudentId, "IX_Orders_StudentID");

            entity.HasIndex(e => e.TutorSubjectId, "IX_Orders_TutorSubjectID");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.TutorSubjectId).HasColumnName("TutorSubjectID");

            entity.HasOne(d => d.Student).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__Orders__StudentI__3587F3E0");

            entity.HasOne(d => d.TutorSubject).WithMany(p => p.Orders)
                .HasForeignKey(d => d.TutorSubjectId)
                .HasConstraintName("FK__Orders__TutorSub__367C1819");
        });

        modelBuilder.Entity<OrderHistory>(entity =>
        {
            entity.HasKey(e => e.OrderHistoryId).HasName("PK__OrderHis__718E6CB3BFEA4F15");

            entity.ToTable("OrderHistory");

            entity.Property(e => e.OrderHistoryId).HasColumnName("OrderHistoryID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.StatusChangeDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderHistories)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderHist__Order__3D2915A8");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId).HasName("PK__Subjects__AC1BA388C70799A3");

            entity.HasIndex(e => e.SubjectName, "IX_Subjects_SubjectName");

            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.SubjectCode).HasMaxLength(50);
            entity.Property(e => e.SubjectName)
                .IsRequired()
                .HasMaxLength(255);

            entity.HasOne(d => d.Course).WithMany(p => p.Subjects)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__Subjects__Course__2CF2ADDF");
        });

        modelBuilder.Entity<TutorSubject>(entity =>
        {
            entity.HasKey(e => e.TutorSubjectId).HasName("PK__TutorSub__2D58B9DB95AC2572");

            entity.HasIndex(e => e.SubjectId, "IX_TutorSubjects_SubjectID");

            entity.HasIndex(e => e.TutorId, "IX_TutorSubjects_TutorID");

            entity.Property(e => e.TutorSubjectId).HasColumnName("TutorSubjectID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Fee).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.SubjectId).HasColumnName("SubjectID");
            entity.Property(e => e.TutorId).HasColumnName("TutorID");

            entity.HasOne(d => d.Subject).WithMany(p => p.TutorSubjects)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK__TutorSubj__Subje__31B762FC");

            entity.HasOne(d => d.Tutor).WithMany(p => p.TutorSubjects)
                .HasForeignKey(d => d.TutorId)
                .HasConstraintName("FK__TutorSubj__Tutor__30C33EC3");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC1E2E782E");

            entity.HasIndex(e => e.Email, "IX_Users_Email");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534D8B31597").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.HourlyRate).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.PasswordHash)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.Rating).HasColumnType("decimal(3, 2)");
            entity.Property(e => e.Role)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Specialization).HasMaxLength(255);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.UserRoleId).HasName("PK__UserRole__3D978A550F8C7338");

            entity.HasIndex(e => e.UserId, "IX_UserRoles_UserID");

            entity.Property(e => e.UserRoleId).HasColumnName("UserRoleID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Role)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserRoles__UserI__2645B050");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
