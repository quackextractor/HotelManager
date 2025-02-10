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

        // Načte dostupné místnosti z DB a naplní ComboBox
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

        // Přidání osoby – otevře formulář AddPersonForm
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

        // Odebrání vybrané osoby
        private void btnRemovePerson_Click(object sender, EventArgs e)
        {
            if (lstPersons.SelectedIndex >= 0)
            {
                lstPersons.Items.RemoveAt(lstPersons.SelectedIndex);
            }
        }

        // Uložení objednávky – včetně převodu vybraných osob a místnosti
        private void btnSaveOrder_Click(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(txtPricePerNight.Text, @"^\d+(\.\d{1,2})?$"))
            {
                MessageBox.Show("Cena za noc musí být číslo s maximálně dvěma desetinnými místy.");
                return;
            }

            Order order = new Order();
            order.PricePerNight = double.Parse(txtPricePerNight.Text);
            order.Nights = int.Parse(txtNights.Text);
            order.OrderDate = DateTime.Now;
            order.CheckinDate = dtpCheckinDate.Value;
            order.Status = cmbStatus.SelectedItem.ToString();
            order.Paid = chkPaid.Checked;

            // Získáme RoomId podle vybrané místnosti
            if (cmbRoom.SelectedItem != null)
            {
                ComboBoxItem selectedRoom = cmbRoom.SelectedItem as ComboBoxItem;
                order.RoomId = selectedRoom.Value;
            }

            // Převod osob z ListBoxu do seznamu
            order.Persons = new System.Collections.Generic.List<Person>();
            foreach (var item in lstPersons.Items)
            {
                if (item is Person person)
                {
                    order.Persons.Add(person);
                }
            }

            try
            {
                OrderDao orderDao = new OrderDao();
                orderDao.Insert(order);
                MessageBox.Show("Objednávka byla úspěšně přidána.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chyba při přidávání objednávky: " + ex.Message);
            }
        }
    }

    // Pomocná třída pro uložení textu a hodnoty v ComboBoxu
    public class ComboBoxItem
    {
        public string Text { get; set; }
        public int Value { get; set; }
        public ComboBoxItem(string text, int value)
        {
            Text = text;
            Value = value;
        }
        public override string ToString()
        {
            return Text;
        }
    }
}
