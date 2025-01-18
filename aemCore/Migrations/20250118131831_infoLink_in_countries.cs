using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AEM.Migrations
{
    /// <inheritdoc />
    public partial class infoLink_in_countries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InfoLink",
                table: "Countries",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InfoLink",
                table: "Countries");
        }
    }
}
