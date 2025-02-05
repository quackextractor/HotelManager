using System.Configuration;
using Microsoft.Data.SqlClient;

namespace HotelManager;

public partial class HlavniOkno : Form
{
    public HlavniOkno()
    {
        InitializeComponent();
    }

    private string _connectionString;
    private SqlConnection _connection;
    private void HlavniOkno_Load(object sender, EventArgs e)
    {
        _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        _connection = new SqlConnection(_connectionString);
        
    }
    

private void connect_Click(object sender, EventArgs e)
{
    try
    {
        _connection.Open();
        MessageBox.Show("Připojení se zdařilo", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
    catch (Exception exception)
    {
        MessageBox.Show(exception.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}

private void quit_Click(object sender, EventArgs e)
{
    Application.Exit();
}

}