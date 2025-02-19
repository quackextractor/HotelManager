using System.Configuration;
using Microsoft.Data.SqlClient;

namespace HotelManager.Data.Utility;

public sealed class SqlConnectionSingleton
{
    private static readonly Lazy<SqlConnectionSingleton> instance = new(() => new SqlConnectionSingleton());

    // private -> instance may only be created here
    private SqlConnectionSingleton()
    {
        var connectionSettings = ConfigurationManager.ConnectionStrings["ConnectionString"];

        // is empty?
        if (connectionSettings == null || string.IsNullOrWhiteSpace(connectionSettings.ConnectionString))
            throw new ConfigurationErrorsException(
                "Řetězec připojení 'ConnectionString' v konfiguračním souboru chybí nebo je prázdný.");

        var connectionString = connectionSettings.ConnectionString;
        Connection = new SqlConnection(connectionString);
    }

    public SqlConnection Connection { get; private set; }

    public static SqlConnectionSingleton Instance => instance.Value;
}