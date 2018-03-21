using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaVideira.Model.Database.Model
{
    public class Carrinho
    {
        public IList<Produto> Produtos { get; set; }
        public Usuario Usuario { get; set; }
    }

    //public class CarrinhoMap : ClassMapping<Carrinho>
    //{
    //    public CarrinhoMap()
    //    {

    //    }
    //}
}
