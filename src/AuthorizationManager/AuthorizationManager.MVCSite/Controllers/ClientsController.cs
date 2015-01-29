using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using AuthorizationManager.Core.FromThinktectureIdentityServer;
using AuthorizationManager.Core.FromThinktectureIdentityServer.EntityFramework.Entities;
using AuthorizationManager.Domain.Services;
using AuthorizationManager.MVCSite.Models;
using Newtonsoft.Json;

namespace AuthorizationManager.MVCSite.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientService _clientService;
        private readonly IScopeService _scopeService;

        public ClientsController(IClientService clientService, IScopeService scopeService)
        {
            _clientService = clientService;
            _scopeService = scopeService;
        }

        // GET: Clients
        public ActionResult Index()
        {
            return View(_clientService.GetClients());
        }

        // GET: Empty
        [HttpGet]
        public ActionResult AddWithImplicitFlow()
        {
            return View(new ClientWithImplicitFlowCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddWithImplicitFlow(ClientWithImplicitFlowCreateViewModel client)
        {
            int clientId = _clientService.CreateWithImplicitFlow(client.Name,client.Uri,client.RedirectLogInUri,client.RedirectLogOutUri);

            return RedirectToAction("Details",new{id=clientId});
        }

        // GET: Empty
        [HttpGet]
        public ActionResult AddWithAuthorizationCodeFlow()
        {
            return View(new ClientWithAuthorizationCodeFlowCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddWithAuthorizationCodeFlow(ClientWithAuthorizationCodeFlowCreateViewModel client)
        {
            int clientId = _clientService.CreateWithAuthorizationCodeFlow(client.Name, client.Uri, client.RedirectLogInUri, client.RedirectLogOutUri);

            return RedirectToAction("Details", new { id = clientId });
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            return View(_clientService.FindById(id));
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(_clientService.FindById(id));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _clientService.DeleteClient(id);

            return RedirectToAction("Index");
        }

        // GET: Empty
        [HttpGet]
        public ActionResult AddWithClientCredentialsFlow()
        {
            return View(new ClientWithClientCredentialsFlowCreateViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddWithClientCredentialsFlow(ClientWithClientCredentialsFlowCreateViewModel client)
        {
            int clientId = _clientService.CreateWithClientCredentialsFlow(client.Name, client.Uri);

            return RedirectToAction("Details", new { id = clientId });
        }


        public ActionResult EditRestrictionScope(int id)
        {
            Client client = _clientService.FindById(id);
            var scopes = _scopeService.GetScopes();

            var clientRestrictionScopesViewModel = new ClientRestrictionScopesViewModel
            {
                DefaultScopes = "[" + String.Join(",", typeof(Constants.StandardScopes).GetFields().Select(x => String.Format("\'{0}\'", x.Name))) + "]",

                CustomScopes = "[" + (scopes!=null && scopes.Any()?String.Join(",", scopes.Select(x=>String.Format("\'{0}\'",x.Name)).ToArray()):"") + "]",
                ClientDisplayName = client.ClientName,
                ClientId = id,
                ScopeRestrictions = "[" + (client.ScopeRestrictions != null && client.ScopeRestrictions.Any() ? String.Join(",", client.ScopeRestrictions.Select(x => String.Format("\'{0}\'", x.Scope)).ToArray()) : "") + "]"
            };

            return View(clientRestrictionScopesViewModel);
        }
    }
}