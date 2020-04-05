namespace LibraryManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "BookCode", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "BookCode");
        }
    }
}
