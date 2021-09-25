using System.Data.Linq;
using WebServer.Models;
using System.Configuration;

namespace WebServer.Database
{
    public class MainDataContext : DataContext
    {
        public Table<Vacante> Vacantes;
        public Table<Prospecto> Prospectos;
        public Table<Entrevista> Entrevistas;
        public MainDataContext(string connection) : base(connection)
        {
            
        }
    }

    public class DatabaseManager
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.AppSettings["SqlConnection"];
        }
    }
}