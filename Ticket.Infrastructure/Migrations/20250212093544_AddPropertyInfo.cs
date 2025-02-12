using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticket.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertyInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "TicketStatusHistory",
                type: "int",
                nullable: false,
                comment: "Open,\r\n    InProgress,\r\n    Finished,\r\n    Closed",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Tickets",
                type: "int",
                nullable: false,
                comment: "Open,\r\n    InProgress,\r\n    Finished,\r\n    Closed",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Priority",
                table: "Tickets",
                type: "int",
                nullable: false,
                comment: "Low,\r\n    Medium,\r\n    High,\r\n    Critical",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Action",
                table: "TicketAudit",
                type: "int",
                nullable: false,
                comment: "Add,\r\n    Edit,\r\n    Update,\r\n    Delete,\r\n    StatusChange",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Field",
                type: "int",
                nullable: false,
                comment: "None ,\r\n    String,\r\n     Int,\r\n   Enum",
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "TicketStatusHistory",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Open,\r\n    InProgress,\r\n    Finished,\r\n    Closed");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Tickets",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Open,\r\n    InProgress,\r\n    Finished,\r\n    Closed");

            migrationBuilder.AlterColumn<int>(
                name: "Priority",
                table: "Tickets",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Low,\r\n    Medium,\r\n    High,\r\n    Critical");

            migrationBuilder.AlterColumn<int>(
                name: "Action",
                table: "TicketAudit",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Add,\r\n    Edit,\r\n    Update,\r\n    Delete,\r\n    StatusChange");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "Field",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "None ,\r\n    String,\r\n     Int,\r\n   Enum");
        }
    }
}
