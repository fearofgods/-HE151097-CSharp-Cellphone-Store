using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace CellphoneStore.Models
{
    public partial class WebContext : DbContext
    {
        public WebContext()
        {
        }

        public WebContext(DbContextOptions<WebContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<ColorDetail> ColorDetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductDetail> ProductDetails { get; set; }
        public virtual DbSet<StorageDetail> StorageDetails { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                optionsBuilder.UseSqlServer(config.GetConnectionString("Connect"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Cid)
                    .HasName("PK__Category__D837D05F7D0A3741");

                entity.ToTable("Category");

                entity.Property(e => e.Cid).HasColumnName("cid");

                entity.Property(e => e.Cname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("cname");
            });

            modelBuilder.Entity<ColorDetail>(entity =>
            {
                entity.HasKey(e => new { e.Pid, e.Color })
                    .HasName("PK__ColorDet__443705741A19ABC1");

                entity.Property(e => e.Pid)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("pid");

                entity.Property(e => e.Color)
                    .HasMaxLength(30)
                    .HasColumnName("color");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.HasOne(d => d.PidNavigation)
                    .WithMany(p => p.ColorDetails)
                    .HasForeignKey(d => d.Pid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ColorDetails_Products");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Orderdate)
                    .HasColumnType("date")
                    .HasColumnName("orderdate");

                entity.Property(e => e.Total).HasColumnName("total");

                entity.Property(e => e.Uname)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("uname");

                entity.HasOne(d => d.UnameNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Uname)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Users");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("color");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .HasColumnName("description");

                entity.Property(e => e.Oid).HasColumnName("oid");

                entity.Property(e => e.Pid)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("pid");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.Storage)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("storage");

                entity.Property(e => e.TotalPrice).HasColumnName("total_price");

                entity.HasOne(d => d.OidNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Oid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetails_Orders");

                entity.HasOne(d => d.PidNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Pid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetails_Products1");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Pid)
                    .HasName("PK__Products__DD37D91AA09F7723");

                entity.Property(e => e.Pid)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("pid");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.Cid).HasColumnName("cid");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Image)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("image")
                    .HasDefaultValueSql("('https://fomantic-ui.com/images/wireframe/white-image.png')");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.HasOne(d => d.CidNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.Cid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_Category");
            });

            modelBuilder.Entity<ProductDetail>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Battery)
                    .HasMaxLength(100)
                    .HasColumnName("battery");

                entity.Property(e => e.Frontcam)
                    .HasMaxLength(100)
                    .HasColumnName("frontcam");

                entity.Property(e => e.Os)
                    .HasMaxLength(100)
                    .HasColumnName("os");

                entity.Property(e => e.Pid)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("pid");

                entity.Property(e => e.Ram)
                    .HasMaxLength(100)
                    .HasColumnName("ram");

                entity.Property(e => e.Rearcam)
                    .HasMaxLength(100)
                    .HasColumnName("rearcam");

                entity.Property(e => e.Screen)
                    .HasMaxLength(100)
                    .HasColumnName("screen");

                entity.Property(e => e.Sim)
                    .HasMaxLength(100)
                    .HasColumnName("sim");

                entity.Property(e => e.Soc)
                    .HasMaxLength(100)
                    .HasColumnName("soc");

                entity.HasOne(d => d.PidNavigation)
                    .WithMany(p => p.ProductDetails)
                    .HasForeignKey(d => d.Pid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductDetails_Products");
            });

            modelBuilder.Entity<StorageDetail>(entity =>
            {
                entity.HasKey(e => new { e.Pid, e.Storage })
                    .HasName("PK__StorageD__CC6124071115EC30");

                entity.Property(e => e.Pid)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("pid");

                entity.Property(e => e.Storage)
                    .HasMaxLength(30)
                    .HasColumnName("storage");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.HasOne(d => d.PidNavigation)
                    .WithMany(p => p.StorageDetails)
                    .HasForeignKey(d => d.Pid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StorageDetails_Products");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__Users__F3DBC573EF12FC22");

                entity.Property(e => e.Username)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasColumnName("address");

                entity.Property(e => e.Birthday)
                    .HasColumnType("date")
                    .HasColumnName("birthday");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("firstname");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("lastname");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
