using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JLokaTestEFCore.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedDataForInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "Id", "Amount", "ContactName", "Description", "DueDate", "InvoiceDate", "InvoiceNumber", "Status" },
                values: new object[] { new Guid("3212976a-1f5a-462b-b989-17a80f9f6e79"), 500000m, "JLoka", "Simple Description", new DateTimeOffset(new DateTime(2025, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 1, 1, 1, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "JLoka-01", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: new Guid("3212976a-1f5a-462b-b989-17a80f9f6e79"));
        }
    }
}
