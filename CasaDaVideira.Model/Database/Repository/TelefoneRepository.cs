using CasaDaVideira.Model.Database.Model;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaVideira.Model.Database.Repository
{
    public class TelefoneRepository : RepositoryBase<Telefone>
    {
        public TelefoneRepository(ISession session) : base(session)
        {

        }

        public Telefone FirstTel(Guid idTelefone)
        {
            var tel = this.Session.Query<Telefone>().FirstOrDefault(f => f.Id == idTelefone);

            return tel;
        }
    }
}
