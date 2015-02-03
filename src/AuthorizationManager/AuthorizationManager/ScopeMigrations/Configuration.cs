using System.Collections.Generic;
using AuthorizationManager.Core.FromThinktectureIdentityServer;
using AuthorizationManager.Core.FromThinktectureIdentityServer.EntityFramework.Entities;
using AuthorizationManager.FromThinktectureIdentityServer.Models;

namespace AuthorizationManager.Core.ScopeMigrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AuthorizationManager.Core.FromThinktectureIdentityServer.EntityFramework.DbContexts.ScopeConfigurationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"ScopeMigrations";
        }

        protected override void Seed(AuthorizationManager.Core.FromThinktectureIdentityServer.EntityFramework.DbContexts.ScopeConfigurationDbContext context)
        {
            var scope = new Scope
            {
                Name = "idmgr",
                DisplayName = "IdentityManager",
                Type = (int)ScopeType.Resource,
                Emphasize = true,
                ShowInDiscoveryDocument = false,
                

                ScopeClaims = new List<ScopeClaim>
                {
                    new ScopeClaim {Name = Constants.ClaimTypes.Name, AlwaysIncludeInIdToken = true},
                    new ScopeClaim {Name = Constants.ClaimTypes.Role, AlwaysIncludeInIdToken = true}
                }
            };

            context.Scopes.Add(scope);
            context.SaveChanges();
        }
    }
}
