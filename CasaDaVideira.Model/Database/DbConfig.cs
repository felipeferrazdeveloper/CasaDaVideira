using CasaDaVideira.Model.Database.Repository;
using MySql.Data.MySqlClient;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Context;
using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static CasaDaVideira.Model.Database.Model.Usuario;

namespace Mvc.Model.Database
{
    public class DbConfig
    {
        private static DbConfig _instance = null;

        private ISessionFactory _sessionFactory;

        public UsuarioRepository UsuarioRepository { get; set; }
        public TelefoneRepository TelefoneRepository { get; set; }
        public EnderecoRepository EnderecoRepository { get; set; }
        public ProdutoRepository ProdutoRepository { get; set; }
       // public CategoriaRepository CategoriaRepository { get; set; }

        private DbConfig()
        {
            Conectar();

            this.UsuarioRepository = new UsuarioRepository(Session);
            this.TelefoneRepository = new TelefoneRepository(Session);
            this.EnderecoRepository = new EnderecoRepository(Session);
            this.ProdutoRepository = new ProdutoRepository(Session);
           // this.CategoriaRepository = new CategoriaRepository(Session);
        }

        public static DbConfig Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DbConfig();
                }
                return _instance;
            }
        }
        private void Conectar()
        {
            try
            {
                var stringConexao = "Persist Security Info=True;"
                                    + "server=localhost;"
                                    + "port=3308;"
                                    + "database=casadavideira;"
                                    + "uid=root;"
                                    + "pwd=";

                var mysql = new MySqlConnection(stringConexao);
                try
                {
                    mysql.Open();
                }
                catch
                {
                    CriarSchemaBanco("localhost", "3308", "casadavideira", "", "root");
                }
                finally
                {
                    mysql.Close();
                }

                ConectarNHibernate(stringConexao);

            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possivel conectar ao banco de dados.", ex);
            }
        }

        private void ConectarNHibernate(String stringConexao)
        {
            try
            {
                var config = new Configuration();

                //Configura a conexao com o banco
                config.DataBaseIntegration(db =>
                {
                    //Dialeto do SQL
                    db.Dialect<NHibernate.Dialect.MySQLDialect>();
                    //String de conexao
                    db.ConnectionString = stringConexao;
                    //Driver de Conexao
                    db.Driver<NHibernate.Driver.MySqlDataDriver>();
                    //Provedor(tipo de servidor) de Conexao
                    db.ConnectionProvider<NHibernate.Connection.DriverConnectionProvider>();
                    //Jeito de criação do banco de dados, com update sempre atualiza o banco de dados sem dropar nem deletar
                    db.SchemaAction = SchemaAutoAction.Update;
                });

                var maps = this.Mapeamento();
                config.AddMapping(maps);

                //Verifica se é web ou descktop
                if (HttpContext.Current == null)
                {
                    config.CurrentSessionContext<ThreadStaticSessionContext>();
                }
                else
                {
                    config.CurrentSessionContext<WebSessionContext>();

                }

                this._sessionFactory = config.BuildSessionFactory();

            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possivel criar o NHibernate.", ex);
            }
        }

        private ISession Session
        {
            get
            {
                try
                {
                    if (CurrentSessionContext.HasBind(_sessionFactory))
                        return _sessionFactory.GetCurrentSession();

                    var session = _sessionFactory.OpenSession();

                    CurrentSessionContext.Bind(session);
                    return session;

                }
                catch (Exception ex)
                {
                    throw new Exception("Não foi possivel criar a sessão", ex);
                }
            }
        }

        private HbmMapping Mapeamento()
        {
            try
            {
                var modelMapper = new ModelMapper();

                /*
                //uma maneirda de add o mapeamento é essa
                modelMapper.AddMapping(TelefoneMap);
                //pada adicionar outro tipo tem quem adicionar novamente
                modelMapper.AddMapping(TelefoneMap);
                */
                //Outra maneira é usar o generics
                modelMapper.AddMappings(
                    Assembly.GetAssembly(typeof(UsuarioMap)).GetTypes()
                );

                return modelMapper.CompileMappingForAllExplicitlyAddedEntities();

            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possivel mapear as classes.", ex);
            }
        }

        private void CriarSchemaBanco(string server, string port, string dbName,
                                      string psw, string user)
        {
            try
            {
                var stringConexao = "server=" + server + ";user=" + user +
                    ";port=" + port + ";password=" + psw + ";";
                var mySql = new MySqlConnection(stringConexao);
                var cmd = mySql.CreateCommand();

                mySql.Open();
                cmd.CommandText = "CREATE DATABASE IF NOT EXISTS `" + dbName + "`;";
                cmd.ExecuteNonQuery();
                mySql.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi criar o banco de dados.", ex);
            }
        }
    }
}
