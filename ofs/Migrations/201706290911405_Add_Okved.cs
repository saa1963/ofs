namespace ofs.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Okved : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "Okved", c => c.String(maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "Okved");
        }
    }
}
