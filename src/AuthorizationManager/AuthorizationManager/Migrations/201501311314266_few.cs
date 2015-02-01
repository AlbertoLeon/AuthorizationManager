namespace AuthorizationManager.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class few : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "EnableLocalLogin", c => c.Boolean(nullable: true));
            DropColumn("dbo.Clients", "AllowLocalLogin");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clients", "AllowLocalLogin", c => c.Boolean(nullable: false));
            DropColumn("dbo.Clients", "EnableLocalLogin");
        }
    }
}
