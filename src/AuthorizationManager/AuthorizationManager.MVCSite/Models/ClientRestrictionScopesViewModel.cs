using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthorizationManager.Core.FromThinktectureIdentityServer.EntityFramework.Entities;

namespace AuthorizationManager.MVCSite.Models
{
    public class ClientRestrictionScopesViewModel
    {
        public string DefaultScopes { get; set; }

        public string CustomScopes { get; set; }

        public string ClientDisplayName { get; set; }

        public int ClientId { get; set; }

        public string ScopeRestrictions { get; set; }
    }
}
