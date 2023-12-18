﻿// <auto-generated />
using System;
using DesafioCurso.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DesafioCurso.Infra.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231218191559_InitalCreateDataBase")]
    partial class InitalCreateDataBase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DesafioCurso.Domain.Entities.Person", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true)
                        .HasColumnName("ativo");

                    b.Property<string>("AlternativeCode")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("codigo_alternativo");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("cidade");

                    b.Property<string>("Document")
                        .HasMaxLength(14)
                        .HasColumnType("character varying(14)")
                        .HasColumnName("documento");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("nome_completo");

                    b.Property<string>("Observation")
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("observacao");

                    b.Property<bool>("ReleaseSale")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("libera_venda");

                    b.HasKey("Id");

                    b.HasIndex("AlternativeCode")
                        .IsUnique();

                    b.HasIndex("Document")
                        .IsUnique();

                    b.ToTable("pessoa", (string)null);
                });

            modelBuilder.Entity("DesafioCurso.Domain.Entities.Product", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("AcronynmUnit")
                        .IsRequired()
                        .HasColumnType("character varying(10)")
                        .HasColumnName("unidade");

                    b.Property<bool>("Active")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(true)
                        .HasColumnName("ativo");

                    b.Property<int>("BarCode")
                        .HasMaxLength(13)
                        .HasColumnType("integer")
                        .HasColumnName("codigo_barras");

                    b.Property<string>("BriefDescription")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("descricao_resumida");

                    b.Property<string>("FullDescription")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("descricao_completa");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("numeric(18,2)")
                        .HasColumnName("preco");

                    b.Property<int>("QuantityStock")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(18, 2)
                        .HasColumnType("integer")
                        .HasDefaultValue(0)
                        .HasColumnName("quantidade_estoque");

                    b.Property<bool>("Saleable")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false)
                        .HasColumnName("vendavel");

                    b.HasKey("Id");

                    b.HasIndex("AcronynmUnit");

                    b.HasIndex("BarCode")
                        .IsUnique();

                    b.ToTable("produto", (string)null);
                });

            modelBuilder.Entity("DesafioCurso.Domain.Entities.Unit", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Acronym")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)")
                        .HasColumnName("sigla");

                    b.Property<string>("Decription")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("descricao");

                    b.HasKey("Id");

                    b.HasIndex("Acronym")
                        .IsUnique();

                    b.ToTable("unidade", (string)null);
                });

            modelBuilder.Entity("DesafioCurso.Domain.Entities.User", b =>
                {
                    b.Property<Guid?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Cpf_Cnpj")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("character varying(14)")
                        .HasColumnName("cpf_cnpj");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("email");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("nome_completo");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("senha");

                    b.Property<string>("Surnamed")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("apelido");

                    b.HasKey("Id");

                    b.HasIndex("Cpf_Cnpj")
                        .IsUnique();

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Surnamed")
                        .IsUnique();

                    b.ToTable("usuario", (string)null);
                });

            modelBuilder.Entity("DesafioCurso.Domain.Entities.Product", b =>
                {
                    b.HasOne("DesafioCurso.Domain.Entities.Unit", "UnitProduct")
                        .WithMany("RelatedProducts")
                        .HasForeignKey("AcronynmUnit")
                        .HasPrincipalKey("Acronym")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("UnitProduct");
                });

            modelBuilder.Entity("DesafioCurso.Domain.Entities.Unit", b =>
                {
                    b.Navigation("RelatedProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
