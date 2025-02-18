using System.ComponentModel;
using System.Text.RegularExpressions;
using HotelManager.Data.Implementations;
using HotelManager.Domain;

namespace HotelManager.UI;

public partial class AddPersonForm : Form
{
    public AddPersonForm()
    {
        InitializeComponent();
        FormBorderStyle = FormBorderStyle.FixedSingle;
        LoadStatusDropdown();
        MaximizeBox = false;
    }

    // Po úspěšném uložení bude tato vlastnost obsahovat nově vytvořenou osobu
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Person Person { get; private set; }

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
            var personDao = new PersonDao();
            personDao.Insert(Person);
            MessageBox.Show("Osoba byla úspěšně uložena.");
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Chyba při ukládání osoby: " + ex.Message);
        }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}