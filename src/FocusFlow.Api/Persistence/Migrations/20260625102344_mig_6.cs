using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FocusFlow.Api.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_pomodoro_settings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FocusDurationMinutes = table.Column<int>(type: "integer", nullable: false, defaultValue: 25),
                    ShortBreakDurationMinutes = table.Column<int>(type: "integer", nullable: false, defaultValue: 5),
                    LongBreakDurationMinutes = table.Column<int>(type: "integer", nullable: false, defaultValue: 15),
                    LongBreakInterval = table.Column<int>(type: "integer", nullable: false, defaultValue: 4),
                    DailyFocusGoalMinutes = table.Column<int>(type: "integer", nullable: false, defaultValue: 120),
                    AutoStartBreaks = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    AutoStartPomodoros = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_pomodoro_settings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_pomodoro_settings_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_pomodoro_settings_UserId",
                table: "user_pomodoro_settings",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_pomodoro_settings");
        }
    }
}
