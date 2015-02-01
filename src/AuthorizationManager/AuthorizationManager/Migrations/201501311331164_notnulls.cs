namespace AuthorizationManager.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notnulls : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clients", "EnableLocalLogin", c => c.Boolean(nullable: false));
        }

        public override void Down()
        {
            AlterColumn("dbo.Clients", "EnableLocalLogin", c => c.Boolean(nullable: true));
        }
    }
}
