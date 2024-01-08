using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesafioCurso.Infra.Data.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "pessoa",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nome_completo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    documento = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: true),
                    cidade = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    observacao = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    codigo_alternativo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    libera_venda = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false),
                    ativo = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pessoa", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "unidade",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    sigla = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    descricao = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_unidade", x => x.id);
                    table.UniqueConstraint("AK_unidade_sigla", x => x.sigla);
                });

            migrationBuilder.CreateTable(
                name: "produto",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    descricao_completa = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    descricao_resumida = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    preco = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    quantidade_estoque = table.Column<int>(type: "integer", precision: 18, scale: 2, nullable: true, defaultValue: 0),
                    codigo_barras = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: true),
                    ativo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    vendavel = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    unidade = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produto", x => x.id);
                    table.ForeignKey(
                        name: "FK_produto_unidade_unidade",
                        column: x => x.unidade,
                        principalTable: "unidade",
                        principalColumn: "sigla",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_pessoa_codigo_alternativo",
                table: "pessoa",
                column: "codigo_alternativo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_pessoa_documento",
                table: "pessoa",
                column: "documento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_produto_codigo_barras",
                table: "produto",
                column: "codigo_barras",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_produto_unidade",
                table: "produto",
                column: "unidade");

            migrationBuilder.CreateIndex(
                name: "IX_unidade_sigla",
                table: "unidade",
                column: "sigla",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pessoa");

            migrationBuilder.DropTable(
                name: "produto");

            migrationBuilder.DropTable(
                name: "unidade");
        }
    }
}