using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCoreVueStarter.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventSet",
                columns: table => new
                {
                    Eventid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventName = table.Column<string>(maxLength: 50, nullable: false),
                    EventDate = table.Column<DateTime>(nullable: false),
                    Location = table.Column<string>(maxLength: 50, nullable: false),
                    Details = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventSet", x => x.Eventid);
                });

            migrationBuilder.CreateTable(
                name: "ParticipantSet",
                columns: table => new
                {
                    Participantid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    Familyname = table.Column<string>(maxLength: 50, nullable: true),
                    code = table.Column<string>(maxLength: 50, nullable: false),
                    NumParticipants = table.Column<int>(nullable: true),
                    Paymentmethod = table.Column<string>(nullable: false),
                    DetailsCompany = table.Column<string>(maxLength: 5000, nullable: true),
                    DetailsPerson = table.Column<string>(maxLength: 1500, nullable: true),
                    ParticipantType = table.Column<string>(nullable: false),
                    EventModelEventid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipantSet", x => x.Participantid);
                    table.ForeignKey(
                        name: "FK_ParticipantSet_EventSet_EventModelEventid",
                        column: x => x.EventModelEventid,
                        principalTable: "EventSet",
                        principalColumn: "Eventid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantSet_EventModelEventid",
                table: "ParticipantSet",
                column: "EventModelEventid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParticipantSet");

            migrationBuilder.DropTable(
                name: "EventSet");
        }
    }
}
