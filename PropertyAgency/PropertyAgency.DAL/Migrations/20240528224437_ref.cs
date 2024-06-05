using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyAgency.DAL.Migrations
{
    /// <inheritdoc />
    public partial class @ref : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_Favorites_FavoriteId",
                table: "Favorites");

            migrationBuilder.DropIndex(
                name: "IX_Favorites_FavoriteId",
                table: "Favorites");

            migrationBuilder.DropColumn(
                name: "FavoriteId",
                table: "Favorites");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "FavoriteId",
                table: "Favorites",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_FavoriteId",
                table: "Favorites",
                column: "FavoriteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_Favorites_FavoriteId",
                table: "Favorites",
                column: "FavoriteId",
                principalTable: "Favorites",
                principalColumn: "Id");
        }
    }
}
