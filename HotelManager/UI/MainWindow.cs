using HotelManager.Data.Utility;

namespace HotelManager.UI
{
public partial class MainWindow : Form
{
private bool _isConnectionValid;
    public MainWindow()
    {
        InitializeComponent();

        // Set fixed window style and disable certain menu items until a valid connection exists.
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
        {
            // At this point the error message has already been shown in VerifyDatabaseConnectionAsync.
            // Exit the application rather than giving the user a retry option.
            Application.Exit();
        }
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
            MessageBox.Show($"Database connection error: {ex.Message}\nThe application will now close.",
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

    // Removed the ShowErrorPanel() and retryButton_Click() methods since the app now immediately exits on error.

    private async void loadDataToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using (var dataLoaderForm = new DataLoaderForm())
        {
            if (dataLoaderForm.ShowDialog() == DialogResult.OK)
                await CheckConnectionAndUpdateUIAsync();
        }
    }

    private void addOrderToolStripMenuItem_Click(object sender, EventArgs e)
    {
        using (var addOrderForm = new AddOrderForm())
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

}