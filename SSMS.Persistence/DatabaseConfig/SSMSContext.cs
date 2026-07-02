using Microsoft.EntityFrameworkCore;
using SSMS.Domain.Entities;

namespace SSMS.Infrustructure.DatabaseConfig
{
    public class SSMSContext(DbContextOptions<SSMSContext> options) : DbContext(options)
    {
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductSizePrice> ProductSizePrices { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        //public virtual DbSet<ProductVariant> ProductVariants { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(e =>
            {
                e.ToTable("Users");

                e.HasKey(u => u.Id);

                e.Property(u => u.Username)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasMaxLength(50);

                e.Property(u => u.Email)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasMaxLength(50);

                e.Property(u => u.KeycloakId)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasMaxLength(50);

                e.Property(u => u.FullName)
                    .HasMaxLength(128);

                e.Property(u => u.Avatar)
                    .HasMaxLength(256);

                e.Property(u => u.Gender)
                    .HasConversion<byte>()
                    .HasColumnType("TINYINT")
                    .IsRequired();

                e.Property(u => u.PhoneNumber)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                e.HasIndex(u => u.Username)
                    .IsUnique();

                e.HasIndex(u => u.Email)
                    .IsUnique();

                e.HasIndex(u => u.KeycloakId)
                    .IsUnique();
            });

            builder.Entity<Category>(e =>
            {
                e.HasKey(e => e.Id);

                e.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                e.Property(e => e.CategoryName)
                    .HasMaxLength(256)
                    .IsRequired();

                e.HasIndex(c => c.CategoryName)
                    .IsUnique();
            });

            builder.Entity<Product>(e =>
            {
                e.HasKey(e => e.Id);

                e.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                e.Property(e => e.ProductName)
                    .HasMaxLength(256)
                    .IsRequired();

                e.Property(e => e.Description)
                    .HasMaxLength(512)
                    .IsRequired(false);

                e.HasIndex(e => e.ProductName)
                    .IsUnique();

                e.HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(p => p.Brand)
                    .WithMany(b => b.Products)
                    .HasForeignKey(p => p.BrandId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Size>(e =>
            {
                e.HasKey(e => e.Id);

                e.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                e.Property(e => e.Value)
                    .IsRequired();

                e.HasIndex(s => s.Value)
                    .IsUnique();
            });

            builder.Entity<Brand>(e =>
            {
                e.HasKey(e => e.Id);

                e.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                e.Property(e => e.BrandName)
                    .HasMaxLength(256)
                    .IsRequired();

                e.HasIndex(b => b.BrandName)
                    .IsUnique();
            });

            builder.Entity<ProductSizePrice>(e =>
            {
                e.HasKey(e => new { e.ProductId, e.SizeId });

                e.Property(e => e.Price)
                    .HasPrecision(18, 2)
                    .IsRequired();

                e.HasOne(psp => psp.Product)
                    .WithMany(p => p.ProductSizePrices)
                    .HasForeignKey(psp => psp.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(psp => psp.Size)
                    .WithMany(s => s.ProductSizePrices)
                    .HasForeignKey(psp => psp.SizeId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<ProductImage>(e =>
            {
                e.HasKey(e => new { e.ProductId, e.Image });

                e.Property(e => e.Image)
                    .HasMaxLength(512)
                    .IsRequired();

                e.Property(e => e.DisplayOrder)
                    .IsRequired();

                e.HasIndex(e => new { e.ProductId, e.DisplayOrder })
                    .IsUnique();

                e.HasOne(pi => pi.Product)
                    .WithMany(p => p.ProductImages)
                    .HasForeignKey(pi => pi.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
