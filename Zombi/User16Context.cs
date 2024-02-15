using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Zombi;

public partial class User16Context : DbContext
{
    public User16Context()
    {
    }

    public User16Context(DbContextOptions<User16Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Plant> Plants { get; set; }

    public virtual DbSet<PlantType> PlantTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Zombie> Zombies { get; set; }

    public virtual DbSet<ZombieType> ZombieTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=192.168.200.35;user=user16;database=user16;password=65644;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Plant>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Damage).HasColumnName("damage");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.PlantTypeId).HasColumnName("plantType_id");

            entity.HasOne(d => d.PlantType).WithMany(p => p.Plants)
                .HasForeignKey(d => d.PlantTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Plants_PlantType");
        });

        modelBuilder.Entity<PlantType>(entity =>
        {
            entity.ToTable("PlantType");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Title)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("title");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");
        });

        modelBuilder.Entity<Zombie>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Health).HasColumnName("health");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.ZombieTypeId).HasColumnName("zombieType_id");

            entity.HasOne(d => d.ZombieType).WithMany(p => p.Zombies)
                .HasForeignKey(d => d.ZombieTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Zombies_ZombieType");
        });

        modelBuilder.Entity<ZombieType>(entity =>
        {
            entity.ToTable("ZombieType");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
