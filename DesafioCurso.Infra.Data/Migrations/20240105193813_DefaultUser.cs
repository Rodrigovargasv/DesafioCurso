using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesafioCurso.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class DefaultUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var adminUser = new
            {
                Id = Guid.NewGuid(),
                NomeCompleto = "Usuário Padrão",
                Apelido = "admin",
                Email = "admin@example.com",
                Senha = "Ro12345#@",
                CpfCnpj = (string)null
            };

            migrationBuilder.InsertData(
                table: "usuario",
                columns: new[] { "id", "nome_completo", "apelido", "email", "senha", "cpf_cnpj" },
                values: new object[] { adminUser.Id, adminUser.NomeCompleto, adminUser.Apelido, adminUser.Email, adminUser.Senha, adminUser.CpfCnpj }
            );

            // Inserir Permissões para o Usuário padrão
            var perfilAcesso = 2;
            migrationBuilder.InsertData(
                table: "permissoes",
                columns: new[] { "id", "perfil_de_acesso", "id_usuario" },
                values: new object[] { Guid.NewGuid(), perfilAcesso, adminUser.Id }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
          
        }
    }
}
