using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthorizationManager.Core.FromThinktectureIdentityServer.EntityFramework.Entities;
using AuthorizationManager.FromThinktectureIdentityServer.Models;

namespace AuthorizationManager.Domain.Model
{
    public class ScopeRoot
    {
        protected ScopeRoot()
        {
            this.Scope = new Scope();
        }

        public ScopeRoot(Scope scope)
        {
            this.Scope = scope;
        }

        public Scope Scope { get; set; }

        public void AddClaim(string name, string description, bool alwaysIncludeInToken)
        {
            this.Scope.ScopeClaims.Add(
                new ScopeClaim
                {
                    AlwaysIncludeInIdToken = alwaysIncludeInToken,
                    Name = name,
                    Description = description
                }
            );
        }

        public void RemoveClaim(int claimId)
        {
            var claim = this.Scope.ScopeClaims.Single(x => x.Id == claimId);

            this.Scope.ScopeClaims.Remove(claim);
        }

        public static ScopeRoot CreateNew(string name, string displayName, string description)
        {
            var scopeRoot = new ScopeRoot();

            scopeRoot.Scope.Name = name;
            scopeRoot.Scope.DisplayName = displayName;
            scopeRoot.Scope.Description = description;
            scopeRoot.Scope.Type = (int)ScopeType.Resource;
            scopeRoot.Scope.Enabled = true;
            scopeRoot.Scope.Emphasize = true;
            scopeRoot.Scope.IncludeAllClaimsForUser = false;
            scopeRoot.Scope.ShowInDiscoveryDocument = false;

            return scopeRoot;
        }

        public void FixName(string name)
        {
            this.Scope.Name = name;
        }

        public void FixDisplayName(string displayName)
        {
            this.Scope.DisplayName = displayName;
        }

        public void Description(string description)
        {
            this.Scope.Description = description;
        }
    }
}
