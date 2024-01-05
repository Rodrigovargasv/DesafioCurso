using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesafioCurso.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
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
                    cpf_cnpj = table.Column<string>(type: "TEXT", maxLength: 14, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "permissoes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    perfil_de_acesso = table.Column<int>(type: "INTEGER", maxLength: 15, nullable: false),
                    id_usuario = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permissoes", x => x.id);
                    table.ForeignKey(
                        name: "FK_permissoes_usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_permissoes_id_usuario",
                table: "permissoes",
                column: "id_usuario",
                unique: true);

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
                name: "permissoes");

            migrationBuilder.DropTable(
                name: "usuario");
        }
    }
}
