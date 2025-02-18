using System.Text.RegularExpressions;
using HotelManager.Data.Implementations;
using HotelManager.Domain;

namespace HotelManager.UI;

public partial class AddOrderForm : Form
{
    public AddOrderForm()
    {
        InitializeComponent();
        FormBorderStyle = FormBorderStyle.FixedSingle;
        LoadStatusDropdown();
        LoadRoomDropdown();
        MaximizeBox = false;
    }

    private void LoadStatusDropdown()
    {
        cmbStatus.Items.Clear();
        cmbStatus.Items.Add("pending");
        cmbStatus.Items.Add("confirmed");
        cmbStatus.SelectedIndex = 0;
    }

    private void LoadRoomDropdown()
    {
        cmbRoom.Items.Clear();
        var roomDao = new RoomDao();
        var rooms = roomDao.GetAll();
        foreach (var room in rooms) cmbRoom.Items.Add(new ComboBoxItem(room.RoomNumber, room.Id));
        if (cmbRoom.Items.Count > 0)
            cmbRoom.SelectedIndex = 0;
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

    private void btnAddRoom_Click(object sender, EventArgs e)
    {
        using (var addRoomForm = new AddRoomForm())
        {
            if (addRoomForm.ShowDialog() == DialogResult.OK)
            {
                LoadRoomDropdown();
                MessageBox.Show("Nová místnost je nyní k dispozici.");
            }
        }
    }

    private void btnRemovePerson_Click(object sender, EventArgs e)
    {
        if (lstPersons.SelectedIndex >= 0)
            lstPersons.Items.RemoveAt(lstPersons.SelectedIndex);
    }

    private void btnSaveOrder_Click(object sender, EventArgs e)
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

        // Validate that the user has entered a role name
        var orderRoleName = txtOrderRole.Text.Trim();
        if (string.IsNullOrEmpty(orderRoleName))
        {
            MessageBox.Show("Zadejte prosím název role objednávky.");
            return;
        }

        var order = new Order
        {
            PricePerNight = double.Parse(txtPricePerNight.Text),
            Nights = int.Parse(txtNights.Text),
            OrderDate = DateTime.Now,
            CheckinDate = dtpCheckinDate.Value,
            Status = cmbStatus.SelectedItem.ToString(),
            Paid = chkPaid.Checked
        };

        if (cmbRoom.SelectedItem != null)
        {
            var selectedRoom = cmbRoom.SelectedItem as ComboBoxItem;
            order.RoomId = selectedRoom.Value;
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

            // First, save the order
            orderDao.Insert(order);

            foreach (var person in order.Persons)
            {
                // Check if the person already exists in the database
                var existingPerson = personDao.GetByEmail(person.Email);
                if (existingPerson != null)
                    person.Id = existingPerson.Id; // Use existing ID
                else
                    personDao.Insert(person); // Insert if not existing

                // Now use the user-specified role name instead of "customer"
                var role = new OrderRole
                {
                    OrderId = order.Id,
                    PersonId = person.Id,
                    Role = orderRoleName
                };
                orderRoleDao.Insert(role);
            }

            MessageBox.Show("Objednávka byla úspěšně přidána.");
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Chyba při přidávání objednávky: " + ex.Message);
        }
    }


    public class ComboBoxItem
    {
        public ComboBoxItem(string text, int value)
        {
            Text = text;
            Value = value;
        }

        public string Text { get; set; }
        public int Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}