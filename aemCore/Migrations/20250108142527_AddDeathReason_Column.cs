using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AEM.Migrations
{
    /// <inheritdoc />
    public partial class AddDeathReason_Column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "Events",
                newName: "Misc");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Misc",
                table: "Events",
                newName: "Notes");
        }
    }
}
