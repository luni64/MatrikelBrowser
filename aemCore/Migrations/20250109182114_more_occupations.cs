using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AEM.Migrations
{
    /// <inheritdoc />
    public partial class more_occupations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Occupation4",
                table: "Events",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Occupation5",
                table: "Events",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Occupation4",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Occupation5",
                table: "Events");
        }
    }
}
