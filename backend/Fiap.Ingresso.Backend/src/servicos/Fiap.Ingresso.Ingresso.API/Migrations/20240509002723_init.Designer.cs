﻿// <auto-generated />
using System;
using Fiap.Ingresso.Ingresso.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Fiap.Ingresso.Ingresso.API.Migrations
{
    [DbContext(typeof(IngressoContext))]
    [Migration("20240509002723_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Fiap.Ingresso.Ingresso.API.Domain.Ingresso", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DataVenda")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("EventoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("IngressosDoEventoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("IngressosDoEventoId");

                    b.ToTable("Ingressos");
                });

            modelBuilder.Entity("Fiap.Ingresso.Ingresso.API.Domain.IngressosDoEvento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataFim")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("datetime2");

                    b.Property<int>("Disponiveis")
                        .HasColumnType("int");

                    b.Property<Guid>("EventoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Preco")
                        .HasColumnType("decimal");

                    b.Property<int>("Total")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("IngressosDosEventos");
                });

            modelBuilder.Entity("Fiap.Ingresso.Ingresso.API.Domain.Ingresso", b =>
                {
                    b.HasOne("Fiap.Ingresso.Ingresso.API.Domain.IngressosDoEvento", null)
                        .WithMany("IngressosVendidos")
                        .HasForeignKey("IngressosDoEventoId");
                });

            modelBuilder.Entity("Fiap.Ingresso.Ingresso.API.Domain.IngressosDoEvento", b =>
                {
                    b.Navigation("IngressosVendidos");
                });
#pragma warning restore 612, 618
        }
    }
}
