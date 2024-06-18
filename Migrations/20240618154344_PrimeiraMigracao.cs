using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoEPI.Migrations
{
    /// <inheritdoc />
    public partial class PrimeiraMigracao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Endereco = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Telefone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.ID);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Administradores",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Senha = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmpresaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administradores", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Administradores_Empresas_EmpresaID",
                        column: x => x.EmpresaID,
                        principalTable: "Empresas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CanteirosDeObra",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Localizacao = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmpresaID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CanteirosDeObra", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CanteirosDeObra_Empresas_EmpresaID",
                        column: x => x.EmpresaID,
                        principalTable: "Empresas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CPF = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Cargo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CanteiroDeObraID = table.Column<int>(type: "int", nullable: false),
                    EmpresaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Funcionarios_CanteirosDeObra_CanteiroDeObraID",
                        column: x => x.CanteiroDeObraID,
                        principalTable: "CanteirosDeObra",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Funcionarios_Empresas_EmpresaID",
                        column: x => x.EmpresaID,
                        principalTable: "Empresas",
                        principalColumn: "ID");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ReconhecimentosEPI",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataHora = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FuncionarioID = table.Column<int>(type: "int", nullable: false),
                    UsoEPI = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CanteiroID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReconhecimentosEPI", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReconhecimentosEPI_CanteirosDeObra_CanteiroID",
                        column: x => x.CanteiroID,
                        principalTable: "CanteirosDeObra",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReconhecimentosEPI_Funcionarios_FuncionarioID",
                        column: x => x.FuncionarioID,
                        principalTable: "Funcionarios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Notificacoes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DataHora = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Tipo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descricao = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmpresaID = table.Column<int>(type: "int", nullable: false),
                    AdministradorID = table.Column<int>(type: "int", nullable: false),
                    ReconhecimentoFacialID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificacoes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Notificacoes_Administradores_AdministradorID",
                        column: x => x.AdministradorID,
                        principalTable: "Administradores",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notificacoes_Empresas_EmpresaID",
                        column: x => x.EmpresaID,
                        principalTable: "Empresas",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notificacoes_ReconhecimentosEPI_ReconhecimentoFacialID",
                        column: x => x.ReconhecimentoFacialID,
                        principalTable: "ReconhecimentosEPI",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Administradores_EmpresaID",
                table: "Administradores",
                column: "EmpresaID");

            migrationBuilder.CreateIndex(
                name: "IX_CanteirosDeObra_EmpresaID",
                table: "CanteirosDeObra",
                column: "EmpresaID");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_CanteiroDeObraID",
                table: "Funcionarios",
                column: "CanteiroDeObraID");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_EmpresaID",
                table: "Funcionarios",
                column: "EmpresaID");

            migrationBuilder.CreateIndex(
                name: "IX_Notificacoes_AdministradorID",
                table: "Notificacoes",
                column: "AdministradorID");

            migrationBuilder.CreateIndex(
                name: "IX_Notificacoes_EmpresaID",
                table: "Notificacoes",
                column: "EmpresaID");

            migrationBuilder.CreateIndex(
                name: "IX_Notificacoes_ReconhecimentoFacialID",
                table: "Notificacoes",
                column: "ReconhecimentoFacialID");

            migrationBuilder.CreateIndex(
                name: "IX_ReconhecimentosEPI_CanteiroID",
                table: "ReconhecimentosEPI",
                column: "CanteiroID");

            migrationBuilder.CreateIndex(
                name: "IX_ReconhecimentosEPI_FuncionarioID",
                table: "ReconhecimentosEPI",
                column: "FuncionarioID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notificacoes");

            migrationBuilder.DropTable(
                name: "Administradores");

            migrationBuilder.DropTable(
                name: "ReconhecimentosEPI");

            migrationBuilder.DropTable(
                name: "Funcionarios");

            migrationBuilder.DropTable(
                name: "CanteirosDeObra");

            migrationBuilder.DropTable(
                name: "Empresas");
        }
    }
}
