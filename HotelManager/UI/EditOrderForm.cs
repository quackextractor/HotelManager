using System.Text.RegularExpressions;
using HotelManager.Data.Implementations;
using HotelManager.Domain;

namespace HotelManager.UI;

public partial class EditOrderForm : Form
{
    private Order order;

    public EditOrderForm(int orderId)
    {
        InitializeComponent();
        FormBorderStyle = FormBorderStyle.FixedSingle;
        LoadStatusDropdown();
        LoadRoomDropdown();
        LoadOrder(orderId);
        MaximizeBox = false;
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
        foreach (var room in rooms) cmbRoom.Items.Add(new AddOrderForm.ComboBoxItem(room.RoomNumber, room.Id));
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
        }
        else
        {
            MessageBox.Show("Objednávka nebyla nalezena.");
            Close();
        }
    }

    private void btnSaveChanges_Click(object sender, EventArgs e)
    {
        if (!Regex.IsMatch(txtPricePerNight.Text, @"^\d+(\.\d{1,2})?$"))
        {
            MessageBox.Show("Cena za noc musí být číslo s maximálně dvěma desetinnými místy.");
            return;
        }

        if (dtpCheckinDate.Value.Date < DateTime.Today)
        {
            MessageBox.Show("Datum příjezdu musí být dnešní nebo budoucí datum.");
            return;
        }

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

        try
        {
            var personDao = new PersonDao();
            var orderDao = new OrderDao();
            var orderRoleDao = new OrderRoleDao();

            // Aktualizace objednávky
            orderDao.Update(order);

            // Nejprve odstranit staré role
            orderRoleDao.DeleteByOrderId(order.Id);

            foreach (var person in order.Persons)
            {
                // Zkontrolovat, zda osoba už existuje v databázi
                var existingPerson = personDao.GetByEmail(person.Email);
                if (existingPerson != null)
                    person.Id = existingPerson.Id; // Použít existující ID
                else
                    personDao.Insert(person); // Pouze pokud neexistuje

                // Přidání nové role
                var role = new OrderRole
                {
                    OrderId = order.Id,
                    PersonId = person.Id,
                    Role = "customer"
                };
                orderRoleDao.Insert(role);
            }

            MessageBox.Show("Objednávka byla úspěšně upravena.");
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Chyba při úpravě objednávky: " + ex.Message);
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
}