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
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateUser()
        {
            var user = new Usuario();
            return View(user);
        }

        public ActionResult EditUser(int idUsuario)
        {
            var user = DbConfig.Instance.UsuarioRepository.FirstUser(idUsuario);
            //.FindAll().FirstOrDefault(f => f.Id == id);

            return View("CreateUser", user);
        }

        public ActionResult DeleteUser(int idUsuario)
        {
            var user = DbConfig.Instance.UsuarioRepository.FirstUser(idUsuario);
            //.FindAll().FirstOrDefault(f => f.Id == id);

            DbConfig.Instance.UsuarioRepository.Excluir(user);

            return RedirectToAction("Index");
        }

        public ActionResult DetailsUser(int idUsuario)
        {
            var user = DbConfig.Instance.UsuarioRepository
                    .FindAll().FirstOrDefault(f => f.IdUsuario == idUsuario);

            return View(user);
        }

        public ActionResult GravarUsuario(Usuario user)
        {
            DbConfig.Instance.UsuarioRepository.Salvar(user);
            //return View("Telefones");
            return View("Telefones", user);

        }

        public ActionResult BuscarUsuario(string busca)
        {
            if (busca == "")
            {
                return RedirectToAction("Index");
            }

            var user = DbConfig.Instance.UsuarioRepository
                    .FindAll().Where(f => f.Nome.ToLower().Contains(busca.ToLower()));

            return View("Index", user);
        }
        public ActionResult Telefones(int id)
        {
            var user = DbConfig.Instance.TelefoneRepository.FirstTel(id);

            if (user != null)
            {
                return View(user);
            }

            return RedirectToAction("Index");
        }

        public ActionResult CriarTelefone(int id, int idUsuario)
        {
            var tel = new Telefone();
            ViewData["IdUsuario"] = idUsuario;

            return View(tel);
        }

        public ActionResult GravarTelefone(Telefone telefone, int idUsuario)
        {
            var user = DbConfig.Instance.UsuarioRepository.FirstUser(idUsuario);

            telefone.Usuario = user;
            try
            {
                DbConfig.Instance.TelefoneRepository.Salvar(telefone);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
            return View("Enderecos", user);
        }

        public ActionResult EditarTelefone(int id)
        {
            Telefone tel;
            try
            {
                tel = DbConfig.Instance.TelefoneRepository.FirstTel(id);
                ViewData["IdUsuario"] = tel.Usuario.IdUsuario;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
            return View("CriarTelefone", tel);

        }

        public ActionResult DeletarTelefone(int id)
        {
            try
            {
                var tel = DbConfig.Instance.TelefoneRepository.FirstTel(id);
                DbConfig.Instance.TelefoneRepository.Excluir(tel);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Enderecos(int id)
        {
            var user = DbConfig.Instance.EnderecoRepository.FirstEnd(id);

            if (user != null)
            {
                return View(user);
            }

            return RedirectToAction("Index");
        }

        public ActionResult CriarEndereco(int id, int idUsuario)
        {
            var tel = new Endereco();
            ViewData["IdUsuario"] = idUsuario;

            return View(tel);
        }

        public ActionResult GravarEndereco(Endereco endereco, int idUsuario)
        {
            var user = DbConfig.Instance.UsuarioRepository.FirstUser(idUsuario);
            endereco.Usuario = user;
            DbConfig.Instance.EnderecoRepository.Salvar(endereco);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult EditarEndereco(int id)
        {
            var end = DbConfig.Instance.EnderecoRepository.FirstEnd(id);

            ViewData["IdUsuario"] = end.Usuario.IdUsuario;
            return View("CriarEndereco", end);

        }

        public ActionResult DeletarEndereco(int id)
        {
            try
            {
                var end = DbConfig.Instance.EnderecoRepository.FirstEnd(id);
                DbConfig.Instance.EnderecoRepository.Excluir(end);
            }catch(Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }

        public ActionResult FazerLogin(string email, string senha)
        {
            if (email.Equals("administrador@casadavideirajf.com.br"))
            {
                var hoje = DateTime.Now;
                string videira = "VIDEIRA";
                //A senha será composta por uma Letra maiúscula V-I-D-E-I-R-A
                //Esta letra será referente ao dia da semana sendo V para domingo e assim por diante
                //Sequencialmente teremos minutos com 2 digitos numéricos
                //dia
                //hora
                //mes (com um ou dois digitos)
                //A letra do dia anterior minúscula
                string password = videira[hoje.DayOfWeek.GetHashCode()].ToString();
                password += hoje.Minute.ToString();
                password += hoje.Day.ToString();
                password += hoje.Hour.ToString();
                password += hoje.Month.ToString();
                password += videira[hoje.AddDays(-1).DayOfWeek.GetHashCode()].ToString().ToLower();

                if(senha.Equals(password))
                    return View("AdminArea");
            }
            else
            {
                var user = DbConfig.Instance.UsuarioRepository.FindUserByEmail(email);
                if (user.Senha.Equals(senha))
                    return View("Index", user);
            }
            return View("Index");
        }

    }
}
