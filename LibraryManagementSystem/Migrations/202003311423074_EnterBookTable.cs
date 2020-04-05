namespace LibraryManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnterBookTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.StudentBookMappings", "SubmissioDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.StudentBookMappings", "SubmissioDate", c => c.DateTime(nullable: false));
        }
    }
}
