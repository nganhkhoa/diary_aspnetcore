using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace diary.Migrations
{
    public partial class TitleEntries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Entry",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Entry");
        }
    }
}
