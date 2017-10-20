using CasaDaVideira.Model.Database;
using System.Linq;
using System.Web.Mvc;

namespace CasaDaVideira.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var usuarios = DbConfig.Instance.UsuarioRepository.FindAll();

            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            var existeAdmin = DbConfig.Instance.UsuarioRepository.FindAll().Where(w => w.Admin).FirstOrDefault();
            ViewBag.ExisteAdmin = existeAdmin == null ? false : true;
            return View();
        }
    }
}