using CarPartsDetailsInformations.Models;
using CarPartsDetailsInformations.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarPartsDetailsInformations.Controllers
{
    public class CarsController : Controller
    {
        VehicleInformationDbContext db = new VehicleInformationDbContext();
        public ActionResult Index()
        {
            return View(db.Categories.Include(x=>x.Vehicles).ToList());
        }
        public ActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Create(InputViewModel data)
        {
            if (ModelState.IsValid)
            {
                Categorie c = new Categorie
                {
                    CategoryName = data.CategoryName,
                    Description = data.Description,
                   
                };
                foreach (var q in data.Vehicles)
                {
                    c.Vehicles.Add(q);
                }
                db.Categories.Add(c);
                db.SaveChanges();
                return Json(new { id = c.CategoryId });
            }
            return Json(null);
        }
        public PartialViewResult AddQuationToForm(InputViewModel c = null, int? index = null)
        {
            if (c == null) c = new InputViewModel();
            if (index.HasValue)
            {
                if (index < c.Vehicles.Count)
                {
                    c.Vehicles.RemoveAt(index.Value);
                }
            }
            else
            {
                c.Vehicles.Add(new Vehicle());
            }

            return PartialView("_VehicleListInfo", c);
        }
        public ActionResult UploadImage(int id, UploadImage pic)
        {
            if (ModelState.IsValid)
            {
                if (pic.Picture != null)
                {
                    Vehicle c = db.Vehicles.First(x => x.VehicleId == id);
                    string ext = Path.GetExtension(pic.Picture.FileName);
                    string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                    string savePath = Path.Combine(Server.MapPath("~/Pictures"), fileName);
                    pic.Picture.SaveAs(savePath);
                    c.Picture = fileName;
                    db.SaveChanges();
                    return Json(fileName);
                }
            }
            return Json(null);
        }
        ///Create

        ////Edit
        /// ////////////////////////////////////////////////////
        ///  
        public ActionResult Edit(int id)
        {
           
            var candidate = db.Categories.Include(c => c.Vehicles).First(c => c.CategoryId == id);

            return View(
                new EditViewModel
                {
                    CategoryId = candidate.CategoryId,
                    CategoryName = candidate.CategoryName,
                    Description = candidate.Description,
                    Vehicles = candidate.Vehicles.ToList()
                }
                );
        }
        [HttpPost]
        public ActionResult Edit(EditViewModel model)
        {
            var existing = db.Categories.First(c => c.CategoryId == model.CategoryId);
            if (ModelState.IsValid)
            {
                existing.CategoryName = model.CategoryName;
                existing.Description = model.Description;
              
                db.Vehicles.RemoveRange(existing.Vehicles.ToList());
                foreach (var q in model.Vehicles)
                {
                    q.CategoryId = existing.CategoryId;
                    db.Vehicles.Add(q);
                }
                db.SaveChanges();
                return Json(existing.CategoryId);
            }
            return Json(null);
        }
        public PartialViewResult AddQuationToEditForm(EditViewModel c, int? index = null)
        {

            if (index.HasValue)
            {
                if (index < c.Vehicles.Count)
                {
                    c.Vehicles.RemoveAt(index.Value);
                }
            }


            return PartialView("_QualificationEditForm", c);
        }
        public PartialViewResult AddMore(EditViewModel c, int? index = null)
        {
            if (c == null) c = new EditViewModel();
            if (index.HasValue)
            {
                if (index < c.Vehicles.Count)
                {
                    c.Vehicles.RemoveAt(index.Value);
                }
            }
            else
            {
                c.Vehicles.Add(new Vehicle());
            }

            return PartialView("_QualificationEditForm", c);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var existing = db.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (existing != null)
            {
                db.Categories.Remove(existing);
                db.SaveChanges();
                return Json(existing.CategoryId);
            }
            else
            {
                return HttpNotFound();
            }
        }
        //end section
    }
}