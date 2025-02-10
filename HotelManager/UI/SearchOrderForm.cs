using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HotelManager.Data;
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
            else if (searchType == "Číslo místnosti")
            {
                orders = orderDao.SearchByRoomNumber(txtSearch.Text).ToList();
            }
            dgvOrders.DataSource = orders;
        }

        // Dvojklik na řádku DataGridView – otevře formulář pro úpravu vybrané objednávky
        private void dgvOrders_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Order selectedOrder = dgvOrders.Rows[e.RowIndex].DataBoundItem as Order;
                if (selectedOrder != null)
                {
                    using (EditOrderForm editForm = new EditOrderForm(selectedOrder.Id))
                    {
                        editForm.ShowDialog();
                    }
                }
            }
        }
    }
}
