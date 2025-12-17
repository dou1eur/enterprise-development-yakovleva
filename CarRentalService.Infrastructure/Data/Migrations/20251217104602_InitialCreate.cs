using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalService.Infrastructure.Data.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Renters",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                LicenseNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                FullName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Renters", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "VehicleModels",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                DriveType = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                SeatCount = table.Column<int>(type: "integer", nullable: false),
                BodyType = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                VehicleClass = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_VehicleModels", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "ModelGenerations",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                Year = table.Column<int>(type: "integer", nullable: false),
                EngineVolume = table.Column<double>(type: "double precision", precision: 3, scale: 1, nullable: false),
                Transmission = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                RentalPricePerHour = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                VehicleModelId = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ModelGenerations", x => x.Id);
                table.ForeignKey(
                    name: "FK_ModelGenerations_VehicleModels_VehicleModelId",
                    column: x => x.VehicleModelId,
                    principalTable: "VehicleModels",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Vehicles",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                LicensePlate = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                Color = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                GenerationId = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Vehicles", x => x.Id);
                table.ForeignKey(
                    name: "FK_Vehicles_ModelGenerations_GenerationId",
                    column: x => x.GenerationId,
                    principalTable: "ModelGenerations",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Rentals",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                RentStartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                DurationHours = table.Column<int>(type: "integer", nullable: false),
                VehicleId = table.Column<Guid>(type: "uuid", nullable: false),
                RenterId = table.Column<Guid>(type: "uuid", nullable: false),
                TotalCost = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Rentals", x => x.Id);
                table.ForeignKey(
                    name: "FK_Rentals_Renters_RenterId",
                    column: x => x.RenterId,
                    principalTable: "Renters",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_Rentals_Vehicles_VehicleId",
                    column: x => x.VehicleId,
                    principalTable: "Vehicles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateIndex(
            name: "IX_ModelGenerations_VehicleModelId",
            table: "ModelGenerations",
            column: "VehicleModelId");

        migrationBuilder.CreateIndex(
            name: "IX_Rentals_RenterId",
            table: "Rentals",
            column: "RenterId");

        migrationBuilder.CreateIndex(
            name: "IX_Rentals_VehicleId",
            table: "Rentals",
            column: "VehicleId");

        migrationBuilder.CreateIndex(
            name: "IX_Renters_LicenseNumber",
            table: "Renters",
            column: "LicenseNumber",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Vehicles_GenerationId",
            table: "Vehicles",
            column: "GenerationId");

        migrationBuilder.CreateIndex(
            name: "IX_Vehicles_LicensePlate",
            table: "Vehicles",
            column: "LicensePlate",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Rentals");

        migrationBuilder.DropTable(
            name: "Renters");

        migrationBuilder.DropTable(
            name: "Vehicles");

        migrationBuilder.DropTable(
            name: "ModelGenerations");

        migrationBuilder.DropTable(
            name: "VehicleModels");
    }
}
