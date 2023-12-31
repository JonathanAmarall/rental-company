﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalCompany.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitMigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Borrowers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Street = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    PostalCode = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    City = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Number = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Borrowers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Initials = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ParentId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_Locations_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CollectionItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Autor = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    Edition = table.Column<string>(type: "text", nullable: true),
                    ItemType = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LocationId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollectionItems_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RentItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RentedQuantity = table.Column<int>(type: "integer", nullable: false),
                    QuantityReturned = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    RentDueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CollectionItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    BorrowerId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentItem_Borrowers_BorrowerId",
                        column: x => x.BorrowerId,
                        principalTable: "Borrowers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RentItem_CollectionItems_CollectionItemId",
                        column: x => x.CollectionItemId,
                        principalTable: "CollectionItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollectionItems_LocationId",
                table: "CollectionItems",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_ParentId",
                table: "Locations",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_RentItem_BorrowerId",
                table: "RentItem",
                column: "BorrowerId");

            migrationBuilder.CreateIndex(
                name: "IX_RentItem_CollectionItemId",
                table: "RentItem",
                column: "CollectionItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RentItem");

            migrationBuilder.DropTable(
                name: "Borrowers");

            migrationBuilder.DropTable(
                name: "CollectionItems");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
