using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
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
            cmbSearchType.SelectedIndexChanged += CmbSearchType_SelectedIndexChanged;
            dtpSearchDate.Visible = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchType = cmbSearchType.SelectedItem?.ToString() ?? string.Empty;
            List<Order> orders = new List<Order>();
            OrderDao orderDao = new OrderDao();

            try
            {
                switch (searchType)
                {
                    case "Číslo objednávky":
                        orders = orderDao.SearchByOrderNumber(txtSearch.Text).ToList();
                        break;

                    case "Jméno osoby":
                        orders = orderDao.SearchByPersonName(txtSearch.Text).ToList();
                        break;

                    case "Datum":
                        orders = orderDao.SearchByDate(dtpSearchDate.Value).ToList();
                        break;

                    case "Číslo místnosti":
                        orders = orderDao.SearchByRoomNumber(txtSearch.Text).ToList();
                        break;

                    default:
                        MessageBox.Show("Neplatný typ vyhledávání.");
                        return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Chyba při vyhledávání: {ex.Message}", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            dgvOrders.DataSource = orders;
        }

        private void CmbSearchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSearchType.SelectedItem.ToString() == "Datum")
            {
                txtSearch.Visible = false;
                dtpSearchDate.Visible = true;
            }
            else
            {
                txtSearch.Visible = true;
                dtpSearchDate.Visible = false;
            }
        }

        private void dgvOrders_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                Order selectedOrder = dgvOrders.Rows[e.RowIndex].DataBoundItem as Order;
                if (selectedOrder != null)
                    using (EditOrderForm editForm = new EditOrderForm(selectedOrder.Id))
                    {
                        editForm.ShowDialog();
                    }
            }
        }
    }
}