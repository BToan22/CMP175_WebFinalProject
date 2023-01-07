using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebFinal.Models;

public partial class WebFinalContext : DbContext
{
    public WebFinalContext()
    {
    }

    public WebFinalContext(DbContextOptions<WebFinalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Lecturer> Lecturers { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server = localhost,1433; Database = WebFinal;User ID = toan;password = 123; Encrypt= False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Lecturer>(entity =>
        {
            entity.ToTable("Lecturer");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.LecturerEmail).HasMaxLength(50);
            entity.Property(e => e.LecturerId)
                .HasMaxLength(50)
                .HasColumnName("LecturerID");
            entity.Property(e => e.LecturerName).HasMaxLength(50);
            entity.Property(e => e.LecturerPhone).HasMaxLength(50);
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.HeadCity).HasMaxLength(255);
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Province1)
                .HasMaxLength(255)
                .HasColumnName("Province");
            entity.Property(e => e.Section).HasMaxLength(255);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("Student");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.StudentEmail).HasMaxLength(50);
            entity.Property(e => e.StudentGender).HasMaxLength(50);
            entity.Property(e => e.StudentId)
                .HasMaxLength(50)
                .HasColumnName("StudentID");
            entity.Property(e => e.StudentName).HasMaxLength(50);
            entity.Property(e => e.StudentPhone).HasMaxLength(50);
            entity.Property(e => e.StudentTown).HasMaxLength(50);
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.ToTable("Subject");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Major).HasMaxLength(50);
            entity.Property(e => e.SubjectId)
                .HasMaxLength(50)
                .HasColumnName("SubjectID");
            entity.Property(e => e.SubjectName).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_User");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Fullname).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
