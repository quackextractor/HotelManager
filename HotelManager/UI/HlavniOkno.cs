using System.Configuration;
using HotelManager.Data.Helpers;
using Microsoft.Data.SqlClient;

namespace HotelManager.UI;

public partial class HlavniOkno : Form
{
    private DatabaseConnection _databaseConnection;
    private SqlConnection _connection;
    
    public HlavniOkno()
    {
        InitializeComponent();
        _databaseConnection = new DatabaseConnection();
    }
    
    private void HlavniOkno_Load(object sender, EventArgs e)
    {
        _connection = _databaseConnection.CreateConnection();
    }
    
    private void connect_Click(object sender, EventArgs e)
    {
        try
        {
            _connection.Open();
            MessageBox.Show("Připojení se zdařilo", "SUCCESS", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            // Enable the test form button after successful connection
            btnOpenTestForm.Enabled = true;
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    
    private void quit_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }
    
    private void btnOpenTestForm_Click(object sender, EventArgs e)
    {
        var testForm = new TestovaciOkno();
        testForm.Show();
    }
}