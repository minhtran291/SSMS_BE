using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SSMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Modify_ProductImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductImages",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "ProductImages");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProductImages",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "ProductImages",
                type: "varchar(1024)",
                unicode: false,
                maxLength: 1024,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StorageKey",
                table: "ProductImages",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductImages",
                table: "ProductImages",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_StorageKey",
                table: "ProductImages",
                column: "StorageKey",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductImages",
                table: "ProductImages");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_StorageKey",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "StorageKey",
                table: "ProductImages");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "ProductImages",
                type: "nvarchar(512)",
                maxLength: 512,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductImages",
                table: "ProductImages",
                columns: new[] { "ProductId", "Image" });
        }
    }
}
