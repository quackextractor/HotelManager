using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using HotelManager.Data.Implementations;
using HotelManager.Domain;

namespace HotelManager.UI
{
    public partial class EditOrderForm : Form
    {
        private Order order;

        // Konstruktor očekává ID objednávky, kterou chceme upravit
        public EditOrderForm(int orderId)
        {
            InitializeComponent();
            LoadStatusDropdown();
            LoadRoomDropdown();
            LoadOrder(orderId);
        }

        private void LoadStatusDropdown()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("pending");
            cmbStatus.Items.Add("confirmed");
        }

        // Načte dostupné místnosti a naplní ComboBox
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

        // Načte objednávku z DB a naplní ovládací prvky
        private void LoadOrder(int orderId)
        {
            OrderDao orderDao = new OrderDao();
            order = orderDao.GetById(orderId);
            if (order != null)
            {
                txtPricePerNight.Text = order.PricePerNight.ToString();
                txtNights.Text = order.Nights.ToString();
                dtpCheckinDate.Value = order.CheckinDate;
                cmbStatus.SelectedItem = order.Status;
                chkPaid.Checked = order.Paid;

                // Nastavení čísla místnosti podle RoomId
                if (order.RoomId.HasValue)
                {
                    foreach (ComboBoxItem item in cmbRoom.Items)
                    {
                        if (item.Value == order.RoomId.Value)
                        {
                            cmbRoom.SelectedItem = item;
                            break;
                        }
                    }
                }

                // Načtení osob do ListBoxu
                lstPersons.Items.Clear();
                if (order.Persons != null)
                {
                    foreach (Person person in order.Persons)
                    {
                        lstPersons.Items.Add(person);
                    }
                }
            }
            else
            {
                MessageBox.Show("Objednávka nebyla nalezena.");
                this.Close();
            }
        }

        // Uložení upravené objednávky
        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(txtPricePerNight.Text, @"^\d+(\.\d{1,2})?$"))
            {
                MessageBox.Show("Cena za noc musí být číslo s maximálně dvěma desetinnými místy.");
                return;
            }

            order.PricePerNight = double.Parse(txtPricePerNight.Text);
            order.Nights = int.Parse(txtNights.Text);
            order.CheckinDate = dtpCheckinDate.Value;
            order.Status = cmbStatus.SelectedItem.ToString();
            order.Paid = chkPaid.Checked;

            if (cmbRoom.SelectedItem != null)
            {
                ComboBoxItem selectedRoom = cmbRoom.SelectedItem as ComboBoxItem;
                order.RoomId = selectedRoom.Value;
            }

            // Aktualizace seznamu osob
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
                orderDao.Update(order);
                MessageBox.Show("Objednávka byla úspěšně upravena.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chyba při úpravě objednávky: " + ex.Message);
            }
        }

        // Přidání osoby – otevření formuláře AddPersonForm
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
    }
}
