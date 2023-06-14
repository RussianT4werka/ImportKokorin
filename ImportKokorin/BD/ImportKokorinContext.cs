using System;
using System.Collections.Generic;
using ImportKokorin.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ImportKokorin.BD
{
    public partial class ImportKokorinContext : DbContext
    {
        private static ImportKokorinContext instance;

        public ImportKokorinContext()
        {
        }

        public ImportKokorinContext(DbContextOptions<ImportKokorinContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Gender> Genders { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderProduct> OrderProducts { get; set; } = null!;
        public virtual DbSet<Photo> Photos { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=192.168.1.14;user=student;password=student;database=ImportKokorin");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gender>(entity =>
            {
                entity.ToTable("Gender");

                entity.Property(e => e.Name).HasMaxLength(6);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Status");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_User");
            });

            modelBuilder.Entity<OrderProduct>(entity =>
            {
                entity.ToTable("OrderProduct");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderProduct_Order");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderProduct_Product");
            });

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.ToTable("Photo");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.PhotoId).HasColumnName("PhotoID");

                entity.HasOne(d => d.Photo)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.PhotoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Photo");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.GenderId).HasColumnName("GenderID");

                entity.Property(e => e.IpAdress).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Gender");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public static ImportKokorinContext GetInstance()
        {
            if (instance == null)
            {
                instance = new ImportKokorinContext();
            }
            return instance;
        }
    }
}
