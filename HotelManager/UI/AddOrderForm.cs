using System.Text.RegularExpressions;
using HotelManager.Data.Implementations;
using HotelManager.Domain;

namespace HotelManager.UI;

/// <summary>
///     Form for creating new hotel orders with room assignments and guest management
/// </summary>
public partial class AddOrderForm : Form
{
    /// <summary>
    ///     Initializes form components and loads initial data
    /// </summary>
    public AddOrderForm()
    {
        InitializeComponent();
        FormBorderStyle = FormBorderStyle.FixedSingle;
        LoadStatusDropdown();
        LoadRoomDropdown();
        MaximizeBox = false;
    }

    /// <summary>
    ///     Populates status dropdown with default order states
    /// </summary>
    private void LoadStatusDropdown()
    {
        cmbStatus.Items.Clear();
        cmbStatus.Items.Add("pending");
        cmbStatus.Items.Add("confirmed");
        cmbStatus.SelectedIndex = 0;
    }

    /// <summary>
    ///     Loads available rooms from database into dropdown
    /// </summary>
    private void LoadRoomDropdown()
    {
        cmbRoom.Items.Clear();
        var roomDao = new RoomDao();
        var rooms = roomDao.GetAll();
        foreach (var room in rooms)
            cmbRoom.Items.Add(new ComboBoxItem(room.RoomNumber, room.Id));
        if (cmbRoom.Items.Count > 0)
            cmbRoom.SelectedIndex = 0;
    }

    // Event handlers below manage user interactions with form elements

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
                MessageBox.Show("Nový pokoj je nyní k dispozici.", "Informace", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
    }

    private void btnRemovePerson_Click(object sender, EventArgs e)
    {
        if (lstPersons.SelectedIndex >= 0)
            lstPersons.Items.RemoveAt(lstPersons.SelectedIndex);
    }

    /// <summary>
    ///     Validates and saves new order to database
    /// </summary>
    private void btnSaveOrder_Click(object sender, EventArgs e)
    {
        if (!Regex.IsMatch(txtPricePerNight.Text, @"^\d+(\.\d{1,2})?$"))
        {
            MessageBox.Show("Cena za noc musí být číslo s maximálně dvěma desetinnými místy.", "Chyba",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (dtpCheckinDate.Value.Date < DateTime.Today)
        {
            MessageBox.Show("Datum příjezdu musí být dnes nebo v budoucnu.", "Chyba", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return;
        }

        var orderRoleName = txtOrderRole.Text.Trim();
        if (string.IsNullOrEmpty(orderRoleName))
        {
            MessageBox.Show("Prosím, zadejte název role objednávky.", "Chyba", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return;
        }

        if (cmbStatus.SelectedItem == null)
        {
            MessageBox.Show("Prosím, vyberte stav objednávky z rozbalovací nabídky.", "Chyba", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return;
        }

        try
        {
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

            var personDao = new PersonDao();
            var orderDao = new OrderDao();
            var orderRoleDao = new OrderRoleDao();

            orderDao.Insert(order);

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

            MessageBox.Show("Objednávka byla úspěšně přidána.", "Informace", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Chyba při ukládání objednávky: " + ex.Message, "Chyba", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    /// <summary>
    ///     Helper class for storing room IDs with display text in dropdown
    /// </summary>
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