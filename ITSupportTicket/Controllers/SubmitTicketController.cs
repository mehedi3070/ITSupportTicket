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
    public class SubmitTicketController : Controller
    {
        private MyCon db = new MyCon();

        // GET: SubmitTicket
        [Authorize]
        public ActionResult Index()
        {
            var tblSubmitTickets = db.tblSubmitTickets.Include(t => t.tblTicket).Where(s=>s.Email==User.Identity.Name);
            return View(tblSubmitTickets.ToList());
        }

        [Authorize]
        public ActionResult Report()
        {
            var tblSubmitTickets = db.tblSubmitTickets.Include(t => t.tblTicket);
            return View(tblSubmitTickets.ToList());
        }

        [Authorize(Roles = "Staff")]
        public ActionResult StaffView()
        {
            var tblSubmitTickets = db.tblSubmitTickets.Include(t => t.tblTicket);
            return View(tblSubmitTickets.ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AdminView()
        {
            var tblSubmitTickets = db.tblSubmitTickets.Include(t => t.tblTicket);
            return View(tblSubmitTickets.ToList());
        }

        // GET: SubmitTicket/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSubmitTicket tblSubmitTicket = db.tblSubmitTickets.Find(id);
            if (tblSubmitTicket == null)
            {
                return HttpNotFound();
            }
            return View(tblSubmitTicket);
        }

        // GET: SubmitTicket/Create
        public ActionResult Create()
        {
            var temp = from t in db.tblTickets
                       join p in db.tblSubmitTickets on t.TicketId equals p.TicketId into pt
                       from x in pt.DefaultIfEmpty()
                       where x.Email == null
                       select new {
                           TicketId=t.TicketId,
                           Name = t.Name,
                       };

            //var temp2 = from p in temp
            //            where p.Email == null
            //            select p;
            ViewBag.TicketId = new SelectList(temp, "TicketId", "Name");
            return View();
        }

        // POST: SubmitTicket/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubmitTicketId,TicketId,Name,Email,Subject,Priority,Attachment,Message")] tblSubmitTicket tblSubmitTicket, HttpPostedFileBase fleFile)
        {
            if (ModelState.IsValid)
            {
                tblSubmitTicket.Attachment = System.IO.Path.GetFileName(fleFile.FileName);
                db.tblSubmitTickets.Add(tblSubmitTicket);
                db.SaveChanges();
                fleFile.SaveAs(Server.MapPath("../Uploads/File/" + tblSubmitTicket.SubmitTicketId.ToString() + "_" + tblSubmitTicket.Attachment));
                return RedirectToAction("Index");
            }

            ViewBag.TicketId = new SelectList(db.tblTickets, "TicketId", "Name", tblSubmitTicket.TicketId);
            return View(tblSubmitTicket);
        }

        // GET: SubmitTicket/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSubmitTicket tblSubmitTicket = db.tblSubmitTickets.Find(id);
            if (tblSubmitTicket == null)
            {
                return HttpNotFound();
            }
            ViewBag.TicketId = new SelectList(db.tblTickets, "TicketId", "Name", tblSubmitTicket.TicketId);
            return View(tblSubmitTicket);
        }

        // POST: SubmitTicket/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubmitTicketId,TicketId,Name,Email,Subject,Priority,Attachment,Message")] tblSubmitTicket tblSubmitTicket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblSubmitTicket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TicketId = new SelectList(db.tblTickets, "TicketId", "Name", tblSubmitTicket.TicketId);
            return View(tblSubmitTicket);
        }

        // GET: SubmitTicket/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblSubmitTicket tblSubmitTicket = db.tblSubmitTickets.Find(id);
            if (tblSubmitTicket == null)
            {
                return HttpNotFound();
            }
            return View(tblSubmitTicket);
        }

        // POST: SubmitTicket/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblSubmitTicket tblSubmitTicket = db.tblSubmitTickets.Find(id);
            db.tblSubmitTickets.Remove(tblSubmitTicket);
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
