using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesafioCurso.Infra.Data.Migrations.SqliteDbcontextMigrations
{
    /// <inheritdoc />
    public partial class ChangedTableRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_permissões_id_usuario",
                table: "permissões");

            migrationBuilder.CreateIndex(
                name: "IX_permissões_id_usuario",
                table: "permissões",
                column: "id_usuario",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_permissões_id_usuario",
                table: "permissões");

            migrationBuilder.CreateIndex(
                name: "IX_permissões_id_usuario",
                table: "permissões",
                column: "id_usuario");
        }
    }
}
