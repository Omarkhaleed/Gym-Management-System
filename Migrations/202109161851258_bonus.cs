namespace OlympicGym.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bonus : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Trainees", "Record", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Trainees", "Record", c => c.String());
        }
    }
}
