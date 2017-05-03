namespace ofs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Balances",
                c => new
                    {
                        Quater = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        Inn = c.String(nullable: false, maxLength: 128),
                        Part = c.String(nullable: false, maxLength: 128),
                        Line = c.String(nullable: false, maxLength: 128),
                        Sm = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Quater, t.Year, t.Inn, t.Part, t.Line });
            
            CreateTable(
                "dbo.Blines",
                c => new
                    {
                        Part = c.String(nullable: false, maxLength: 1),
                        Line = c.String(nullable: false, maxLength: 2),
                        Name = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => new { t.Part, t.Line });
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Inn = c.String(nullable: false, maxLength: 12),
                        Name = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.Inn);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Clients");
            DropTable("dbo.Blines");
            DropTable("dbo.Balances");
        }
    }
}
