using Microsoft.AspNetCore.Mvc;
using WebApplication12.Model;
using WebApplication12.ViewModels;

namespace WebApplication12.Controllers
{
    public class HomeController : Controller
    {
        ProductDContext db;
        public HomeController(ProductDContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            var data = new ProductViewModel
            {
                Wearables= db.Wearables.OfType<Wearable>().ToList(),    
                Edibles= db.Edibles.OfType<Edible>().ToList()
            };
            return View(data);  
        }
    }
}
