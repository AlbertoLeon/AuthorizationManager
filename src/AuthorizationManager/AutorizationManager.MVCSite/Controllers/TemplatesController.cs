using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutorizationManager.MVCSite.Controllers
{
    public class TemplatesController : Controller
    {
        public ActionResult Template(string templateName)
        {
            return View(templateName);
        }
    }
}