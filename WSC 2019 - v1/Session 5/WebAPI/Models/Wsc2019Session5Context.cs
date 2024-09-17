﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Asp.net_core_web_api.Models;

public partial class Wsc2019Session5Context : DbContext
{
    public Wsc2019Session5Context()
    {
    }

    public Wsc2019Session5Context(DbContextOptions<Wsc2019Session5Context> options)
        : base(options)
    {
    }

    public virtual DbSet<RockType> RockTypes { get; set; }

    public virtual DbSet<Well> Wells { get; set; }

    public virtual DbSet<WellLayer> WellLayers { get; set; }

    public virtual DbSet<WellType> WellTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=STEPHEN-SIM\\SQLEXPRESS;Initial Catalog=WSC2019_Session5;Integrated Security=True;Trust Server Certificate=True")
        .UseLazyLoadingProxies();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RockType>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BackgroundColor).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Well>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.WellName).HasMaxLength(50);
            entity.Property(e => e.WellTypeId).HasColumnName("WellTypeID");

            entity.HasOne(d => d.WellType).WithMany(p => p.Wells)
                .HasForeignKey(d => d.WellTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Wells_WellTypes");
        });

        modelBuilder.Entity<WellLayer>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.RockTypeId).HasColumnName("RockTypeID");
            entity.Property(e => e.WellId).HasColumnName("WellID");

            entity.HasOne(d => d.RockType).WithMany(p => p.WellLayers)
                .HasForeignKey(d => d.RockTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WellLayers_RockTypes");

            entity.HasOne(d => d.Well).WithMany(p => p.WellLayers)
                .HasForeignKey(d => d.WellId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WellLayers_Wells");
        });

        modelBuilder.Entity<WellType>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
