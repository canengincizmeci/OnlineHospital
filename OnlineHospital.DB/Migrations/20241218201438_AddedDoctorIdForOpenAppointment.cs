using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineHospital.DB.Migrations
{
    /// <inheritdoc />
    public partial class AddedDoctorIdForOpenAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_OpenAppointmentSlots_DoctorId",
                table: "OpenAppointmentSlots",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_OpenAppointmentSlots_Doctors_DoctorId",
                table: "OpenAppointmentSlots",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OpenAppointmentSlots_Doctors_DoctorId",
                table: "OpenAppointmentSlots");

            migrationBuilder.DropIndex(
                name: "IX_OpenAppointmentSlots_DoctorId",
                table: "OpenAppointmentSlots");
        }
    }
}
