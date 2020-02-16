using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCoreVueStarter.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventModel",
                columns: table => new
                {
                    Eventid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventName = table.Column<string>(maxLength: 50, nullable: false),
                    EventDate = table.Column<DateTime>(nullable: false),
                    Location = table.Column<string>(maxLength: 50, nullable: false),
                    Details = table.Column<string>(maxLength: 1500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventModel", x => x.Eventid);
                });

            migrationBuilder.CreateTable(
                name: "ParticipantModel",
                columns: table => new
                {
                    Participantid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(maxLength: 50, nullable: false),
                    Familyname = table.Column<string>(maxLength: 50, nullable: true),
                    code = table.Column<string>(maxLength: 50, nullable: false),
                    NumParticipants = table.Column<int>(nullable: true),
                    Paymentmethod = table.Column<string>(nullable: false),
                    Details = table.Column<string>(maxLength: 5000, nullable: false),
                    ParticipantType = table.Column<string>(nullable: false),
                    Eventid = table.Column<int>(nullable: false),
                    EventModelEventid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipantModel", x => x.Participantid);
                    table.ForeignKey(
                        name: "FK_ParticipantModel_EventModel_EventModelEventid",
                        column: x => x.EventModelEventid,
                        principalTable: "EventModel",
                        principalColumn: "Eventid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantModel_EventModelEventid",
                table: "ParticipantModel",
                column: "EventModelEventid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParticipantModel");

            migrationBuilder.DropTable(
                name: "EventModel");
        }
    }
}
