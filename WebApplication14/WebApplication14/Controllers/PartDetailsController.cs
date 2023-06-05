using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication14.Models;

namespace WebApplication14.Controllers
{
    public class PartDetailsController : Controller
    {
        private CarDbContextInforamtions db = new CarDbContextInforamtions();

        // GET: PartDetails
        public ActionResult Index()
        {
            var partDetails = db.PartDetails.Include(p => p.CarDetail);
            return View(partDetails.ToList());
        }

        // GET: PartDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartDetails partDetails = db.PartDetails.Find(id);
            if (partDetails == null)
            {
                return HttpNotFound();
            }
            return View(partDetails);
        }

        // GET: PartDetails/Create
        public ActionResult Create()
        {
            ViewBag.CarId = new SelectList(db.CarDetails, "CarId", "CarName");
            return View();
        }

        // POST: PartDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PartId,PartName,PartType,CarId")] PartDetails partDetails)
        {
            if (ModelState.IsValid)
            {
                db.PartDetails.Add(partDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CarId = new SelectList(db.CarDetails, "CarId", "CarName", partDetails.CarId);
            return View(partDetails);
        }

        // GET: PartDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartDetails partDetails = db.PartDetails.Find(id);
            if (partDetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarId = new SelectList(db.CarDetails, "CarId", "CarName", partDetails.CarId);
            return View(partDetails);
        }

        // POST: PartDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PartId,PartName,PartType,CarId")] PartDetails partDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(partDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CarId = new SelectList(db.CarDetails, "CarId", "CarName", partDetails.CarId);
            return View(partDetails);
        }

        // GET: PartDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartDetails partDetails = db.PartDetails.Find(id);
            if (partDetails == null)
            {
                return HttpNotFound();
            }
            return View(partDetails);
        }

        // POST: PartDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PartDetails partDetails = db.PartDetails.Find(id);
            db.PartDetails.Remove(partDetails);
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
