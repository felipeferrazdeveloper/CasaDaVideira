using CasaDaVideira.Model.Database.Model;
using CasaDaVideira.Model.Database.Repository;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaVideira.Model.Database.Repository
{
    public class UsuarioRepository : RepositoryBase<Usuario>
    {
        public UsuarioRepository(ISession session) : base(session)
        {

        }
        public Usuario FirstUser(int idUsuario)
        {
            var usuario = this.Session.Query<Usuario>().FirstOrDefault(f => f.IdUsuario == idUsuario);

            return usuario;
        }
    }
}
