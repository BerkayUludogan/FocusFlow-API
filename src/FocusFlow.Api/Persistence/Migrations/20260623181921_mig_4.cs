using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FocusFlow.Api.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_user_email_verification_codes_CodeHash",
                table: "user_email_verification_codes");

            migrationBuilder.CreateIndex(
                name: "IX_user_email_verification_codes_UserId_CodeHash",
                table: "user_email_verification_codes",
                columns: new[] { "UserId", "CodeHash" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_user_email_verification_codes_UserId_CodeHash",
                table: "user_email_verification_codes");

            migrationBuilder.CreateIndex(
                name: "IX_user_email_verification_codes_CodeHash",
                table: "user_email_verification_codes",
                column: "CodeHash",
                unique: true);
        }
    }
}
