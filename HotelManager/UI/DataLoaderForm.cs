using System.Xml.Serialization;
using HotelManager.Data;
using HotelManager.Data.Implementations;
using HotelManager.Domain;

namespace HotelManager.UI;

public partial class DataLoaderForm : Form
{
    public DataLoaderForm()
    {
        InitializeComponent();
        FormBorderStyle = FormBorderStyle.FixedSingle;
        AllowDrop = true;
        DragEnter += DataLoaderForm_DragEnter;
        DragDrop += DataLoaderForm_DragDrop;
        MaximizeBox = false;
    }

    private void DataLoaderForm_DragEnter(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
            e.Effect = DragDropEffects.Copy;
    }

    private void DataLoaderForm_DragDrop(object sender, DragEventArgs e)
    {
        string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
        if (files.Length > 0) txtFilePath.Text = files[0];
    }

    private void btnBrowse_Click(object sender, EventArgs e)
    {
        var ofd = new OpenFileDialog();
        ofd.Filter = "Config files (*.config;*.xml)|*.config;*.xml|All files (*.*)|*.*";
        if (ofd.ShowDialog() == DialogResult.OK) txtFilePath.Text = ofd.FileName;
    }

    private void btnLoadConfig_Click(object sender, EventArgs e)
    {
        var filePath = txtFilePath.Text.Trim();
        if (!File.Exists(filePath))
        {
            MessageBox.Show("File not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        try
        {
            // Deserialize XML
            TablesetXml tableset;
            var serializer = new XmlSerializer(typeof(TablesetXml));
            using (var fs = new FileStream(filePath, FileMode.Open))
            {
                tableset = (TablesetXml)serializer.Deserialize(fs);
            }

            // Process Rooms
            var roomNumberToId = new Dictionary<string, int>();
            var roomDao = new RoomDao();
            foreach (var roomXml in tableset.Rooms)
            {
                var room = new Room
                {
                    RoomNumber = roomXml.RoomNumber,
                    RoomType = roomXml.RoomType,
                    Capacity = roomXml.Capacity,
                    Price = roomXml.Price
                };
                roomDao.Insert(room);
                roomNumberToId[roomXml.RoomNumber] = room.Id;
            }

            // Process Persons
            var emailToPersonId = new Dictionary<string, int>();
            var personDao = new PersonDao();
            foreach (var personXml in tableset.Persons)
            {
                var person = new Person
                {
                    FirstName = personXml.FirstName,
                    LastName = personXml.LastName,
                    Email = personXml.Email,
                    Phone = personXml.Phone,
                    Status = personXml.Status,
                    RegistrationDate = personXml.RegistrationDate,
                    LastVisitDate = personXml.LastVisitDate
                };
                personDao.Insert(person);
                emailToPersonId[personXml.Email] = person.Id;
            }

            // Process Orders
            var orderXmlIdToId = new Dictionary<string, int>();
            var orderDao = new OrderDao();
            foreach (var orderXml in tableset.Orders)
            {
                if (!roomNumberToId.TryGetValue(orderXml.RoomNumber, out var roomId))
                    throw new Exception($"Room number {orderXml.RoomNumber} not found for order {orderXml.XmlId}.");

                var order = new Order
                {
                    PricePerNight = orderXml.PricePerNight,
                    Nights = orderXml.Nights,
                    OrderDate = orderXml.OrderDate,
                    CheckinDate = orderXml.CheckinDate,
                    Status = orderXml.Status,
                    Paid = orderXml.Paid,
                    RoomId = roomId
                };
                orderDao.Insert(order);
                orderXmlIdToId[orderXml.XmlId] = order.Id;
            }

            // Process OrderRoles
            var orderRoleDao = new OrderRoleDao();
            foreach (var roleXml in tableset.OrderRoles)
            {
                if (!orderXmlIdToId.TryGetValue(roleXml.OrderXmlId, out var orderId))
                    throw new Exception($"Order XML ID {roleXml.OrderXmlId} not found.");

                if (!emailToPersonId.TryGetValue(roleXml.PersonEmail, out var personId))
                    throw new Exception($"Person email {roleXml.PersonEmail} not found.");

                var role = new OrderRole
                {
                    OrderId = orderId,
                    PersonId = personId,
                    Role = roleXml.Role
                };
                orderRoleDao.Insert(role);
            }

            // Process Payments
            var paymentDao = new PaymentDao();
            foreach (var paymentXml in tableset.Payments)
            {
                if (!orderXmlIdToId.TryGetValue(paymentXml.OrderXmlId, out var orderId))
                    throw new Exception($"Order XML ID {paymentXml.OrderXmlId} not found.");

                var payment = new Payment
                {
                    OrderId = orderId,
                    Amount = paymentXml.Amount,
                    PaymentDate = paymentXml.PaymentDate,
                    PaymentMethod = paymentXml.PaymentMethod,
                    Note = paymentXml.Note
                };
                paymentDao.Insert(payment);
            }

            MessageBox.Show("Data imported successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }
        catch (Exception ex)
        {
            var errorMessage = "An error occurred while importing data.\n";
            if (ex.Message.Contains("CK__Order_checkin"))
                errorMessage +=
                    "One or more orders have a check-in date set in the past. Please ensure that all check-in dates are today or in the future.";
            else if (ex.Message.Contains("UNIQUE"))
                errorMessage += "A duplicate entry was detected, violating the unique key constraint.";
            else
                errorMessage += ex.Message;
            MessageBox.Show(errorMessage, "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}