namespace AuthorizationManager.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class names : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ClientScopeRestrictions", name: "ClientId", newName: "Client_Id");
            RenameIndex(table: "dbo.ClientScopeRestrictions", name: "IX_ClientId", newName: "IX_Client_Id");
            AddColumn("dbo.ClientSecrets", "ClientSecretType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ClientSecrets", "ClientSecretType");
            RenameIndex(table: "dbo.ClientScopeRestrictions", name: "IX_Client_Id", newName: "IX_ClientId");
            RenameColumn(table: "dbo.ClientScopeRestrictions", name: "Client_Id", newName: "ClientId");
        }
    }
}
