using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SSMS.Infrustructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_DisplayOrder_To_ProductImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "ProductImages",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "ProductImages");
        }
    }
}
