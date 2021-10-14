namespace OlympicGym.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Trainees", "Coach_CoachId", "dbo.Coaches");
            DropIndex("dbo.Trainees", new[] { "Coach_CoachId" });
            RenameColumn(table: "dbo.Trainees", name: "Coach_CoachId", newName: "CoachId");
            AlterColumn("dbo.Trainees", "CoachId", c => c.Int(nullable: false));
            CreateIndex("dbo.Trainees", "CoachId");
            AddForeignKey("dbo.Trainees", "CoachId", "dbo.Coaches", "CoachId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trainees", "CoachId", "dbo.Coaches");
            DropIndex("dbo.Trainees", new[] { "CoachId" });
            AlterColumn("dbo.Trainees", "CoachId", c => c.Int());
            RenameColumn(table: "dbo.Trainees", name: "CoachId", newName: "Coach_CoachId");
            CreateIndex("dbo.Trainees", "Coach_CoachId");
            AddForeignKey("dbo.Trainees", "Coach_CoachId", "dbo.Coaches", "CoachId");
        }
    }
}
