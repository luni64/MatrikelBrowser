using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MbCore.Migrations
{
    /// <inheritdoc />
    public partial class renames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageLink",
                table: "Pages",
                newName: "ImageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "Pages",
                newName: "ImageLink");
        }
    }
}
