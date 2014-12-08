namespace TwitBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class twitdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Twits", "date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Twits", "date");
        }
    }
}
