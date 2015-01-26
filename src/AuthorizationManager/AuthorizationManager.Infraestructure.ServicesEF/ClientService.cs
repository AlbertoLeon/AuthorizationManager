using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthorizationManager.Core.FromThinktectureIdentityServer.EntityFramework.DbContexts;
using AuthorizationManager.Core.FromThinktectureIdentityServer.EntityFramework.Entities;
using AuthorizationManager.Domain.Model;
using AuthorizationManager.Domain.Services;

namespace AuthorizationManager.Infraestructure.ServicesEF
{
    public class ClientService : IClientService
    {
        private readonly ClientConfigurationDbContext _clientConfigurationDbContext;

        public ClientService(ClientConfigurationDbContext clientConfigurationDbContext)
        {
            _clientConfigurationDbContext = clientConfigurationDbContext;
        }

        public int CreateWithImplicitFlow(string clientName, string clientUri, string clientRedirectUri, string logoutRedirectUri)
        {
            var clientRoot = ClientRoot.CreateWithImplicitFlow(clientName, clientUri, clientRedirectUri,
                logoutRedirectUri);

            _clientConfigurationDbContext.Clients.Add(clientRoot.Client);
            _clientConfigurationDbContext.SaveChanges();

            return clientRoot.Client.Id;
        }

        public int CreateWithAuthorizationCodeFlow(string clientName, string clientUri, string clientRedirectUri, string logoutRedirectUri)
        {
            var clientRoot = ClientRoot.CreateWithAuthorizationCodeFlow(clientName, clientUri, clientRedirectUri,
                logoutRedirectUri);

            _clientConfigurationDbContext.Clients.Add(clientRoot.Client);
            _clientConfigurationDbContext.SaveChanges();

            return clientRoot.Client.Id;
        }

        public int CreateWithClientCredentialsFlow(string clientName, string clientUri)
        {
            var clientRoot = ClientRoot.CreateWithClientCredentials(clientName, clientUri);

            _clientConfigurationDbContext.Clients.Add(clientRoot.Client);
            _clientConfigurationDbContext.SaveChanges();

            return clientRoot.Client.Id;
        }

        public IEnumerable<Client> GetClients()
        {
            return _clientConfigurationDbContext.Clients.ToArray();
        }

        public void DisableClient(int id)
        {
            var client = _clientConfigurationDbContext.Clients.Single(x => x.Id == id);
            var clientRoot = new ClientRoot(client);
            clientRoot.Disable();

            _clientConfigurationDbContext.SaveChanges();
        }

        public void EnableClient(int id)
        {
            var client = _clientConfigurationDbContext.Clients.Single(x => x.Id == id);
            var clientRoot = new ClientRoot(client);
            clientRoot.Enable();

            _clientConfigurationDbContext.SaveChanges();
        }

        public string ReGenerateSecret(int id)
        {
            var client = _clientConfigurationDbContext.Clients.Single(x => x.Id == id);
            var clientRoot = new ClientRoot(client);
            string clientSecret = clientRoot.ReGenerateSecret();

            _clientConfigurationDbContext.SaveChanges();

            return clientSecret;
        }

        public string FixUri(int id, string uri)
        {
            throw new NotImplementedException();
        }

        public string FixClientName(int id, string clientName)
        {
            throw new NotImplementedException();
        }

        public void RemoveRedirectLogInUrl(int clientId, int redirectUriId)
        {
            throw new NotImplementedException();
        }

        public void AddRedirectLogInUrl(int clientId, string uri)
        {
            throw new NotImplementedException();
        }

        public void FixRedirectLogInUrl(int clientId, int redirectUriId, string uri)
        {
            throw new NotImplementedException();
        }

        public void RemoveRedirectLogOutUrl(int clientId, int redirectUriId)
        {
            throw new NotImplementedException();
        }

        public void AddRedirectLogOutUrl(int clientId, string uri)
        {
            throw new NotImplementedException();
        }

        public void FixRedirectLogOutUrl(int clientId, int redirectUriId, string uri)
        {
            throw new NotImplementedException();
        }

        public Client FindById(int id)
        {
            return _clientConfigurationDbContext.Clients
                .Include("ClientSecrets")
                .Include("RedirectUris")
                .Include("PostLogoutRedirectUris")
                .Include("ScopeRestrictions")
                .Include("IdentityProviderRestrictions")
                .Include("Claims")
                .Include("CustomGrantTypeRestrictions")
                
                .Single(x => x.Id == id);
        }

        public void DeleteClient(int id)
        {
            var client = _clientConfigurationDbContext.Clients.Single(x => x.Id == id);

            _clientConfigurationDbContext.Clients.Remove(client);

            _clientConfigurationDbContext.SaveChanges();
        }
    }
}
