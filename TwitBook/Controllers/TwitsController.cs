using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TwitBook.Models;
using Microsoft.AspNet.Identity;

namespace TwitBook.Controllers
{
    public class TwitsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Twits
        public ActionResult Index()
        {
            return View(db.Twits.ToList());
        }

        // GET: Twits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Twit twit = db.Twits.Find(id);
            if (twit == null)
            {
                return HttpNotFound();
            }
            return View(twit);
        }

        // GET: Twits/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Twits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,message")] Twit twit)
        {
            if (ModelState.IsValid)
            {

                ApplicationUser user = db.Users.Find(User.Identity.GetUserId());
                twit.User = user;
                twit.date = DateTime.Now;
                db.Twits.Add(twit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(twit);
        }

        // GET: Twits/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Twit twit = db.Twits.Find(id);

            if (twit == null)
            {
                return HttpNotFound();
            }
            if (twit.User.Id != User.Identity.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            return View(twit);
        }

        // POST: Twits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,message")] Twit twit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(twit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(twit);
        }

        // GET: Twits/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Twit twit = db.Twits.Find(id);
            if (twit == null)
            {
                return HttpNotFound();
            }
            if (twit.User.Id != User.Identity.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            return View(twit);
        }

        // POST: Twits/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Twit twit = db.Twits.Find(id);
            if (twit == null)
            {
                return HttpNotFound();
            }
            if (twit.User.Id != User.Identity.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }
            db.Twits.Remove(twit);
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
    }
}
