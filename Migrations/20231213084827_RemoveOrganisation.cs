using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace grc_copie.Migrations
{
    public partial class RemoveOrganisation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Organisation_OrganisationId",
                table: "Jobs");

            migrationBuilder.DropTable(
                name: "AppUserOrganisation");

            migrationBuilder.DropTable(
                name: "AppUser");

            migrationBuilder.DropTable(
                name: "Organisation");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_OrganisationId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "OrganisationId",
                table: "Jobs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganisationId",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AppUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organisation",
                columns: table => new
                {
                    OrganisationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganisationName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organisation", x => x.OrganisationId);
                });

            migrationBuilder.CreateTable(
                name: "AppUserOrganisation",
                columns: table => new
                {
                    AppUserListId = table.Column<int>(type: "int", nullable: false),
                    OrganisationListeOrganisationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserOrganisation", x => new { x.AppUserListId, x.OrganisationListeOrganisationId });
                    table.ForeignKey(
                        name: "FK_AppUserOrganisation_AppUser_AppUserListId",
                        column: x => x.AppUserListId,
                        principalTable: "AppUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserOrganisation_Organisation_OrganisationListeOrganisationId",
                        column: x => x.OrganisationListeOrganisationId,
                        principalTable: "Organisation",
                        principalColumn: "OrganisationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_OrganisationId",
                table: "Jobs",
                column: "OrganisationId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserOrganisation_OrganisationListeOrganisationId",
                table: "AppUserOrganisation",
                column: "OrganisationListeOrganisationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Organisation_OrganisationId",
                table: "Jobs",
                column: "OrganisationId",
                principalTable: "Organisation",
                principalColumn: "OrganisationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
