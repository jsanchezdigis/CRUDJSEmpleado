using Microsoft.AspNetCore.Mvc;

namespace PL_MVC.Controllers
{
    public class EmpleadoController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            return View();
        }
    }
}
