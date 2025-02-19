using System.ComponentModel;
using System.Text.RegularExpressions;
using HotelManager.Data.Implementations;
using HotelManager.Domain;

namespace HotelManager.UI;

/// <summary>
///     Form for adding new guests/persons to the system
/// </summary>
public partial class AddPersonForm : Form
{
    /// <summary>
    ///     Initializes form with status dropdown
    /// </summary>
    public AddPersonForm()
    {
        InitializeComponent();
        FormBorderStyle = FormBorderStyle.FixedSingle;
        LoadStatusDropdown();
        MaximizeBox = false;
    }

    /// <summary>
    ///     Contains the newly created person after successful submission
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Person Person { get; private set; }

    /// <summary>
    ///     Sets up status options in dropdown
    /// </summary>
    private void LoadStatusDropdown()
    {
        cmbStatus.Items.Clear();
        cmbStatus.Items.Add("active");
        cmbStatus.Items.Add("inactive");
        cmbStatus.SelectedIndex = 0;
    }

    /// <summary>
    ///     Validates and saves new person to database
    /// </summary>
    private void btnSave_Click(object sender, EventArgs e)
    {
        if (!Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            MessageBox.Show("Zadejte prosím platnou e-mailovou adresu.");
            return;
        }

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

    /// <summary>
    ///     Closes form without saving changes
    /// </summary>
    private void btnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}