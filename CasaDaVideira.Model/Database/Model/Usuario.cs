using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaVideira.Model.Database.Model
{
    public class Usuario
    {
        public virtual Guid IdUsuario { get; set; }
        public virtual string Email { get; set; }
        public virtual string Senha { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Sobrenome { get; set; }
        public virtual string Cpf { get; set; }
        public virtual int Pontos { get; set; }
        public virtual DateTime DtNascimento { get; set; }
        public virtual DateTime UltimoAcesso { get; set; }
        public virtual IList<Telefone> Telefones { get; set; }
        public virtual IList<Endereco> Enderecos { get; set; }
        public virtual bool Admin { get; set; }

        public Usuario()
        {
            this.Telefones = new List<Telefone>();
            this.Enderecos = new List<Endereco>();
        }

        public class UsuarioMap : ClassMapping<Usuario>
        {
            public UsuarioMap()
            {
                //esta mapeando uma primarykey
                Id(x => x.IdUsuario, m => m.Generator(Generators.Guid));

                Property(x => x.Email);
                Property(x => x.Senha);
                Property(x => x.Nome);
                Property(x => x.Sobrenome);
                Property(x => x.Cpf);
                Property(x => x.Pontos);
                Property(x => x.Admin, m =>
                {
                    m.Type(NHibernateUtil.Boolean);
                    m.NotNullable(true);
                });
                Property(x => x.DtNascimento, m =>
                {
                    m.Type(NHibernateUtil.Date);
                    m.NotNullable(false);
                });
                Property(x => x.UltimoAcesso, m =>
                {
                    m.Type(NHibernateUtil.Date);
                    m.NotNullable(false);
                });
                Bag<Telefone>(x => x.Telefones, m =>
                {
                    m.Cascade(Cascade.All);
                    m.Key(k => k.Column("idUsuario"));
                    m.Lazy(CollectionLazy.NoLazy);
                    m.Inverse(true);
                },
                r => r.OneToMany());
            }
        }

    }

}