namespace OlympicGym.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sports", "TotalTrainees", c => c.Int());
            AddColumn("dbo.Sports", "TotalCoaches", c => c.Int());
            AlterColumn("dbo.Plans", "PlanName", c => c.String(nullable: false, maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Plans", "PlanName", c => c.String(nullable: false, maxLength: 8));
            DropColumn("dbo.Sports", "TotalCoaches");
            DropColumn("dbo.Sports", "TotalTrainees");
        }
    }
}
