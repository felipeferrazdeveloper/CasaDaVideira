using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CasaDaVideira.Model.Database.Utils
{
    public static class IniUtils
    {
        public static IniData LerArquivoIni()
        {
            try
            {
                var dir = System.Environment.CurrentDirectory;
                var file = dir + "/Config/DbConfig.ini";
                if (HttpContext.Current != null)
                {
                    file = HttpContext.Current.Server.MapPath("/Config/DbConfig.ini")
                        .Replace("\\", "/");
                }
                if (!System.IO.File.Exists(file))
                {
                    throw new Exception("Arquivo de configuração inexistente.");
                }

                var parser = new FileIniDataParser();

                return parser.ReadFile(file);

            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possivel ler o arquivo ini", ex);
            }
        }
    }
}
