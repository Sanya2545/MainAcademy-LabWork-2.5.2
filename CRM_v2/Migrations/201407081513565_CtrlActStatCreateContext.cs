namespace CRM_v2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CtrlActStatCreateContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CtrlActStat",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Action = c.String(),
                        Controller = c.String(),
                        ActionParams = c.String(),
                        OriginalUrl = c.String(),
                        UserHost = c.String(),
                        UserAgent = c.String(),
                        HttpMtthod = c.String(),
                        Form = c.String(),
                        Query = c.String(),
                        Referer = c.String(),
                        Hearders = c.String(),
                        Cookies = c.String(),
                        UserName = c.String(),
                        StartExecution = c.DateTime(nullable: false),
                        FinishExecution = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CtrlActStat");
        }
    }
}
