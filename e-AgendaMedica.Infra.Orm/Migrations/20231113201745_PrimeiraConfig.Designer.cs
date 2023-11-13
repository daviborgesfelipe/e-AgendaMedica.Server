﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using e_AgendaMedica.Infra.Orm.Compartilhado;

#nullable disable

namespace e_AgendaMedica.Infra.Orm.Migrations
{
    [DbContext(typeof(eAgendaMedicaDbContext))]
    [Migration("20231113201745_PrimeiraConfig")]
    partial class PrimeiraConfig
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("e_AgendaMedica.Dominio.ModuloMedico.Medico", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CRM")
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Especialidade")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.ToTable("TBMedico", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
