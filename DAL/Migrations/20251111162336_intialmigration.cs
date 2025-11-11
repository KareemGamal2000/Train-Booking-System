using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class intialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProfilePicture = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    LastLoginDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", maxLength: 10, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Class_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassNameEN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassNameAR = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Class_ID);
                });

            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    StationID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StationNameEN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StationNameAR = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StationCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.StationID);
                });

            migrationBuilder.CreateTable(
                name: "Trains",
                columns: table => new
                {
                    Train_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trains", x => x.Train_ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Coaches",
                columns: table => new
                {
                    Coach_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalSeats = table.Column<int>(type: "int", nullable: false),
                    IsSeatless = table.Column<bool>(type: "bit", nullable: false),
                    CoachType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coaches", x => x.Coach_ID);
                    table.ForeignKey(
                        name: "FK_Coaches_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Class_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    Trip_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainID = table.Column<long>(type: "bigint", nullable: false),
                    DepartureStationID = table.Column<long>(type: "bigint", nullable: false),
                    ArrivalStationID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Trip_ID);
                    table.ForeignKey(
                        name: "FK_Trips_Stations_ArrivalStationID",
                        column: x => x.ArrivalStationID,
                        principalTable: "Stations",
                        principalColumn: "StationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trips_Stations_DepartureStationID",
                        column: x => x.DepartureStationID,
                        principalTable: "Stations",
                        principalColumn: "StationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trips_Trains_TrainID",
                        column: x => x.TrainID,
                        principalTable: "Trains",
                        principalColumn: "Train_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    Seat_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeatNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoachID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.Seat_ID);
                    table.ForeignKey(
                        name: "FK_Seats_Coaches_CoachID",
                        column: x => x.CoachID,
                        principalTable: "Coaches",
                        principalColumn: "Coach_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainCoach",
                columns: table => new
                {
                    TrainCoach_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoachID = table.Column<long>(type: "bigint", nullable: false),
                    TrainID = table.Column<long>(type: "bigint", nullable: false),
                    CoachNumber = table.Column<int>(type: "int", nullable: false),
                    AvailableSeats = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainCoach", x => x.TrainCoach_ID);
                    table.ForeignKey(
                        name: "FK_TrainCoach_Coaches_CoachID",
                        column: x => x.CoachID,
                        principalTable: "Coaches",
                        principalColumn: "Coach_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainCoach_Trains_TrainID",
                        column: x => x.TrainID,
                        principalTable: "Trains",
                        principalColumn: "Train_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TripStop",
                columns: table => new
                {
                    TripStopID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TripID = table.Column<int>(type: "int", nullable: false),
                    StationID = table.Column<long>(type: "bigint", nullable: false),
                    StopSequence = table.Column<int>(type: "int", nullable: false),
                    ArrivalTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    DepartureTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    DistanceFromStartKM = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripStop", x => x.TripStopID);
                    table.ForeignKey(
                        name: "FK_TripStop_Stations_StationID",
                        column: x => x.StationID,
                        principalTable: "Stations",
                        principalColumn: "StationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TripStop_Trips_TripID",
                        column: x => x.TripID,
                        principalTable: "Trips",
                        principalColumn: "Trip_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Booking_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookingStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TripID = table.Column<int>(type: "int", nullable: false),
                    DepartureStopID = table.Column<int>(type: "int", nullable: false),
                    ArrivalStopID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Booking_ID);
                    table.ForeignKey(
                        name: "FK_Bookings_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_TripStop_ArrivalStopID",
                        column: x => x.ArrivalStopID,
                        principalTable: "TripStop",
                        principalColumn: "TripStopID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_TripStop_DepartureStopID",
                        column: x => x.DepartureStopID,
                        principalTable: "TripStop",
                        principalColumn: "TripStopID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Trips_TripID",
                        column: x => x.TripID,
                        principalTable: "Trips",
                        principalColumn: "Trip_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TripSegmentPrice",
                columns: table => new
                {
                    SegmentPriceID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TripID = table.Column<int>(type: "int", nullable: false),
                    StartStopID = table.Column<int>(type: "int", nullable: false),
                    EndStopID = table.Column<int>(type: "int", nullable: false),
                    ClassID = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripSegmentPrice", x => x.SegmentPriceID);
                    table.ForeignKey(
                        name: "FK_TripSegmentPrice_Classes_ClassID",
                        column: x => x.ClassID,
                        principalTable: "Classes",
                        principalColumn: "Class_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TripSegmentPrice_TripStop_EndStopID",
                        column: x => x.EndStopID,
                        principalTable: "TripStop",
                        principalColumn: "TripStopID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TripSegmentPrice_TripStop_StartStopID",
                        column: x => x.StartStopID,
                        principalTable: "TripStop",
                        principalColumn: "TripStopID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TripSegmentPrice_Trips_TripID",
                        column: x => x.TripID,
                        principalTable: "Trips",
                        principalColumn: "Trip_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Ticket_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TicketReference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookingID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeatID = table.Column<long>(type: "bigint", nullable: false),
                    ClassID = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Ticket_ID);
                    table.ForeignKey(
                        name: "FK_Tickets_Bookings_BookingID",
                        column: x => x.BookingID,
                        principalTable: "Bookings",
                        principalColumn: "Booking_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_Classes_ClassID",
                        column: x => x.ClassID,
                        principalTable: "Classes",
                        principalColumn: "Class_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_Seats_SeatID",
                        column: x => x.SeatID,
                        principalTable: "Seats",
                        principalColumn: "Seat_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ArrivalStopID",
                table: "Bookings",
                column: "ArrivalStopID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_DepartureStopID",
                table: "Bookings",
                column: "DepartureStopID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TripID",
                table: "Bookings",
                column: "TripID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserID",
                table: "Bookings",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Coaches_ClassId",
                table: "Coaches",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_CoachID",
                table: "Seats",
                column: "CoachID");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_BookingID",
                table: "Tickets",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ClassID",
                table: "Tickets",
                column: "ClassID");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_SeatID",
                table: "Tickets",
                column: "SeatID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainCoach_CoachID",
                table: "TrainCoach",
                column: "CoachID");

            migrationBuilder.CreateIndex(
                name: "IX_TrainCoach_TrainID",
                table: "TrainCoach",
                column: "TrainID");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_ArrivalStationID",
                table: "Trips",
                column: "ArrivalStationID");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_DepartureStationID",
                table: "Trips",
                column: "DepartureStationID");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_TrainID",
                table: "Trips",
                column: "TrainID");

            migrationBuilder.CreateIndex(
                name: "IX_TripSegmentPrice_ClassID",
                table: "TripSegmentPrice",
                column: "ClassID");

            migrationBuilder.CreateIndex(
                name: "IX_TripSegmentPrice_EndStopID",
                table: "TripSegmentPrice",
                column: "EndStopID");

            migrationBuilder.CreateIndex(
                name: "IX_TripSegmentPrice_StartStopID",
                table: "TripSegmentPrice",
                column: "StartStopID");

            migrationBuilder.CreateIndex(
                name: "IX_TripSegmentPrice_TripID",
                table: "TripSegmentPrice",
                column: "TripID");

            migrationBuilder.CreateIndex(
                name: "IX_TripStop_StationID",
                table: "TripStop",
                column: "StationID");

            migrationBuilder.CreateIndex(
                name: "IX_TripStop_TripID",
                table: "TripStop",
                column: "TripID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "TrainCoach");

            migrationBuilder.DropTable(
                name: "TripSegmentPrice");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "TripStop");

            migrationBuilder.DropTable(
                name: "Coaches");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Stations");

            migrationBuilder.DropTable(
                name: "Trains");
        }
    }
}
