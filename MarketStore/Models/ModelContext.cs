using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace MarketStore.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aboutu> Aboutus { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Contactu> Contactus { get; set; }
        public virtual DbSet<Home> Homes { get; set; }
        public virtual DbSet<Order2> Order2s { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Productorder> Productorders { get; set; }
        public virtual DbSet<PublicReview> PublicReviews { get; set; }
        public virtual DbSet<RoleLogin> RoleLogins { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<Testimonial> Testimonials { get; set; }
        public virtual DbSet<User1> User1s { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
        public virtual DbSet<VisaCard> VisaCards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("USER ID=TAH13_User17;PASSWORD=SE@assaf1997;DATA SOURCE=94.56.229.181:3488/traindb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TAH13_USER17");

            modelBuilder.Entity<Aboutu>(entity =>
            {
                entity.ToTable("ABOUTUS");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Text)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("TEXT");
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("CART");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.ProductId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("PRODUCT_ID");

                entity.Property(e => e.Quntity)
                    .HasPrecision(4)
                    .HasColumnName("QUNTITY");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_PRDUCTC");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_USER");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("CATEGORY");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CATEGORY_NAME");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");
            });

            modelBuilder.Entity<Contactu>(entity =>
            {
                entity.ToTable("CONTACTUS");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Location)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LOCATION");

                entity.Property(e => e.MapLink)
                    .HasMaxLength(600)
                    .IsUnicode(false)
                    .HasColumnName("MAP_LINK");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Phone)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("PHONE");
            });

            modelBuilder.Entity<Home>(entity =>
            {
                entity.ToTable("HOME");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Text)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("TEXT");
            });

            modelBuilder.Entity<Order2>(entity =>
            {
                entity.ToTable("ORDER2");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.LocationDelivery)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("LOCATION_DELIVERY");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("DATE")
                    .HasColumnName("ORDER_DATE");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.Property(e => e.StoreId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("STORE_ID");

                entity.Property(e => e.Totalprice)
                    .HasColumnType("NUMBER")
                    .HasColumnName("TOTALPRICE");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Order2s)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_STORE_ORDER");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Order2s)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_USER_ORDER");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("PRODUCT");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.CostPrice)
                    .HasColumnType("NUMBER(8,2)")
                    .HasColumnName("COST_PRICE");

                entity.Property(e => e.Details)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("DETAILS");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PRODUCT_NAME");

                entity.Property(e => e.Quntity)
                    .HasPrecision(10)
                    .HasColumnName("QUNTITY");

                entity.Property(e => e.SellingPrice)
                    .HasColumnType("NUMBER(8,2)")
                    .HasColumnName("SELLING_PRICE");

                entity.Property(e => e.StoreId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("STORE_ID");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_STORE");
            });

            modelBuilder.Entity<Productorder>(entity =>
            {
                entity.ToTable("PRODUCTORDER");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.OrderId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ORDER_ID");

                entity.Property(e => e.ProductId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("PRODUCT_ID");

                entity.Property(e => e.Quntity)
                    .HasPrecision(4)
                    .HasColumnName("QUNTITY");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Productorders)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_PORDER");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Productorders)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_OPRODUCT");
            });

            modelBuilder.Entity<PublicReview>(entity =>
            {
                entity.ToTable("PUBLIC_REVIEW");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Masseage)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("MASSEAGE");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<RoleLogin>(entity =>
            {
                entity.ToTable("ROLE_LOGIN");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAME");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("STORE");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.CategoryId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("CATEGORY_ID");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");

                entity.Property(e => e.MonthlyFee)
                    .HasColumnType("NUMBER(8,2)")
                    .HasColumnName("MONTHLY_FEE");

                entity.Property(e => e.StoreName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("STORE_NAME");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_CATEGORY");
            });

            modelBuilder.Entity<Testimonial>(entity =>
            {
                entity.ToTable("TESTIMONIAL");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Masseage)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("MASSEAGE");

                entity.Property(e => e.StatusTestimonials)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("STATUS_TESTIMONIALS");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Testimonials)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_TEST_USER");
            });

            modelBuilder.Entity<User1>(entity =>
            {
                entity.ToTable("USER1");

                entity.HasIndex(e => e.Email, "SYS_C00216703")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Fname)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("FNAME");

                entity.Property(e => e.ImagePath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE_PATH");

                entity.Property(e => e.Lname)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("LNAME");

                entity.Property(e => e.Phone)
                    .HasPrecision(14)
                    .HasColumnName("PHONE");
            });

            modelBuilder.Entity<UserLogin>(entity =>
            {
                entity.ToTable("USER_LOGIN");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORD");

                entity.Property(e => e.RoleloginId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("ROLELOGIN_ID");

                entity.HasOne(d => d.Rolelogin)
                    .WithMany(p => p.UserLogins)
                    .HasForeignKey(d => d.RoleloginId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_ROLELOGIN");
            });

            modelBuilder.Entity<VisaCard>(entity =>
            {
                entity.ToTable("VISA_CARD");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER(38)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Balance)
                    .HasColumnType("NUMBER(8,2)")
                    .HasColumnName("BALANCE");

                entity.Property(e => e.CardNumber)
                    .HasPrecision(14)
                    .HasColumnName("CARD_NUMBER");

                entity.Property(e => e.ExpiryDate)
                    .HasColumnType("DATE")
                    .HasColumnName("EXPIRY_DATE");

                entity.Property(e => e.SecurityCode)
                    .HasPrecision(4)
                    .HasColumnName("SECURITY_CODE");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.VisaCards)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_USER_VISA");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
