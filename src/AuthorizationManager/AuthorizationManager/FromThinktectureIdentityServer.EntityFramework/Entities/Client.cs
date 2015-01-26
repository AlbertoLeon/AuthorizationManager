/*
 * Copyright 2014 Dominick Baier, Brock Allen
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AuthorizationManager.FromThinktectureIdentityServer.Models;

namespace AuthorizationManager.Core.FromThinktectureIdentityServer.EntityFramework.Entities
{
    public class Client
    {
        public Client()
        {
            this.ClientSecrets = new HashSet<ClientSecret>();
            this.RedirectUris = new HashSet<ClientRedirectUri>();
            this.PostLogoutRedirectUris = new HashSet<ClientPostLogoutRedirectUri>();
            this.Claims = new HashSet<ClientClaim>();
            this.ScopeRestrictions = new HashSet<ClientScopeRestriction>();
            this.IdentityProviderRestrictions = new HashSet<ClientIdPRestriction>();
            this.CustomGrantTypeRestrictions = new HashSet<ClientGrantTypeRestriction>();
        }

        [Key]
        public int Id { get; set; }

        public bool Enabled { get; set; }

        [Required]
        [StringLength(200)]
        public string ClientId { get; set; }

        public virtual ICollection<ClientSecret> ClientSecrets { get; set; }
        
        [Required]
        [StringLength(200)]
        public string ClientName { get; set; }

        [StringLength(2000)]
        public string ClientUri { get; set; }

        public string LogoUri { get; set; }

        public bool RequireConsent { get; set; }
        public bool AllowRememberConsent { get; set; }

        public Flows Flow { get; set; }

        public virtual ICollection<ClientRedirectUri> RedirectUris { get; set; }

        public virtual ICollection<ClientPostLogoutRedirectUri> PostLogoutRedirectUris { get; set; }

        public virtual ICollection<ClientScopeRestriction> ScopeRestrictions { get; set; }

        // in seconds
        [Range(0, Int32.MaxValue)]
        public int IdentityTokenLifetime { get; set; }
        [Range(0, Int32.MaxValue)]
        public int AccessTokenLifetime { get; set; }
        [Range(0, Int32.MaxValue)]
        public int AuthorizationCodeLifetime { get; set; }
        
        [Range(0, Int32.MaxValue)]
        public int AbsoluteRefreshTokenLifetime { get; set; }
        [Range(0, Int32.MaxValue)]
        public int SlidingRefreshTokenLifetime { get; set; }

        public TokenUsage RefreshTokenUsage { get; set; }
        public TokenExpiration RefreshTokenExpiration { get; set; }
        
        public SigningKeyTypes IdentityTokenSigningKeyType { get; set; }
        public AccessTokenType AccessTokenType { get; set; }

        public bool AllowLocalLogin { get; set; }

        public virtual ICollection<ClientIdPRestriction> IdentityProviderRestrictions { get; set; }
        
        public bool IncludeJwtId { get; set; }

        public virtual ICollection<ClientClaim> Claims { get; set; }
        public bool AlwaysSendClientClaims { get; set; }
        public bool PrefixClientClaims { get; set; }

        public virtual ICollection<ClientGrantTypeRestriction> CustomGrantTypeRestrictions { get; set; }
    }
}
