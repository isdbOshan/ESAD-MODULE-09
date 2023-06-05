using Microsoft.AspNetCore.Mvc;
using WebApplication12.Model;

namespace WebApplication12.Controllers
{
    public class WearablesController : Controller
    {
        ProductDContext db;
       public WearablesController(ProductDContext db) 
        { 
            this.db = db;
        }
        public IActionResult Index()
        {
            return View(db.Wearables.ToList());
        }
    }
}
