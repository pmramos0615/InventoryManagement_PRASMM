using Microsoft.AspNetCore.Mvc;
namespace InventoryManagement_PRASMM.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
