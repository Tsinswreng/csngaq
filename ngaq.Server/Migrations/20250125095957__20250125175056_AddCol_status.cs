using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ngaq.Server.Migrations
{
    /// <inheritdoc />
    public partial class _20250125175056_AddCol_status : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "bl",
                table: "WordKV",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "WordKV",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "bl",
                table: "_KV",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "_KV",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "WordKV");

            migrationBuilder.DropColumn(
                name: "status",
                table: "_KV");

            migrationBuilder.AlterColumn<string>(
                name: "bl",
                table: "WordKV",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "bl",
                table: "_KV",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
