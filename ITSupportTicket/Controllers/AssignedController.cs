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
    public class AssignedController : Controller
    {
        private MyCon db = new MyCon();

        // GET: Assigned
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var tblAssigneds = db.tblAssigneds.Include(t => t.tblTicket);
            return View(tblAssigneds.ToList());
        }

        [Authorize(Roles = "Staff")]
        public ActionResult AssignedT()
        {
            var tblAssigneds = db.tblAssigneds.Include(t => t.tblTicket).Where(p => p.Email == User.Identity.Name);
            return View(tblAssigneds.ToList());
        }

        // GET: Assigned/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblAssigned tblAssigned = db.tblAssigneds.Find(id);
            if (tblAssigned == null)
            {
                return HttpNotFound();
            }
            return View(tblAssigned);
        }

        // GET: Assigned/Create
        public ActionResult Create()
        {
            ViewBag.TicketId = new SelectList(db.tblTickets, "TicketId", "Name");
            return View();
        }

        // POST: Assigned/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AssignedId,TicketId,StaffName,Email")] tblAssigned tblAssigned)
        {
            if (ModelState.IsValid)
            {
                db.tblAssigneds.Add(tblAssigned);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TicketId = new SelectList(db.tblTickets, "TicketId", "Name", tblAssigned.TicketId);
            return View(tblAssigned);
        }

        // GET: Assigned/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblAssigned tblAssigned = db.tblAssigneds.Find(id);
            if (tblAssigned == null)
            {
                return HttpNotFound();
            }
            ViewBag.TicketId = new SelectList(db.tblTickets, "TicketId", "Name", tblAssigned.TicketId);
            return View(tblAssigned);
        }

        // POST: Assigned/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssignedId,TicketId,StaffName,Email")] tblAssigned tblAssigned)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblAssigned).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TicketId = new SelectList(db.tblTickets, "TicketId", "Name", tblAssigned.TicketId);
            return View(tblAssigned);
        }

        // GET: Assigned/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblAssigned tblAssigned = db.tblAssigneds.Find(id);
            if (tblAssigned == null)
            {
                return HttpNotFound();
            }
            return View(tblAssigned);
        }

        // POST: Assigned/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblAssigned tblAssigned = db.tblAssigneds.Find(id);
            db.tblAssigneds.Remove(tblAssigned);
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
