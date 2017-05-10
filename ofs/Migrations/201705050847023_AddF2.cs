namespace ofs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddF2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Balances");
            AddColumn("dbo.Balances", "Code", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Blines", "Code", c => c.String(maxLength: 4));
            AddPrimaryKey("dbo.Balances", new[] { "Quater", "Year", "Inn", "Code" });
            DropColumn("dbo.Balances", "Part");
            DropColumn("dbo.Balances", "Line");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Balances", "Line", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Balances", "Part", c => c.String(nullable: false, maxLength: 128));
            DropPrimaryKey("dbo.Balances");
            DropColumn("dbo.Blines", "Code");
            DropColumn("dbo.Balances", "Code");
            AddPrimaryKey("dbo.Balances", new[] { "Quater", "Year", "Inn", "Part", "Line" });
        }
    }
}
