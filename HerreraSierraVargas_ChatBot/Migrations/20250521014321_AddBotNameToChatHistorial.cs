using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HerreraSierraVargas_ChatBot.Migrations
{
    /// <inheritdoc />
    public partial class AddBotNameToChatHistorial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BotName",
                table: "ChatHistorial",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BotName",
                table: "ChatHistorial");
        }
    }
}
