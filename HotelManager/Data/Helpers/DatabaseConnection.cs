using System.Configuration;
using Microsoft.Data.SqlClient;

namespace HotelManager.Data.Helpers;

public class DatabaseConnection
{
    private readonly string _connectionString;
    
    
    public DatabaseConnection()
    {
        _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        if (string.IsNullOrEmpty(_connectionString))
        {
            throw new ConfigurationErrorsException("Nenealezeno 'ConnectionString' v konfiguračním souboru App.config.");
        }
    }

    public SqlConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }
}