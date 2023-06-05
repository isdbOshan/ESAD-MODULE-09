using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalEvidence.Models;
using Microsoft.AspNetCore.Authorization;
using FinalEvidence.ViewModels;

namespace FinalEvidence.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CarDetailsController : ControllerBase
    {
        public readonly CarInformationDbContext db;
        public IWebHostEnvironment env;
        public CarDetailsController(CarInformationDbContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarDetail>>> GetDevices()
        {

            return Ok(await db.CarDetails.ToListAsync());

        }

        [HttpGet("Include/{id}")]
        public async Task<ActionResult<IEnumerable<CarDetail>>> GetDevice(int id)
        {

            return Ok(await db.CarDetails.Include(x => x.PartsDetails).FirstOrDefaultAsync(d => d.CarDetailId == id));

        }
        [HttpGet("Include")]
        public async Task<ActionResult<IEnumerable<CarDetail>>> GetDeviceWithSpecs()
        {

            var data = await db.CarDetails.Include(d => d.PartsDetails).ToListAsync();
            return data;

        }
        [HttpPost]
        public async Task<ActionResult<CarDetail>> PostDevice(CarDataInputViewModel model)
        {
            var device = new CarDetail
            {
                CarName = model.CarName,
                LaunchDate = model.LaunchDate,
                Picture = model.Picture,
                Price = model.Price,
                IsStock = model.IsStock
            };
            foreach (var s in model.PartsDetails)
            {
                device.PartsDetails.Add(new PartsDetail { PartName = s.PartName, PartsPrice = s.PartsPrice });
            }
            await db.CarDetails.AddAsync(device);
            await db.SaveChangesAsync();
            return device;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> PutDevice(int id, CarDataEditViewModel model)
        {
            if (id != model.CarDetailId) return BadRequest("Id mismatch");
            var device = await db.CarDetails.Include(x => x.PartsDetails).FirstOrDefaultAsync(x => x.CarDetailId == id);
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
            await db.SaveChangesAsync();
            return Ok();

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDevice(int id)
        {
            var device = await db.CarDetails.FirstOrDefaultAsync(x => x.CarDetailId == id);
            if (device == null) return NotFound(id);
            db.CarDetails.Remove(device);
            await db.SaveChangesAsync();
            return Ok();
        }
        [HttpPost("Upload")]
        public async Task<ActionResult<UploadResponse>> Upload(IFormFile file)
        {
            try
            {
                string ext = Path.GetExtension(file.FileName);
                string f = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                if (!Directory.Exists(env.WebRootPath + "\\Iamges\\"))
                {
                    Directory.CreateDirectory(env.WebRootPath + "\\Images\\");
                }
                using FileStream filestream = System.IO.File.Create(env.WebRootPath + "\\Images\\" + f);

                file.CopyTo(filestream);
                filestream.Flush();

                filestream.Close();
                return new UploadResponse { SavedFile = f };


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
