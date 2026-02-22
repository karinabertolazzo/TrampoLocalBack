using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrampoLocal.API.Migrations
{
    /// <inheritdoc />
    public partial class RefactorCategoriaToRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categoria",
                table: "Profissionais");

            migrationBuilder.AddColumn<int>(
                name: "CategoriaId",
                table: "Profissionais",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Profissionais_CategoriaId",
                table: "Profissionais",
                column: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profissionais_Categorias_CategoriaId",
                table: "Profissionais",
                column: "CategoriaId",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profissionais_Categorias_CategoriaId",
                table: "Profissionais");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropIndex(
                name: "IX_Profissionais_CategoriaId",
                table: "Profissionais");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "Profissionais");

            migrationBuilder.AddColumn<string>(
                name: "Categoria",
                table: "Profissionais",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
