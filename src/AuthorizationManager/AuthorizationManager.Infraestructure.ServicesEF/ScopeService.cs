using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using AuthorizationManager.Core.FromThinktectureIdentityServer.EntityFramework.DbContexts;
using AuthorizationManager.Core.FromThinktectureIdentityServer.EntityFramework.Entities;
using AuthorizationManager.Domain.Model;
using AuthorizationManager.Domain.Services;

namespace AuthorizationManager.Infraestructure.ServicesEF
{
    public class ScopeService : IScopeService
    {
        private readonly ScopeConfigurationDbContext _scopeConfigurationDbContext;

        public ScopeService(ScopeConfigurationDbContext scopeConfigurationDbContext)
        {
            _scopeConfigurationDbContext = scopeConfigurationDbContext;
        }

        public int CreateScope(string name, string displayName, string description)
        {
            var scopeRoot = ScopeRoot.CreateNew(name, displayName, description);

            _scopeConfigurationDbContext.Scopes.Add(scopeRoot.Scope);
            _scopeConfigurationDbContext.SaveChanges();

            return scopeRoot.Scope.Id;
        }

        public void DeleteScope(int id)
        {
            var scope = _scopeConfigurationDbContext.Scopes.Single(x => x.Id == id);

            _scopeConfigurationDbContext.Scopes.Remove(scope);

            _scopeConfigurationDbContext.SaveChanges();
        }

        public void AddClaim(int scopeId, string name, string description, bool allwaysIncludeInIdentityToken)
        {
            var scope = _scopeConfigurationDbContext.Scopes.Single(x => x.Id == scopeId);
            var scopeRoot = new ScopeRoot(scope);

            scopeRoot.AddClaim(name, description,allwaysIncludeInIdentityToken);

            _scopeConfigurationDbContext.SaveChanges();

        }

        public void RemoveClaim(int scopeId, int claimId)
        {
            var scope = _scopeConfigurationDbContext.Scopes.Single(x => x.Id == scopeId);
            var claim = scope.ScopeClaims.Single(x => x.Id == claimId);

            scope.ScopeClaims.Remove(claim);

            _scopeConfigurationDbContext.SaveChanges();
        }

        public IEnumerable<Scope> GetScopes()
        {
            return _scopeConfigurationDbContext.Scopes.ToArray();
        }

        public Scope FindById(int id)
        {
            var scope = _scopeConfigurationDbContext.Scopes.Single(x => x.Id == id);
            return scope;
        }

        public void UpdateScope(int id, string name, string displayName, string description)
        {
            var scope = _scopeConfigurationDbContext.Scopes.Single(x => x.Id == id);
            ScopeRoot scopeRoot = new ScopeRoot(scope);

            scopeRoot.FixName(name);
            scopeRoot.FixDisplayName(displayName);
            scopeRoot.Description(description);

            _scopeConfigurationDbContext.SaveChanges();

        }
    }
}