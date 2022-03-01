
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class DBContextWebAPI : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Data Source=DESKTOP-NVB7S6L;Initial Catalog=DA_FashionCheap2322;Persist Security Info=True;User ID=kieu96;Password=123");
                // thay đường dẫ Data ở đây
            }
        }

        //khai báo bảng
        public DbSet<PRODUCTS> Productses { get; set; }
        public DbSet<PRODUCTS_VARIANTS> ProductsVariantses { get; set; }
        public DbSet<PRODUCTS_OPTIONS> ProductsOptionses { get; set; }
        public DbSet<OPTIONS> Optionses { get; set; }
        public DbSet<OPTIONS_VALUES> OptionsValueses { get; set; }
        public DbSet<VARIANTS_VALUES> VariantsValueses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            ////Bảng PRODUCTS
            modelBuilder.Entity<PRODUCTS>(entity =>
            {
                entity.ToTable("PRODUCTS");
                entity.HasKey(p => p.id_Product);
                entity.Property(e => e.products_Name).HasMaxLength(50);
            });
            // bảng Option
            modelBuilder.Entity<OPTIONS>(entity =>
            {
                entity.ToTable("OPTIONS");
                entity.HasKey(p => p.id_Option);
                entity.Property(e => e.option_Name).HasMaxLength(50);
            });
            // bảng Products_OPtion
            modelBuilder.Entity<PRODUCTS_OPTIONS>(entity =>
            {
                entity.ToTable("PRODUCTS_OPTIONS");
                entity.HasKey(p => new { p.id_Product, p.id_Option });

                entity.HasOne(p => p.Products)
                    .WithMany(c => c.ProductsOptionses)
                    .HasForeignKey(c => c.id_Product)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(p => p.Optionses)
                    .WithMany(c => c.OptionsesProductses)
                    .HasForeignKey(c => c.id_Option)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
            // bảng Option_Values
            modelBuilder.Entity<OPTIONS_VALUES>(entity =>
            {
                entity.ToTable("OPTIONS_VALUES");
                entity.HasKey(p => p.id_Values);
                entity.HasOne(p => p.Options)
                    .WithMany(c => c.OptionsValueses)
                    .HasForeignKey(c => c.id_Option)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
            // Bảng PRODUCTS_VARIANTS
            modelBuilder.Entity<PRODUCTS_VARIANTS>(entity =>
            {
                entity.ToTable("PRODUCTS_VARIANTS");
                entity.HasKey(p => p.id_Variant);
                entity.HasOne(p => p.Products)
                    .WithMany(c => c.ProductsVariantses)
                    .HasForeignKey(c => c.id_Product)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
            modelBuilder.Entity<VARIANTS_VALUES>(entity =>
            {
                entity.ToTable("VARIANTS_VALUES");
                entity.HasKey(p => new { p.id_Product, p.id_Variant, p.id_Option, p.id_Values });
                //noi bang PRoducts_Variants
                entity.HasOne<PRODUCTS_VARIANTS>(p => p.ProductsVariants)
                    .WithMany(c => c.VariantValues)
                    .HasForeignKey(d => d.id_Variant)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                // noi bang PROducts_option
                entity.HasOne<PRODUCTS_OPTIONS>(p => p.ProductsOptions)
                    .WithMany(c => c.ProductOPtionses)
                    .HasForeignKey(c => new { c.id_Product, c.id_Option })
                    .OnDelete(DeleteBehavior.ClientSetNull);
                // noi bang OPTION_VALUES
                entity.HasOne<OPTIONS_VALUES>(i => i.OptionsValues)
                    .WithMany(c => c.OptionValueses)
                    .HasForeignKey(c => c.id_Values).OnDelete(DeleteBehavior.ClientSetNull);
            });
        }
    }
}
