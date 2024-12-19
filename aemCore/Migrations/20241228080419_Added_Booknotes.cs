using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MbCore.Migrations
{
    /// <inheritdoc />
    public partial class Added_Booknotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Books",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "Books");
        }
    }
}
