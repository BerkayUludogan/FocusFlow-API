using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FocusFlow.Api.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    RefreshToken = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    RefreshTokenExpiresAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsEmailVerified = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    LastLoginAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "task_items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CompletedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DueDateUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EstimatedPomodoroCount = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    CompletedPomodoroCount = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_task_items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_task_items_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_task_items_UserId_ClientId",
                table: "task_items",
                columns: new[] { "UserId", "ClientId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_Email",
                table: "users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "task_items");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
