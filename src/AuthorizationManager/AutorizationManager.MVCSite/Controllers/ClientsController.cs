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
        public ActionResult CreateWithImplicitFlow()
        {
            return View(new ClientWithImplicitFlowCreateViewModel());
        }

        [HttpPost]
        public ActionResult CreateWithImplicitFlow(ClientWithImplicitFlowCreateViewModel client)
        {
            int id = _clientService.CreateWithClientCredentialsFlow(client.Name,client.Uri,client.RedirectLogInUri,client.RedirectLogOutUri);

            return RedirectToAction("Details");
        }

        [HttpGet]
        public ActionResult Details(string clientId)
        {
            return View(_clientService.FindById(clientId));
        }


    }
}