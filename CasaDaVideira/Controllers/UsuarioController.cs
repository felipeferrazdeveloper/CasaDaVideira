using CasaDaVideira.Model.Database;
using CasaDaVideira.Model.Database.Model;
using CasaDaVideira.Model.Database.Utils;
using Mvc.Model.Utils;
using System;
using System.Linq;
using System.Web.Mvc;

namespace CasaDaVideira.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            if (LoginUtils.Usuario.Admin)
            {
                var usuarios = DbConfig.Instance.UsuarioRepository.FindAll();
                return View(usuarios);
            }
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult CreateUser(Boolean admin=false)
        {
            var user = new Usuario();
            if(LoginUtils.Usuario != null)
                if (LoginUtils.Usuario.Admin)
                {
                    user.Admin = admin;
                }
            return View(user);
        }

        public ActionResult EditUser(Guid idUsuario)
        {
            Usuario user;
            if (LoginUtils.Usuario.Admin)
                user = DbConfig.Instance.UsuarioRepository.FirstUser(idUsuario);
            else
                user = LoginUtils.Usuario;

            return View("CreateUser", user);
        }

        public ActionResult DeleteUser(Guid idUsuario)
        {
            if (LoginUtils.Usuario.Admin)
            {
                var user = DbConfig.Instance.UsuarioRepository.FirstUser(idUsuario);
                DbConfig.Instance.UsuarioRepository.Excluir(user);
            }
            return RedirectToAction("Index");

        }

        public ActionResult DetailsUser()
        {
            var user = LoginUtils.Usuario;

            return View(user);
        }

        [AllowAnonymous]
        public ActionResult GravarUsuario(Usuario user)
        {
            //user.DtNascimento = DateTime.Now;
            DbConfig.Instance.UsuarioRepository.Salvar(user);
            LoginUtils.Logar(user.Email, user.Senha);
            return RedirectToAction("Index", "Home");
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

        public ActionResult Telefones(Guid id)
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

        public ActionResult GravarTelefone(Telefone telefone, Guid idUsuario)
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

        public ActionResult EditarTelefone(Guid id)
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

        public ActionResult DeletarTelefone(Guid id)
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

        public ActionResult Enderecos(Guid id)
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

        public ActionResult GravarEndereco(Endereco endereco, Guid idUsuario)
        {
            var user = DbConfig.Instance.UsuarioRepository.FirstUser(idUsuario);
            endereco.Usuario = user;
            DbConfig.Instance.EnderecoRepository.Salvar(endereco);
            return RedirectToAction("Index", "Usuario");
        }

        public ActionResult EditarEndereco(Guid id)
        {
            var end = DbConfig.Instance.EnderecoRepository.FirstEnd(id);

            ViewData["IdUsuario"] = end.Usuario.IdUsuario;
            return View("CriarEndereco", end);

        }

        public ActionResult DeletarEndereco(Guid id)
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

        [AllowAnonymous]
        public ActionResult FazerLogin(string email, string senha)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
                return RedirectToAction("Login", "Home");
            var iniFile = IniUtils.LerArquivoIni();
            if (email.Equals(iniFile["AdminFirstUser"]["key"]) && senha.Equals(iniFile["AdminFirstUser"]["password"]))
            {
                //var hoje = DateTime.Now;
                //string videira = "VIDEIRA";
                ////A senha será composta por uma Letra maiúscula V-I-D-E-I-R-A
                ////Esta letra será referente ao dia da semana sendo V para domingo e assim por diante
                ////Sequencialmente teremos minutos com 2 digitos numéricos
                ////dia
                ////hora
                ////mes (com um ou dois digitos)
                ////A letra do dia anterior minúscula
                //string password = videira[hoje.DayOfWeek.GetHashCode()].ToString();
                //password += hoje.Minute.ToString();
                //password += hoje.Day.ToString();
                //password += hoje.Hour.ToString();
                //password += hoje.Month.ToString();
                //password += videira[hoje.AddDays(-1).DayOfWeek.GetHashCode()].ToString().ToLower();

                var us = new Usuario
                {
                    Nome = "Administrador",
                    Email = "meu@email.com",
                    Senha = "123123",
                    Admin = true
                };
                return View("CreateUser", us);
            }
            else
            {
                try
                {
                    LoginUtils.Logar(email, senha);
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "Home", "Login"));
                }
            }
        }

        public ActionResult Deslogar(Usuario user)
        {
            LoginUtils.Deslogar();

            return RedirectToAction("Index", "Home");
        }
    }
}
