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
        public virtual Guid IdProduto { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Descricao { get; set; }
        public virtual double Preco
        {
            get
            {
                return this.Preco;
            }
            set
            {
                this.PrecoAntigo = this.Preco;
                this.Preco = value;
            }
        }
        public virtual int Qtd { get; set; }
        public virtual double PrecoAntigo { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual bool Oferta { get; set; }
        public virtual int Classificacao { get; set; }
        public virtual string  Imagem { get; set; }
    }

    public class ProdutoMap : ClassMapping<Produto>
    {
        public ProdutoMap()
        {
            //esta mapeando uma primarykey
            Id(x => x.IdProduto, m => m.Generator(Generators.Guid));

            Property(x => x.Nome);
            Property(x => x.Descricao);
            Property(x => x.Preco);
            Property(x => x.Qtd);
            Property(x => x.PrecoAntigo);
            Property(x => x.Oferta);
            Property(x => x.Classificacao);
            Property(x => x.Imagem);
            ManyToOne(x => x.Categoria, m =>
            {
                m.Column("IdCategoria");
            });

        }
    }
}
