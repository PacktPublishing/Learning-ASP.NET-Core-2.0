using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TicTacToe.Migrations
{
    public partial class IdentityDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsEmailConfirmed",
                table: "UserModel",
                newName: "TwoFactorEnabled");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "UserModel",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "UserModel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "UserModel",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "UserModel",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "UserModel",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "UserModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "UserModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "UserModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "UserModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "UserModel",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "UserModel",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "UserModel",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "UserModel",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RoleModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleModel", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameSessionModel_WinnerId",
                table: "GameSessionModel",
                column: "WinnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameSessionModel_UserModel_WinnerId",
                table: "GameSessionModel",
                column: "WinnerId",
                principalTable: "UserModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameSessionModel_UserModel_WinnerId",
                table: "GameSessionModel");

            migrationBuilder.DropTable(
                name: "RoleModel");

            migrationBuilder.DropIndex(
                name: "IX_GameSessionModel_WinnerId",
                table: "GameSessionModel");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "UserModel");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "UserModel");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "UserModel");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "UserModel");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "UserModel");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "UserModel");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "UserModel");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "UserModel");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "UserModel");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "UserModel");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "UserModel");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "UserModel");

            migrationBuilder.RenameColumn(
                name: "TwoFactorEnabled",
                table: "UserModel",
                newName: "IsEmailConfirmed");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "UserModel",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
