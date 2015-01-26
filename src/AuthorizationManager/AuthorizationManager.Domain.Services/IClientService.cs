using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client = AuthorizationManager.Core.FromThinktectureIdentityServer.EntityFramework.Entities.Client;

namespace AuthorizationManager.Domain.Services
{
    public interface IClientService
    {
        int CreateWithImplicitFlow(string clientName, string clientUri, string clientRedirectUri, string logoutRedirectUri);

        int CreateWithAuthorizationCodeFlow(string clientName, string clientUri, string clientRedirectUri, string logoutRedirectUri);

        int CreateWithClientCredentialsFlow(string clientName, string clientUri);

        IEnumerable<Client> GetClients();

        void DisableClient(int id);

        void EnableClient(int id);

        string ReGenerateSecret(int id);

        string FixUri(int id, string uri);

        string FixClientName(int id, string clientName);

        void RemoveRedirectLogInUrl(int clientId, int redirectUriId);

        void AddRedirectLogInUrl(int clientId, string uri);

        void FixRedirectLogInUrl(int clientId, int redirectUriId, string uri);

        void RemoveRedirectLogOutUrl(int clientId, int redirectUriId);

        void AddRedirectLogOutUrl(int clientId, string uri);

        void FixRedirectLogOutUrl(int clientId, int redirectUriId, string uri);
        
        Client FindById(int id);
        void DeleteClient(int id);

        void AddRestrictionScope(int clientId, string name);
        
        void RemoveRestrictionScope(int clientId, int restrictionScopeId);
    }
}
