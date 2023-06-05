using WebApplication12.Model;

namespace WebApplication12.ViewModels
{
    public class ProductViewModel
    {
        public List<Wearable> Wearables { get; set; }= new List<Wearable>();
        public List<Edible> Edibles { get; set; } = new List<Edible>();
    }
}
