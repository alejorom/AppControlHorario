using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GHL.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConfirmExistingUsersEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Existing users registered before email confirmation became mandatory
            migrationBuilder.Sql("UPDATE [AspNetUsers] SET [EmailConfirmed] = 1 WHERE [EmailConfirmed] = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Data-only migration: the previous confirmation state cannot be restored
        }
    }
}
