namespace AuthorizationManager.MVCSite.Models
{
    public class ClaimScopesViewModel
    {
        public string DefaultClaims { get; set; }

        public string ScopeName { get; set; }

        public string CurrentClaims { get; set; }

        public int ScopeId { get; set; }
    }
}