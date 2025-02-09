using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using HotelManager.Data.Implementations; // Předpokládáme, že zde máte implementaci DAO (např. OrderDao)
using HotelManager.Domain;    // A třídy domény (Order, Person, …)

namespace HotelManager.UI
{
    public partial class AddOrderForm : Form
    {
        public AddOrderForm()
        {
            InitializeComponent();
            LoadStatusDropdown();
        }

        /// <summary>
        /// Naplní rozbalovací seznam (ComboBox) pro výběr statusu objednávky.
        /// V praxi můžete získávat dostupné typy přímo z DB.
        /// </summary>
        private void LoadStatusDropdown()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("pending");
            cmbStatus.Items.Add("confirmed");
            cmbStatus.SelectedIndex = 0;
        }

        // Přidání osoby do objednávky – otevře další formulář pro zadání detailů osoby
        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            using (AddPersonForm addPersonForm = new AddPersonForm())
            {
                if (addPersonForm.ShowDialog() == DialogResult.OK)
                {
                    // Předpokládáme, že AddPersonForm vrací nově vytvořený objekt Person
                    Person newPerson = addPersonForm.Person;
                    lstPersons.Items.Add(newPerson);
                }
            }
        }

        // Odebrání vybrané osoby z listboxu
        private void btnRemovePerson_Click(object sender, EventArgs e)
        {
            if (lstPersons.SelectedIndex >= 0)
            {
                lstPersons.Items.RemoveAt(lstPersons.SelectedIndex);
            }
        }

        // Uloží objednávku do DB s validací vstupů (např. pomocí Regex)
        private void btnSaveOrder_Click(object sender, EventArgs e)
        {
            // Validace ceny za noc – příklad regulárního výrazu pro číslo s až 2 desetinnými místy
            if (!Regex.IsMatch(txtPricePerNight.Text, @"^\d+(\.\d{1,2})?$"))
            {
                MessageBox.Show("Cena za noc musí být číslo s maximálně dvěma desetinnými místy.");
                return;
            }

            // Vytvoříme objekt objednávky a naplníme data z formuláře
            Order order = new Order();
            order.PricePerNight = double.Parse(txtPricePerNight.Text);
            order.Nights = int.Parse(txtNights.Text);
            order.OrderDate = DateTime.Now;
            order.CheckinDate = dtpCheckinDate.Value;
            order.Status = cmbStatus.SelectedItem.ToString();
            order.Paid = chkPaid.Checked;
            // Seznam osob (order.Persons) můžete doplnit převodem položek z lstPersons

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
}
