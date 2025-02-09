using System.Configuration;
using Microsoft.Data.SqlClient;

namespace HotelManager.Data.Utility
{
    public sealed class SqlConnectionSingleton
    {
        private static readonly Lazy<SqlConnectionSingleton> instance = 
            new Lazy<SqlConnectionSingleton>(() => new SqlConnectionSingleton());

        public SqlConnection Connection { get; private set; }

        // Privátní konstruktor zajistí, že instance lze vytvořit pouze zde
        private SqlConnectionSingleton()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDbConnection"].ConnectionString;
            Connection = new SqlConnection(connectionString);
        }

        public static SqlConnectionSingleton Instance
        {
            get { return instance.Value; }
        }
    }
}