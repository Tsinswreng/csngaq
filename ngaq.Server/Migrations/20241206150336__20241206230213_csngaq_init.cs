using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ngaq.Server.Migrations
{
    /// <inheritdoc />
    public partial class _20241206230213_csngaq_init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "_KV",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    bl = table.Column<string>(type: "TEXT", nullable: false),
                    ct = table.Column<long>(type: "INTEGER", nullable: false, defaultValueSql: "(strftime('%s', 'now') || substr(strftime('%f', 'now'), 4))"),
                    ut = table.Column<long>(type: "INTEGER", nullable: false, defaultValueSql: "(strftime('%s', 'now') || substr(strftime('%f', 'now'), 4))"),
                    kType = table.Column<string>(type: "TEXT", nullable: false, defaultValue: "STR"),
                    kStr = table.Column<string>(type: "TEXT", nullable: true),
                    kI64 = table.Column<long>(type: "INTEGER", nullable: true),
                    kDesc = table.Column<string>(type: "TEXT", nullable: true),
                    vType = table.Column<string>(type: "TEXT", nullable: false, defaultValue: "STR"),
                    vDesc = table.Column<string>(type: "TEXT", nullable: true),
                    vStr = table.Column<string>(type: "TEXT", nullable: true),
                    vI64 = table.Column<long>(type: "INTEGER", nullable: true),
                    vF64 = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__KV", x => x.id);
                });

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
                    kStr = table.Column<string>(type: "TEXT", nullable: true),
                    kI64 = table.Column<long>(type: "INTEGER", nullable: true),
                    kDesc = table.Column<string>(type: "TEXT", nullable: true),
                    vType = table.Column<string>(type: "TEXT", nullable: false, defaultValue: "STR"),
                    vDesc = table.Column<string>(type: "TEXT", nullable: true),
                    vStr = table.Column<string>(type: "TEXT", nullable: true),
                    vI64 = table.Column<long>(type: "INTEGER", nullable: true),
                    vF64 = table.Column<double>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordKV", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX__KV_bl",
                table: "_KV",
                column: "bl");

            migrationBuilder.CreateIndex(
                name: "IX__KV_ct",
                table: "_KV",
                column: "ct");

            migrationBuilder.CreateIndex(
                name: "IX__KV_kDesc",
                table: "_KV",
                column: "kDesc");

            migrationBuilder.CreateIndex(
                name: "IX__KV_kI64",
                table: "_KV",
                column: "kI64");

            migrationBuilder.CreateIndex(
                name: "IX__KV_kStr",
                table: "_KV",
                column: "kStr");

            migrationBuilder.CreateIndex(
                name: "IX__KV_ut",
                table: "_KV",
                column: "ut");

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
                name: "_KV");

            migrationBuilder.DropTable(
                name: "WordKV");
        }
    }
}
