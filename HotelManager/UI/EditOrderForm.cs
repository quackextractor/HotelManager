using System.Text.RegularExpressions;
using HotelManager.Data.Implementations;
using HotelManager.Domain;

namespace HotelManager.UI;

public partial class EditOrderForm : Form
{
    private bool _orderNotFound;
    private Order order;

    public EditOrderForm(int orderId)
    {
        InitializeComponent();
        FormBorderStyle = FormBorderStyle.FixedSingle;
        LoadStatusDropdown();
        LoadRoomDropdown();
        LoadOrder(orderId);
        MaximizeBox = false;

        // Delay error handling until after the form is displayed.
        Shown += EditOrderForm_Shown;
    }

    // This event fires after the form is displayed.
    private void EditOrderForm_Shown(object sender, EventArgs e)
    {
        if (_orderNotFound)
        {
            MessageBox.Show("Objednávka nebyla nalezena.", "Chyba",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            // Optionally set a DialogResult so the calling code can react.
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

    private void LoadOrder(int orderId)
    {
        var orderDao = new OrderDao();
        order = orderDao.GetById(orderId);
        if (order != null)
        {
            txtPricePerNight.Text = order.PricePerNight.ToString();
            txtNights.Text = order.Nights.ToString();
            dtpCheckinDate.Value = order.CheckinDate;
            cmbStatus.SelectedItem = order.Status;
            chkPaid.Checked = order.Paid;
            if (order.RoomId.HasValue)
                foreach (AddOrderForm.ComboBoxItem item in cmbRoom.Items)
                    if (item.Value == order.RoomId.Value)
                    {
                        cmbRoom.SelectedItem = item;
                        break;
                    }

            lstPersons.Items.Clear();
            if (order.Persons != null)
                foreach (var person in order.Persons)
                    lstPersons.Items.Add(person);

            var orderRoleDao = new OrderRoleDao();
            var roles = orderRoleDao.GetByOrderId(order.Id);
            txtOrderRole.Text = roles != null && roles.Any() ? roles.First().Role : "customer";
        }
        else
        {
            // Mark the error so that the Shown event can handle it.
            _orderNotFound = true;
        }
    }

    private void btnSaveChanges_Click(object sender, EventArgs e)
    {
        if (!Regex.IsMatch(txtPricePerNight.Text, @"^\d+(\.\d{1,2})?$"))
        {
            MessageBox.Show("Cena za noc musí být číslo s maximálně dvěma desetinnými místy.",
                "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (dtpCheckinDate.Value.Date < DateTime.Today)
        {
            MessageBox.Show("Datum příjezdu musí být dnešní nebo budoucí datum.",
                "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        var orderRoleName = txtOrderRole.Text.Trim();
        if (string.IsNullOrEmpty(orderRoleName))
        {
            MessageBox.Show("Zadejte prosím název role objednávky.",
                "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (cmbStatus.SelectedItem == null)
        {
            MessageBox.Show("Vyberte prosím status objednávky z dropdown nabídky.",
                "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        try
        {
            // Aktualizace vlastností objednávky.
            order.PricePerNight = double.Parse(txtPricePerNight.Text);
            order.Nights = int.Parse(txtNights.Text);
            order.CheckinDate = dtpCheckinDate.Value;
            order.Status = cmbStatus.SelectedItem.ToString();
            order.Paid = chkPaid.Checked;

            if (cmbRoom.SelectedItem != null)
            {
                var selectedRoom = cmbRoom.SelectedItem as AddOrderForm.ComboBoxItem;
                order.RoomId = selectedRoom?.Value;
            }

            order.Persons = new List<Person>();
            foreach (var item in lstPersons.Items)
                if (item is Person person)
                    order.Persons.Add(person);

            var personDao = new PersonDao();
            var orderDao = new OrderDao();
            var orderRoleDao = new OrderRoleDao();

            // Aktualizace objednávky.
            orderDao.Update(order);

            // Smazání starých rolí.
            orderRoleDao.DeleteByOrderId(order.Id);

            foreach (var person in order.Persons)
            {
                var existingPerson = personDao.GetByEmail(person.Email);
                if (existingPerson != null)
                    person.Id = existingPerson.Id;
                else
                    personDao.Insert(person);

                var role = new OrderRole
                {
                    OrderId = order.Id,
                    PersonId = person.Id,
                    Role = orderRoleName
                };
                orderRoleDao.Insert(role);
            }

            MessageBox.Show("Objednávka byla úspěšně upravena.");
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Chyba při úpravě objednávky: " + ex.Message,
                "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnAddPerson_Click(object sender, EventArgs e)
    {
        using (var addPersonForm = new AddPersonForm())
        {
            if (addPersonForm.ShowDialog() == DialogResult.OK)
            {
                var newPerson = addPersonForm.Person;
                lstPersons.Items.Add(newPerson);
            }
        }
    }

    private void btnRemovePerson_Click(object sender, EventArgs e)
    {
        if (lstPersons.SelectedIndex >= 0)
            lstPersons.Items.RemoveAt(lstPersons.SelectedIndex);
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        var confirmation = MessageBox.Show("Opravdu chcete smazat tuto objednávku?",
            "Potvrzení smazání",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);
        if (confirmation == DialogResult.Yes)
            try
            {
                var orderDao = new OrderDao();
                orderDao.Delete(order.Id);
                MessageBox.Show("Objednávka byla úspěšně smazána.");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chyba při mazání objednávky: " + ex.Message);
            }
    }
}