namespace LingoBingoGen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LingoWords",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Word = c.String(nullable: false, maxLength: 45),
                        Category = c.String(nullable: false, maxLength: 45),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LingoWords");
        }
    }
}
