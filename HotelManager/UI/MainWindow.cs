using HotelManager.Data.Implementations;
using HotelManager.Data.Utility;

namespace HotelManager.UI;

public partial class MainWindow : Form
{
    private bool _isConnectionValid;

    public MainWindow()
    {
        InitializeComponent();
        FormBorderStyle = FormBorderStyle.FixedSingle;
        objednavkaToolStripMenuItem.Enabled = false;
        loadConfigToolStripMenuItem.Enabled = false;
        MaximizeBox = false;
        Shown += MainWindow_Shown;
    }

    private async void MainWindow_Shown(object? sender, EventArgs e)
    {
        await CheckConnectionAndUpdateUIAsync();
    }

    private async Task CheckConnectionAndUpdateUIAsync()
    {
        ShowLoadingPanel();
        await VerifyDatabaseConnectionAsync();
        UpdateMenuState();
        HideLoadingPanel();

        if (!_isConnectionValid)
            ShowErrorPanel();
        else
            errorPanel.Visible = false;
    }

    private async Task VerifyDatabaseConnectionAsync()
    {
        try
        {
            var connection = SqlConnectionSingleton.Instance.Connection;
            await connection.OpenAsync();
            connection.Close();
            _isConnectionValid = true;
        }
        catch (Exception ex)
        {
            _isConnectionValid = false;
            MessageBox.Show($"Database connection error: {ex.Message}\nPlease configure the connection.",
                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void UpdateMenuState()
    {
        objednavkaToolStripMenuItem.Enabled = _isConnectionValid;
        loadConfigToolStripMenuItem.Enabled = _isConnectionValid;
    }

    private void ShowLoadingPanel()
    {
        loadingPanel.Visible = true;
        loadingPanel.BringToFront();
        Application.DoEvents();
    }

    private void HideLoadingPanel()
    {
        loadingPanel.Visible = false;
    }

    private void ShowErrorPanel()
    {
        errorPanel.Visible = true;
        errorPanel.BringToFront();
    }

    private async void retryButton_Click(object sender, EventArgs e)
    {
        await CheckConnectionAndUpdateUIAsync();
    }

    private async void loadDataToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using (var dataLoaderForm = new DataLoaderForm())
        {
            if (dataLoaderForm.ShowDialog() == DialogResult.OK) await CheckConnectionAndUpdateUIAsync();
        }
    }

    private void addOrderToolStripMenuItem_Click(object sender, EventArgs e)
    {
        var roomDao = new RoomDao();
        var personDao = new PersonDao();
        var orderDao = new OrderDao();
        var orderRoleDao = new OrderRoleDao();
    
        using (var addOrderForm = new AddOrderForm(roomDao, personDao, orderDao, orderRoleDao))
        {
            addOrderForm.ShowDialog();
        }
    }

    private void searchOrderToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using (var searchOrderForm = new SearchOrderForm())
        {
            searchOrderForm.ShowDialog();
        }
    }
}
