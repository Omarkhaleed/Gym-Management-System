namespace OlympicGym.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 10),
                        SecondName = c.String(nullable: false, maxLength: 10),
                        Password = c.Int(nullable: false),
                        PhoneNumber = c.String(nullable: false, maxLength: 11),
                    })
                .PrimaryKey(t => t.AdminId);
            
            CreateTable(
                "dbo.Trainees",
                c => new
                    {
                        TraineeId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 10),
                        SecondName = c.String(nullable: false, maxLength: 10),
                        PhoneNumber = c.String(nullable: false, maxLength: 11),
                        Gender = c.String(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        DateOfRegister = c.DateTime(nullable: false),
                        Record = c.String(),
                        SportId = c.Int(nullable: false),
                        PlanId = c.Int(nullable: false),
                        AdminId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TraineeId)
                .ForeignKey("dbo.Admins", t => t.AdminId, cascadeDelete: true)
                .ForeignKey("dbo.Plans", t => t.PlanId, cascadeDelete: true)
                .ForeignKey("dbo.Sports", t => t.SportId, cascadeDelete: true)
                .Index(t => t.SportId)
                .Index(t => t.PlanId)
                .Index(t => t.AdminId);
            
            CreateTable(
                "dbo.Plans",
                c => new
                    {
                        PlanId = c.Int(nullable: false, identity: true),
                        PlanName = c.String(nullable: false, maxLength: 8),
                        PlanClasses = c.Int(nullable: false),
                        PlanDescription = c.String(nullable: false),
                        PlanPrice = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PlanId);
            
            CreateTable(
                "dbo.Sports",
                c => new
                    {
                        SportId = c.Int(nullable: false, identity: true),
                        SportName = c.String(nullable: false, maxLength: 8),
                    })
                .PrimaryKey(t => t.SportId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trainees", "SportId", "dbo.Sports");
            DropForeignKey("dbo.Trainees", "PlanId", "dbo.Plans");
            DropForeignKey("dbo.Trainees", "AdminId", "dbo.Admins");
            DropIndex("dbo.Trainees", new[] { "AdminId" });
            DropIndex("dbo.Trainees", new[] { "PlanId" });
            DropIndex("dbo.Trainees", new[] { "SportId" });
            DropTable("dbo.Sports");
            DropTable("dbo.Plans");
            DropTable("dbo.Trainees");
            DropTable("dbo.Admins");
        }
    }
}
