using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace prof_edna_teles_shop_api.Migrations
{
    /// <inheritdoc />
    public partial class AddNewPropertyToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GameType",
                table: "prof_product",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameType",
                table: "prof_product");
        }
    }
}
