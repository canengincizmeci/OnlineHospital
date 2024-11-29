using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace OnlineHospital.DB.Migrations
{
    /// <inheritdoc />
    public partial class createdAppointmentTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_DoctorSpecialty_SpecialtyId",
                table: "Doctors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorSpecialty",
                table: "DoctorSpecialty");

            migrationBuilder.RenameTable(
                name: "DoctorSpecialty",
                newName: "DoctorSpecialties");

            migrationBuilder.AddColumn<bool>(
                name: "ActivityStatus",
                table: "Doctors",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthYear",
                table: "Doctors",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DoctorName",
                table: "Doctors",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DoctorUserName",
                table: "Doctors",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorSpecialties",
                table: "DoctorSpecialties",
                column: "SpecialtyId");

            migrationBuilder.CreateTable(
                name: "Administrators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AdminId = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Administrators_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OpenAppointmentSlots",
                columns: table => new
                {
                    AppointmentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DoctorId = table.Column<int>(type: "integer", nullable: false),
                    AppointmentDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenAppointmentSlots", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_OpenAppointmentSlots_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientRelationsWorker",
                columns: table => new
                {
                    WorkerId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    WorkerName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    WorkerUserName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    BirthYear = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ActivityStatus = table.Column<bool>(type: "boolean", nullable: false),
                    Availability = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientRelationsWorker", x => x.WorkerId);
                    table.ForeignKey(
                        name: "FK_PatientRelationsWorker_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatinetId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BirthYear = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ActivityStatus = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatinetId);
                    table.ForeignKey(
                        name: "FK_Patients_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CreatedAppointments",
                columns: table => new
                {
                    CreatedAppointmentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PatientId = table.Column<int>(type: "integer", nullable: false),
                    OpenedAppointmentId = table.Column<int>(type: "integer", nullable: false),
                    OpendAppointmentSlotAppointmentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreatedAppointments", x => x.CreatedAppointmentId);
                    table.ForeignKey(
                        name: "FK_CreatedAppointments_OpenAppointmentSlots_OpendAppointmentSl~",
                        column: x => x.OpendAppointmentSlotAppointmentId,
                        principalTable: "OpenAppointmentSlots",
                        principalColumn: "AppointmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CreatedAppointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatinetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompletedAppointments",
                columns: table => new
                {
                    CompletedAppointmentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Results = table.Column<string>(type: "text", nullable: false),
                    CompletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAppointmentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedAppointments", x => x.CompletedAppointmentId);
                    table.ForeignKey(
                        name: "FK_CompletedAppointments_CreatedAppointments_CreatedAppointmen~",
                        column: x => x.CreatedAppointmentId,
                        principalTable: "CreatedAppointments",
                        principalColumn: "CreatedAppointmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Administrators_UserId",
                table: "Administrators",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedAppointments_CreatedAppointmentId",
                table: "CompletedAppointments",
                column: "CreatedAppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedAppointments_OpendAppointmentSlotAppointmentId",
                table: "CreatedAppointments",
                column: "OpendAppointmentSlotAppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CreatedAppointments_PatientId",
                table: "CreatedAppointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenAppointmentSlots_UserId",
                table: "OpenAppointmentSlots",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientRelationsWorker_UserId",
                table: "PatientRelationsWorker",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_UserId",
                table: "Patients",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_DoctorSpecialties_SpecialtyId",
                table: "Doctors",
                column: "SpecialtyId",
                principalTable: "DoctorSpecialties",
                principalColumn: "SpecialtyId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_DoctorSpecialties_SpecialtyId",
                table: "Doctors");

            migrationBuilder.DropTable(
                name: "Administrators");

            migrationBuilder.DropTable(
                name: "CompletedAppointments");

            migrationBuilder.DropTable(
                name: "PatientRelationsWorker");

            migrationBuilder.DropTable(
                name: "CreatedAppointments");

            migrationBuilder.DropTable(
                name: "OpenAppointmentSlots");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorSpecialties",
                table: "DoctorSpecialties");

            migrationBuilder.DropColumn(
                name: "ActivityStatus",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "BirthYear",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "DoctorName",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "DoctorUserName",
                table: "Doctors");

            migrationBuilder.RenameTable(
                name: "DoctorSpecialties",
                newName: "DoctorSpecialty");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorSpecialty",
                table: "DoctorSpecialty",
                column: "SpecialtyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_DoctorSpecialty_SpecialtyId",
                table: "Doctors",
                column: "SpecialtyId",
                principalTable: "DoctorSpecialty",
                principalColumn: "SpecialtyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
