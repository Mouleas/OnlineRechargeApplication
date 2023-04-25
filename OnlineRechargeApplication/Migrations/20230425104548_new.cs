using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineRechargeApplication.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SelectedPlanModel_CustomerId",
                table: "SelectedPlanModel",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_SelectedPlanModel_PlanId",
                table: "SelectedPlanModel",
                column: "PlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_SelectedPlanModel_CustomerModel_CustomerId",
                table: "SelectedPlanModel",
                column: "CustomerId",
                principalTable: "CustomerModel",
                principalColumn: "CustomerId"
                );

            migrationBuilder.AddForeignKey(
                name: "FK_SelectedPlanModel_PlanModel_PlanId",
                table: "SelectedPlanModel",
                column: "PlanId",
                principalTable: "PlanModel",
                principalColumn: "PlanId"
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SelectedPlanModel_CustomerModel_CustomerId",
                table: "SelectedPlanModel");

            migrationBuilder.DropForeignKey(
                name: "FK_SelectedPlanModel_PlanModel_PlanId",
                table: "SelectedPlanModel");

            migrationBuilder.DropIndex(
                name: "IX_SelectedPlanModel_CustomerId",
                table: "SelectedPlanModel");

            migrationBuilder.DropIndex(
                name: "IX_SelectedPlanModel_PlanId",
                table: "SelectedPlanModel");
        }
    }
}
