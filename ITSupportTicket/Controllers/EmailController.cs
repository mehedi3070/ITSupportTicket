using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ITSupportTicket;

namespace ITSupportTicket.Controllers
{
    public class EmailController : Controller
    {
        private MyCon db = new MyCon();

        // GET: Email
        [Authorize]
        public ActionResult Index()
        {
            return View(db.tblEmails.ToList());
        }
        [Authorize(Roles = "Staff")]
        public ActionResult StaffMessage()
        {
            return View(db.tblEmails.ToList());
        }


        // GET: Email/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEmail tblEmail = db.tblEmails.Find(id);
            if (tblEmail == null)
            {
                return HttpNotFound();
            }
            return View(tblEmail);
        }

        // GET: Email/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Email/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmailId,From,To,Message")] tblEmail tblEmail)
        {
            if (ModelState.IsValid)
            {
                db.tblEmails.Add(tblEmail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblEmail);
        }

        // GET: Email/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEmail tblEmail = db.tblEmails.Find(id);
            if (tblEmail == null)
            {
                return HttpNotFound();
            }
            return View(tblEmail);
        }

        // POST: Email/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmailId,From,To,Message")] tblEmail tblEmail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblEmail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblEmail);
        }

        // GET: Email/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblEmail tblEmail = db.tblEmails.Find(id);
            if (tblEmail == null)
            {
                return HttpNotFound();
            }
            return View(tblEmail);
        }

        // POST: Email/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblEmail tblEmail = db.tblEmails.Find(id);
            db.tblEmails.Remove(tblEmail);
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
