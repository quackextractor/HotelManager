using HotelManager.Data.Implementations;
using HotelManager.Domain;

namespace HotelManager.UI;

public partial class AddRoomForm : Form
{
    public AddRoomForm()
    {
        InitializeComponent();
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
    }

    // Uloží novou místnost do DB
    private void btnSaveRoom_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtRoomNumber.Text) ||
            string.IsNullOrWhiteSpace(txtRoomType.Text) ||
            !int.TryParse(txtCapacity.Text, out var capacity) ||
            !double.TryParse(txtPrice.Text, out var price))
        {
            MessageBox.Show("Zkontrolujte prosím vyplnění všech údajů.");
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
            MessageBox.Show("Místnost byla úspěšně přidána.");
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Chyba při ukládání místnosti: " + ex.Message);
        }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}