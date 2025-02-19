using HotelManager.Data.Implementations;
using HotelManager.Domain;

namespace HotelManager.UI;

/// <summary>
///     Form for adding new hotel rooms to the system
/// </summary>
public partial class AddRoomForm : Form
{
    public AddRoomForm()
    {
        InitializeComponent();
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
    }

    /// <summary>
    ///     Validates inputs and saves new room to database
    /// </summary>
    private void btnSaveRoom_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtRoomNumber.Text) ||
            string.IsNullOrWhiteSpace(txtRoomType.Text) ||
            !int.TryParse(txtCapacity.Text, out var capacity) ||
            !double.TryParse(txtPrice.Text, out var price))
        {
            MessageBox.Show("Prosím, zkontrolujte, zda jsou všechna pole vyplněna správně.");
            return;
        }

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
            roomDao.Insert(room);
            MessageBox.Show("Pokoj byl úspěšně přidán.");
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Chyba při ukládání pokoje: " + ex.Message);
        }
    }

    /// <summary>
    ///     Cancels room creation and closes form
    /// </summary>
    private void btnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}