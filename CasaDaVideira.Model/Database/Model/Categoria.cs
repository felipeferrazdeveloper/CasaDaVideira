//using NHibernate.Mapping.ByCode;
//using NHibernate.Mapping.ByCode.Conformist;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CasaDaVideira.Model.Database.Model
//{
//    public class Categoria
//    {
//        public virtual Guid IdCategoria { get; set; }
//        public virtual string Nome { get; set; }
//        public virtual IList<Produto> Produtos { get; set; }
//    }

//    public class CategoriaMap : ClassMapping<Categoria>
//    {
//        public CategoriaMap()
//        {
//            Id(x => x.IdCategoria, m => m.Generator(Generators.Guid));

//            Property(x => x.Nome);
                       
//            Bag<Produto>(x => x.Produtos, m =>
//            {
//                m.Cascade(Cascade.All);
//                m.Key(k => k.Column("idProduto"));
//                m.Lazy(CollectionLazy.NoLazy);
//                m.Inverse(true);
//            },
//            r => r.OneToMany());
//        }
//    }
//}
