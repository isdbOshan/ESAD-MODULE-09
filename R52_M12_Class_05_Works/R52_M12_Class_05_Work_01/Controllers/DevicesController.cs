using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using R52_M12_Class_05_Work_01.Models;
using R52_M12_Class_05_Work_01.ViewModels;

namespace R52_M12_Class_05_Work_01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DevicesController : ControllerBase
    {
        public readonly DeviceDbContext db;
        public IWebHostEnvironment env;
        public DevicesController(DeviceDbContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Device>>> GetDevices()
        {
           
           return Ok(await db.Devices.ToListAsync());
          
        }
       
        [HttpGet("Include/{id}")]
        public async Task<ActionResult<IEnumerable<Device>>> GetDevice(int id)
        {

            return Ok(await db.Devices.Include(x=> x.Specs).FirstOrDefaultAsync(d=> d.DeviceId== id));

        }
        [HttpGet("Include")]
        public async Task<ActionResult<IEnumerable<Device>>> GetDeviceWithSpecs()
        {

           var data = await db.Devices.Include(d=> d.Specs).ToListAsync();
            return data;

        }
        [HttpPost]
        public async Task<ActionResult<Device>> PostDevice(DeviceInputModel model)
        {
            var device = new Device
            {
                DeviceName = model.DeviceName,
                ReleaseDate = model.ReleaseDate,
                Picture = model.Picture,
                Price = model.Price,
                OnSale = model.OnSale
            };
            foreach(var s in model.Specs)
            {
                device.Specs.Add(new Spec {  SpecName=s.SpecName, Value=s.Value});
            }
            await db.Devices.AddAsync(device);
            await db.SaveChangesAsync();
            return device;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> PutDevice(int id, DeviceEditModel model)
        {
            if (id != model.DeviceId) return BadRequest("Id mismatch");
            var device = await db.Devices.Include(x => x.Specs).FirstOrDefaultAsync(x => x.DeviceId == id);
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
            await db.SaveChangesAsync();
            return Ok();
            
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDevice(int id)
        {
            var device = await db.Devices.FirstOrDefaultAsync(x => x.DeviceId == id);
            if (device == null) return NotFound(id);
            db.Devices.Remove(device);
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
