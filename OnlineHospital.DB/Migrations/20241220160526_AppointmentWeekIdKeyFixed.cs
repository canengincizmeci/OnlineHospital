using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineHospital.DB.Migrations
{
    /// <inheritdoc />
    public partial class AppointmentWeekIdKeyFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OpenAppointmentSlots_WeekForAppointments_WeekIdAppointmentW~",
                table: "OpenAppointmentSlots");

            migrationBuilder.DropColumn(
                name: "AppointmentWeekId",
                table: "OpenAppointmentSlots");

            migrationBuilder.RenameColumn(
                name: "WeekIdAppointmentWeekId",
                table: "OpenAppointmentSlots",
                newName: "WeekForAppointmentId");

            migrationBuilder.RenameIndex(
                name: "IX_OpenAppointmentSlots_WeekIdAppointmentWeekId",
                table: "OpenAppointmentSlots",
                newName: "IX_OpenAppointmentSlots_WeekForAppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_OpenAppointmentSlots_WeekForAppointments_WeekForAppointment~",
                table: "OpenAppointmentSlots",
                column: "WeekForAppointmentId",
                principalTable: "WeekForAppointments",
                principalColumn: "AppointmentWeekId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OpenAppointmentSlots_WeekForAppointments_WeekForAppointment~",
                table: "OpenAppointmentSlots");

            migrationBuilder.RenameColumn(
                name: "WeekForAppointmentId",
                table: "OpenAppointmentSlots",
                newName: "WeekIdAppointmentWeekId");

            migrationBuilder.RenameIndex(
                name: "IX_OpenAppointmentSlots_WeekForAppointmentId",
                table: "OpenAppointmentSlots",
                newName: "IX_OpenAppointmentSlots_WeekIdAppointmentWeekId");

            migrationBuilder.AddColumn<int>(
                name: "AppointmentWeekId",
                table: "OpenAppointmentSlots",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_OpenAppointmentSlots_WeekForAppointments_WeekIdAppointmentW~",
                table: "OpenAppointmentSlots",
                column: "WeekIdAppointmentWeekId",
                principalTable: "WeekForAppointments",
                principalColumn: "AppointmentWeekId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
