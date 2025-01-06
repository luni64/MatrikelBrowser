using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AEM.Migrations
{
    /// <inheritdoc />
    public partial class Restructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Persons_BrideFatherId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Persons_BrideId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Persons_BrideMotherId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Persons_GroomFatherId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Persons_GroomId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Persons_GroomMotherId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Persons_WitnessesId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Events_BrideFatherId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_BrideId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_BrideMotherId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_GroomFatherId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_GroomId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_GroomMotherId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_WitnessesId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "BrideFatherId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "BrideId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "BrideMotherId",
                table: "Events");

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
                name: "Discriminator",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Father",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "GodParent",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "GroomFatherId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "GroomId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "GroomMotherId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Mother",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "WitnessesId",
                table: "Events");

            migrationBuilder.AddColumn<string>(
                name: "Date1",
                table: "Events",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Date2",
                table: "Events",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Date3",
                table: "Events",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Date4",
                table: "Events",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Occupation1",
                table: "Events",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Occupation2",
                table: "Events",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Occupation3",
                table: "Events",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Person1",
                table: "Events",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Person2",
                table: "Events",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Person3",
                table: "Events",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Person4",
                table: "Events",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Person5",
                table: "Events",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Person6",
                table: "Events",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Person7",
                table: "Events",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date1",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Date2",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Date3",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Date4",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Occupation1",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Occupation2",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Occupation3",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Person1",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Person2",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Person3",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Person4",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Person5",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Person6",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Person7",
                table: "Events");

            migrationBuilder.AddColumn<int>(
                name: "BrideFatherId",
                table: "Events",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BrideId",
                table: "Events",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BrideMotherId",
                table: "Events",
                type: "INTEGER",
                nullable: true);

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
                name: "Discriminator",
                table: "Events",
                type: "TEXT",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.AddColumn<int>(
                name: "GroomFatherId",
                table: "Events",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GroomId",
                table: "Events",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GroomMotherId",
                table: "Events",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mother",
                table: "Events",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WitnessesId",
                table: "Events",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BaptismDate = table.Column<string>(type: "TEXT", nullable: false),
                    BirthDate = table.Column<string>(type: "TEXT", nullable: false),
                    DeathDate = table.Column<string>(type: "TEXT", nullable: false),
                    Living = table.Column<bool>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Occupation = table.Column<string>(type: "TEXT", nullable: false),
                    State = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_BrideFatherId",
                table: "Events",
                column: "BrideFatherId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_BrideId",
                table: "Events",
                column: "BrideId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_BrideMotherId",
                table: "Events",
                column: "BrideMotherId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_GroomFatherId",
                table: "Events",
                column: "GroomFatherId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_GroomId",
                table: "Events",
                column: "GroomId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_GroomMotherId",
                table: "Events",
                column: "GroomMotherId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_WitnessesId",
                table: "Events",
                column: "WitnessesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Persons_BrideFatherId",
                table: "Events",
                column: "BrideFatherId",
                principalTable: "Persons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Persons_BrideId",
                table: "Events",
                column: "BrideId",
                principalTable: "Persons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Persons_BrideMotherId",
                table: "Events",
                column: "BrideMotherId",
                principalTable: "Persons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Persons_GroomFatherId",
                table: "Events",
                column: "GroomFatherId",
                principalTable: "Persons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Persons_GroomId",
                table: "Events",
                column: "GroomId",
                principalTable: "Persons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Persons_GroomMotherId",
                table: "Events",
                column: "GroomMotherId",
                principalTable: "Persons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Persons_WitnessesId",
                table: "Events",
                column: "WitnessesId",
                principalTable: "Persons",
                principalColumn: "Id");
        }
    }
}
