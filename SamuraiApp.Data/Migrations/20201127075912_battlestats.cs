using Microsoft.EntityFrameworkCore.Migrations;

namespace SamuraiApp.Data.Migrations
{
    public partial class battlestats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE OR ALTER VIEW dbo.SamuraiBattleStats
                                    AS
                                    SELECT S.Id, S.Name, COUNT(SB.BattleId) as NoOfBattles
                                    FROM dbo.SamuraiBattles SB
                                    INNER JOIN Samurais S on SB.SamuraiId = S.Id
                                    GROUP BY S.Name, S.Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW dbo.SamuraiBattleStats");
        }
    }
}
