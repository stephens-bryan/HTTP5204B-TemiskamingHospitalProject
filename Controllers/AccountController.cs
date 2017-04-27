using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using $safeprojectname$.Models;
using System.Net.Mail; // library specific for sending email via Simple Mail Transfer Protocol (SMTP); includes classes for creation of email message & passing of message to an SMTP server for sending
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Net;
using System.Data.Entity;

namespace $safeprojectname$.Controllers
{
    /*
     * Note(bryanstephens): next step - integrate Membership provider into projects to handle user validation & login (more secure; classes predefined to handle logging in, password reset, creation of user)
     */
    [AllowAnonymous]
    public class AccountController : Controller
    {
        PetaByteContext db = new PetaByteContext();
        
        string param;

        int SessionUserId;

        
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        // GET: Account
        public ActionResult Login(User user)
        {
            // ensure that password & email is to one user
            // count() ==> returns # of elements in sequence
            var count = db.Users.Where(u => u.Password.password1 == user.Password.password1 && u.Password.email == user.Password.email).Count();
            // SingleOrDefault() ==> returns only element of a sequence
            var name = db.Users.Where(u => u.Password.password1 == user.Password.password1 && u.Password.email == user.Password.email).SingleOrDefault();
            // if count returns 1
            if (count == 1)
            {
                // manages form authentication; creates cookie for logged in user
                FormsAuthentication.SetAuthCookie(user.Password.email, false);
                Session["userId"] = name.userId.ToString();
                Session["username"] = name.FullName().ToString();
                return RedirectToAction("AdminPanel");
            }
            // if count returns 0
            else
            {
                ViewBag.Message = "Invalid User";

                return View();
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login");
        }

        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(User user)
        {
            // check if modelstate is valid before proceeding
            //if (ModelState.IsValid)
            //{
                /*
                 * Note(bryanstephens): when the user requests a password reset, a unique key is generated and sent to the requested user's email.
                 * This is done in order to validate the user. A reset password token must be unique, as well as only be valid for request for a
                 * password reset. If the user attempts to to reuse a reset password token, they will encounter an error.
                 */
                // #1 ==> generate encrpyt key
                // RNG ==> cryptographic Random Number Generator
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                //// byte ==> integral type; stores values (8-bit integer)
                byte[] data = new byte[4];
                //// store in byte to then call GetBytes() ==> fills byte array with result of rng
                rng.GetBytes(data);
                //// user BitConverter to change byte array into integer
                int value = BitConverter.ToInt32(data, 0);
                // #2 ==> assign encrpyt key to database
                // LINQ ==> SELECT email FROM Password WHERE email = :email
                var update = (from p in db.Passwords
                              where p.email == user.Password.email
                              select p).FirstOrDefault();
                update.passwordReset = value.ToString();
                // set reset token
                // #3 ==> save token to database
                db.SaveChanges();
                
                /*
                    * Note(bryanstephens):  Once a password reset token has been requested, the must be notified of the change. B/c they are unable to log into their account, they will be sent a email (one that is registered in the system) in order to reset their password. Within the email, a link is also sent with the user, which contains a redirect to reset.cs, along with a reset token to authenticate the user's request
                */
                var firstname = db.Users.Where(x => x.Password.email == user.Password.email).FirstOrDefault().firstName;
                var resetToken = db.Users.Where(x => x.Password.email == user.Password.email).FirstOrDefault().Password.passwordReset;
                // body of email message
                var emailBody = "<h3>Password Reset</h3> <p> Sorry to hear you forgot your password " + firstname + ". <a href=http://http5204b-stephensbryan.azurewebsites.net/account/reset?token=" + resetToken + "> click here to reset your password</a>";
                // new instance of MailMessage
                var msg = new MailMessage();
                // receipient address
                msg.To.Add(new MailAddress(user.Password.email));
                msg.Subject = "Password Reset";
                msg.Body = string.Format(emailBody);
                // format body of email as html
                msg.IsBodyHtml = true;
                // smtp credentials
                // ***credentials are stored in web.config file under system.net/mailSettings***
                using (var smtp = new SmtpClient())
                {
                        await smtp.SendMailAsync(msg);
                        ViewBag.Sent = "Please check your email inbox for link to reset your password";
                    
                }
            //}
            return View();
        }

        [HttpGet]
        public ActionResult reset()
        {
            param = Request.QueryString["token"];
            var validRequest = db.Users.Where(u => u.Password.passwordReset == param).Count();

            // if return is equal to 1 ==> user returns with valid reset password token
            if (validRequest == 1)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult reset(PasswordReset passwordReset)
        {
            /*
            *  Note(bryanstephens): token is a GET parameter received via an email user was sent to reset their password. 
            *  logic must check that the token belongs to a user, which allows them to reset their password.
            *  if the token is invalid, leave them no choice but to go back to Login Index
            *  Once a user navigates to Account/reset and reset's their password, token is destroyed.
            */
            if (ModelState.IsValid)
            {
                param = Request.QueryString["token"];
                // LINQ ==> SELECT email FROM Password WHERE email = :email
                // Update password1 with value from resetpassword
                var reset = (from p in db.Passwords
                             where p.passwordReset == param
                             select p).FirstOrDefault();
                reset.email = db.Users.Where(x => x.Password.passwordReset == param).SingleOrDefault().Password.email;
                reset.password1 = passwordReset.Password;
                // set to reset token to NULL
                reset.passwordReset = null;
                db.SaveChanges();
                ViewBag.Results = "Password reset";

                return RedirectToAction("Login");
            }
            return View();
        }

        /*
         * Note(bryanstephens): Session is an object, so convert to int then perform LINQ 
         */
        public ActionResult AdminPanel()
        {
            SessionUserId = Convert.ToInt16(Session["userId"]);
            var user = db.Users.Where(x => x.userId == SessionUserId).SingleOrDefault();
            return View(user);
        }

        /*
         * Note(bryanstephens): When user logs in, need to validate that email entered is within DB; check against known results and if not a match, inform user email not on record
         *  
         */ 
         public JsonResult IsEmailValid(string email)
         {
           return Json(db.Users.Any(x => x.Password.email != email),JsonRequestBehavior.AllowGet);
         }
        [HttpGet]
        public ActionResult ContactEdit(int? userId)
        {
            var user = db.Users.Where(x => x.userId == userId).SingleOrDefault();

            if(user != null)
            {
                return View(user);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ContactEdit([Bind(Include ="phone")] User user)
        {
            if (ModelState.IsValid)
            {
                SessionUserId = Convert.ToInt16(Session["userId"]);
                // LINQ ==> SELECT FROM USERS WHERE userId = :userId
                // Update User details with value from user
                var update = (from u in db.Users
                             where u.userId == SessionUserId
                             select u).FirstOrDefault();
                update.phone = user.phone;
                db.SaveChanges();
                return RedirectToAction("AdminPanel");
            }
            return View();
        }

        public ActionResult DonorProfiles()
        {
            return View();
        }

        /*
         * Note(bryanstephens): Donor Profile Feature
         * This is built off of Jessica's Donation feature. The idea behind this is to allow admin users to profile or feature donations for anonymous users. On the home page underneath the hero image the third blue box will contain the donor profile. For each donor profiled, there is also a message for potential donors, which would explain why someone donated to the hospital. 
         * 3 parts:
         * 1.) Add a Donor profile
         * 2.) Delete a Donor profile
         * 3.) Edit a Donor Profile
         * 
         * Next steps: Allow for profile to be viewed on main page
         */
        public ActionResult AddDonorProfiles()
        {
            ViewBag.donationId = new SelectList(db.Donations, "donationsId", "donorFN");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddDonorProfiles([Bind(Include = "donationprofileId,donationMessage,donationId")] DonationProfile donationProfile)
        {
            if (ModelState.IsValid)
            {
                db.DonationProfiles.Add(donationProfile);
                db.SaveChanges();
                return RedirectToAction("AdminPanel");
            }
            ViewBag.donationId = new SelectList(db.Donations, "donationsId", "donorFN", donationProfile.donationId);
            return View(donationProfile);
        }

        public ActionResult ViewDonorProfiles()
        {
            return View(db.DonationProfiles.ToList());
        }

        public ActionResult DeleteDonorProfiles(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonationProfile donationProfile = db.DonationProfiles.Find(id);
            if (donationProfile == null)
            {
                return HttpNotFound();
            }
            return View(donationProfile);
        }

        [HttpPost, ActionName("DeleteDonorProfiles")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDonorProfiles(int id)
        {
            DonationProfile donationProfile = db.DonationProfiles.Find(id);
            db.DonationProfiles.Remove(donationProfile);
            db.SaveChanges();
            return RedirectToAction("ViewDonorProfiles");
        }

        public ActionResult EditDonorProfiles(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonationProfile donationProfile = db.DonationProfiles.Find(id);
            if (donationProfile == null)
            {
                return HttpNotFound();
            }
            ViewBag.donationId = new SelectList(db.Donations, "donationsId", "donorFN", donationProfile.donationId);
            return View(donationProfile);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDonorProfiles([Bind(Include = "donationprofileId,donationMessage,donationId")] DonationProfile donationProfile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donationProfile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewDonorProfiles");
            }
            ViewBag.donationId = new SelectList(db.Donations, "donationsId", "donorFN", donationProfile.donationId);
            return View(donationProfile);
        }

        /*
         * Note(bryanstephens): Gift Shop Feature
         * This allows for anonymous users to purchase online gifts through Temiskaming Hospital.
         * 3 admin actions:
         * 1.) Create gift items
         * 2.) Delete gift items
         * 3.) Edit gift items
         */
        public ActionResult GiftShop()
        {
            return View();
        }

        public ActionResult ViewGiftShopItems()
        {
            var giftItems = db.GiftItems.Include(g => g.GiftType);
            return View(giftItems.ToList());
        }
        public ActionResult CreateGiftItem()
        {
            ViewBag.GiftTypeId = new SelectList(db.GiftTypes, "GiftTypeId", "Name");
            return View();
        }

        public ActionResult GiftItemDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GiftItem giftItem = db.GiftItems.Find(id);
            if (giftItem == null)
            {
                return HttpNotFound();
            }
            return View(giftItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGiftItem([Bind(Include = "GiftItemId,Name,Price,Description,LastUpdated,GiftTypeId")] GiftItem giftItem)
        {
            if (ModelState.IsValid)
            {
                db.GiftItems.Add(giftItem);
                db.SaveChanges();
                return RedirectToAction("AdminPanel");
            }

            ViewBag.GiftTypeId = new SelectList(db.GiftTypes, "GiftTypeId", "Name", giftItem.GiftTypeId);
            return View(giftItem);
        }

        public ActionResult EditGiftitem(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GiftItem giftItem = db.GiftItems.Find(id);
            if (giftItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.GiftTypeId = new SelectList(db.GiftTypes, "GiftTypeId", "Name", giftItem.GiftTypeId);
            return View(giftItem);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditGiftItem([Bind(Include = "GiftItemId,Name,Price,Description,LastUpdated,GiftTypeId")] GiftItem giftItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(giftItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AdminPanel");
            }
            ViewBag.GiftTypeId = new SelectList(db.GiftTypes, "GiftTypeId", "Name", giftItem.GiftTypeId);
            return View(giftItem);
        }
    }
}