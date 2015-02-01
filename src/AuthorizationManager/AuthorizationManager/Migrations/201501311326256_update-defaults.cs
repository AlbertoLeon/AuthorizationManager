namespace AuthorizationManager.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedefaults : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clients","EnableLocalLogin",c=>c.Boolean(defaultValue:true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clients", "EnableLocalLogin", c => c.Boolean(nullable:true));
        }
    }
}
