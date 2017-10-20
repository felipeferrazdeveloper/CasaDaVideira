using CasaDaVideira.Model.Database.Repository;
using CasaDaVideira.Model.Database.Utils;
using IniParser;
using IniParser.Model;
using MySql.Data.MySqlClient;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Context;
using NHibernate.Mapping.ByCode;
using System;
using System.Reflection;
using System.Web;
using static CasaDaVideira.Model.Database.Model.Usuario;

namespace CasaDaVideira.Model.Database
{
    public class DbConfig
    {
        private static DbConfig _instance = null;
        private ISessionFactory _sessionFactory;
        public UsuarioRepository UsuarioRepository { get; set; }
        public TelefoneRepository TelefoneRepository { get; set; }
        public ProdutoRepository ProdutoRepository { get; set; }
        public EnderecoRepository EnderecoRepository { get; set; }

        private DbConfig()
        {
            Conectar();
            this.UsuarioRepository = new UsuarioRepository(Session);
            this.TelefoneRepository = new TelefoneRepository(Session);
            this.ProdutoRepository = new ProdutoRepository(Session);
            this.EnderecoRepository = new EnderecoRepository(Session);
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
                var iniFile = IniUtils.LerArquivoIni();
                
                var server = iniFile["DbConfig"]["server"];
                var port = iniFile["DbConfig"]["port"];
                var database = iniFile["DbConfig"]["database"];
                var user = iniFile["DbConfig"]["user"];
                var pwd = iniFile["DbConfig"]["pwd"];

                var stringConexao = "Persist Security Info=True;"
                    + "server=" + server + "; "
                    + "port=" + port + "; "
                    + "database=" + database + ";"
                    + "pwd=" + pwd + ";"
                    + "user=" + user + ";";

                var mysql = new MySqlConnection(stringConexao);
                try
                {
                    mysql.Open();
                }
                catch
                {
                    CriarSchemaBanco(server, port, database, pwd, user);
                }
                finally
                {
                    mysql.Close();
                }

                ConectarNHibernate(stringConexao);

            }
            catch (Exception ex)
            {

                throw new Exception("Não foi possível conectar ao banco de dados.", ex);
            }
        }

        private HbmMapping Mapeamento()
        {
            try
            {
                var modelMapper = new ModelMapper();

                modelMapper.AddMappings(
                        Assembly.GetAssembly(typeof(UsuarioMap)).GetTypes()
                );

                return modelMapper.CompileMappingForAllExplicitlyAddedEntities();
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível criar o Hibernate", ex);
            }
        }

        private void ConectarNHibernate(String stringConexao)
        {
            try
            {
                var config = new Configuration();
                //Configuro a conexão com o banco
                config.DataBaseIntegration(db =>
                {
                    //Dialeto do SQL
                    db.Dialect<NHibernate.Dialect.MySQLDialect>();
                    //String de conexão
                    db.ConnectionString = stringConexao;
                    //Driver de conexão
                    db.Driver<NHibernate.Driver.MySqlDataDriver>();
                    //Provedorde Conexão
                    db.ConnectionProvider<NHibernate.Connection.DriverConnectionProvider>();
                    //Jeito de criação do banco de dados
                    db.SchemaAction = SchemaAutoAction.Update;

                });

                var maps = this.Mapeamento();
                config.AddMapping(maps);

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
                throw new Exception("Não foi possível criar o NHibernate", ex);
            }
        }

        private void CriarSchemaBanco(string server, string port, string dbName, string psw, string user)
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

                throw new Exception("Não foi possível criar o schema de banco de dados.", ex);
            }
        }

        private ISession Session
        {
            get
            {
                try
                {
                    if (CurrentSessionContext.HasBind(_sessionFactory))
                    {
                        return _sessionFactory.GetCurrentSession();
                    }

                    var session = _sessionFactory.OpenSession();

                    CurrentSessionContext.Bind(session);
                    return session;
                }
                catch (Exception ex)
                {

                    throw new Exception("Não foi possível criar a sessão.", ex);
                }
            }
        }
    }
}
