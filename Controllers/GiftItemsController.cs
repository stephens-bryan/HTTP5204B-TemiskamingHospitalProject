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
    /*
     * Note(bryanstephens): This feature is incomplete. The premise was to have an online gift shop, in which anonymous users could add various gift items to a cart. Once the user was happy with their purchase, they could then proceed to the checkout page, which would allow them to also specify a patient currently in the hosptial they would like to have the gift deleivered to and which date to have it deleivered on. 
     */
    [AllowAnonymous]
    public class GiftItemsController : Controller
    {
        private PetaByteContext db = new PetaByteContext();

        // GET: GiftItems
        public ActionResult Index(int? id = 1)
        {
            //var giftItems = db.GiftItems.Include(g => g.GiftType);
            //return View(giftItems.ToList());
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            GiftType type = db.GiftTypes.Find(id);

            if (type == null)
            {
                return HttpNotFound();
            }
            return View(type);
        }

        // GET: GiftItems/Details/5
        public ActionResult Details(int? id)
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

        // GET: GiftItems/Create
        public ActionResult Create()
        {
            ViewBag.GiftTypeId = new SelectList(db.GiftTypes, "GiftTypeId", "Name");
            return View();
        }

        // POST: GiftItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GiftItemId,Name,Price,Description,LastUpdated,GiftTypeId")] GiftItem giftItem)
        {
            if (ModelState.IsValid)
            {
                db.GiftItems.Add(giftItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GiftTypeId = new SelectList(db.GiftTypes, "GiftTypeId", "Name", giftItem.GiftTypeId);
            return View(giftItem);
        }

        // GET: GiftItems/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: GiftItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GiftItemId,Name,Price,Description,LastUpdated,GiftTypeId")] GiftItem giftItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(giftItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GiftTypeId = new SelectList(db.GiftTypes, "GiftTypeId", "Name", giftItem.GiftTypeId);
            return View(giftItem);
        }

        // GET: GiftItems/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: GiftItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GiftItem giftItem = db.GiftItems.Find(id);
            db.GiftItems.Remove(giftItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult sample()
        {
            return View();
        }
    }
}
