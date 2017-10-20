using CasaDaVideira.Model.Database;
using CasaDaVideira.Model.Database.Model;
using System;
using System.Linq;
using System.Web.Mvc;

namespace CasaDaVideira.Controllers
{
    public class ProdutoController : Controller
    {
        // GET: Produto
        public ActionResult Index()
        {
            var produtos = DbConfig.Instance.ProdutoRepository.FindAll();

            return View(produtos);
        }

        public ActionResult CreateProduto()
        {
            var prod = new Produto();
            return View(prod);
        }

        public ActionResult EditProduto(Guid idProduto)
        {
            var prod = DbConfig.Instance.ProdutoRepository.FindAll().FirstOrDefault(f => f.IdProduto == idProduto);

            return View("CreateProduto", prod);
        }

        public ActionResult DeleteProduto(Guid idProduto)
        {
            var prod = DbConfig.Instance.ProdutoRepository.FindAll().FirstOrDefault(f => f.IdProduto == idProduto);

            DbConfig.Instance.ProdutoRepository.Excluir(prod);

            return RedirectToAction("Index");
        }

        public ActionResult DetailsProduto(Guid idProduto)
        {
            var prod = DbConfig.Instance.ProdutoRepository
                    .FindAll().FirstOrDefault(f => f.IdProduto == idProduto);

            return View(prod);
        }

        public ActionResult GravarProduto(Produto prod)
        {
            DbConfig.Instance.ProdutoRepository.Salvar(prod);
            //return View("Telefones");
            return RedirectToAction("Index");

        }

        public ActionResult BuscarProduto(string buscaP)
        {
            if (buscaP == "")
            {
                return RedirectToAction("Index", "Home");
            }
            var prod = DbConfig.Instance.ProdutoRepository.FindAll().Where(f => f.Nome.ToLower().Contains(buscaP.ToLower()));
            return View("Index", prod);
        }

        //public ActionResult Categoria(int idCategoria)
        //{
        //    var cat = DbConfig.Instance.CategoriaRepository.FirstCategoria(idCategoria);

        //    if (cat != null)
        //    {
        //        return View(cat);
        //    }

        //    return RedirectToAction("Index");
        //}

        //public ActionResult CriarCategoria(int idCategoria, int idProduto)
        //{
        //    var cat = new Categoria();
        //    ViewData["IdCProduto"] = idProduto;

        //    return View(cat);
        //}

        //public ActionResult GravarCategoria(Categoria categoria)
        //{
        //    DbConfig.Instance.CategoriaRepository.Salvar(categoria);
        //    //return View("Telefones");
        //    return RedirectToAction("Index");
        //}

        //public ActionResult EditarCategoria(int categoria)
        //{
        //    var cat = DbConfig.Instance.CategoriaRepository.FirstCategoria(categoria);

        //    ViewData["IdProduto"] = cat.Produto.IdProduto;
        //    return View("CriarCategpria", cat);

        //}

        //public ActionResult DeletarCategoria(int idCategoria)
        //{
        //    var cat = DbConfig.Instance.CategoriaRepository.FirstCategoria(idCategoria);

        //    DbConfig.Instance.CategoriaRepository.Excluir(cat);

        //    return RedirectToAction("Index");
        //}
        
    }
}