namespace ofs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddF2_1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Blines");
            AlterColumn("dbo.Blines", "Code", c => c.String(nullable: false, maxLength: 4));
            AddPrimaryKey("dbo.Blines", "Code");
            DropColumn("dbo.Blines", "Part");
            DropColumn("dbo.Blines", "Line");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Blines", "Line", c => c.String(nullable: false, maxLength: 2));
            AddColumn("dbo.Blines", "Part", c => c.String(nullable: false, maxLength: 1));
            DropPrimaryKey("dbo.Blines");
            AlterColumn("dbo.Blines", "Code", c => c.String(maxLength: 4));
            AddPrimaryKey("dbo.Blines", new[] { "Part", "Line" });
        }
    }
}
