using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using HotelManager.Data.Implementations;
using HotelManager.Domain; // Doménové třídy, např. Person
using System.ComponentModel;

namespace HotelManager.UI
{
    public partial class AddPersonForm : Form
    {
        // Po úspěšném uložení bude tato vlastnost obsahovat nově vytvořenou osobu
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Person Person { get; private set; }

        public AddPersonForm()
        {
            InitializeComponent();
            LoadStatusDropdown();
        }

        private void LoadStatusDropdown()
        {
            cmbStatus.Items.Clear();
            cmbStatus.Items.Add("active");
            cmbStatus.Items.Add("inactive");
            cmbStatus.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validace emailu pomocí regulárního výrazu
            if (!Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Zadejte platnou emailovou adresu.");
                return;
            }

            // Vytvoříme instanci třídy Person a naplníme ji daty z formuláře
            Person = new Person
            {
                FirstName = txtFirstName.Text.Trim(),
                LastName = txtLastName.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Status = cmbStatus.SelectedItem.ToString(),
                RegistrationDate = DateTime.Now
            };

            try
            {
                // Uložíme osobu do DB pomocí DAO
                PersonDao personDao = new PersonDao();
                personDao.Insert(Person);
                MessageBox.Show("Osoba byla úspěšně uložena.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Chyba při ukládání osoby: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
