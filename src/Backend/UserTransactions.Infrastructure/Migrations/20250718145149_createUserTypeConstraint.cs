using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserTransactions.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class createUserTypeConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_Users_UserType",
                table: "Users",
                sql: "UserType IN ('User', 'Merchant')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Users_UserType",
                table: "Users");
        }
    }
}
