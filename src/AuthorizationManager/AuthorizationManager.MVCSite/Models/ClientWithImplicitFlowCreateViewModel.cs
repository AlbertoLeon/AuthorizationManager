using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationManager.MVCSite.Models
{
    public class ClientWithImplicitFlowCreateViewModel
    {
        public string Name { get; set; }

        public string Uri { get; set; }

        public string RedirectLogInUri { get; set; }

        public string RedirectLogOutUri { get; set; }
    }

    public class ClientWithAuthorizationCodeFlowCreateViewModel
    {
        public string Name { get; set; }

        public string Uri { get; set; }

        public string RedirectLogInUri { get; set; }

        public string RedirectLogOutUri { get; set; }
    }

    public class ClientWithClientCredentialsFlowCreateViewModel
    {
        public string Name { get; set; }

        public string Uri { get; set; }
    }
}
