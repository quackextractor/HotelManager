using System;
using System.Windows.Forms;
using HotelManager.Data.Utility;

namespace HotelManager.UI
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();

            // Ověříme spojení s databází při spuštění hlavního okna
            try
            {
                var connection = SqlConnectionSingleton.Instance.Connection;
                connection.Open();
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chyba připojení k databázi: " + ex.Message, "Database Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        // Otevře formulář pro přidání nové objednávky
        private void addOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (AddOrderForm addOrderForm = new AddOrderForm())
            {
                addOrderForm.ShowDialog();
            }
        }

        // Otevře formulář pro editaci objednávky
        // Opraveno: předáváme do konstruktoru identifikátor objednávky (např. 1)
        private void editOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Poznámka: V praxi byste měli získat skutečné ID objednávky, kterou chcete upravit.
            int orderId = 1; // Ukázkové ID objednávky
            using (EditOrderForm editOrderForm = new EditOrderForm(orderId))
            {
                editOrderForm.ShowDialog();
            }
        }

        // Otevře formulář pro vyhledávání objednávek
        private void searchOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (SearchOrderForm searchOrderForm = new SearchOrderForm())
            {
                searchOrderForm.ShowDialog();
            }
        }

        // Otevře formulář pro načítání konfigurace
        private void loadConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ConfigLoaderForm configLoaderForm = new ConfigLoaderForm())
            {
                configLoaderForm.ShowDialog();
            }
        }
    }
}
