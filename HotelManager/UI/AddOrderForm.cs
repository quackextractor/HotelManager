using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using HotelManager.Data.Implementations;
using HotelManager.Domain;

namespace HotelManager.UI
{
    public partial class AddOrderForm : Form
    {
        public AddOrderForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
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
            RoomDao roomDao = new RoomDao();
            var rooms = roomDao.GetAll();
            foreach (var room in rooms)
            {
                cmbRoom.Items.Add(new ComboBoxItem(room.RoomNumber, room.Id));
            }
            if (cmbRoom.Items.Count > 0)
                cmbRoom.SelectedIndex = 0;
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            using (AddPersonForm addPersonForm = new AddPersonForm())
            {
                if (addPersonForm.ShowDialog() == DialogResult.OK)
                {
                    Person newPerson = addPersonForm.Person;
                    lstPersons.Items.Add(newPerson);
                }
            }
        }

        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            using (AddRoomForm addRoomForm = new AddRoomForm())
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

            Order order = new Order
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
                ComboBoxItem selectedRoom = cmbRoom.SelectedItem as ComboBoxItem;
                order.RoomId = selectedRoom.Value;
            }

            order.Persons = new System.Collections.Generic.List<Person>();
            foreach (var item in lstPersons.Items)
            {
                if (item is Person person)
                    order.Persons.Add(person);
            }

            try
            {
                PersonDao personDao = new PersonDao();
                OrderDao orderDao = new OrderDao();
                OrderRoleDao orderRoleDao = new OrderRoleDao();

                // Nejprve uložení objednávky
                orderDao.Insert(order);

                foreach (Person person in order.Persons)
                {
                    // Zkontrolovat, zda osoba už existuje v databázi
                    Person existingPerson = personDao.GetByEmail(person.Email);
                    if (existingPerson != null)
                    {
                        person.Id = existingPerson.Id; // Použít existující ID místo insertu
                    }
                    else
                    {
                        personDao.Insert(person); // Pouze pokud osoba neexistuje
                    }

                    // Přidání role do objednávky
                    OrderRole role = new OrderRole
                    {
                        OrderId = order.Id,
                        PersonId = person.Id,
                        Role = "customer"
                    };
                    orderRoleDao.Insert(role);
                }

                MessageBox.Show("Objednávka byla úspěšně přidána.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chyba při přidávání objednávky: " + ex.Message);
            }
        }

        public class ComboBoxItem
        {
            public string Text { get; set; }
            public int Value { get; set; }
            public ComboBoxItem(string text, int value)
            {
                Text = text;
                Value = value;
            }
            public override string ToString() => Text;
        }
    }
}
