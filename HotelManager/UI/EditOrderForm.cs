using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using HotelManager.Data.Implementations;
using HotelManager.Domain;    // A odpovídající doménové třídy (Order, Person, …)

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
            LoadOrder(orderId);
        }

        private void LoadStatusDropdown()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("pending");
            cmbStatus.Items.Add("confirmed");
        }

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

                // Pokud objednávka obsahuje seznam osob, naplníme ListBox
                if (order.Persons != null)
                {
                    lstPersons.Items.Clear();
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

        // Uloží provedené změny do DB
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
            // Další úprava např. seznamu osob (pokud byly přidány/odebrány)

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

        // Přidání nové osoby (otevře AddPersonForm)
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

        // Odebrání vybrané osoby z ListBoxu
        private void btnRemovePerson_Click(object sender, EventArgs e)
        {
            if (lstPersons.SelectedIndex >= 0)
            {
                lstPersons.Items.RemoveAt(lstPersons.SelectedIndex);
            }
        }
    }
}
