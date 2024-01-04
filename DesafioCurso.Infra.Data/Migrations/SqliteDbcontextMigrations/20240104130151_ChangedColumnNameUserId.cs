using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesafioCurso.Infra.Data.Migrations.SqliteDbcontextMigrations
{
    /// <inheritdoc />
    public partial class ChangedColumnNameUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_permissões_usuario_UserId",
                table: "permissões");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "permissões",
                newName: "id_usuario");

            migrationBuilder.RenameIndex(
                name: "IX_permissões_UserId",
                table: "permissões",
                newName: "IX_permissões_id_usuario");

            migrationBuilder.AddForeignKey(
                name: "FK_permissões_usuario_id_usuario",
                table: "permissões",
                column: "id_usuario",
                principalTable: "usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_permissões_usuario_id_usuario",
                table: "permissões");

            migrationBuilder.RenameColumn(
                name: "id_usuario",
                table: "permissões",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_permissões_id_usuario",
                table: "permissões",
                newName: "IX_permissões_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_permissões_usuario_UserId",
                table: "permissões",
                column: "UserId",
                principalTable: "usuario",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
