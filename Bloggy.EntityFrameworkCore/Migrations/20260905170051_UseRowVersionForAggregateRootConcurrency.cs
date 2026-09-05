using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bloggy.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class UseRowVersionForAggregateRootConcurrency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Blog");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Blog",
                type: "rowversion",
                rowVersion: true,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "Blog");

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Blog",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");
        }
    }
}
