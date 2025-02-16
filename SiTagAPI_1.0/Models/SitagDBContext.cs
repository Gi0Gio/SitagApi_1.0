using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SiTagAPI_1._0.Models;

public partial class SitagDbContext : DbContext
{
    public SitagDbContext()
    {
    }

    public SitagDbContext(DbContextOptions<SitagDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Activity> Activities { get; set; }

    public virtual DbSet<Animal> Animals { get; set; }

    public virtual DbSet<AnimalDatum> AnimalData { get; set; }

    public virtual DbSet<Farm> Farms { get; set; }

    public virtual DbSet<FarmDivision> FarmDivisions { get; set; }

    public virtual DbSet<Finance> Finances { get; set; }

    public virtual DbSet<MedicalService> MedicalServices { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=sitag2.database.windows.net;Database=SitagDB;User Id=adminSitag;Password=Geshk1234;Encrypt=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Activity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__service__3213E83FAA60F185");

            entity.ToTable("activity");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AnimalId).HasColumnName("animalId");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Type)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("type");

            entity.HasOne(d => d.Animal).WithMany(p => p.Activities)
                .HasForeignKey(d => d.AnimalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_service");
        });

        modelBuilder.Entity<Animal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__animal__3213E83F32190061");

            entity.ToTable("animal");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Birthdate).HasColumnName("birthdate");
            entity.Property(e => e.Color)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("color");
            entity.Property(e => e.Number)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("number");
            entity.Property(e => e.Race)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("race");
            entity.Property(e => e.Sex).HasColumnName("sex");
            entity.Property(e => e.Specie)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("specie");
        });

        modelBuilder.Entity<AnimalDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__data__3213E83F4C5A5A0B");

            entity.ToTable("animalData");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AnimalId).HasColumnName("animal_id");
            entity.Property(e => e.DivisionId).HasColumnName("divisionId");
            entity.Property(e => e.EntryDate)
                .HasColumnType("datetime")
                .HasColumnName("entryDate");
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.Weight)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("weight");

            entity.HasOne(d => d.Animal).WithMany(p => p.AnimalData)
                .HasForeignKey(d => d.AnimalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_data");

            entity.HasOne(d => d.Division).WithMany(p => p.AnimalData)
                .HasForeignKey(d => d.DivisionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_division_data");
        });

        modelBuilder.Entity<Farm>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__farm__3213E83FE9B09C93");

            entity.ToTable("farm");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Location)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("location");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Farms)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_farm");
        });

        modelBuilder.Entity<FarmDivision>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__division__3213E83F5C3F5CE9");

            entity.ToTable("farmDivision");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FarmId).HasColumnName("farmId");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.State)
                .HasDefaultValue((byte)1)
                .HasColumnName("state");

            entity.HasOne(d => d.Farm).WithMany(p => p.FarmDivisions)
                .HasForeignKey(d => d.FarmId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_farm_division");
        });

        modelBuilder.Entity<Finance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__record__3213E83F26670EEB");

            entity.ToTable("finance");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Sender)
                .HasMaxLength(100)
                .HasDefaultValue("Anonymous")
                .HasColumnName("sender");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.UserId).HasColumnName("userId");

            entity.HasOne(d => d.User).WithMany(p => p.Finances)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_record");
        });

        modelBuilder.Entity<MedicalService>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__medicalL__3213E83F72192B10");

            entity.ToTable("medicalService");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AnimalId).HasColumnName("animalId");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Drug)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("drug");
            entity.Property(e => e.Reason)
                .HasColumnType("text")
                .HasColumnName("reason");

            entity.HasOne(d => d.Animal).WithMany(p => p.MedicalServices)
                .HasForeignKey(d => d.AnimalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_animal_medAplication");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__user__3213E83FC0ED6D27");

            entity.ToTable("user");

            entity.HasIndex(e => e.Cellphone, "UQ_cellphone").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cellphone)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("cellphone");
            entity.Property(e => e.Email)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.LastName)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("lastName");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
