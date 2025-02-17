using System;
using System.Windows.Forms;
using HotelManager.Data.Utility;

namespace HotelManager.UI
{
    public partial class MainWindow : Form
    {
        private bool _isConnectionValid;

        public MainWindow()
        {
            InitializeComponent();
            this.Shown += MainWindow_Shown;
        }

        
        private void MainWindow_Shown(object? sender, EventArgs e)
        {
            VerifyDatabaseConnection();
            UpdateMenuState();
        }

        private void VerifyDatabaseConnection()
        {
            try
            {
                var connection = SqlConnectionSingleton.Instance.Connection;
                connection.Open();
                connection.Close();
                _isConnectionValid = true;
            }
            catch (Exception ex)
            {
                _isConnectionValid = false;
                MessageBox.Show($"Chyba připojení k databázi: {ex.Message}\nProsím nakonfigurujte připojení.", 
                    "Database Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateMenuState()
        {
            objednavkaToolStripMenuItem.Enabled = _isConnectionValid;
            loadConfigToolStripMenuItem.Enabled = _isConnectionValid;
        }

        private void loadConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ConfigLoaderForm dataLoaderForm = new ConfigLoaderForm())
            {
                if (dataLoaderForm.ShowDialog() == DialogResult.OK)
                {
                    VerifyDatabaseConnection();
                    UpdateMenuState();
                }
            }
        }
        
        private void addOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AddOrderForm addOrderForm = new AddOrderForm())
            {
                addOrderForm.ShowDialog();
            }
        }

        private void editOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int orderId = 1;
            using (EditOrderForm editOrderForm = new EditOrderForm(orderId))
            {
                editOrderForm.ShowDialog();
            }
        }

        private void searchOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SearchOrderForm searchOrderForm = new SearchOrderForm())
            {
                searchOrderForm.ShowDialog();
            }
        }
    }
}