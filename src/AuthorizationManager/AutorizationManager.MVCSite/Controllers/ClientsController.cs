using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using AuthorizationManager.Domain.Services;
using AutorizationManager.MVCSite.Models;

namespace AutorizationManager.MVCSite.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
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
    }
}