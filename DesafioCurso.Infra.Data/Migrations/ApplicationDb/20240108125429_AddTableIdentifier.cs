using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesafioCurso.Infra.Data.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class AddTableIdentifier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Identificador",
                table: "unidade",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Identificador",
                table: "produto",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Identificador",
                table: "pessoa",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_unidade_Identificador",
                table: "unidade",
                column: "Identificador",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_produto_Identificador",
                table: "produto",
                column: "Identificador",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_pessoa_Identificador",
                table: "pessoa",
                column: "Identificador",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_unidade_Identificador",
                table: "unidade");

            migrationBuilder.DropIndex(
                name: "IX_produto_Identificador",
                table: "produto");

            migrationBuilder.DropIndex(
                name: "IX_pessoa_Identificador",
                table: "pessoa");

            migrationBuilder.DropColumn(
                name: "Identificador",
                table: "unidade");

            migrationBuilder.DropColumn(
                name: "Identificador",
                table: "produto");

            migrationBuilder.DropColumn(
                name: "Identificador",
                table: "pessoa");
        }
    }
}