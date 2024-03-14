using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial_5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "StatusId",
                table: "Statements",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("0d47d368-e5d0-4e02-af24-8b7e5b372bd7"),
                oldClrType: typeof(Guid),
                oldType: "uuid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "StatusId",
                table: "Statements",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldDefaultValue: new Guid("0d47d368-e5d0-4e02-af24-8b7e5b372bd7"));
        }
    }
}
