using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers
{
    public class CarsController : Controller
    {
        VehicleDbContexts db = new VehicleDbContexts();
        public ActionResult Index()
        {
            return View(db.CarDetails.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(CarInputViewModel model)
        {
            if(ModelState.IsValid)
            {
                CarDetail car = new CarDetail
                {
                    CarId= model.CarId,
                    CarName= model.CarName,
                    Price = model.Price,    
                    IsStock= model.IsStock,
                    Picture= model.Picture,
                };
                foreach(var c in db.PartDetail)
                { 
                    car.PartDetail.Add(c);

                }
                db.SaveChanges();
                return Json(new { id = car.CarId });
            }
            return View();
        }

        public PartialViewResult CarInputController(CarInputViewModel car=null, int? index=null)
        {
            if(car==null) car=new CarInputViewModel();
            if (index.HasValue)
            {
                if(index < car.PartDetail.Count)
                {
                    car.PartDetail.RemoveAt(index.Value);
                }

            }
            else
            {
               car.PartDetail.Add(new PartDetail());
            }
            return PartialView("_PartialCarInput", car);
        }
        public ActionResult ImageUpload(int id,UploadImageViewModel pic)
        {
            if (ModelState.IsValid)
            {
                if(pic.Picture != null)
                {
                    CarDetail car = db.CarDetails.First(c => c.CarId == id);
                    string ext = Path.GetExtension(pic.Picture.FileName);
                    string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName())+ ext;
                    string savePath = Path.Combine(Server.MapPath("~/Pictures"));
                    pic.Picture.SaveAs(savePath);
                    car.Picture = fileName;
                    db.SaveChanges();
                    return Json(fileName);

                }
            }
            return Json(null);
        }


        public ActionResult Edit(int id)
        {
            var car =db.CarDetails.Include(x=>x.CarId).First(x=>x.CarId==id);
            return View(
                new CarEditViewModel{
                    CarId= car.CarId,
                    CarName= car.CarName,
                    Price= car.Price,
                    IsStock= car.IsStock,
                    Picture= car.Picture,
                    PartDetail = car.PartDetail.ToList(),

            }
                
                
                );
        }
        [HttpPost]
        public ActionResult Edit(CarEditViewModel model)
        {
            var existing = db.CarDetails.First(x=>x.CarId== model.CarId);
            if(ModelState.IsValid)
            {
                existing.CarName = model.CarName;   
                existing.Price = model.Price;
                existing.IsStock = model.IsStock;
                existing.Picture = model.Picture;
                db.PartDetail.RemoveRange(existing.PartDetail.ToList());
                foreach(var q in model.PartDetail)
                {
                    q.CarId = existing.CarId;
                    db.PartDetail.Add(q);
                }
                db.SaveChanges();
                return Json(existing.CarId);

            }
            return View();
        }

        public PartialViewResult AddQuationToEditForm(CarEditViewModel c, int? index = null)
        {

            if (index.HasValue)
            {
                if (index < c.PartDetail.Count)
                {
                    c.PartDetail.RemoveAt(index.Value);
                }
            }


            return PartialView("_PartialCarEditform", c);
        }
        public PartialViewResult AddMore(CarEditViewModel c, int? index = null)
        {
            if (c == null) c = new CarEditViewModel();
            if (index.HasValue)
            {
                if (index < c.PartDetail.Count)
                {
                    c.PartDetail.RemoveAt(index.Value);
                }
            }
            else
            {
                c.PartDetail.Add(new PartDetail());
            }

            return PartialView("_PartialCarEditform", c);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var existing = db.CarDetails.FirstOrDefault(c => c.CarId == id);
            if (existing != null)
            {
                db.CarDetails.Remove(existing);
                db.SaveChanges();
                return Json(existing.CarId);
            }
            else
            {
                return HttpNotFound();
            }
        }
        /// last line
    }
}