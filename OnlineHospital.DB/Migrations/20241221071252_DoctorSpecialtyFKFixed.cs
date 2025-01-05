using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineHospital.DB.Migrations
{
    /// <inheritdoc />
    public partial class DoctorSpecialtyFKFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_DoctorSpecialties_MedicalSpecialtyId",
                table: "Doctors");

            migrationBuilder.RenameColumn(
                name: "MedicalSpecialtyId",
                table: "Doctors",
                newName: "DoctorSpecialtyId");

            migrationBuilder.RenameIndex(
                name: "IX_Doctors_MedicalSpecialtyId",
                table: "Doctors",
                newName: "IX_Doctors_DoctorSpecialtyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_DoctorSpecialties_DoctorSpecialtyId",
                table: "Doctors",
                column: "DoctorSpecialtyId",
                principalTable: "DoctorSpecialties",
                principalColumn: "SpecialtyId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_DoctorSpecialties_DoctorSpecialtyId",
                table: "Doctors");

            migrationBuilder.RenameColumn(
                name: "DoctorSpecialtyId",
                table: "Doctors",
                newName: "MedicalSpecialtyId");

            migrationBuilder.RenameIndex(
                name: "IX_Doctors_DoctorSpecialtyId",
                table: "Doctors",
                newName: "IX_Doctors_MedicalSpecialtyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_DoctorSpecialties_MedicalSpecialtyId",
                table: "Doctors",
                column: "MedicalSpecialtyId",
                principalTable: "DoctorSpecialties",
                principalColumn: "SpecialtyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
