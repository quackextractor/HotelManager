using HotelManager.Data.Implementations;
using HotelManager.Domain;

namespace HotelManager.UI;

public partial class AddRoomForm : Form
{
    public AddRoomForm()
    {
        InitializeComponent();
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
    }

    // Uloží novou místnost do DB
    private void btnSaveRoom_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtRoomNumber.Text) ||
            string.IsNullOrWhiteSpace(txtRoomType.Text) ||
            !int.TryParse(txtCapacity.Text, out int capacity) ||
            !double.TryParse(txtPrice.Text, out double price))
        {
            MessageBox.Show("Zkontrolujte prosím vyplnění všech údajů.");
            return;
        }

        Room room = new Room
        {
            RoomNumber = txtRoomNumber.Text.Trim(),
            RoomType = txtRoomType.Text.Trim(),
            Capacity = capacity,
            Price = price
        };

        try
        {
            RoomDao roomDao = new RoomDao();
            roomDao.Insert(room);
            MessageBox.Show("Místnost byla úspěšně přidána.");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Chyba při ukládání místnosti: " + ex.Message);
        }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        this.DialogResult = DialogResult.Cancel;
        this.Close();
    }
}