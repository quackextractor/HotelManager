using System.Globalization;
using HotelManager.Data.Implementations;
using HotelManager.Domain;

namespace HotelManager.UI;

/// <summary>
///     A form that allows users to search for orders.
/// </summary>
public partial class SearchOrderForm : Form
{
    /// <summary>
    ///     Initializes a new instance of the SearchOrderForm class.
    /// </summary>
    public SearchOrderForm()
    {
        InitializeComponent();
        FormBorderStyle = FormBorderStyle.FixedSingle;
        cmbSearchType.SelectedIndex = 0;
        cmbSearchType.SelectedIndexChanged += CmbSearchType_SelectedIndexChanged;
        dtpSearchDate.Visible = false;
        MaximizeBox = false;
        dgvOrders.CellFormatting += DgvOrders_CellFormatting;
    }

    /// <summary>
    ///     Handles the CellFormatting event for the DataGridView to ensure proper decimal display.
    /// </summary>
    private void DgvOrders_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.Value is double)
        {
            // Format double values using invariant culture (e.g., 6.81 instead of 6,81)
            e.Value = ((double)e.Value).ToString(CultureInfo.InvariantCulture);
            e.FormattingApplied = true;
        }
    }

    /// <summary>
    ///     Handles the click event for the search button.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event arguments.</param>
    private void btnSearch_Click(object sender, EventArgs e)
    {
        var searchType = cmbSearchType.SelectedItem?.ToString() ?? string.Empty;
        var orders = new List<Order>();
        var orderDao = new OrderDao();

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
            MessageBox.Show($"Chyba při vyhledávání: {ex.Message}", "Chyba", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return;
        }

        dgvOrders.DataSource = orders;
    }

    /// <summary>
    ///     Handles the SelectedIndexChanged event for the search type dropdown.
    /// </summary>
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

    /// <summary>
    ///     Handles the CellDoubleClick event for the orders DataGridView.
    /// </summary>
    private void dgvOrders_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
        {
            var selectedOrder = dgvOrders.Rows[e.RowIndex].DataBoundItem as Order;
            if (selectedOrder != null)
                using (var editForm = new EditOrderForm(selectedOrder.Id))
                {
                    editForm.ShowDialog();
                }
        }
    }
}