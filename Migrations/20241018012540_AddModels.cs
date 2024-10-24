﻿using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace prof_edna_teles_shop_api.Migrations
{
    /// <inheritdoc />
    public partial class AddModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "prof_category",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prof_category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "prof_user",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Password = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prof_user", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "prof_product",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    PriceInCents = table.Column<long>(type: "bigint", nullable: false),
                    CoverImage = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Images = table.Column<string[]>(type: "text[]", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    TotalPieces = table.Column<long>(type: "bigint", nullable: true),
                    TotalPlayers = table.Column<long>(type: "bigint", nullable: true),
                    TotalGames = table.Column<long>(type: "bigint", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prof_product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_prof_product_prof_product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "prof_product",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_prof_product_prof_user_UserId",
                        column: x => x.UserId,
                        principalTable: "prof_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "prof_category-product",
                columns: table => new
                {
                    CategoriesId = table.Column<long>(type: "bigint", nullable: false),
                    ProductsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prof_category-product", x => new { x.CategoriesId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_prof_category-product_prof_category_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "prof_category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_prof_category-product_prof_product_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "prof_product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_prof_category-product_ProductsId",
                table: "prof_category-product",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_prof_product_ProductId",
                table: "prof_product",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_prof_product_UserId",
                table: "prof_product",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "prof_category-product");

            migrationBuilder.DropTable(
                name: "prof_category");

            migrationBuilder.DropTable(
                name: "prof_product");

            migrationBuilder.DropTable(
                name: "prof_user");
        }
    }
}
