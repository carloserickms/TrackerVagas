using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Migrations
{
    /// <inheritdoc />
    public partial class new_fields_in_the_jobvacance_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "InterestLevelId",
                table: "JobVacancy",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "JobVacancy",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<float>(
                name: "Salary",
                table: "JobVacancy",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TypeOfContractId",
                table: "JobVacancy",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<float>(
                name: "Workload",
                table: "JobVacancy",
                type: "float",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InterestLevel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterestLevel", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TypeOfContract",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfContract", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_JobVacancy_InterestLevelId",
                table: "JobVacancy",
                column: "InterestLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_JobVacancy_TypeOfContractId",
                table: "JobVacancy",
                column: "TypeOfContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobVacancy_InterestLevel_InterestLevelId",
                table: "JobVacancy",
                column: "InterestLevelId",
                principalTable: "InterestLevel",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobVacancy_TypeOfContract_TypeOfContractId",
                table: "JobVacancy",
                column: "TypeOfContractId",
                principalTable: "TypeOfContract",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobVacancy_InterestLevel_InterestLevelId",
                table: "JobVacancy");

            migrationBuilder.DropForeignKey(
                name: "FK_JobVacancy_TypeOfContract_TypeOfContractId",
                table: "JobVacancy");

            migrationBuilder.DropTable(
                name: "InterestLevel");

            migrationBuilder.DropTable(
                name: "TypeOfContract");

            migrationBuilder.DropIndex(
                name: "IX_JobVacancy_InterestLevelId",
                table: "JobVacancy");

            migrationBuilder.DropIndex(
                name: "IX_JobVacancy_TypeOfContractId",
                table: "JobVacancy");

            migrationBuilder.DropColumn(
                name: "InterestLevelId",
                table: "JobVacancy");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "JobVacancy");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "JobVacancy");

            migrationBuilder.DropColumn(
                name: "TypeOfContractId",
                table: "JobVacancy");

            migrationBuilder.DropColumn(
                name: "Workload",
                table: "JobVacancy");
        }
    }
}
