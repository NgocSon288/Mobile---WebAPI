using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ElectronicShopAPI.Models.EF
{
    public partial class ElectronicDBContext : DbContext
    {
        public ElectronicDBContext()
            : base("name=ElectronicDBContext")
        {
        }

        public virtual DbSet<Advertisement> Advertisements { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<BillInfo> BillInfoes { get; set; }
        public virtual DbSet<BillStatu> BillStatus { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<DiscountCode> DiscountCodes { get; set; }
        public virtual DbSet<KeyWord> KeyWords { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductDetail> ProductDetails { get; set; }
        public virtual DbSet<ProductOption> ProductOptions { get; set; }
        public virtual DbSet<Promotion> Promotions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Advertisement>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Bill>()
                .Property(e => e.TotalBill)
                .HasPrecision(18, 0);

            modelBuilder.Entity<BillInfo>()
                .Property(e => e.Total)
                .HasPrecision(18, 0);

            modelBuilder.Entity<BillStatu>()
                .HasMany(e => e.Bills)
                .WithOptional(e => e.BillStatu)
                .HasForeignKey(e => e.BillStatusID);

            modelBuilder.Entity<Category>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.PassWord)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Avatar)
                .IsUnicode(false);

            modelBuilder.Entity<DiscountCode>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Image1)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Image2)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Image3)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Image4)
                .IsUnicode(false);

            modelBuilder.Entity<ProductOption>()
                .Property(e => e.DelPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ProductOption>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ProductOption>()
                .HasMany(e => e.BillInfoes)
                .WithOptional(e => e.ProductOption)
                .HasForeignKey(e => e.OptionProduct);
        }
    }
}
