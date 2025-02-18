using System.Configuration;
using Microsoft.Data.SqlClient;

namespace HotelManager.Data.Utility;

public sealed class SqlConnectionSingleton
{
    private static readonly Lazy<SqlConnectionSingleton> instance = new(() => new SqlConnectionSingleton());

    // Privátní konstruktor zajistí, že instance lze vytvořit pouze zde
    private SqlConnectionSingleton()
    {
        var connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        Connection = new SqlConnection(connectionString);
    }

    public SqlConnection Connection { get; private set; }

    public static SqlConnectionSingleton Instance => instance.Value;
}