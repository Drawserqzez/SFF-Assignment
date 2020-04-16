using Microsoft.EntityFrameworkCore.Migrations;

namespace SFF.Domain.Migrations
{
    public partial class AddedStudios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Borrowed",
                table: "Movies",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "StudioID",
                table: "Movies",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Studios",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studios", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_StudioID",
                table: "Movies",
                column: "StudioID");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Studios_StudioID",
                table: "Movies",
                column: "StudioID",
                principalTable: "Studios",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Studios_StudioID",
                table: "Movies");

            migrationBuilder.DropTable(
                name: "Studios");

            migrationBuilder.DropIndex(
                name: "IX_Movies_StudioID",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Borrowed",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "StudioID",
                table: "Movies");
        }
    }
}
