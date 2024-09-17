using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models;

public partial class Asc2023MobileMyContext : DbContext
{
    public Asc2023MobileMyContext()
    {
    }

    public Asc2023MobileMyContext(DbContextOptions<Asc2023MobileMyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<Competitor> Competitors { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<Sponsorship> Sponsorships { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=STEPHEN-SIM\\SQLEXPRESS;Initial Catalog=ASC2023_Mobile_MY;Integrated Security=True;Encrypt=True;Trust Server Certificate=True")
        .UseLazyLoadingProxies();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Card>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CardNo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Cvv)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("CVV");
        });

        modelBuilder.Entity<Competitor>(entity =>
        {
            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.RequiredAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Country).WithMany(p => p.Competitors)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Competitors_Countries");

            entity.HasOne(d => d.Skill).WithMany(p => p.Competitors)
                .HasForeignKey(d => d.SkillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Competitors_Skills");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasOne(d => d.Currency).WithMany(p => p.Countries)
                .HasForeignKey(d => d.CurrencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Countries_Currencies");
        });

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.Property(e => e.Rate).HasColumnType("decimal(20, 8)");
        });

        modelBuilder.Entity<Sponsorship>(entity =>
        {
            entity.Property(e => e.Amount).HasColumnType("decimal(20, 2)");
            entity.Property(e => e.DateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Competitor).WithMany(p => p.Sponsorships)
                .HasForeignKey(d => d.CompetitorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sponsorships_Competitors");

            entity.HasOne(d => d.Currency).WithMany(p => p.Sponsorships)
                .HasForeignKey(d => d.CurrencyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sponsorships_Currencies");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
