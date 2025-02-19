using HotelManager.Data.Implementations;
using HotelManager.Domain;

namespace HotelManager.UI;

/// <summary>
/// Form for adding a new room to the database.
/// </summary>
public partial class AddRoomForm : Form
{
    /// <summary>
    /// Initializes the AddRoomForm.
    /// </summary>
    public AddRoomForm()
    {
        InitializeComponent();
        FormBorderStyle = FormBorderStyle.FixedSingle; 
        MaximizeBox = false;
    }

    /// <summary>
    /// Handles the click event for the "Save Room" button.
    /// Validates input fields and saves the new room to the database.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">Event arguments.</param>
    private void btnSaveRoom_Click(object sender, EventArgs e)
    {
        // Validate input fields
        if (string.IsNullOrWhiteSpace(txtRoomNumber.Text) ||
            string.IsNullOrWhiteSpace(txtRoomType.Text) ||
            !int.TryParse(txtCapacity.Text, out var capacity) ||
            !double.TryParse(txtPrice.Text, out var price))
        {
            MessageBox.Show("Zkontrolujte prosím vyplnění všech údajů.");
            return;
        }

        // Create a new Room object
        var room = new Room
        {
            RoomNumber = txtRoomNumber.Text.Trim(),
            RoomType = txtRoomType.Text.Trim(),
            Capacity = capacity,
            Price = price
        };

        try
        {
            var roomDao = new RoomDao();
            
            // Insert room into the database
            roomDao.Insert(room);

            MessageBox.Show("Místnost byla úspěšně přidána.");
            DialogResult = DialogResult.OK;
            
            // Close the form
            Close(); 
        }
        catch (Exception ex)
        {
            MessageBox.Show("Chyba při ukládání místnosti: " + ex.Message);
        }
    }

    /// <summary>
    /// Handles the click event for the "Cancel" button.
    /// Closes the form without saving.
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">Event arguments.</param>
    private void btnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}
