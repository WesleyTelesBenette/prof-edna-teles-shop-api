using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace prof_edna_teles_shop_api.Migrations
{
    /// <inheritdoc />
    public partial class RemovePropertyToCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "prof_category");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "prof_category",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
