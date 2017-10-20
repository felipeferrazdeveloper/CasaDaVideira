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
        public Usuario FirstUser(Guid idUsuario)
        {
            try
            {
                var usuario = this.Session.Query<Usuario>().FirstOrDefault(f => f.IdUsuario == idUsuario);
                return usuario;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        public Usuario GetUserByLoginAndPassword(string login, string password)
        {
            try
            {
                var user = this.Session.Query<Usuario>().FirstOrDefault(f => f.Email == login && f.Senha == password);
                return user;
            }catch(Exception ex)
            {
                throw ex;
            }
        }
        
        public Usuario FindUserByEmail(string email)
        {
            try
            {
                var user = this.Session.Query<Usuario>().FirstOrDefault(f => f.Email == email);
                return user;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public Usuario Buscar(string email, string senha)
        {
            return this.Session.Query<Usuario>().FirstOrDefault(f => f.Senha == senha && f.Email == email);
        }
    }
}
