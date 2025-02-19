using System.Globalization;
using System.Text.RegularExpressions;
using HotelManager.Data.Implementations;
using HotelManager.Domain;

namespace HotelManager.UI;

/// <summary>
///     A form for editing an order.
/// </summary>
public partial class EditOrderForm : Form
{
    private Order _order;
    private bool _orderNotFound;

    /// <summary>
    ///     Initializes a new instance of the EditOrderForm class with the specified order ID.
    /// </summary>
    /// <param name="orderId">The ID of the order to edit.</param>
    public EditOrderForm(int orderId)
    {
        InitializeComponent();
        LoadStatusDropdown();
        LoadRoomDropdown();
        LoadOrder(orderId);
        MaximizeBox = false;
        FormBorderStyle = FormBorderStyle.FixedSingle;

        // Delay error handling until after the form is displayed.
        Shown += EditOrderForm_Shown;
    }

    /// <summary>
    ///     Handles the Shown event after the form is displayed.
    /// </summary>
    private void EditOrderForm_Shown(object sender, EventArgs e)
    {
        if (_orderNotFound)
        {
            MessageBox.Show("Objednávka nebyla nalezena.", "Chyba",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }

    private void LoadStatusDropdown()
    {
        cmbStatus.Items.Clear();
        cmbStatus.Items.Add("pending");
        cmbStatus.Items.Add("confirmed");
    }

    /// <summary>
    ///     Loads available rooms into the room dropdown.
    /// </summary>
    private void LoadRoomDropdown()
    {
        cmbRoom.Items.Clear();
        var roomDao = new RoomDao();
        var rooms = roomDao.GetAll();
        foreach (var room in rooms)
            cmbRoom.Items.Add(new AddOrderForm.ComboBoxItem(room.RoomNumber, room.Id));
        if (cmbRoom.Items.Count > 0)
            cmbRoom.SelectedIndex = 0;
    }

    /// <summary>
    ///     Loads the order details into the form using the specified order ID.
    /// </summary>
    /// <param name="orderId">The ID of the order to load.</param>
    private void LoadOrder(int orderId)
    {
        var orderDao = new OrderDao();
        _order = orderDao.GetById(orderId);
        if (_order == null)
        {
            _orderNotFound = true;
            return;
        }

        txtPricePerNight.Text = _order.PricePerNight.ToString(CultureInfo.InvariantCulture);
        txtNights.Text = _order.Nights.ToString();
        dtpCheckinDate.Value = _order.CheckinDate;
        cmbStatus.SelectedItem = _order.Status;
        chkPaid.Checked = _order.Paid;

        if (_order.RoomId.HasValue)
            foreach (AddOrderForm.ComboBoxItem item in cmbRoom.Items)
                if (item.Value == _order.RoomId.Value)
                {
                    cmbRoom.SelectedItem = item;
                    break;
                }

        lstPersons.Items.Clear();
        foreach (var person in _order.Persons)
            lstPersons.Items.Add(person);

        var orderRoleDao = new OrderRoleDao();
        var roles = orderRoleDao.GetByOrderId(_order.Id);
        txtOrderRole.Text = roles.Any() ? roles.First().Role : "customer";

        LoadPayments();
    }

    private void LoadPayments()
    {
        lstPayments.Items.Clear();
        var payments = new PaymentDao().GetByOrderId(_order.Id);
        lstPayments.DisplayMember = "PaymentInfo";
        foreach (var payment in payments)
            lstPayments.Items.Add(payment);
    }

    private void btnAddPayment_Click(object sender, EventArgs e)
    {
        using (var paymentForm = new PaymentForm(_order.Id))
        {
            if (paymentForm.ShowDialog() == DialogResult.OK)
                LoadPayments();
        }
    }

    private void btnRemovePayment_Click(object sender, EventArgs e)
    {
        if (lstPayments.SelectedItem is Payment selectedPayment)
        {
            var confirm = MessageBox.Show("Opravdu chcete smazat tuto platbu?",
                "Potvrzení", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm == DialogResult.Yes)
            {
                new PaymentDao().Delete(selectedPayment.Id);
                LoadPayments();
            }
        }
    }

    /// <summary>
    ///     Saves the changes made to the order.
    /// </summary>
    private void btnSaveChanges_Click(object sender, EventArgs e)
    {
        if (!ValidateInputs()) return;

        try
        {
            UpdateOrderDetails();
            SaveOrderChanges();
            MessageBox.Show("Objednávka byla úspěšně upravena.");
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Chyba při úpravě objednávky: " + ex.Message,
                "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private bool ValidateInputs()
    {
        if (!Regex.IsMatch(txtPricePerNight.Text, @"^\d+(\.\d{1,2})?$"))
        {
            ShowError("Cena za noc musí být číslo s maximálně dvěma desetinnými místy.");
            return false;
        }

        if (dtpCheckinDate.Value.Date < DateTime.Today)
        {
            ShowError("Datum příjezdu musí být dnešní nebo budoucí datum.");
            return false;
        }

        if (string.IsNullOrEmpty(txtOrderRole.Text.Trim()))
        {
            ShowError("Zadejte prosím název role objednávky.");
            return false;
        }

        if (cmbStatus.SelectedItem == null)
        {
            ShowError("Vyberte prosím status objednávky z dropdown nabídky.");
            return false;
        }

        return true;
    }

    private void ShowError(string message)
    {
        MessageBox.Show(message, "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    private void UpdateOrderDetails()
    {
        _order.PricePerNight = double.Parse(txtPricePerNight.Text, CultureInfo.InvariantCulture);
        _order.Nights = int.Parse(txtNights.Text);
        _order.CheckinDate = dtpCheckinDate.Value;
        _order.Status = cmbStatus.SelectedItem.ToString();
        _order.Paid = chkPaid.Checked;

        if (cmbRoom.SelectedItem != null)
        {
            var selectedRoom = cmbRoom.SelectedItem as AddOrderForm.ComboBoxItem;
            _order.RoomId = selectedRoom?.Value;
        }

        _order.Persons = new List<Person>();
        foreach (var item in lstPersons.Items)
            if (item is Person person)
                _order.Persons.Add(person);
    }

    private void SaveOrderChanges()
    {
        var orderDao = new OrderDao();
        orderDao.Update(_order);

        var orderRoleDao = new OrderRoleDao();
        orderRoleDao.DeleteByOrderId(_order.Id);

        var personDao = new PersonDao();
        foreach (var person in _order.Persons)
        {
            var existingPerson = personDao.GetByEmail(person.Email);
            person.Id = existingPerson.Id;

            orderRoleDao.Insert(new OrderRole
            {
                OrderId = _order.Id,
                PersonId = person.Id,
                Role = txtOrderRole.Text.Trim()
            });
        }
    }

    /// <summary>
    ///     Opens a dialog to add a new person to the order.
    /// </summary>
    private void btnAddPerson_Click(object sender, EventArgs e)
    {
        using (var addPersonForm = new AddPersonForm())
        {
            if (addPersonForm.ShowDialog() == DialogResult.OK) lstPersons.Items.Add(addPersonForm.Person);
        }
    }

    /// <summary>
    ///     Removes the selected person from the order.
    /// </summary>
    private void btnRemovePerson_Click(object sender, EventArgs e)
    {
        if (lstPersons.SelectedIndex >= 0)
            lstPersons.Items.RemoveAt(lstPersons.SelectedIndex);
    }

    /// <summary>
    ///     Deletes the order after user confirmation.
    /// </summary>
    private void btnDelete_Click(object sender, EventArgs e)
    {
        var confirmation = MessageBox.Show("Opravdu chcete smazat tuto objednávku?",
            "Potvrzení smazání",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);
        if (confirmation == DialogResult.Yes)
            try
            {
                new OrderDao().Delete(_order.Id);
                MessageBox.Show("Objednávka byla úspěšně smazána.");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chyba při mazání objednávky: " + ex.Message);
            }
    }
}