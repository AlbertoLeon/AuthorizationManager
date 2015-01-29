namespace AuthorizationManager.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class scoperestrictionfk : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ClientScopeRestrictions", name: "Client_Id", newName: "ClientId");
            RenameIndex(table: "dbo.ClientScopeRestrictions", name: "IX_Client_Id", newName: "IX_ClientId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ClientScopeRestrictions", name: "IX_ClientId", newName: "IX_Client_Id");
            RenameColumn(table: "dbo.ClientScopeRestrictions", name: "ClientId", newName: "Client_Id");
        }
    }
}
