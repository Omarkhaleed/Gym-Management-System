namespace OlympicGym.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialcreate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trainees", "classes", c => c.Int(nullable: false));
            AlterColumn("dbo.Sports", "SportName", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sports", "SportName", c => c.String(nullable: false, maxLength: 8));
            DropColumn("dbo.Trainees", "classes");
        }
    }
}
