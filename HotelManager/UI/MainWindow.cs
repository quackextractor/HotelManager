using HotelManager.Data.Utility;

namespace HotelManager.UI;

public partial class MainWindow : Form
{
    private bool _isConnectionValid;

    public MainWindow()
    {
        InitializeComponent();
        FormBorderStyle = FormBorderStyle.FixedSingle;
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

        HideLoadingPanel();

        if (!_isConnectionValid)
            // Error was already shown in VerifyDatabaseConnectionAsync
            Application.Exit();
        else
        {
            // Show the menu if connection is ok
            panelButtons.Visible = true;
        }
    }

    /// <summary>
    ///     Checks the connection without freezing UI
    /// </summary>
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
                $"Chyba připojení k databázi: {ex.Message}\nAplikace se nyní ukončí.",
                "Chyba databáze",
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

    /// Opens addOrderForm
    private void buttonNewOrder_Click(object sender, EventArgs e)
    {
        using (var addOrderForm = new AddOrderForm())
        {
            addOrderForm.ShowDialog();
        }
    }

    /// Opens searchOrderForm
    private void buttonSearchOrder_Click(object sender, EventArgs e)
    {
        using (var searchOrderForm = new SearchOrderForm())
        {
            searchOrderForm.ShowDialog();
        }
    }

    /// Opens dataLoaderForm
    private async void buttonLoadTables_Click(object sender, EventArgs e)
    {
        using (var dataLoaderForm = new DataLoaderForm())
        {
            if (dataLoaderForm.ShowDialog() == DialogResult.OK)
                await CheckConnectionAndUpdateUIAsync();
        }
    }

    /// Exits the App
    private void buttonExit_Click(object sender, EventArgs e)
    {
        Application.Exit();
    }
}