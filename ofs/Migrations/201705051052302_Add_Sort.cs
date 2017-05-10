namespace ofs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Sort : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Balances");
            AddColumn("dbo.Blines", "CodeSort", c => c.Int(nullable: false));
            AlterColumn("dbo.Balances", "Inn", c => c.String(nullable: false, maxLength: 12));
            AlterColumn("dbo.Balances", "Code", c => c.String(nullable: false, maxLength: 4));
            AddPrimaryKey("dbo.Balances", new[] { "Quater", "Year", "Inn", "Code" });
            CreateIndex("dbo.Balances", "Inn");
            CreateIndex("dbo.Balances", "Code");
            AddForeignKey("dbo.Balances", "Code", "dbo.Blines", "Code", cascadeDelete: true);
            AddForeignKey("dbo.Balances", "Inn", "dbo.Clients", "Inn", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Balances", "Inn", "dbo.Clients");
            DropForeignKey("dbo.Balances", "Code", "dbo.Blines");
            DropIndex("dbo.Balances", new[] { "Code" });
            DropIndex("dbo.Balances", new[] { "Inn" });
            DropPrimaryKey("dbo.Balances");
            AlterColumn("dbo.Balances", "Code", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Balances", "Inn", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Blines", "CodeSort");
            AddPrimaryKey("dbo.Balances", new[] { "Quater", "Year", "Inn", "Code" });
        }
    }
}
