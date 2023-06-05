using R52_Mid_09_Evidence.Models;
using R52_Mid_09_Evidence.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace R52_Mid_09_Evidence.Controllers
{
    public class DevicesController : ApiController
    {
        private DeviceDbContext db = new DeviceDbContext();
        [HttpGet]
        public  IQueryable<Device> GetDevices()
        {
            return db.Devices.Include(x=> x.Specs).AsQueryable();
        }
        [HttpGet]
        public IHttpActionResult GetDevice(int id)
        {
            var d=db.Devices.Include(x=> x.Specs).FirstOrDefault(x=> x.DeviceId == id);
            if (d != null)
                return Ok(d);
            else
                return NotFound();
        }
        [HttpPost]
        public IHttpActionResult PostDevice(DeviceInputModel model)
        {
            if (ModelState.IsValid)
            {
                var device = new Device
                {
                    DeviceName = model.DeviceName,
                    ReleaseDate = model.ReleaseDate,
                    Price = model.Price,
                    Picture = model.Picture,
                    OnSale = model.OnSale

                };
                model.Specs.ForEach(s =>
                {
                    device.Specs.Add(new Spec { SpecName = s.SpecName, Value = s.Value });
                });
                db.Devices.Add(device);
                db.SaveChanges();
                return Ok(device);
            }
            return BadRequest("Data invalid");
        }
        [HttpPut]
        public IHttpActionResult PutDevice(int id, DeviceEditModel model) 
        {
            if (id != model.DeviceId) return BadRequest("Id mismatch");
            if (ModelState.IsValid)
            {
                var device = db.Devices.Include(x=> x.Specs).First(x=> x.DeviceId == id);
                if (device == null) return NotFound();
                device.DeviceName = model.DeviceName;
                device.ReleaseDate = model.ReleaseDate;
                device.Price = model.Price;
                device.OnSale = model.OnSale;
                device.Picture = model.Picture;
                db.Specs.RemoveRange(device.Specs);
                model.Specs.ForEach(s =>
                {
                    device.Specs.Add(new Spec { SpecName = s.SpecName, Value = s.Value });
                });
                db.SaveChanges();
                return Ok(device);
            }
            return BadRequest("Data invalid");
        }
        [HttpDelete]
        public IHttpActionResult DeleteDevice(int id)
        {
            var d = db.Devices.FirstOrDefault(x=> x.DeviceId == id);
            if (d == null) return NotFound();
            db.Devices.Remove(d);
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
