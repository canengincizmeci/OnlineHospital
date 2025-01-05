using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineHospital.DB.Migrations
{
    /// <inheritdoc />
    public partial class FixFKForOppo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreatedAppointments_OpenAppointmentSlots_OpendAppointmentSl~",
                table: "CreatedAppointments");

            migrationBuilder.DropIndex(
                name: "IX_CreatedAppointments_OpendAppointmentSlotAppointmentId",
                table: "CreatedAppointments");

            migrationBuilder.DropColumn(
                name: "OpendAppointmentSlotAppointmentId",
                table: "CreatedAppointments");

            migrationBuilder.RenameColumn(
                name: "OpenedAppointmentId",
                table: "CreatedAppointments",
                newName: "OpenAppointmentSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedAppointments_OpenAppointmentSlotId",
                table: "CreatedAppointments",
                column: "OpenAppointmentSlotId");

            migrationBuilder.AddForeignKey(
                name: "FK_CreatedAppointments_OpenAppointmentSlots_OpenAppointmentSlo~",
                table: "CreatedAppointments",
                column: "OpenAppointmentSlotId",
                principalTable: "OpenAppointmentSlots",
                principalColumn: "AppointmentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CreatedAppointments_OpenAppointmentSlots_OpenAppointmentSlo~",
                table: "CreatedAppointments");

            migrationBuilder.DropIndex(
                name: "IX_CreatedAppointments_OpenAppointmentSlotId",
                table: "CreatedAppointments");

            migrationBuilder.RenameColumn(
                name: "OpenAppointmentSlotId",
                table: "CreatedAppointments",
                newName: "OpenedAppointmentId");

            migrationBuilder.AddColumn<int>(
                name: "OpendAppointmentSlotAppointmentId",
                table: "CreatedAppointments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CreatedAppointments_OpendAppointmentSlotAppointmentId",
                table: "CreatedAppointments",
                column: "OpendAppointmentSlotAppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CreatedAppointments_OpenAppointmentSlots_OpendAppointmentSl~",
                table: "CreatedAppointments",
                column: "OpendAppointmentSlotAppointmentId",
                principalTable: "OpenAppointmentSlots",
                principalColumn: "AppointmentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
