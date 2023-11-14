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
                name: "TBAtividade",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Paciente = table.Column<string>(type: "varchar(200)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HorarioTermino = table.Column<TimeSpan>(type: "time", nullable: false),
                    HorarioInicio = table.Column<TimeSpan>(type: "time", nullable: false),
                    TipoAtividade = table.Column<int>(type: "int", nullable: false),
                    MedicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBAtividade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBMedico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(200)", nullable: false),
                    Especialidade = table.Column<string>(type: "varchar(200)", nullable: true),
                    CRM = table.Column<string>(type: "varchar(20)", nullable: true),
                    AtividadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBMedico", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBAtividadeMedico",
                columns: table => new
                {
                    ListaAtividadesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ListaMedicosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBAtividadeMedico", x => new { x.ListaAtividadesId, x.ListaMedicosId });
                    table.ForeignKey(
                        name: "FK_TBAtividadeMedico_TBAtividade_ListaAtividadesId",
                        column: x => x.ListaAtividadesId,
                        principalTable: "TBAtividade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TBAtividadeMedico_TBMedico_ListaMedicosId",
                        column: x => x.ListaMedicosId,
                        principalTable: "TBMedico",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBAtividadeMedico_ListaMedicosId",
                table: "TBAtividadeMedico",
                column: "ListaMedicosId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBAtividadeMedico");

            migrationBuilder.DropTable(
                name: "TBAtividade");

            migrationBuilder.DropTable(
                name: "TBMedico");
        }
    }
}
