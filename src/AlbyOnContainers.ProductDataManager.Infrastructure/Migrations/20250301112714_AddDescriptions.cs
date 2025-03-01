﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AlbyOnContainers.ProductDataManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DescriptionTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DescriptionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryDescriptionType",
                columns: table => new
                {
                    CategoriesId = table.Column<Guid>(type: "uuid", nullable: false),
                    DescriptionTypesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryDescriptionType", x => new { x.CategoriesId, x.DescriptionTypesId });
                    table.ForeignKey(
                        name: "FK_CategoryDescriptionType_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryDescriptionType_DescriptionTypes_DescriptionTypesId",
                        column: x => x.DescriptionTypesId,
                        principalTable: "DescriptionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DescriptionValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    DescriptionTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedBy = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Enabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DescriptionValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DescriptionValues_DescriptionTypes_DescriptionTypeId",
                        column: x => x.DescriptionTypeId,
                        principalTable: "DescriptionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryDescriptionType_DescriptionTypesId",
                table: "CategoryDescriptionType",
                column: "DescriptionTypesId");

            migrationBuilder.CreateIndex(
                name: "IX_DescriptionValues_DescriptionTypeId",
                table: "DescriptionValues",
                column: "DescriptionTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryDescriptionType");

            migrationBuilder.DropTable(
                name: "DescriptionValues");

            migrationBuilder.DropTable(
                name: "DescriptionTypes");
        }
    }
}
