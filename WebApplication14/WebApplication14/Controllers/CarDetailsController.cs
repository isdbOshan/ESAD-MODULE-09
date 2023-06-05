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
    public class CarDetailsController : Controller
    {
        private CarDbContextInforamtions db = new CarDbContextInforamtions();

        // GET: CarDetails
        public ActionResult Index()
        {
            return View(db.CarDetails.ToList());
        }
        public PartialViewResult PartsPartialView()
        {
            return PartialView(db.PartDetails.ToList());
        
        }

        // GET: CarDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarDetail carDetail = db.CarDetails.Find(id);
            if (carDetail == null)
            {
                return HttpNotFound();
            }
            return View(carDetail);
        }

        // GET: CarDetails/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CarId,CarName,MakeYear,IsStock,Price,Picture")] CarDetail carDetail)
        {
            if (ModelState.IsValid)
            {
                db.CarDetails.Add(carDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(carDetail);
        }

   
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarDetail carDetail = db.CarDetails.Find(id);
            if (carDetail == null)
            {
                return HttpNotFound();
            }
            return View(carDetail);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CarId,CarName,MakeYear,IsStock,Price,Picture")] CarDetail carDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(carDetail);
        }

      
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarDetail carDetail = db.CarDetails.Find(id);
            if (carDetail == null)
            {
                return HttpNotFound();
            }
            return View(carDetail);
        }

     
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CarDetail carDetail = db.CarDetails.Find(id);
            db.CarDetails.Remove(carDetail);
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
