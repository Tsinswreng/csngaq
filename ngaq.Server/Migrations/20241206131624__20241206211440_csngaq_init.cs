using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ngaq.Migrations
{
    /// <inheritdoc />
    public partial class _20241206211440_csngaq_init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KV");

            migrationBuilder.CreateTable(
                name: "WordKV",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    bl = table.Column<string>(type: "TEXT", nullable: false),
                    ct = table.Column<long>(type: "INTEGER", nullable: false, defaultValueSql: "(strftime('%s', 'now') || substr(strftime('%f', 'now'), 4))"),
                    ut = table.Column<long>(type: "INTEGER", nullable: false, defaultValueSql: "(strftime('%s', 'now') || substr(strftime('%f', 'now'), 4))"),
                    kType = table.Column<string>(type: "TEXT", nullable: false, defaultValue: "STR"),
                    kStr = table.Column<string>(type: "TEXT", nullable: false),
                    kI64 = table.Column<long>(type: "INTEGER", nullable: false),
                    kDesc = table.Column<string>(type: "TEXT", nullable: false),
                    vType = table.Column<string>(type: "TEXT", nullable: false, defaultValue: "STR"),
                    vDesc = table.Column<string>(type: "TEXT", nullable: false),
                    vStr = table.Column<string>(type: "TEXT", nullable: false),
                    vI64 = table.Column<long>(type: "INTEGER", nullable: false),
                    vF64 = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordKV", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WordKV_bl",
                table: "WordKV",
                column: "bl");

            migrationBuilder.CreateIndex(
                name: "IX_WordKV_ct",
                table: "WordKV",
                column: "ct");

            migrationBuilder.CreateIndex(
                name: "IX_WordKV_kDesc",
                table: "WordKV",
                column: "kDesc");

            migrationBuilder.CreateIndex(
                name: "IX_WordKV_kI64",
                table: "WordKV",
                column: "kI64");

            migrationBuilder.CreateIndex(
                name: "IX_WordKV_kStr",
                table: "WordKV",
                column: "kStr");

            migrationBuilder.CreateIndex(
                name: "IX_WordKV_ut",
                table: "WordKV",
                column: "ut");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WordKV");

            migrationBuilder.CreateTable(
                name: "KV",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    bl = table.Column<string>(type: "TEXT", nullable: true),
                    ct = table.Column<long>(type: "INTEGER", nullable: false, defaultValueSql: "(strftime('%s', 'now') || substr(strftime('%f', 'now'), 4))"),
                    kDesc = table.Column<string>(type: "TEXT", nullable: true),
                    kI64 = table.Column<long>(type: "INTEGER", nullable: true),
                    kStr = table.Column<string>(type: "TEXT", nullable: true),
                    kType = table.Column<string>(type: "TEXT", nullable: false, defaultValue: "STR"),
                    ut = table.Column<long>(type: "INTEGER", nullable: false, defaultValueSql: "(strftime('%s', 'now') || substr(strftime('%f', 'now'), 4))"),
                    vDesc = table.Column<string>(type: "TEXT", nullable: true),
                    vF64 = table.Column<double>(type: "REAL", nullable: true),
                    vI64 = table.Column<long>(type: "INTEGER", nullable: true),
                    vStr = table.Column<string>(type: "TEXT", nullable: true),
                    vType = table.Column<string>(type: "TEXT", nullable: false, defaultValue: "STR")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KV", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KV_bl",
                table: "KV",
                column: "bl");

            migrationBuilder.CreateIndex(
                name: "IX_KV_ct",
                table: "KV",
                column: "ct");

            migrationBuilder.CreateIndex(
                name: "IX_KV_kDesc",
                table: "KV",
                column: "kDesc");

            migrationBuilder.CreateIndex(
                name: "IX_KV_kI64",
                table: "KV",
                column: "kI64");

            migrationBuilder.CreateIndex(
                name: "IX_KV_kStr",
                table: "KV",
                column: "kStr");

            migrationBuilder.CreateIndex(
                name: "IX_KV_ut",
                table: "KV",
                column: "ut");
        }
    }
}
