namespace OlympicGym.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class last2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Trainees", "AdminId", "dbo.Admins");
            DropForeignKey("dbo.Trainees", "CoachId", "dbo.Coaches");
            DropForeignKey("dbo.Trainees", "PlanId", "dbo.Plans");
            DropForeignKey("dbo.Trainees", "SportId", "dbo.Sports");
            DropForeignKey("dbo.Coaches", "SportId", "dbo.Sports");
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        ReportId = c.Int(nullable: false, identity: true),
                        DateOfStarting = c.DateTime(nullable: false),
                        DateOfEnding = c.DateTime(nullable: false),
                        Num_Of_Trainees = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        TotalPrice = c.Int(nullable: false),
                        CoachId = c.Int(nullable: false),
                        SportId = c.Int(),
                        AdminId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReportId)
                .ForeignKey("dbo.Admins", t => t.AdminId)
                .ForeignKey("dbo.Coaches", t => t.CoachId)
                .ForeignKey("dbo.Sports", t => t.SportId)
                .Index(t => t.CoachId)
                .Index(t => t.SportId)
                .Index(t => t.AdminId);
            
            AddForeignKey("dbo.Trainees", "AdminId", "dbo.Admins", "AdminId");
            AddForeignKey("dbo.Trainees", "CoachId", "dbo.Coaches", "CoachId");
            AddForeignKey("dbo.Trainees", "PlanId", "dbo.Plans", "PlanId");
            AddForeignKey("dbo.Trainees", "SportId", "dbo.Sports", "SportId");
            AddForeignKey("dbo.Coaches", "SportId", "dbo.Sports", "SportId");
            DropColumn("dbo.Coaches", "DateOfEnding");
            DropColumn("dbo.Coaches", "Trainees_In_Month");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Coaches", "Trainees_In_Month", c => c.Int(nullable: false));
            AddColumn("dbo.Coaches", "DateOfEnding", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.Coaches", "SportId", "dbo.Sports");
            DropForeignKey("dbo.Trainees", "SportId", "dbo.Sports");
            DropForeignKey("dbo.Trainees", "PlanId", "dbo.Plans");
            DropForeignKey("dbo.Trainees", "CoachId", "dbo.Coaches");
            DropForeignKey("dbo.Trainees", "AdminId", "dbo.Admins");
            DropForeignKey("dbo.Reports", "SportId", "dbo.Sports");
            DropForeignKey("dbo.Reports", "CoachId", "dbo.Coaches");
            DropForeignKey("dbo.Reports", "AdminId", "dbo.Admins");
            DropIndex("dbo.Reports", new[] { "AdminId" });
            DropIndex("dbo.Reports", new[] { "SportId" });
            DropIndex("dbo.Reports", new[] { "CoachId" });
            DropTable("dbo.Reports");
            AddForeignKey("dbo.Coaches", "SportId", "dbo.Sports", "SportId", cascadeDelete: true);
            AddForeignKey("dbo.Trainees", "SportId", "dbo.Sports", "SportId", cascadeDelete: true);
            AddForeignKey("dbo.Trainees", "PlanId", "dbo.Plans", "PlanId", cascadeDelete: true);
            AddForeignKey("dbo.Trainees", "CoachId", "dbo.Coaches", "CoachId", cascadeDelete: true);
            AddForeignKey("dbo.Trainees", "AdminId", "dbo.Admins", "AdminId", cascadeDelete: true);
        }
    }
}
