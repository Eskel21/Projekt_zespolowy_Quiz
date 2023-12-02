﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt_Quizy.Data.Migrations
{
    public partial class AktualizacjaTabeliUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "AspNetUsers",
                type: "varbinary(max)",
                nullable: true);
            


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "AspNetUsers");

        }
    }
}
