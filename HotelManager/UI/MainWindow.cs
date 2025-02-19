using HotelManager.Data.Utility;

namespace HotelManager.UI
{
public partial class MainWindow : Form
{
private bool _isConnectionValid;

    public MainWindow()
    {
        InitializeComponent();

        // Set fixed window style and disable maximize button.
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;

        // Check the database connection when the main window is shown.
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

        HideLoadingPanel();

        if (!_isConnectionValid)
        {
            // The error message was already shown in VerifyDatabaseConnectionAsync.
            Application.Exit();
        }
        else
        {
            // Show the central panel with buttons after a successful connection.
            panelButtons.Visible = true;
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
            MessageBox.Show(
                $"Database connection error: {ex.Message}\nThe application will now close.",
                "Database Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
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

    /// Event handler for the "Nová Objednavka" button.
    private void buttonNewOrder_Click(object sender, EventArgs e)
    {
        using (var addOrderForm = new AddOrderForm())
        {
            addOrderForm.ShowDialog();
        }
    }

    /// Event handler for the "Vyhledat" button.
    private void buttonSearchOrder_Click(object sender, EventArgs e)
    {
        using (var searchOrderForm = new SearchOrderForm())
        {
            searchOrderForm.ShowDialog();
        }
    }

    /// Event handler for the "Načíst Tabulky" button.
    private async void buttonLoadTables_Click(object sender, EventArgs e)
    {
        using (var dataLoaderForm = new DataLoaderForm())
        {
            if (dataLoaderForm.ShowDialog() == DialogResult.OK)
                await CheckConnectionAndUpdateUIAsync();
        }
    }

    /// Event handler for the "Konec" button.
    private void buttonExit_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }
}

}