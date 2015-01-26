using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AuthorizationManager.Core.FromThinktectureIdentityServer.EntityFramework.Entities;
using AuthorizationManager.Domain.Services;
using AutorizationManager.MVCSite.Models;

namespace AutorizationManager.MVCSite.Controllers
{
    public class ScopesController : Controller
    {
        private readonly IScopeService _scopeService;

        public ScopesController(IScopeService scopeService)
        {
            _scopeService = scopeService;
        }

        // GET: Scopes
        public ActionResult Index()
        {
            return View(_scopeService.GetScopes().Select(scope =>
            {
                return new ScopeViewModel
                {
                    Id = scope.Id,
                    Description = scope.Description,
                    DisplayName = scope.DisplayName,
                    Name = scope.Name
                };
            }));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new ScopeViewModel());
        }

        [HttpPost]
        public ActionResult Create(ScopeViewModel scope)
        {
            _scopeService.CreateScope(scope.Name, scope.DisplayName, scope.Description);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var scope = _scopeService.FindById(id);

            return View(new ScopeViewModel
            {
                Id = scope.Id,
                Description = scope.Description,
                DisplayName = scope.DisplayName,
                Name = scope.Name
            });
        }

        [HttpPost]
        public ActionResult Edit(int id, ScopeViewModel scope)
        {
            _scopeService.UpdateScope(id, scope.Name, scope.DisplayName, scope.Description);
            return RedirectToAction("Details",new{id});
        }

        public ActionResult Details(int id)
        {
            var scope = _scopeService.FindById(id);

            return View(new ScopeViewModel
            {
                Id = scope.Id,
                Description = scope.Description,
                DisplayName = scope.DisplayName,
                Name = scope.Name
            });
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var scope = _scopeService.FindById(id);

            return View(new ScopeViewModel
            {
                Id = scope.Id,
                Description = scope.Description,
                DisplayName = scope.DisplayName,
                Name = scope.Name
            });
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _scopeService.DeleteScope(id);
            return RedirectToAction("Index");
        }
    }
}