using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StorageLocations",
                columns: table => new
                {
                    StorageLocationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    StorageLocationName = table.Column<string>(type: "TEXT", nullable: false),
                    SkiStorageCapacity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageLocations", x => x.StorageLocationId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    HomeAddress = table.Column<string>(type: "TEXT", nullable: false),
                    AddressCity = table.Column<string>(type: "TEXT", nullable: false),
                    AddressState = table.Column<string>(type: "TEXT", nullable: false),
                    AddressZipCode = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "CityLocations",
                columns: table => new
                {
                    CityLocationId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CityName = table.Column<string>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    CityDirectorId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityLocations", x => x.CityLocationId);
                    table.ForeignKey(
                        name: "FK_CityLocations_Users_CityDirectorId",
                        column: x => x.CityDirectorId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "SkiGears",
                columns: table => new
                {
                    SkiGearId = table.Column<Guid>(type: "TEXT", nullable: false),
                    GearName = table.Column<string>(type: "TEXT", nullable: false),
                    IsStoredOnSite = table.Column<bool>(type: "INTEGER", nullable: false),
                    GearOwnerId = table.Column<Guid>(type: "TEXT", nullable: false),
                    StorageLocationId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkiGears", x => x.SkiGearId);
                    table.ForeignKey(
                        name: "FK_SkiGears_StorageLocations_StorageLocationId",
                        column: x => x.StorageLocationId,
                        principalTable: "StorageLocations",
                        principalColumn: "StorageLocationId");
                    table.ForeignKey(
                        name: "FK_SkiGears_Users_GearOwnerId",
                        column: x => x.GearOwnerId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SkiTrips",
                columns: table => new
                {
                    SkiTripId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TripTitle = table.Column<string>(type: "TEXT", nullable: false),
                    IsPastTrip = table.Column<bool>(type: "INTEGER", nullable: false),
                    TripOriginCity = table.Column<string>(type: "TEXT", nullable: false),
                    TripOriginLocation = table.Column<string>(type: "TEXT", nullable: false),
                    TripDestinationLocation = table.Column<string>(type: "TEXT", nullable: false),
                    NumberOfSeats = table.Column<int>(type: "INTEGER", nullable: false),
                    TripOriginDepartureTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TripOriginArrivalTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TripDestinationDepartureTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TripDestinationArrivalTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsRoundTrip = table.Column<bool>(type: "INTEGER", nullable: false),
                    TotalMiles = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalCost = table.Column<double>(type: "REAL", nullable: false),
                    TripCoordinatorId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkiTrips", x => x.SkiTripId);
                    table.ForeignKey(
                        name: "FK_SkiTrips_Users_TripCoordinatorId",
                        column: x => x.TripCoordinatorId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SkiTripUser",
                columns: table => new
                {
                    AttendingTripsSkiTripId = table.Column<Guid>(type: "TEXT", nullable: false),
                    RegisteredUsersUserId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkiTripUser", x => new { x.AttendingTripsSkiTripId, x.RegisteredUsersUserId });
                    table.ForeignKey(
                        name: "FK_SkiTripUser_SkiTrips_AttendingTripsSkiTripId",
                        column: x => x.AttendingTripsSkiTripId,
                        principalTable: "SkiTrips",
                        principalColumn: "SkiTripId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkiTripUser_Users_RegisteredUsersUserId",
                        column: x => x.RegisteredUsersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CityLocations_CityDirectorId",
                table: "CityLocations",
                column: "CityDirectorId");

            migrationBuilder.CreateIndex(
                name: "IX_SkiGears_GearOwnerId",
                table: "SkiGears",
                column: "GearOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_SkiGears_StorageLocationId",
                table: "SkiGears",
                column: "StorageLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_SkiTrips_TripCoordinatorId",
                table: "SkiTrips",
                column: "TripCoordinatorId");

            migrationBuilder.CreateIndex(
                name: "IX_SkiTripUser_RegisteredUsersUserId",
                table: "SkiTripUser",
                column: "RegisteredUsersUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CityLocations");

            migrationBuilder.DropTable(
                name: "SkiGears");

            migrationBuilder.DropTable(
                name: "SkiTripUser");

            migrationBuilder.DropTable(
                name: "StorageLocations");

            migrationBuilder.DropTable(
                name: "SkiTrips");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
