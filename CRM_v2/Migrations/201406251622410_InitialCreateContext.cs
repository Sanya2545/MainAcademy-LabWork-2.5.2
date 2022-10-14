namespace CRM_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreateContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UsrStat",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.String(),
                        UserName = c.String(),
                        LastLogin = c.DateTime(nullable: false),
                        isActive = c.Boolean(nullable: false),
                        LastRole = c.String(),
                        isEnabled = c.Boolean(nullable: false),
                        LastAction = c.String(),
                        LastActionTime = c.DateTime(nullable: false),
                        ModTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UsrStat");
        }
    }
}
