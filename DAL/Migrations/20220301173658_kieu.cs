using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class kieu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OPTIONS",
                columns: table => new
                {
                    id_Option = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    option_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    status_Delete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OPTIONS", x => x.id_Option);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCTS",
                columns: table => new
                {
                    id_Product = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    products_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    status_Delete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCTS", x => x.id_Product);
                });

            migrationBuilder.CreateTable(
                name: "OPTIONS_VALUES",
                columns: table => new
                {
                    id_Values = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_Option = table.Column<int>(type: "int", nullable: false),
                    option_Values = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status_Delete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OPTIONS_VALUES", x => x.id_Values);
                    table.ForeignKey(
                        name: "FK_OPTIONS_VALUES_OPTIONS_id_Option",
                        column: x => x.id_Option,
                        principalTable: "OPTIONS",
                        principalColumn: "id_Option");
                });

            migrationBuilder.CreateTable(
                name: "PRODUCTS_OPTIONS",
                columns: table => new
                {
                    id_Product = table.Column<int>(type: "int", nullable: false),
                    id_Option = table.Column<int>(type: "int", nullable: false),
                    status_Delete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCTS_OPTIONS", x => new { x.id_Product, x.id_Option });
                    table.ForeignKey(
                        name: "FK_PRODUCTS_OPTIONS_OPTIONS_id_Option",
                        column: x => x.id_Option,
                        principalTable: "OPTIONS",
                        principalColumn: "id_Option");
                    table.ForeignKey(
                        name: "FK_PRODUCTS_OPTIONS_PRODUCTS_id_Product",
                        column: x => x.id_Product,
                        principalTable: "PRODUCTS",
                        principalColumn: "id_Product");
                });

            migrationBuilder.CreateTable(
                name: "PRODUCTS_VARIANTS",
                columns: table => new
                {
                    id_Variant = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_Product = table.Column<int>(type: "int", nullable: false),
                    Products_Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    import_Price = table.Column<decimal>(type: "money", nullable: false),
                    price = table.Column<decimal>(type: "money", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    status_Delete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCTS_VARIANTS", x => x.id_Variant);
                    table.ForeignKey(
                        name: "FK_PRODUCTS_VARIANTS_PRODUCTS_id_Product",
                        column: x => x.id_Product,
                        principalTable: "PRODUCTS",
                        principalColumn: "id_Product");
                });

            migrationBuilder.CreateTable(
                name: "VARIANTS_VALUES",
                columns: table => new
                {
                    id_Product = table.Column<int>(type: "int", nullable: false),
                    id_Variant = table.Column<int>(type: "int", nullable: false),
                    id_Option = table.Column<int>(type: "int", nullable: false),
                    id_Values = table.Column<int>(type: "int", nullable: false),
                    status_Delete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VARIANTS_VALUES", x => new { x.id_Product, x.id_Variant, x.id_Option, x.id_Values });
                    table.ForeignKey(
                        name: "FK_VARIANTS_VALUES_OPTIONS_VALUES_id_Values",
                        column: x => x.id_Values,
                        principalTable: "OPTIONS_VALUES",
                        principalColumn: "id_Values");
                    table.ForeignKey(
                        name: "FK_VARIANTS_VALUES_PRODUCTS_OPTIONS_id_Product_id_Option",
                        columns: x => new { x.id_Product, x.id_Option },
                        principalTable: "PRODUCTS_OPTIONS",
                        principalColumns: new[] { "id_Product", "id_Option" });
                    table.ForeignKey(
                        name: "FK_VARIANTS_VALUES_PRODUCTS_VARIANTS_id_Variant",
                        column: x => x.id_Variant,
                        principalTable: "PRODUCTS_VARIANTS",
                        principalColumn: "id_Variant");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OPTIONS_VALUES_id_Option",
                table: "OPTIONS_VALUES",
                column: "id_Option");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCTS_OPTIONS_id_Option",
                table: "PRODUCTS_OPTIONS",
                column: "id_Option");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCTS_VARIANTS_id_Product",
                table: "PRODUCTS_VARIANTS",
                column: "id_Product");

            migrationBuilder.CreateIndex(
                name: "IX_VARIANTS_VALUES_id_Product_id_Option",
                table: "VARIANTS_VALUES",
                columns: new[] { "id_Product", "id_Option" });

            migrationBuilder.CreateIndex(
                name: "IX_VARIANTS_VALUES_id_Values",
                table: "VARIANTS_VALUES",
                column: "id_Values");

            migrationBuilder.CreateIndex(
                name: "IX_VARIANTS_VALUES_id_Variant",
                table: "VARIANTS_VALUES",
                column: "id_Variant");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VARIANTS_VALUES");

            migrationBuilder.DropTable(
                name: "OPTIONS_VALUES");

            migrationBuilder.DropTable(
                name: "PRODUCTS_OPTIONS");

            migrationBuilder.DropTable(
                name: "PRODUCTS_VARIANTS");

            migrationBuilder.DropTable(
                name: "OPTIONS");

            migrationBuilder.DropTable(
                name: "PRODUCTS");
        }
    }
}
