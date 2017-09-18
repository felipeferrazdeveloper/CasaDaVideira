using CasaDaVideira.Model.Database;
using CasaDaVideira.Model.Database.Model;
using Mvc.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CasaDaVideira.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //var usuarios = DbConfig.Instance.UsuarioRepository.FindAll();

            return View("Index");
        }

        public ActionResult Login()
        {
            return View("Login");
        }

    }
}