using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MbCore.Migrations
{
    /// <inheritdoc />
    public partial class Book_start_and_end_dates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Books",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Books",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Books");
        }
    }
}
