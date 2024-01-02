using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesafioCurso.Infra.Data.Migrations.SqliteDbcontextMigrations
{
    /// <inheritdoc />
    public partial class CreateDataBaseSQLite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    nome_completo = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    apelido = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    senha = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    cpf_cnpj = table.Column<string>(type: "TEXT", maxLength: 14, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_usuario_apelido",
                table: "usuario",
                column: "apelido",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_usuario_cpf_cnpj",
                table: "usuario",
                column: "cpf_cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_usuario_email",
                table: "usuario",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "usuario");
        }
    }
}
