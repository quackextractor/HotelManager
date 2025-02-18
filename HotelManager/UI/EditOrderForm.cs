using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using HotelManager.Data.Interfaces;
using HotelManager.Domain;

namespace HotelManager.UI
{
    public partial class EditOrderForm : Form
    {
        private readonly IRoomDao _roomDao;
        private readonly IPersonDao _personDao;
        private readonly IOrderDao _orderDao;
        private readonly IOrderRoleDao _orderRoleDao;
        private Order order;
        private bool _orderNotFound = false;

        public EditOrderForm(
            int orderId,
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
            LoadOrder(orderId);

            // Delay error handling until after the form is displayed.
            this.Shown += EditOrderForm_Shown;
        }

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

        private void LoadRoomDropdown()
        {
            cmbRoom.Items.Clear();
            var rooms = _roomDao.GetAll();
            foreach (var room in rooms)
                cmbRoom.Items.Add(new AddOrderForm.ComboBoxItem(room.RoomNumber, room.Id));
            if (cmbRoom.Items.Count > 0)
                cmbRoom.SelectedIndex = 0;
        }

        private void LoadOrder(int orderId)
        {
            order = _orderDao.GetById(orderId);
            if (order != null)
            {
                txtPricePerNight.Text = order.PricePerNight.ToString();
                txtNights.Text = order.Nights.ToString();
                dtpCheckinDate.Value = order.CheckinDate;
                cmbStatus.SelectedItem = order.Status;
                chkPaid.Checked = order.Paid;
                if (order.RoomId.HasValue)
                {
                    foreach (AddOrderForm.ComboBoxItem item in cmbRoom.Items)
                    {
                        if (item.Value == order.RoomId.Value)
                        {
                            cmbRoom.SelectedItem = item;
                            break;
                        }
                    }
                }

                lstPersons.Items.Clear();
                if (order.Persons != null)
                {
                    foreach (var person in order.Persons)
                        lstPersons.Items.Add(person);
                }

                // Assuming GetByOrderId exists in IOrderRoleDao
                var roles = _orderRoleDao.GetByOrderId(order.Id);
                txtOrderRole.Text = (roles != null && roles.Any()) ? roles.First().Role : "customer";
            }
            else
            {
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
                {
                    if (item is Person person)
                        order.Persons.Add(person);
                }

                // Update order using the injected DAOs
                _orderDao.Update(order);

                // Delete old roles and insert updated ones
                _orderRoleDao.DeleteByOrderId(order.Id);

                foreach (var person in order.Persons)
                {
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
            using (var addPersonForm = new AddPersonForm(/* inject dependencies if needed */))
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
            {
                try
                {
                    _orderDao.Delete(order.Id);
                    MessageBox.Show("Objednávka byla úspěšně smazána.");
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Chyba při mazání objednávky: " + ex.Message);
                }
            }
        }
    }
}