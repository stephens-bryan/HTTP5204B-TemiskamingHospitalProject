using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using $safeprojectname$.Models;

namespace $safeprojectname$.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        PetaByteContext db = new PetaByteContext();
        
        // GET: Home

        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        protected override void HandleUnknownAction(string actionName)
        {
            ViewData["actionName"] = actionName;
            View().ExecuteResult(ControllerContext);

        }
    }
}