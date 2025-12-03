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
                name: "renters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LicenseNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    FullName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_renters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "vehicle_models",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DriveType = table.Column<int>(type: "integer", nullable: false),
                    SeatCount = table.Column<int>(type: "integer", nullable: false),
                    BodyType = table.Column<int>(type: "integer", nullable: false),
                    VehicleClass = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicle_models", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "model_generations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    EngineVolume = table.Column<double>(type: "numeric(3,1)", nullable: false),
                    Transmission = table.Column<int>(type: "integer", nullable: false),
                    RentalPricePerHour = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    VehicleModelId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_model_generations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_model_generations_vehicle_models_VehicleModelId",
                        column: x => x.VehicleModelId,
                        principalTable: "vehicle_models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "vehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LicensePlate = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Color = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    ModelGenerationId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_vehicles_model_generations_ModelGenerationId",
                        column: x => x.ModelGenerationId,
                        principalTable: "model_generations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "rentals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RentStartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DurationHours = table.Column<int>(type: "integer", nullable: false),
                    TotalCost = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    VehicleId = table.Column<Guid>(type: "uuid", nullable: false),
                    RenterId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rentals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_rentals_renters_RenterId",
                        column: x => x.RenterId,
                        principalTable: "renters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_rentals_vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_model_generations_VehicleModelId_Year",
                table: "model_generations",
                columns: new[] { "VehicleModelId", "Year" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_rentals_RenterId",
                table: "rentals",
                column: "RenterId");

            migrationBuilder.CreateIndex(
                name: "IX_rentals_RentStartTime",
                table: "rentals",
                column: "RentStartTime");

            migrationBuilder.CreateIndex(
                name: "IX_rentals_VehicleId",
                table: "rentals",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_renters_LicenseNumber",
                table: "renters",
                column: "LicenseNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_vehicle_models_Name",
                table: "vehicle_models",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_vehicles_LicensePlate",
                table: "vehicles",
                column: "LicensePlate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_vehicles_ModelGenerationId",
                table: "vehicles",
                column: "ModelGenerationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "rentals");

            migrationBuilder.DropTable(
                name: "renters");

            migrationBuilder.DropTable(
                name: "vehicles");

            migrationBuilder.DropTable(
                name: "model_generations");

            migrationBuilder.DropTable(
                name: "vehicle_models");
        }
    }
