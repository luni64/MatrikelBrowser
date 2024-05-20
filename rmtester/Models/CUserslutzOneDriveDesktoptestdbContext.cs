﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace rmtester.Models;

public partial class CUserslutzOneDriveDesktoptestdbContext : DbContext
{
    public CUserslutzOneDriveDesktoptestdbContext(DbContextOptions<CUserslutzOneDriveDesktoptestdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Family> Families { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<PersonFamily> PersonFamilies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Family>(entity =>
        {
            entity.ToTable("Family");

            entity.Property(e => e.FatherId).HasColumnName("FatherID");
            entity.Property(e => e.MotherId).HasColumnName("MotherID");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.ToTable("Person");
        });

        modelBuilder.Entity<PersonFamily>(entity =>
        {
            entity.ToTable("PersonFamily");

            entity.Property(e => e.FamilyId).HasColumnName("FamilyID");
            entity.Property(e => e.PersonId).HasColumnName("PersonID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}