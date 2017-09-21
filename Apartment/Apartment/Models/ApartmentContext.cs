using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace Apartment.Models
{
    public partial class ApartmentContext : DbContext
    {
        public ApartmentContext(DbContextOptions<ApartmentContext> options)
    : base(options)
{ }

        public virtual DbSet<Contract> Contract { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<District> District { get; set; }
        public virtual DbSet<Province> Province { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<RoomType> RoomType { get; set; }

      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contract>(entity =>
            {
                entity.Property(e => e.ContractId).HasColumnName("ContractID");

                entity.Property(e => e.CustomerName).HasColumnType("nvarchar(50)");

                entity.Property(e => e.BeginDate).HasColumnType("date");

                entity.Property(e => e.ContarctNo).HasColumnType("nchar(10)");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Contract)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Contract_Customer");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Contract)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK_Contract_Room");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.BirthDay).HasColumnType("date");

                entity.Property(e => e.CustomerName).HasMaxLength(50);

                entity.Property(e => e.DistrictId).HasColumnName("DistrictID");

                entity.Property(e => e.Email).HasColumnType("varchar(50)");

                entity.Property(e => e.Gender).HasColumnType("char(10)");

                entity.Property(e => e.ProvinceId).HasColumnName("ProvinceID");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.DistrictId)
                    .HasConstraintName("FK_Customer_District");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Customer)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK_Customer_Province");
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.Property(e => e.DistrictId).HasColumnName("DistrictID");

                entity.Property(e => e.DistrictName).HasMaxLength(150);

                entity.Property(e => e.ProvinceId).HasColumnName("ProvinceID");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.District)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK_District_Province");
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.Property(e => e.ProvinceId).HasColumnName("ProvinceID");

                entity.Property(e => e.ProvinceName).HasMaxLength(150);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(e => e.RoomId).HasColumnName("RoomID");

                entity.Property(e => e.RoomNo).HasMaxLength(50);

                entity.Property(e => e.Area).HasMaxLength(50);

                entity.Property(e => e.RoomTypeId).HasColumnName("RoomTypeID");

                entity.HasOne(d => d.RoomType)
                    .WithMany(p => p.Room)
                    .HasForeignKey(d => d.RoomTypeId)
                    .HasConstraintName("FK_Room_RoomType");
            });

            modelBuilder.Entity<RoomType>(entity =>
            {
                entity.Property(e => e.RoomTypeId)
                    .HasColumnName("RoomTypeID")
                    .ValueGeneratedNever();

                entity.Property(e => e.RoomTypeName).HasMaxLength(150);
                entity.Property(e => e.PriceByType).HasColumnName("PriceByType");
                entity.Property(e => e.AreaByType).HasMaxLength(50);
            });
        }
    }
}