using System.IO;

namespace API.Controllers
{
    public class FallBack : Controller
    {
        [AllowAnonymous] 
        public IActionResult Index()
        {
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot", "index.html"), "text/HTML");
        }
    }
}