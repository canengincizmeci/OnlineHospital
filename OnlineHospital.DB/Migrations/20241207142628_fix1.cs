using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineHospital.DB.Migrations
{
    /// <inheritdoc />
    public partial class fix1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_DoctorSpecialties_SpecialtyId",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_SpecialtyId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "SpecialtyId",
                table: "Doctors");

            migrationBuilder.AlterColumn<int>(
                name: "MedicalSpecialtyId",
                table: "Doctors",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_MedicalSpecialtyId",
                table: "Doctors",
                column: "MedicalSpecialtyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_DoctorSpecialties_MedicalSpecialtyId",
                table: "Doctors",
                column: "MedicalSpecialtyId",
                principalTable: "DoctorSpecialties",
                principalColumn: "SpecialtyId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_DoctorSpecialties_MedicalSpecialtyId",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_MedicalSpecialtyId",
                table: "Doctors");

            migrationBuilder.AlterColumn<int>(
                name: "MedicalSpecialtyId",
                table: "Doctors",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "SpecialtyId",
                table: "Doctors",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_SpecialtyId",
                table: "Doctors",
                column: "SpecialtyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_DoctorSpecialties_SpecialtyId",
                table: "Doctors",
                column: "SpecialtyId",
                principalTable: "DoctorSpecialties",
                principalColumn: "SpecialtyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
