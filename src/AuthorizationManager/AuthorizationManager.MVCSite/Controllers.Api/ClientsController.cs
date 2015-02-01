using System;
using System.Web.Http;
using AuthorizationManager.Domain.Services;

namespace AuthorizationManager.MVCSite.Controllers.Api
{
    [RoutePrefix("api/Clients")]
    public class ClientsController : ApiController
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [Route("{id}")]
        [HttpPost]
        public IHttpActionResult AddRestrictionScope(int id, [FromBody]dynamic value)
        {
            string scopeName = value.scopeName;
            bool added = false;
            string error = String.Empty;
            try
            {
                _clientService.AddRestrictionScope(id, scopeName);
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
        [Route("{id}/{scopeName}")]
        [HttpDelete]
        public IHttpActionResult RemoveRestrictionScope(int id, string scopeName)
        {
            bool removed = false;
            string error = String.Empty;

            try
            {
                _clientService.RemoveRestrictionScope(id, scopeName);
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
