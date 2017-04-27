using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using $safeprojectname$.Models;

namespace $safeprojectname$.Controllers
{
    [AllowAnonymous]
    public class DonationProfilesController : Controller
    {
        private PetaByteContext db = new PetaByteContext();

        // GET: DonationProfiles
        public ActionResult Index()
        {
            var donationProfiles = db.DonationProfiles.Include(d => d.Donation);
            return View(donationProfiles.ToList());
        }
        
    }
}
