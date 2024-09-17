using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TrainingWEBAPI.Models;

public partial class SgDbContext : DbContext
{
    public SgDbContext()
    {
        this.ChangeTracker.LazyLoadingEnabled = true;
    }

    public SgDbContext(DbContextOptions<SgDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attempt> Attempts { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Quiz> Quizzes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=STEPHEN-SIM\\SQLEXPRESS;Initial Catalog=sg_db;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attempt>(entity =>
        {
            entity.Property(e => e.AttemptId).ValueGeneratedNever();
            entity.Property(e => e.UserId).HasMaxLength(255);

            entity.HasOne(d => d.Qn).WithMany(p => p.Attempts)
                .HasForeignKey(d => d.QnId)
                .HasConstraintName("FK_Attempts_Questions");

            entity.HasOne(d => d.User).WithMany(p => p.Attempts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Attempts_Users");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QnId);

            entity.Property(e => e.QnId).ValueGeneratedNever();
            entity.Property(e => e.Question1).HasColumnName("Question");

            entity.HasOne(d => d.Quiz).WithMany(p => p.Questions)
                .HasForeignKey(d => d.QuizId)
                .HasConstraintName("FK_Questions_Quizzes");
        });

        modelBuilder.Entity<Quiz>(entity =>
        {
            entity.Property(e => e.QuizId).ValueGeneratedNever();
            entity.Property(e => e.ForCompetitor)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ForExpert)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.UserId).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
