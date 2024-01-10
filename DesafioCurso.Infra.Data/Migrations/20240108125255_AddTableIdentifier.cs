using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesafioCurso.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTableIdentifier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Identificador",
                table: "usuario",
                type: "TEXT",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "id_usuario",
                table: "permissoes",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "Identificador",
                table: "permissoes",
                type: "TEXT",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_Identificador",
                table: "usuario",
                column: "Identificador",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_permissoes_Identificador",
                table: "permissoes",
                column: "Identificador",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_usuario_Identificador",
                table: "usuario");

            migrationBuilder.DropIndex(
                name: "IX_permissoes_Identificador",
                table: "permissoes");

            migrationBuilder.DropColumn(
                name: "Identificador",
                table: "usuario");

            migrationBuilder.DropColumn(
                name: "Identificador",
                table: "permissoes");

            migrationBuilder.AlterColumn<Guid>(
                name: "id_usuario",
                table: "permissoes",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}