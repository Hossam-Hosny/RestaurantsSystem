using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurants.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedUserRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(@"
        INSERT INTO AspNetUserRoles (UserId, RoleId)
        VALUES 
        ('f9e61661-bb71-48f9-9b0a-1383ebd3a894','99a23f28-cf7e-42d8-b03b-48c03f8b603a'),
        ('0d39d96b-0623-4d7c-9607-1a65e45824bb','65e90bdd-a16c-48cb-b803-f73a067a96e6'),
        ('0b10089c-86c4-4088-896d-190c9a3cd5eb','6f0402ac-7220-4dd0-add8-814b88b828bb');
    ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
        DELETE FROM AspNetUserRoles
        WHERE (UserId = 'f9e61661-bb71-48f9-9b0a-1383ebd3a894' AND RoleId = '99a23f28-cf7e-42d8-b03b-48c03f8b603a')
           OR (UserId = '0d39d96b-0623-4d7c-9607-1a65e45824bb' AND RoleId = '65e90bdd-a16c-48cb-b803-f73a067a96e6')
           OR (UserId = '0b10089c-86c4-4088-896d-190c9a3cd5eb' AND RoleId = '6f0402ac-7220-4dd0-add8-814b88b828bb');
    ");
        }
    }
}
