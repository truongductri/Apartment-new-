using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BuildingApp.Models
{
    public partial class ApartmentDBContext : DbContext
    {
        public ApartmentDBContext(DbContextOptions<ApartmentDBContext> options)
    : base(options)
{ }

        public virtual DbSet<Contract> Contract { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<District> District { get; set; }
        public virtual DbSet<Price> Price { get; set; }
        public virtual DbSet<Province> Province { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<Type> Type { get; set; }

        // Unable to generate entity type for table 'dbo.Type_Floor'. Please see the warning messages.

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contract>(entity =>
            {
                entity.HasKey(e => e.ConstractID)
                    .HasName("PK_Contract");

                entity.Property(e => e.ConstractID).HasColumnName("ConstractID");

                entity.Property(e => e.BeginDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerID).HasColumnName("CustomerID");

                entity.Property(e => e.EndDate).HasColumnType("datetime");
                entity.Property(e => e.AmountOfMoney).HasColumnType("bigint");

                entity.Property(e => e.MetaTitle).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.RoomID).HasColumnName("RoomID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Contract)
                    .HasForeignKey(d => d.CustomerID)
                    .HasConstraintName("FK_Contract_Customer");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Contract)
                    .HasForeignKey(d => d.RoomID)
                    .HasConstraintName("FK_Contract_Room");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerID).HasColumnName("CustomerID");

                entity.Property(e => e.BirthDay).HasColumnType("date");

                entity.Property(e => e.DistrictID).HasColumnName("DistrictID");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.MetaTitle).HasColumnType("nchar(10)");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.ProvinceID).HasColumnName("ProvinceID");

                entity.Property(e => e.Sex).HasMaxLength(50);

                entity.HasOne(d => d.Province)
                     .WithMany(p => p.Customer)
                     .HasForeignKey(d => d.ProvinceID)
                     .HasConstraintName("FK_Customer_Province");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.DistrictID)
                    .HasConstraintName("FK_Customer_District");
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.Property(e => e.DistrictID)
                    .HasColumnName("DistrictID")
                    .ValueGeneratedNever();

                entity.Property(e => e.DistrictName).HasMaxLength(150);

                entity.Property(e => e.ProvinceID).HasColumnName("ProvinceID");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.District)
                    .HasForeignKey(d => d.ProvinceID)
                    .HasConstraintName("FK_District_Province");
            });

            modelBuilder.Entity<Price>(entity =>
            {
                entity.Property(e => e.PriceID)
                    .HasColumnName("PriceID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Price1).HasColumnName("Price");
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.Property(e => e.ProvinceID)
                    .HasColumnName("ProvinceID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ProvinceName).HasMaxLength(150);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(e => e.RoomID).HasColumnName("RoomID");

                entity.Property(e => e.Image).HasMaxLength(50);

                entity.Property(e => e.MetaTiTle).HasMaxLength(50);

                entity.Property(e => e.Name).HasColumnType("nchar(10)");

                entity.Property(e => e.PriceID).HasColumnName("PriceID");

                entity.Property(e => e.TypeID).HasColumnName("TypeID");

                entity.HasOne(d => d.Price)
                    .WithMany(p => p.Room)
                    .HasForeignKey(d => d.PriceID)
                    .HasConstraintName("FK_Room_Price");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Room)
                    .HasForeignKey(d => d.TypeID)
                    .HasConstraintName("FK_Room_Type");
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.Property(e => e.TypeID).HasColumnName("TypeID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Price)
                    .WithMany(p => p.Type)
                    .HasForeignKey(d => d.PriceID)
                    .HasConstraintName("FK_Type_Price");
            });
        }
    }
}