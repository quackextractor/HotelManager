using HotelManager.Data.Implementations;
using HotelManager.Domain;

namespace HotelManager.UI
{
    public partial class SearchOrderForm : Form
    {
        public SearchOrderForm()
        {
            InitializeComponent();
            cmbSearchType.SelectedIndex = 0;
        }

        // Vyhledá objednávky podle zvoleného kritéria
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchType = cmbSearchType.SelectedItem.ToString();
            List<Order> orders = new List<Order>();
            OrderDao orderDao = new OrderDao();

            if (searchType == "Číslo objednávky")
            {
                orders = orderDao.SearchByOrderNumber(txtSearch.Text).ToList();
            }
            else if (searchType == "Jméno osoby")
            {
                orders = orderDao.SearchByPersonName(txtSearch.Text).ToList();
            }
            else if (searchType == "Datum")
            {
                if (DateTime.TryParse(txtSearch.Text, out DateTime date))
                {
                    orders = orderDao.SearchByDate(date).ToList();
                }
                else
                {
                    MessageBox.Show("Zadejte platné datum.");
                    return;
                }
            }
            dgvOrders.DataSource = orders;
        }
    }
}