using HotelManager.Data.Implementations;
using HotelManager.Data.Helpers;
using HotelManager.Domain;

namespace HotelManager.UI;

public partial class TestovaciOkno : Form
{
    private readonly OsobaDao _osobaDao;
    private readonly ListBox _resultListBox;
    
    public TestovaciOkno()
    {
        InitializeComponent();
        _osobaDao = new OsobaDao(new DatabaseConnection());
        
        // Create and configure the ListBox for displaying results
        _resultListBox = new ListBox
        {
            Dock = DockStyle.Right,
            Width = 300
        };
        Controls.Add(_resultListBox);
        
        CreateTestButtons();
    }
    
    private void CreateTestButtons()
    {
        var btnAddTest = new Button
        {
            Text = "Test Add Person",
            Location = new Point(20, 20),
            Size = new Size(150, 40)
        };
        btnAddTest.Click += BtnAddTest_Click;
        
        var btnGetAll = new Button
        {
            Text = "Get All People",
            Location = new Point(20, 70),
            Size = new Size(150, 40)
        };
        btnGetAll.Click += BtnGetAll_Click;
        
        var btnGetById = new Button
        {
            Text = "Get Person by ID",
            Location = new Point(20, 120),
            Size = new Size(150, 40)
        };
        btnGetById.Click += BtnGetById_Click;
        
        var btnUpdate = new Button
        {
            Text = "Update Test Person",
            Location = new Point(20, 170),
            Size = new Size(150, 40)
        };
        btnUpdate.Click += BtnUpdate_Click;
        
        var btnDelete = new Button
        {
            Text = "Delete Person",
            Location = new Point(20, 220),
            Size = new Size(150, 40)
        };
        btnDelete.Click += BtnDelete_Click;
        
        Controls.AddRange(new Control[] { btnAddTest, btnGetAll, btnGetById, btnUpdate, btnDelete });
    }
    
    private void BtnAddTest_Click(object sender, EventArgs e)
    {
        try
        {
            var testOsoba = new Osoba
            {
                Jmeno = "Test",
                Prijmeni = "Person",
                Email = "test@example.com",
                DatumRegistrace = DateTime.Now,
                StatusId = 1
            };
            
            _osobaDao.Add(testOsoba);
            MessageBox.Show("Test person added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshPersonList();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error adding person: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    
    private void BtnGetAll_Click(object sender, EventArgs e)
    {
        try
        {
            RefreshPersonList();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error getting people: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    
    private void BtnGetById_Click(object sender, EventArgs e)
    {
        try
        {
            using (var inputForm = new Form())
            {
                var textBox = new TextBox { Location = new Point(10, 10) };
                var button = new Button { Text = "OK", Location = new Point(10, 40) };
                
                button.Click += (s, args) => inputForm.DialogResult = DialogResult.OK;
                inputForm.Controls.AddRange(new Control[] { textBox, button });
                
                if (inputForm.ShowDialog() == DialogResult.OK && int.TryParse(textBox.Text, out int id))
                {
                    var osoba = _osobaDao.GetById(id);
                    if (osoba != null)
                    {
                        _resultListBox.Items.Clear();
                        _resultListBox.Items.Add($"{osoba.Id}: {osoba.Jmeno} {osoba.Prijmeni}");
                    }
                    else
                    {
                        MessageBox.Show("Person not found!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error getting person: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    
    private void BtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            using (var inputForm = new Form())
            {
                var idBox = new TextBox { Location = new Point(10, 10), Text = "Enter ID" };
                var button = new Button { Text = "OK", Location = new Point(10, 40) };
                
                button.Click += (s, args) => inputForm.DialogResult = DialogResult.OK;
                inputForm.Controls.AddRange(new Control[] { idBox, button });
                
                if (inputForm.ShowDialog() == DialogResult.OK && int.TryParse(idBox.Text, out int id))
                {
                    var osoba = _osobaDao.GetById(id);
                    if (osoba != null)
                    {
                        osoba.Jmeno = "Updated";
                        osoba.Prijmeni = "Person";
                        osoba.NaposledyNavstiveno = DateTime.Now;
                        
                        _osobaDao.Update(osoba);
                        MessageBox.Show("Person updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RefreshPersonList();
                    }
                    else
                    {
                        MessageBox.Show("Person not found!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error updating person: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    
    private void BtnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            using (var inputForm = new Form())
            {
                var textBox = new TextBox { Location = new Point(10, 10) };
                var button = new Button { Text = "OK", Location = new Point(10, 40) };
                
                button.Click += (s, args) => inputForm.DialogResult = DialogResult.OK;
                inputForm.Controls.AddRange(new Control[] { textBox, button });
                
                if (inputForm.ShowDialog() == DialogResult.OK && int.TryParse(textBox.Text, out int id))
                {
                    _osobaDao.Delete(id);
                    MessageBox.Show("Person deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshPersonList();
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error deleting person: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    
    private void RefreshPersonList()
    {
        var people = _osobaDao.GetAll();
        _resultListBox.Items.Clear();
        foreach (var osoba in people)
        {
            _resultListBox.Items.Add($"{osoba.Id}: {osoba.Jmeno} {osoba.Prijmeni}");
        }
    }   
}