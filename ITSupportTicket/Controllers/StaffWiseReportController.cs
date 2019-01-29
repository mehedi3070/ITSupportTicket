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
    public class StaffWiseReportController : Controller
    {
        private MyCon db = new MyCon();

        // GET: StaffWiseReport
        [Authorize(Roles = "Staff")]
        public ActionResult Index()
        {
            return View(db.StaffWiseReports.ToList());
        }
        [Authorize(Roles = "Admin")]
        public ActionResult StaffDailyR()
        {
            return View(db.StaffWiseReports.ToList());
        }

        // GET: StaffWiseReport/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffWiseReport staffWiseReport = db.StaffWiseReports.Find(id);
            if (staffWiseReport == null)
            {
                return HttpNotFound();
            }
            return View(staffWiseReport);
        }

        // GET: StaffWiseReport/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StaffWiseReport/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StaffID,StaffName,StaffEmail,SolveReport,PendingReport")] StaffWiseReport staffWiseReport)
        {
            if (ModelState.IsValid)
            {
                db.StaffWiseReports.Add(staffWiseReport);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(staffWiseReport);
        }

        // GET: StaffWiseReport/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffWiseReport staffWiseReport = db.StaffWiseReports.Find(id);
            if (staffWiseReport == null)
            {
                return HttpNotFound();
            }
            return View(staffWiseReport);
        }

        // POST: StaffWiseReport/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StaffID,StaffName,StaffEmail,SolveReport,PendingReport")] StaffWiseReport staffWiseReport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staffWiseReport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(staffWiseReport);
        }

        // GET: StaffWiseReport/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StaffWiseReport staffWiseReport = db.StaffWiseReports.Find(id);
            if (staffWiseReport == null)
            {
                return HttpNotFound();
            }
            return View(staffWiseReport);
        }

        // POST: StaffWiseReport/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StaffWiseReport staffWiseReport = db.StaffWiseReports.Find(id);
            db.StaffWiseReports.Remove(staffWiseReport);
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
