using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineHospital.DB.Migrations
{
    /// <inheritdoc />
    public partial class DoctorProfileUpdatedAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsProfileUpdated",
                table: "Doctors",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsProfileUpdated",
                table: "Doctors");
        }
    }
}
