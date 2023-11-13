using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_AgendaMedica.Infra.Orm.Migrations
{
    public partial class PrimeiraConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBMedico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(200)", nullable: false),
                    Especialidade = table.Column<string>(type: "varchar(200)", nullable: true),
                    CRM = table.Column<string>(type: "varchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBMedico", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBMedico");
        }
    }
}
