using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationManager.Domain.Services
{
    public interface IScopeService
    {
        int CreateScope(string name, string displayName, string description);

        void DeleteScope(int id);

        void AddClaim(int scopeId, string name, string description, bool allwaysIncludeInIdentityToken);

        void RemoveClaim(int scopeId, int claimId);
    }
}
