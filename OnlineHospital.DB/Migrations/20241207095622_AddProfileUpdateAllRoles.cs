using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineHospital.DB.Migrations
{
    /// <inheritdoc />
    public partial class AddProfileUpdateAllRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthYear",
                table: "Patients",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<bool>(
                name: "IsProfileUpdated",
                table: "Patients",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsProfileUpdated",
                table: "PatientRelationsWorker",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsProfileUpdated",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "IsProfileUpdated",
                table: "PatientRelationsWorker");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthYear",
                table: "Patients",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);
        }
    }
}
