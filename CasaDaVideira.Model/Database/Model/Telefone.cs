using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaVideira.Model.Database.Model
{
    public class Telefone
    {

        public virtual Guid Id { get; set; }
        public virtual string Ddd { get; set; }
        public virtual string Numero { get; set; }
        public virtual string Tipo { get; set; }

        public virtual Usuario Usuario { get; set; }
    }

    public class TelefoneMap : ClassMapping<Telefone>
    {
        public TelefoneMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Guid));

            Property(x => x.Ddd);
            Property(x => x.Numero);
            Property(x => x.Tipo);

            ManyToOne(x => x.Usuario, m =>
            {
                m.Column("idUsuario");
            });
        }
    }
}
