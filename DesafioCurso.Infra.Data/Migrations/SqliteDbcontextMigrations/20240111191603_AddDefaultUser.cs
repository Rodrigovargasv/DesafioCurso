using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Text;

#nullable disable

namespace DesafioCurso.Infra.Data.Migrations.SqliteDbcontextMigrations
{
    /// <inheritdoc />
    public partial class AddDefaultUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string shortIdUser = GenerateShortId();

            var adminUser = new
            {
                Id = Guid.NewGuid(),
                NomeCompleto = "Usuário Padrão",
                Apelido = "admin",
                Email = "admin@example.com",
                Senha = "Ro12345#@",
                CpfCnpj = (string)null,
                Identificador = shortIdUser
            };

            migrationBuilder.InsertData(
                table: "usuario",
                columns: new[] { "id", "nome_completo", "apelido", "email", "senha", "cpf_cnpj", "identificador" },
                values: new object[] { adminUser.Id, adminUser.NomeCompleto, adminUser.Apelido, adminUser.Email, HashPassword(adminUser.Senha), adminUser.CpfCnpj, adminUser.Identificador }
            );

            // Inserir Permissões para o Usuário padrão
            var perfilAcesso = 2;
            string shortIdPermission = GenerateShortId();
            migrationBuilder.InsertData(
                table: "permissoes",
                columns: new[] { "id", "perfil_de_acesso", "id_usuario", "identificador" },
                values: new object[] { Guid.NewGuid(), perfilAcesso, adminUser.Id, shortIdPermission }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }

        public string GenerateShortId()
        {
            // Gera um GUID (Globally Unique Identifier)
            var id = Guid.NewGuid().ToString("N").ToUpper();

            // Converte o GUID para base64
            string base64String = Convert.ToBase64String(Encoding.UTF8.GetBytes(id));

            // Pega os primeiros 10 caracteres
            string shortId = base64String.Substring(0, Math.Min(base64String.Length, 10));

            return shortId;
        }

        private string HashPassword(string password)
        {
            var passwordHasher = new PasswordHasher<object>();
            return passwordHasher.HashPassword(null, password);
        }
    }
}