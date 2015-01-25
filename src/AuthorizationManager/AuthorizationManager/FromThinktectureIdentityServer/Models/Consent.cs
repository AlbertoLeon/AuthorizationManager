/*
 * Copyright (c) Dominick Baier, Brock Allen.  All rights reserved.
 * see license
 */
namespace AuthorizationManager.FromThinktectureIdentityServer.Models
{
    public class Consent
    {
        public string Subject { get; set; }
        public string ClientId { get; set; }
        public string Scopes { get; set; }
    }
}