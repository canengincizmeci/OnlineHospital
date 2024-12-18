using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace OnlineHospital.DB.Migrations
{
    /// <inheritdoc />
    public partial class AddedWeekForAppointmentsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppointmentWeekId",
                table: "OpenAppointmentSlots",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WeekIdAppointmentWeekId",
                table: "OpenAppointmentSlots",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "WeekForAppointments",
                columns: table => new
                {
                    AppointmentWeekId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WeekFirstDay = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeekForAppointments", x => x.AppointmentWeekId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OpenAppointmentSlots_WeekIdAppointmentWeekId",
                table: "OpenAppointmentSlots",
                column: "WeekIdAppointmentWeekId");

            migrationBuilder.AddForeignKey(
                name: "FK_OpenAppointmentSlots_WeekForAppointments_WeekIdAppointmentW~",
                table: "OpenAppointmentSlots",
                column: "WeekIdAppointmentWeekId",
                principalTable: "WeekForAppointments",
                principalColumn: "AppointmentWeekId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OpenAppointmentSlots_WeekForAppointments_WeekIdAppointmentW~",
                table: "OpenAppointmentSlots");

            migrationBuilder.DropTable(
                name: "WeekForAppointments");

            migrationBuilder.DropIndex(
                name: "IX_OpenAppointmentSlots_WeekIdAppointmentWeekId",
                table: "OpenAppointmentSlots");

            migrationBuilder.DropColumn(
                name: "AppointmentWeekId",
                table: "OpenAppointmentSlots");

            migrationBuilder.DropColumn(
                name: "WeekIdAppointmentWeekId",
                table: "OpenAppointmentSlots");
        }
    }
}
