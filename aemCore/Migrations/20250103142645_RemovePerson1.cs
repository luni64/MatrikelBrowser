using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AEM.Migrations
{
    /// <inheritdoc />
    public partial class RemovePerson1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Findings_Persons_ChildId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Findings_Persons_FatherId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Findings_Persons_GodParentId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Findings_Persons_MotherId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Findings_ChildId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Findings_FatherId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Findings_GodParentId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Findings_MotherId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ChildId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "FatherId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "GodParentId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "MotherId",
                table: "Events");

            migrationBuilder.AddColumn<string>(
                name: "ChildBaptizeDay",
                table: "Events",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChildBirthday",
                table: "Events",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChildName",
                table: "Events",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Father",
                table: "Events",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GodParent",
                table: "Events",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mother",
                table: "Events",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChildBaptizeDay",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ChildBirthday",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ChildName",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Father",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "GodParent",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Mother",
                table: "Events");

            migrationBuilder.AddColumn<int>(
                name: "ChildId",
                table: "Events",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FatherId",
                table: "Events",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GodParentId",
                table: "Events",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MotherId",
                table: "Events",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Findings_ChildId",
                table: "Events",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_Findings_FatherId",
                table: "Events",
                column: "FatherId");

            migrationBuilder.CreateIndex(
                name: "IX_Findings_GodParentId",
                table: "Events",
                column: "GodParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Findings_MotherId",
                table: "Events",
                column: "MotherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Findings_Persons_ChildId",
                table: "Events",
                column: "ChildId",
                principalTable: "Persons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Findings_Persons_FatherId",
                table: "Events",
                column: "FatherId",
                principalTable: "Persons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Findings_Persons_GodParentId",
                table: "Events",
                column: "GodParentId",
                principalTable: "Persons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Findings_Persons_MotherId",
                table: "Events",
                column: "MotherId",
                principalTable: "Persons",
                principalColumn: "Id");
        }
    }
}
