using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using HotelManager.Data;
using HotelManager.Data.Implementations;
using HotelManager.Domain;

namespace HotelManager.UI
{
    public partial class DataLoaderForm : Form
    {
        public DataLoaderForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.AllowDrop = true;
            this.DragEnter += DataLoaderForm_DragEnter;
            this.DragDrop += DataLoaderForm_DragDrop;
        }

        private void DataLoaderForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void DataLoaderForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0)
            {
                txtFilePath.Text = files[0];
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Config files (*.config;*.xml)|*.config;*.xml|All files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = ofd.FileName;
            }
        }

        private void btnLoadConfig_Click(object sender, EventArgs e)
{
    string filePath = txtFilePath.Text.Trim();
    if (!File.Exists(filePath))
    {
        MessageBox.Show("File not found.");
        return;
    }

    try
    {
        // Deserialize XML
        TablesetXml tableset;
        XmlSerializer serializer = new XmlSerializer(typeof(TablesetXml));
        using (FileStream fs = new FileStream(filePath, FileMode.Open))
        {
            tableset = (TablesetXml)serializer.Deserialize(fs);
        }

        // Process Rooms
        Dictionary<string, int> roomNumberToId = new Dictionary<string, int>();
        RoomDao roomDao = new RoomDao();
        foreach (var roomXml in tableset.Rooms)
        {
            Room room = new Room
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
        Dictionary<string, int> emailToPersonId = new Dictionary<string, int>();
        PersonDao personDao = new PersonDao();
        foreach (var personXml in tableset.Persons)
        {
            Person person = new Person
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
        Dictionary<string, int> orderXmlIdToId = new Dictionary<string, int>();
        OrderDao orderDao = new OrderDao();
        foreach (var orderXml in tableset.Orders)
        {
            if (!roomNumberToId.TryGetValue(orderXml.RoomNumber, out int roomId))
            {
                throw new Exception($"Room number {orderXml.RoomNumber} not found for order {orderXml.XmlId}.");
            }

            Order order = new Order
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
        OrderRoleDao orderRoleDao = new OrderRoleDao();
        foreach (var roleXml in tableset.OrderRoles)
        {
            if (!orderXmlIdToId.TryGetValue(roleXml.OrderXmlId, out int orderId))
            {
                throw new Exception($"Order XML ID {roleXml.OrderXmlId} not found.");
            }

            if (!emailToPersonId.TryGetValue(roleXml.PersonEmail, out int personId))
            {
                throw new Exception($"Person email {roleXml.PersonEmail} not found.");
            }

            OrderRole role = new OrderRole
            {
                OrderId = orderId,
                PersonId = personId,
                Role = roleXml.Role
            };
            orderRoleDao.Insert(role);
        }

        // Process Payments
        PaymentDao paymentDao = new PaymentDao();
        foreach (var paymentXml in tableset.Payments)
        {
            if (!orderXmlIdToId.TryGetValue(paymentXml.OrderXmlId, out int orderId))
            {
                throw new Exception($"Order XML ID {paymentXml.OrderXmlId} not found.");
            }

            Payment payment = new Payment
            {
                OrderId = orderId,
                Amount = paymentXml.Amount,
                PaymentDate = paymentXml.PaymentDate,
                PaymentMethod = paymentXml.PaymentMethod,
                Note = paymentXml.Note
            };
            paymentDao.Insert(payment);
        }

        MessageBox.Show("Data imported successfully!");
        this.Close();
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Error importing data: {ex.Message}");
    }
}
    }
}
