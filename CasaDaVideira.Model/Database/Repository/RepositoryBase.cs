using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaVideira.Model.Database.Repository
{
    //classe com tipo generico |  where T : class para garantir q é uma classe em isso pode usar qualquer coisa
    public abstract class RepositoryBase<T> where T : class
    {
        public ISession Session = null;

        public RepositoryBase(ISession session)
        {
            this.Session = session;
        }

        public T FirstOrDefault()
        {
            return this.Session.Query<T>().FirstOrDefault();
        }

        public IList<T> FindAll()
        {
            return Session.CreateCriteria<T>().List<T>();
        }

        public virtual T Salvar(T model)
        {
            try
            {
                this.Session.Clear();
                var transaction = this.Session.BeginTransaction();

                this.Session.SaveOrUpdate(model);

                transaction.Commit();

                return model;
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possivel salvar " + typeof(T) + "\nErro:" + ex.Message);
            }
        }

        public virtual void Excluir(T model)
        {
            try
            {
                this.Session.Clear();
                var transaction = this.Session.BeginTransaction();

                this.Session.Delete(model);

                transaction.Commit();

            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possivel excluir " + typeof(T) + "\nErro:" + ex.Message);
            }
        }
    }
}
