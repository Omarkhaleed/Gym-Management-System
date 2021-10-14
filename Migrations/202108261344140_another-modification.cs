namespace OlympicGym.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class anothermodification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trainees", "Age", c => c.Int(nullable: false));
            DropColumn("dbo.Trainees", "DateOfBirth");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trainees", "DateOfBirth", c => c.DateTime(nullable: false));
            DropColumn("dbo.Trainees", "Age");
        }
    }
}
