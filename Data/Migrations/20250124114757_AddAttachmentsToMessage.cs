using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAttachmentsToMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MessageAttachments_MessageId",
                table: "MessageAttachments",
                column: "MessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageAttachments_Messages_MessageId",
                table: "MessageAttachments",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageAttachments_Messages_MessageId",
                table: "MessageAttachments");

            migrationBuilder.DropIndex(
                name: "IX_MessageAttachments_MessageId",
                table: "MessageAttachments");
        }
    }
}
