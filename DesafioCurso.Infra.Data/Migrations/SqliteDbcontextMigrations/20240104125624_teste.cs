using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesafioCurso.Infra.Data.Migrations.SqliteDbcontextMigrations
{
    /// <inheritdoc />
    public partial class teste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserPermissionId",
                table: "usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserPermissionId",
                table: "usuario",
                type: "TEXT",
                nullable: true);
        }
    }
}
