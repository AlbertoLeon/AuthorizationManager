// /*
//  * Copyright 2015 Alberto León Tiscar
//  *
//  * Licensed under the Apache License, Version 2.0 (the "License");
//  * you may not use this file except in compliance with the License.
//  * You may obtain a copy of the License at
//  *
//  *   http://www.apache.org/licenses/LICENSE-2.0
//  *
//  * Unless required by applicable law or agreed to in writing, software
//  * distributed under the License is distributed on an "AS IS" BASIS,
//  * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  * See the License for the specific language governing permissions and
//  * limitations under the License.
//  */

using System;
using System.Linq;
using System.Security.Cryptography;
using AuthorizationManager.Core.FromThinktectureIdentityServer.EntityFramework.Entities;
using AuthorizationManager.FromThinktectureIdentityServer.Models;
using Client = AuthorizationManager.Core.FromThinktectureIdentityServer.EntityFramework.Entities.Client;

namespace AuthorizationManager.Domain.Model
{
    public class ClientRoot
    {
        protected ClientRoot()
        {
            
        }

        public ClientRoot(Client client)
        {
            this.Client = client;
        }

        protected void CreateClient(Flows flow, string clientName, string clientUri)
        {
            var client = new Client();
            client.ClientId = Guid.NewGuid().ToString();
            client.ClientName = clientName;
            client.ClientUri = clientUri;
            client.Flow = flow;

            this.Client = client;

            client.ClientSecrets.Add(this.GenerateSecret());
        }

        /// <summary>
        /// The client persisted state
        /// </summary>
        public Client Client { get; private set; }

        public void AddRestrictionScope(string scopeName)
        {
            this.Client.ScopeRestrictions.Add(
                    new ClientScopeRestriction
                    {
                        Scope = scopeName,
                        ClientId = this.Client.Id
                    }
                );
        }

        public ClientScopeRestriction RemoveRestrictionScope(string scopeName)
        {
            var restrictionScope = this.Client.ScopeRestrictions.Single(x => x.Scope == scopeName);
            
            this.Client.ScopeRestrictions.Remove(restrictionScope);

            return restrictionScope;
        }

        /// <summary>
        /// Creates a client with the implicity flow (brower client app)
        /// </summary>
        /// <returns>Returns a client aggreate root</returns>
        public static ClientRoot CreateWithImplicitFlow(string clientName, string clientUri, string clientRedirectUri, string logoutRedirectUri)
        {
            var clientRoot = new ClientRoot();
            clientRoot.CreateClient(Flows.Implicit, clientName, clientUri);
            clientRoot.Client.RedirectUris.Add(
                    new ClientRedirectUri
                    {
                        Uri = clientRedirectUri
                    }
                );

            clientRoot.Client.PostLogoutRedirectUris.Add(
                    new ClientPostLogoutRedirectUri
                    {
                        Uri = logoutRedirectUri
                    }
                );
            return clientRoot;
        }

        /// <summary>
        /// Creates a client with the code flow (server side up)
        /// </summary>
        /// <returns>Returns a client aggreate root</returns>
        public static ClientRoot CreateWithAuthorizationCodeFlow(string clientName, string clientUri,  string clientRedirectUri, string logoutRedirectUri)
        {
            var clientRoot = new ClientRoot();
            clientRoot.CreateClient(Flows.Code, clientName, clientUri);
            
            clientRoot.Client.RedirectUris.Add(
                    new ClientRedirectUri
                    {
                        Uri = clientRedirectUri
                    }
                );

            clientRoot.Client.PostLogoutRedirectUris.Add(
                new ClientPostLogoutRedirectUri
                {
                    Uri = logoutRedirectUri
                }
            );

            return clientRoot;
        }

        /// <summary>
        /// Creates a client with the client credentials flow
        /// </summary>
        /// <returns>Returns a client aggreate root</returns>
        public static ClientRoot CreateWithClientCredentials(string clientName, string clientUri)
        {
            var clientRoot = new ClientRoot();
            clientRoot.CreateClient(Flows.ClientCredentials, clientName, clientUri);
            
            return clientRoot;
        }

        private ClientSecret GenerateSecret()
        {
            RandomNumberGenerator cryptoRandomDataGenerator = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[32];
            cryptoRandomDataGenerator.GetBytes(buffer);
            string uniq = Convert.ToBase64String(buffer);

            ClientSecret clientSecret = new ClientSecret();
            clientSecret.Value = uniq;
            clientSecret.Description = "Client secret for " + this.Client.ClientName;

            return clientSecret;
        }

        public void Disable()
        {
            this.Client.Enabled = false;
        }

        public void Enable()
        {
            this.Client.Enabled = true;
        }

        public string ReGenerateSecret()
        {
            var clientSecrets = this.Client.ClientSecrets.ToArray();
            foreach (var clientSecret in clientSecrets)
            {
                this.Client.ClientSecrets.Remove(clientSecret);
            }

            var newClientSecret = this.GenerateSecret();

            this.Client.ClientSecrets.Add(newClientSecret);

            return newClientSecret.Value;
        }
    }
}