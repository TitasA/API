using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Currency = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CustomerType = table.Column<int>(type: "int", nullable: false),
                    CashBack = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Currency = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OperationType = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Currency", "CustomerType", "Status" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 2, 2, 1, 1 },
                    { 3, 1, 1, 2 }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "CashBack", "Currency", "CustomerType", "Status" },
                values: new object[,]
                {
                    { 4, 0.01m, 1, 2, 1 },
                    { 5, 0.05m, 2, 2, 1 },
                    { 6, 0.01m, 1, 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "Transaction",
                columns: new[] { "Id", "AccountId", "Amount", "Currency", "Date", "OperationType" },
                values: new object[,]
                {
                    { 1, 1, 100m, 1, new DateTime(2022, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 1, 100m, 1, new DateTime(2022, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, 2, 50m, 2, new DateTime(2022, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 4, 3, 100m, 1, new DateTime(2022, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_AccountId",
                table: "Transaction",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
