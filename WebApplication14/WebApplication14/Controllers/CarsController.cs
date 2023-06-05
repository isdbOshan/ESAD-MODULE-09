using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication14.Models;
using WebApplication14.ViewModel;

namespace WebApplication14.Controllers
{
    public class CarsController : Controller
    {
        CarDbContextInforamtions db = new CarDbContextInforamtions();   
        public ActionResult Index()
        {
            return View(db.CarDetails.Include(x => x.PartDetail).ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(CarInputViewM data)
        {
            if(ModelState.IsValid) 
            {
                CarDetail car = new CarDetail
                {
                    CarName= data.CarName,
                    MakeYear= data.MakeYear,
                    Price= data.Price,
                    Picture= data.Picture,  
                    IsStock= data.IsStock,
                };
                foreach(var c in data.PartDetail)
                {
                    car.PartDetail.Add(c);
                }
                db.CarDetails.Add(car);
                db.SaveChanges();
                return View();
            }
            return Json(null);
        }



        // 
    }
}