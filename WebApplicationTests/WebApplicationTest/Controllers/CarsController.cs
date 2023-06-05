using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApplicationTest.Models;
using WebApplicationTest.ViewModels;

namespace WebApplicationTest.Controllers
{
    public class CarsController : ApiController
    {
        private CarInformationDbContext db = new CarInformationDbContext();
        [HttpGet]
        public IQueryable<CarDetail> GetCars()
        {
            return db.CarDetails.Include(x => x.PartsDetails).AsQueryable();
        }
        [HttpGet]
        public IHttpActionResult GetCar(int id)
        {
            var d = db.CarDetails.Include(x => x.PartsDetails).FirstOrDefault(x => x.CarDetailId == id);
            if (d != null)
                return Ok(d);
            else
                return NotFound();
        }

        [HttpPost]
        public IHttpActionResult PostCars(CarDataInputViewModel model)
        {
            if (ModelState.IsValid)
            {
                var carInfo = new CarDetail
                {
                    CarName = model.CarName,
                    LaunchDate = model.LaunchDate,
                    Price = model.Price,
                    Picture = model.Picture,
                    IsStock = model.IsStock

                };
                model.PartsDetails.ForEach(s =>
                {
                    carInfo.PartsDetails.Add(new PartsDetail { PartName = s.PartName, PartsPrice = s.PartsPrice });
                });
                db.CarDetails.Add(carInfo);
                db.SaveChanges();
                return Ok(carInfo);
            }
            return BadRequest("Data invalid");
        }
        [HttpPut]
        public IHttpActionResult PutDevice(int id, CarDataEditViewModel model)
        {
            if (id != model.CarDetailId) return BadRequest("Id mismatch");
            if (ModelState.IsValid)
            {
                var device = db.CarDetails.Include(x => x.PartsDetails).First(x => x.CarDetailId == id);
                if (device == null) return NotFound();
                device.CarName = model.CarName;
                device.LaunchDate = model.LaunchDate;
                device.Price = model.Price;
                device.IsStock = model.IsStock;
                device.Picture = model.Picture;
                db.PartsDetails.RemoveRange(device.PartsDetails);
                model.PartsDetails.ForEach(s =>
                {
                    device.PartsDetails.Add(new PartsDetail { PartName = s.PartName, PartsPrice = s.PartsPrice });
                });
                db.SaveChanges();
                return Ok(device);
            }
            return BadRequest("Data invalid");
        }
        [HttpDelete]
        public IHttpActionResult DeleteDevice(int id)
        {
            var d = db.CarDetails.FirstOrDefault(x => x.CarDetailId == id);
            if (d == null) return NotFound();
            db.CarDetails.Remove(d);
            db.SaveChanges();
            return Ok("Deleted");
        }
        [Route("Image/Upload")]
        [HttpPost]
        public IHttpActionResult Upload()
        {
            var file = HttpContext.Current.Request.Files.Count > 0 ?
        HttpContext.Current.Request.Files[0] : null;
            if (file != null && file.ContentLength > 0)
            {
                string ext = Path.GetExtension(file.FileName);
                string f = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                string savePath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/Pictures"), f);
                file.SaveAs(savePath);
                return Ok(f);


            }
            return BadRequest();
        }
    }
}