using CasaDaVideira.Model.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Linq;

namespace CasaDaVideira.Model.Database.Repository
{
    public class ProdutoRepository : RepositoryBase<Produto>
    {
        public ProdutoRepository(ISession session) : base(session)
        {

        }
        public Produto FirstProduto(int idProduto)
        {
            var produto = this.Session.Query<Produto>().FirstOrDefault(f => f.IdProduto == idProduto);

            return produto;
        }
    }
}
