using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

public partial class RenTurtorToStudentContext : DbContext
{
    public RenTurtorToStudentContext()
    {
    }

    public RenTurtorToStudentContext(DbContextOptions<RenTurtorToStudentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Tutor> Tutors { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserApprovalLog> UserApprovalLogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(local);Uid=sa;Pwd=1234567890;Database=RenTurtorToStudent; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A2B7701AE88");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName)
                .IsRequired()
                .HasMaxLength(255);
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Courses__C92D71873D3CA8D5");

            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CourseName)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TutorId).HasColumnName("TutorID");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Category).WithMany(p => p.Courses)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Courses__Categor__5812160E");

            entity.HasOne(d => d.Tutor).WithMany(p => p.Courses)
                .HasForeignKey(d => d.TutorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Courses__TutorID__59063A47");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__6A4BEDF65C3D8283");

            entity.ToTable("Feedback");

            entity.Property(e => e.FeedbackId).HasColumnName("FeedbackID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");

            entity.HasOne(d => d.Course).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Feedback__Course__6C190EBB");

            entity.HasOne(d => d.Student).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Feedback__Studen__6B24EA82");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BAFE4438CED");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");
            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Student).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__StudentI__5DCAEF64");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__OrderDet__D3B9D30CEC01AE01");

            entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.TotalPrice)
                .HasComputedColumnSql("([UnitPrice]*[Quantity])", false)
                .HasColumnType("decimal(29, 2)");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Course).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__Cours__619B8048");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__Order__60A75C0F");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Students__32C52A79EB1B6115");

            entity.Property(e => e.StudentId)
                .ValueGeneratedNever()
                .HasColumnName("StudentID");

            entity.HasOne(d => d.StudentNavigation).WithOne(p => p.Student)
                .HasForeignKey<Student>(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Students__Studen__5165187F");
        });

        modelBuilder.Entity<Tutor>(entity =>
        {
            entity.HasKey(e => e.TutorId).HasName("PK__Tutors__77C70FC23A526FC0");

            entity.Property(e => e.TutorId)
                .ValueGeneratedNever()
                .HasColumnName("TutorID");
            entity.Property(e => e.Experience).HasMaxLength(255);
            entity.Property(e => e.Qualifications).HasMaxLength(255);
            entity.Property(e => e.Specialization).HasMaxLength(255);

            entity.HasOne(d => d.TutorNavigation).WithOne(p => p.Tutor)
                .HasForeignKey<Tutor>(d => d.TutorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tutors__TutorID__4E88ABD4");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC6D7CAAA5");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(255);
            entity.Property(e => e.PasswordHash)
                .IsRequired()
                .HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Role)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<UserApprovalLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__UserAppr__5E5499A85BA64BCF");

            entity.Property(e => e.LogId).HasColumnName("LogID");
            entity.Property(e => e.AdminId).HasColumnName("AdminID");
            entity.Property(e => e.Decision)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.DecisionDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Reason).HasMaxLength(255);
            entity.Property(e => e.TutorId).HasColumnName("TutorID");

            entity.HasOne(d => d.Admin).WithMany(p => p.UserApprovalLogs)
                .HasForeignKey(d => d.AdminId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserAppro__Admin__66603565");

            entity.HasOne(d => d.Tutor).WithMany(p => p.UserApprovalLogs)
                .HasForeignKey(d => d.TutorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserAppro__Tutor__656C112C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
