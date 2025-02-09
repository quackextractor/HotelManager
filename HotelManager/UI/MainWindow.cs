using System;
using System.Windows.Forms;

namespace HotelManager.UI
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
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

        // Otevře formulář pro správu typů (např. StavOsoby, RoleVObjednavce, atd.)
        private void manageTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (TypeManagementForm typeManagementForm = new TypeManagementForm())
            {
                typeManagementForm.ShowDialog();
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
