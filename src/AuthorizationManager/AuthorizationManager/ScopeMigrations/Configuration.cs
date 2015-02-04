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
                DisplayName = "Identity Manager",
                Type = (int)ScopeType.Resource,
                Emphasize = true,
                ShowInDiscoveryDocument = false,
                Enabled = true,

                ScopeClaims = new List<ScopeClaim>
                {
                    new ScopeClaim {Name = Constants.ClaimTypes.Name, AlwaysIncludeInIdToken = true},
                    new ScopeClaim {Name = Constants.ClaimTypes.Role, AlwaysIncludeInIdToken = true}
                }
            };

            context.Scopes.Add(scope);


            context.Scopes.Add(new Scope
            {
                Name = Constants.StandardScopes.OpenId,
                DisplayName = Constants.StandardScopes.OpenId,
                Required = true,
                Type = (int)ScopeType.Identity,
                Enabled = true,

                ScopeClaims = new List<ScopeClaim>
                    {
                        new ScopeClaim{Name=Constants.ClaimTypes.Subject, AlwaysIncludeInIdToken= true}
                    }
            });

            context.Scopes.Add(new Scope
            {
                Name = Constants.StandardScopes.Profile,
                DisplayName = Constants.StandardScopes.Profile,
                Type= (int)ScopeType.Identity,
                Emphasize = true,
                Enabled = true,
                ScopeClaims = (Constants.ScopeToClaimsMapping[Constants.StandardScopes.Profile].Select(claim => new ScopeClaim { Name = claim}).ToList())
            });

            context.Scopes.Add(new Scope
            {
                Name = Constants.StandardScopes.Profile,
                DisplayName = "Profile always include",
                Type= (int)ScopeType.Identity,
                Emphasize = true,
                ScopeClaims = (Constants.ScopeToClaimsMapping[Constants.StandardScopes.Profile].Select(claim => new ScopeClaim{Name =claim,AlwaysIncludeInIdToken=true} )).ToList()
            });

            context.Scopes.Add(new Scope
            {
                Name = Constants.StandardScopes.Email,
                DisplayName = Constants.StandardScopes.Email,
                Type= (int)ScopeType.Identity,
                Emphasize = true,
                Enabled = true,
                ScopeClaims = (Constants.ScopeToClaimsMapping[Constants.StandardScopes.Email].Select(claim => new ScopeClaim{Name =claim,AlwaysIncludeInIdToken=true}).ToList())
            });

            context.Scopes.Add(new Scope
            {
                Name = Constants.StandardScopes.Email,
                DisplayName = "Email always include",
                Type= (int)ScopeType.Identity,
                Emphasize = true,
                Enabled = true,
                ScopeClaims = (Constants.ScopeToClaimsMapping[Constants.StandardScopes.Email].Select(claim => new ScopeClaim{Name =claim,AlwaysIncludeInIdToken=true}).ToList())
            });

            context.Scopes.Add(new Scope
            {
                Name = Constants.StandardScopes.Phone,
                DisplayName = Constants.StandardScopes.Phone,
                Type= (int)ScopeType.Identity,
                Emphasize = true,
                Enabled = true,
                ScopeClaims = (Constants.ScopeToClaimsMapping[Constants.StandardScopes.Phone].Select(claim => new ScopeClaim{Name =claim}).ToList())
            });

            context.Scopes.Add(new Scope
            {
                Name = Constants.StandardScopes.Phone,
                DisplayName = "Phone always include",
                Type= (int)ScopeType.Identity,
                Emphasize = true,
                Enabled = true,
                ScopeClaims = (Constants.ScopeToClaimsMapping[Constants.StandardScopes.Phone].Select(claim => new ScopeClaim{Name =claim,AlwaysIncludeInIdToken=true}).ToList())
            });

            context.Scopes.Add(new Scope
            {
                Name = Constants.StandardScopes.Address,
                DisplayName = Constants.StandardScopes.Address,
                Type= (int)ScopeType.Identity,
                Emphasize = true,
                Enabled = true,
                ScopeClaims = (Constants.ScopeToClaimsMapping[Constants.StandardScopes.Address].Select(claim => new ScopeClaim{Name =claim})).ToList()
            });

            context.Scopes.Add(new Scope
            {
                Name = Constants.StandardScopes.Address,
                DisplayName = "Address always include",
                Type= (int)ScopeType.Identity,
                Emphasize = true,
                Enabled = true,
                ScopeClaims = (Constants.ScopeToClaimsMapping[Constants.StandardScopes.Address].Select(claim => new ScopeClaim{Name =claim,AlwaysIncludeInIdToken=true}).ToList())
            });

            context.Scopes.Add(new Scope
            {
                Name = "all_claims",
                DisplayName = "All claims",
                Type= (int)ScopeType.Identity,
                Emphasize = true,
                Enabled = true,
                IncludeAllClaimsForUser = true
            });

            context.Scopes.Add(new Scope
            {
                Name = "roles",
                DisplayName = "roles",
                Type= (int)ScopeType.Identity,
                Emphasize = true,
                Enabled = true,
                ScopeClaims = new List<ScopeClaim> 
                    {
                        new ScopeClaim{Name=Constants.ClaimTypes.Role}
                    }
            });

            context.Scopes.Add(new Scope
            {
                Name = "roles",
                DisplayName = "Roles always include",
                Type= (int)ScopeType.Identity,
                Emphasize = true,
                Enabled = true,
                ScopeClaims = new List<ScopeClaim>
                    {
                        new ScopeClaim{Name=Constants.ClaimTypes.Role, AlwaysIncludeInIdToken = true}
                    }
            });

            context.Scopes.Add(new Scope
            {
                Name = Constants.StandardScopes.OfflineAccess,
                DisplayName = Constants.StandardScopes.OfflineAccess,
                Type= (int)ScopeType.Resource,
                Emphasize = true,
                Enabled = true,
            });
            context.SaveChanges();
        }
    }
}
