using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class addindexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TripStops_TripID",
                table: "TripStops");

            migrationBuilder.DropIndex(
                name: "IX_TripSegmentPrices_TripID",
                table: "TripSegmentPrices");

            migrationBuilder.DropIndex(
                name: "IX_Trips_DepartureStationID",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_UserID",
                table: "Bookings");

            migrationBuilder.AlterColumn<string>(
                name: "TrainName",
                table: "Trains",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TripStops_TripID_StopSequence",
                table: "TripStops",
                columns: new[] { "TripID", "StopSequence" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TripSegmentPrices_TripID_ClassID",
                table: "TripSegmentPrices",
                columns: new[] { "TripID", "ClassID" });

            migrationBuilder.CreateIndex(
                name: "IX_Trips_Departure_Arrival",
                table: "Trips",
                columns: new[] { "DepartureStationID", "ArrivalStationID" });

            migrationBuilder.CreateIndex(
                name: "IX_Trains_TrainName",
                table: "Trains",
                column: "TrainName");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserID_TripID",
                table: "Bookings",
                columns: new[] { "UserID", "TripID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TripStops_TripID_StopSequence",
                table: "TripStops");

            migrationBuilder.DropIndex(
                name: "IX_TripSegmentPrices_TripID_ClassID",
                table: "TripSegmentPrices");

            migrationBuilder.DropIndex(
                name: "IX_Trips_Departure_Arrival",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trains_TrainName",
                table: "Trains");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_UserID_TripID",
                table: "Bookings");

            migrationBuilder.AlterColumn<string>(
                name: "TrainName",
                table: "Trains",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TripStops_TripID",
                table: "TripStops",
                column: "TripID");

            migrationBuilder.CreateIndex(
                name: "IX_TripSegmentPrices_TripID",
                table: "TripSegmentPrices",
                column: "TripID");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_DepartureStationID",
                table: "Trips",
                column: "DepartureStationID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserID",
                table: "Bookings",
                column: "UserID");
        }
    }
}
