using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using HotelManager.Data.Interfaces;
using HotelManager.Domain;

namespace HotelManager.UI
{
    public partial class AddOrderForm : Form
    {
        private readonly IRoomDao _roomDao;
        private readonly IPersonDao _personDao;
        private readonly IOrderDao _orderDao;
        private readonly IOrderRoleDao _orderRoleDao;

        // Inject the DAO interfaces via the constructor
        public AddOrderForm(
            IRoomDao roomDao,
            IPersonDao personDao,
            IOrderDao orderDao,
            IOrderRoleDao orderRoleDao)
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            
            _roomDao = roomDao;
            _personDao = personDao;
            _orderDao = orderDao;
            _orderRoleDao = orderRoleDao;
            
            LoadStatusDropdown();
            LoadRoomDropdown();
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
            var rooms = _roomDao.GetAll();
            foreach (var room in rooms)
                cmbRoom.Items.Add(new ComboBoxItem(room.RoomNumber, room.Id));
            if (cmbRoom.Items.Count > 0)
                cmbRoom.SelectedIndex = 0;
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            using (var addPersonForm = new AddPersonForm(/* inject dependencies if needed */))
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
            using (var addRoomForm = new AddRoomForm(/* inject dependencies if needed */))
            {
                if (addRoomForm.ShowDialog() == DialogResult.OK)
                {
                    LoadRoomDropdown();
                    MessageBox.Show("Nová místnost je nyní k dispozici.", "Informace", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Cena za noc musí být číslo s maximálně dvěma desetinnými místy.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (dtpCheckinDate.Value.Date < DateTime.Today)
            {
                MessageBox.Show("Datum příjezdu musí být dnešní nebo budoucí datum.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var orderRoleName = txtOrderRole.Text.Trim();
            if (string.IsNullOrEmpty(orderRoleName))
            {
                MessageBox.Show("Zadejte prosím název role objednávky.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (cmbStatus.SelectedItem == null)
            {
                MessageBox.Show("Vyberte prosím status objednávky z dropdown nabídky.", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                {
                    if (item is Person person)
                        order.Persons.Add(person);
                }

                // Insert the order using the interface
                _orderDao.Insert(order);

                foreach (var person in order.Persons)
                {
                    // Assuming GetByEmail is implemented in your concrete PersonDao.
                    // If needed, add GetByEmail to your IPersonDao interface.
                    var existingPerson = _personDao.GetByEmail(person.Email);
                    if (existingPerson != null)
                        person.Id = existingPerson.Id;
                    else
                        _personDao.Insert(person);

                    var role = new OrderRole
                    {
                        OrderId = order.Id,
                        PersonId = person.Id,
                        Role = orderRoleName
                    };
                    _orderRoleDao.Insert(role);
                }

                MessageBox.Show("Objednávka byla úspěšně přidána.", "Informace", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chyba při přidávání objednávky: " + ex.Message, "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            public override string ToString() => Text;
        }
    }
}
