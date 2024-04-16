using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETickets.Migrations
{
    /// <inheritdoc />
    public partial class SYSDATETIME : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MovieStatus",
                table: "Movies",
                type: "int",
                nullable: false,
                computedColumnSql: "CASE WHEN SYSDATETIME() > EndDate THEN 2 WHEN SYSDATETIME() >= StartDate THEN 1 ELSE 0 END",
                oldClrType: typeof(int),
                oldType: "int",
                oldComputedColumnSql: "CASE WHEN GETUTCDATE() > EndDate THEN 2 WHEN GETUTCDATE() >= StartDate THEN 1 ELSE 0 END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MovieStatus",
                table: "Movies",
                type: "int",
                nullable: false,
                computedColumnSql: "CASE WHEN GETUTCDATE() > EndDate THEN 2 WHEN GETUTCDATE() >= StartDate THEN 1 ELSE 0 END",
                oldClrType: typeof(int),
                oldType: "int",
                oldComputedColumnSql: "CASE WHEN SYSDATETIME() > EndDate THEN 2 WHEN SYSDATETIME() >= StartDate THEN 1 ELSE 0 END");
        }
    }
}
