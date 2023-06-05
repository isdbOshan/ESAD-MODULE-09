using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication14.Models;
using WebApplication14.ViewModel;

namespace WebApplication14.Controllers
{
    public class MastersController : Controller
    {
        CarDbContextInforamtions db = new CarDbContextInforamtions();
        public ActionResult Index()
        {
            return View(db.PartDetails.ToList());
        }
        public ActionResult Create()
        {
            return View();  
        }
        public ActionResult Create(CarInputViewM data)
        {
            if(ModelState.IsValid)
            {
                CarDetail car = new CarDetail
                {
                    CarName=data.CarName,
                    MakeYear=data.MakeYear,
                    Price=data.Price,
                    IsStock=data.IsStock,
                    Picture = data.Picture,
                };
                foreach(var c in db.PartDetails) 
                {
                    db.PartDetails.Add(c);
                }
                db.CarDetails.Add(car);
                db.SaveChanges();
                return Json(new { id = car.CarId });


            }
            return View();
        }
        public ActionResult ImgCollection(UploadImages pic, int id)
        {
            if (ModelState.IsValid)
            {
                if(pic.Picture== null)
                {
                    CarDetail car = db.CarDetails.First(x=>x.CarId==id);
                    var ext = Path.GetExtension(pic.Picture.FileName).ToLower();    
                    var fileName = Path.GetFileName(pic.Picture.FileName);
                    var saveFile = Path.Combine(Server.MapPath("~/Pictures"), fileName);
                    pic.Picture.SaveAs(saveFile);
                    car.Picture = fileName;
                    db.SaveChanges();
                    return Json(fileName);
                   
                }
            }
            return View();
        }

        ////
    }
}