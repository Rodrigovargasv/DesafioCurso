using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesafioCurso.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class ModifidPropertyBarCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "codigo_barras",
                table: "produto",
                type: "character varying(13)",
                maxLength: 13,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldMaxLength: 13);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "codigo_barras",
                table: "produto",
                type: "integer",
                maxLength: 13,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "character varying(13)",
                oldMaxLength: 13,
                oldNullable: true);
        }
    }
}
