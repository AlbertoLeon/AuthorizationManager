using System;
using System.Web.Http;
using AuthorizationManager.Domain.Services;

namespace AuthorizationManager.MVCSite.Controllers.Api
{
    [RoutePrefix("api/Scopes")]
    public class ScopesController : ApiController
    {
        private readonly IScopeService _scopeService;

        public ScopesController(IScopeService scopeService)
        {
            _scopeService = scopeService;
        }

        [Route("{id}/Claims")]
        [HttpPost]
        public IHttpActionResult AddClaim(int id, [FromBody]dynamic value)
        {
            string scopeName = value.scopeName;
            bool added = false;
            string error = String.Empty;
            try
            {
                _scopeService.AddClaim(id, scopeName,String.Empty,false);
                added = true;
            }
            catch (Exception ep)
            {
                added = false;
                error = ep.Message;
            }

            if (added)
            {
                return Ok(new {added, error});
            }
            else
            {
                return BadRequest();
            }
            
        }
        // api\Clients\RemoveRestrictionScope
        [Route("{id}/Claims/{claimName}")]
        [HttpDelete]
        public IHttpActionResult RemoveClaim(int id, string claimName)
        {
            bool removed = false;
            string error = String.Empty;

            try
            {
                _scopeService.RemoveClaim(id, claimName);
                removed = true;
            }
            catch (Exception ep)
            {
                removed = false;
                error = ep.Message;
            }

            if (removed)
            {
                return Ok(new { removed, error });
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
