using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaVideira.Model.Database.Model
{
    public class Produto
    {
        public virtual int IdProduto { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Descricao { get; set; }
        public virtual double Preco { get; set; }
        public virtual string Qtd { get; set; }
      //  public virtual Categoria Categoria { get; set; }
    }

    public class ProdutoMap : ClassMapping<Produto>
    {
        public ProdutoMap()
        {
            //esta mapeando uma primarykey
            Id(x => x.IdProduto, m => m.Generator(Generators.Identity));

            Property(x => x.Nome);
            Property(x => x.Descricao);
            Property(x => x.Preco);
            Property(x => x.Qtd);
           // Property(x => x.Categoria);
            
        }
    }
}
