namespace ofs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Neg_Calc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blines", "IsNegative", c => c.Boolean(nullable: false));
            AddColumn("dbo.Blines", "Calculated", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Blines", "Calculated");
            DropColumn("dbo.Blines", "IsNegative");
        }
    }
}
