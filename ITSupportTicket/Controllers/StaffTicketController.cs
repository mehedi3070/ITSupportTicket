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
    public class StaffTicketController : Controller
    {
        private MyCon db = new MyCon();

        // GET: StaffTicket
        [Authorize(Roles = "Staff")]
        public ActionResult Index()
        {
            var tblStaffTickets = db.tblStaffTickets.Include(t => t.tblTicket);
            return View(tblStaffTickets.ToList());
        }
        [Authorize(Roles ="Admin")]
        public ActionResult SolveReport()
        {
            var tblStaffTickets = db.tblStaffTickets.Include(t => t.tblTicket);
            return View(tblStaffTickets.ToList());
        }

        [Authorize]
        public ActionResult ViewSolve()
        {
            var tblStaffTickets = db.tblStaffTickets.Include(t => t.tblTicket).Where(s => s.UserEmail == User.Identity.Name);
            return View(tblStaffTickets.ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ViewSolveAdmin()
        {
            var tblStaffTickets = db.tblStaffTickets.Include(t => t.tblTicket);
            return View(tblStaffTickets.ToList());
        }
        // GET: StaffTicket/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblStaffTicket tblStaffTicket = db.tblStaffTickets.Find(id);
            if (tblStaffTicket == null)
            {
                return HttpNotFound();
            }
            return View(tblStaffTicket);
        }

        // GET: StaffTicket/Create
        public ActionResult Create()
        {
            ViewBag.TicketId = new SelectList(db.tblTickets, "TicketId", "Name");
            return View();
        }

        // POST: StaffTicket/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StaffTicketId,TicketId,StaffEmail,UserEmail,Subject,Attachment,Message")] tblStaffTicket tblStaffTicket, HttpPostedFileBase fleFile)
        {
            if (ModelState.IsValid)
            {
                tblStaffTicket.Attachment = System.IO.Path.GetFileName(fleFile.FileName);
                db.tblStaffTickets.Add(tblStaffTicket);
                db.SaveChanges();
                fleFile.SaveAs(Server.MapPath("../Uploads/File/" + tblStaffTicket.StaffTicketId.ToString() + "_" + tblStaffTicket.Attachment));
                return RedirectToAction("Index");
            }

            ViewBag.TicketId = new SelectList(db.tblTickets, "TicketId", "Name", tblStaffTicket.TicketId);
            return View(tblStaffTicket);
        }

        // GET: StaffTicket/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblStaffTicket tblStaffTicket = db.tblStaffTickets.Find(id);
            if (tblStaffTicket == null)
            {
                return HttpNotFound();
            }
            ViewBag.TicketId = new SelectList(db.tblTickets, "TicketId", "Name", tblStaffTicket.TicketId);
            return View(tblStaffTicket);
        }

        // POST: StaffTicket/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StaffTicketId,TicketId,StaffEmail,UserEmail,Subject,Attachment,Message")] tblStaffTicket tblStaffTicket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblStaffTicket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TicketId = new SelectList(db.tblTickets, "TicketId", "Name", tblStaffTicket.TicketId);
            return View(tblStaffTicket);
        }

        // GET: StaffTicket/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblStaffTicket tblStaffTicket = db.tblStaffTickets.Find(id);
            if (tblStaffTicket == null)
            {
                return HttpNotFound();
            }
            return View(tblStaffTicket);
        }

        // POST: StaffTicket/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblStaffTicket tblStaffTicket = db.tblStaffTickets.Find(id);
            db.tblStaffTickets.Remove(tblStaffTicket);
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
