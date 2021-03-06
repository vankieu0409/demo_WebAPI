// <auto-generated />
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(DBContextWebAPI))]
    partial class DBContextWebAPIModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DAL.OPTIONS", b =>
                {
                    b.Property<int>("id_Option")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_Option"), 1L, 1);

                    b.Property<string>("option_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("status_Delete")
                        .HasColumnType("bit");

                    b.HasKey("id_Option");

                    b.ToTable("OPTIONS", (string)null);
                });

            modelBuilder.Entity("DAL.OPTIONS_VALUES", b =>
                {
                    b.Property<int>("id_Values")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_Values"), 1L, 1);

                    b.Property<int>("id_Option")
                        .HasColumnType("int");

                    b.Property<string>("option_Values")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("status_Delete")
                        .HasColumnType("bit");

                    b.HasKey("id_Values");

                    b.HasIndex("id_Option");

                    b.ToTable("OPTIONS_VALUES", (string)null);
                });

            modelBuilder.Entity("DAL.PRODUCTS", b =>
                {
                    b.Property<int>("id_Product")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_Product"), 1L, 1);

                    b.Property<string>("products_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("status_Delete")
                        .HasColumnType("bit");

                    b.HasKey("id_Product");

                    b.ToTable("PRODUCTS", (string)null);
                });

            modelBuilder.Entity("DAL.PRODUCTS_OPTIONS", b =>
                {
                    b.Property<int>("id_Product")
                        .HasColumnType("int");

                    b.Property<int>("id_Option")
                        .HasColumnType("int");

                    b.Property<bool>("status_Delete")
                        .HasColumnType("bit");

                    b.HasKey("id_Product", "id_Option");

                    b.HasIndex("id_Option");

                    b.ToTable("PRODUCTS_OPTIONS", (string)null);
                });

            modelBuilder.Entity("DAL.PRODUCTS_VARIANTS", b =>
                {
                    b.Property<int>("id_Variant")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id_Variant"), 1L, 1);

                    b.Property<string>("Products_Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("id_Product")
                        .HasColumnType("int");

                    b.Property<decimal>("import_Price")
                        .HasColumnType("money");

                    b.Property<decimal>("price")
                        .HasColumnType("money");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.Property<bool>("status_Delete")
                        .HasColumnType("bit");

                    b.HasKey("id_Variant");

                    b.HasIndex("id_Product");

                    b.ToTable("PRODUCTS_VARIANTS", (string)null);
                });

            modelBuilder.Entity("DAL.VARIANTS_VALUES", b =>
                {
                    b.Property<int>("id_Product")
                        .HasColumnType("int");

                    b.Property<int>("id_Variant")
                        .HasColumnType("int");

                    b.Property<int>("id_Option")
                        .HasColumnType("int");

                    b.Property<int>("id_Values")
                        .HasColumnType("int");

                    b.Property<bool>("status_Delete")
                        .HasColumnType("bit");

                    b.HasKey("id_Product", "id_Variant", "id_Option", "id_Values");

                    b.HasIndex("id_Values");

                    b.HasIndex("id_Variant");

                    b.HasIndex("id_Product", "id_Option");

                    b.ToTable("VARIANTS_VALUES", (string)null);
                });

            modelBuilder.Entity("DAL.OPTIONS_VALUES", b =>
                {
                    b.HasOne("DAL.OPTIONS", "Options")
                        .WithMany("OptionsValueses")
                        .HasForeignKey("id_Option")
                        .IsRequired();

                    b.Navigation("Options");
                });

            modelBuilder.Entity("DAL.PRODUCTS_OPTIONS", b =>
                {
                    b.HasOne("DAL.OPTIONS", "Optionses")
                        .WithMany("OptionsesProductses")
                        .HasForeignKey("id_Option")
                        .IsRequired();

                    b.HasOne("DAL.PRODUCTS", "Products")
                        .WithMany("ProductsOptionses")
                        .HasForeignKey("id_Product")
                        .IsRequired();

                    b.Navigation("Optionses");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("DAL.PRODUCTS_VARIANTS", b =>
                {
                    b.HasOne("DAL.PRODUCTS", "Products")
                        .WithMany("ProductsVariantses")
                        .HasForeignKey("id_Product")
                        .IsRequired();

                    b.Navigation("Products");
                });

            modelBuilder.Entity("DAL.VARIANTS_VALUES", b =>
                {
                    b.HasOne("DAL.OPTIONS_VALUES", "OptionsValues")
                        .WithMany("OptionValueses")
                        .HasForeignKey("id_Values")
                        .IsRequired();

                    b.HasOne("DAL.PRODUCTS_VARIANTS", "ProductsVariants")
                        .WithMany("VariantValues")
                        .HasForeignKey("id_Variant")
                        .IsRequired();

                    b.HasOne("DAL.PRODUCTS_OPTIONS", "ProductsOptions")
                        .WithMany("ProductOPtionses")
                        .HasForeignKey("id_Product", "id_Option")
                        .IsRequired();

                    b.Navigation("OptionsValues");

                    b.Navigation("ProductsOptions");

                    b.Navigation("ProductsVariants");
                });

            modelBuilder.Entity("DAL.OPTIONS", b =>
                {
                    b.Navigation("OptionsValueses");

                    b.Navigation("OptionsesProductses");
                });

            modelBuilder.Entity("DAL.OPTIONS_VALUES", b =>
                {
                    b.Navigation("OptionValueses");
                });

            modelBuilder.Entity("DAL.PRODUCTS", b =>
                {
                    b.Navigation("ProductsOptionses");

                    b.Navigation("ProductsVariantses");
                });

            modelBuilder.Entity("DAL.PRODUCTS_OPTIONS", b =>
                {
                    b.Navigation("ProductOPtionses");
                });

            modelBuilder.Entity("DAL.PRODUCTS_VARIANTS", b =>
                {
                    b.Navigation("VariantValues");
                });
#pragma warning restore 612, 618
        }
    }
}
