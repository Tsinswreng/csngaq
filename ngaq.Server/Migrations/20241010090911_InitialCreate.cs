﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ngaq.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KV",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    kType = table.Column<string>(type: "TEXT", nullable: false, defaultValue: "STR"),
                    kStr = table.Column<string>(type: "TEXT", nullable: true),
                    kI64 = table.Column<long>(type: "INTEGER", nullable: true),
                    kDesc = table.Column<string>(type: "TEXT", nullable: true),
                    vType = table.Column<string>(type: "TEXT", nullable: false, defaultValue: "STR"),
                    vDesc = table.Column<string>(type: "TEXT", nullable: true),
                    vStr = table.Column<string>(type: "TEXT", nullable: true),
                    vI64 = table.Column<long>(type: "INTEGER", nullable: true),
                    vF64 = table.Column<double>(type: "REAL", nullable: true),
                    bl = table.Column<string>(type: "TEXT", nullable: true),
                    ct = table.Column<long>(type: "INTEGER", nullable: false, defaultValueSql: "(strftime('%s', 'now') || substr(strftime('%f', 'now'), 4))"),
                    ut = table.Column<long>(type: "INTEGER", nullable: false, defaultValueSql: "(strftime('%s', 'now') || substr(strftime('%f', 'now'), 4))")
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KV");
        }
    }
}
