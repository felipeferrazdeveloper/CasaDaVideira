using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaDaVideira.Model.Database.Model
{
    public class Usuario
    {
        public virtual Guid IdUsuario { get; set; }
        [Required(ErrorMessage = "Email é obrigatorio", AllowEmptyStrings = false)]
        public virtual string Email { get; set; }
        [Required(ErrorMessage = "Senha é obrigatorio", AllowEmptyStrings = false)]
        public virtual string Senha { get; set; }
        [Required(ErrorMessage = "Nome é obrigatorio", AllowEmptyStrings = false)]
        public virtual string Nome { get; set; }
        [Required(ErrorMessage = "Sobrenome é obrigatorio", AllowEmptyStrings = false)]
        public virtual string Sobrenome { get; set; }
        [Required(ErrorMessage = "CPF é obrigatorio", AllowEmptyStrings = false)]
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

                Property(x => x.Email, m =>
                {
                    m.NotNullable(true);
                });
                Property(x => x.Senha, m =>
                {
                    m.NotNullable(true);
                });
                Property(x => x.Nome, m =>
                {
                    m.NotNullable(true);
                });
                Property(x => x.Sobrenome, m =>
                {
                    m.NotNullable(true);
                });
                Property(x => x.Cpf, m =>
                {
                    m.NotNullable(true);
                });
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