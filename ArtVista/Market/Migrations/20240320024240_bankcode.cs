using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Market.Migrations
{
    /// <inheritdoc />
    public partial class bankcode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("AspNetUsers", "Remain");

            /*
            migrationBuilder.AddColumn<string>(
                 name: "ConfirmCode",
                 table: "AspNetUsers",
                 type: "nvarchar(450)",
                 nullable: false,
                 defaultValue: "");
            
            */


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
