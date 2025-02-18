using System.Xml.Serialization;

namespace HotelManager.Data
{
    [XmlRoot("Tableset")]
    public class TablesetXml
    {
        [XmlArray("Rooms")]
        [XmlArrayItem("Room")]
        public List<RoomXml> Rooms { get; set; }

        [XmlArray("Persons")]
        [XmlArrayItem("Person")]
        public List<PersonXml> Persons { get; set; }

        [XmlArray("Orders")]
        [XmlArrayItem("Order")]
        public List<OrderXml> Orders { get; set; }

        [XmlArray("OrderRoles")]
        [XmlArrayItem("OrderRole")]
        public List<OrderRoleXml> OrderRoles { get; set; }

        [XmlArray("Payments")]
        [XmlArrayItem("Payment")]
        public List<PaymentXml> Payments { get; set; }
    }

    [XmlType("Room")]
    public class RoomXml
    {
        [XmlAttribute("RoomNumber")]
        public string RoomNumber { get; set; }

        [XmlAttribute("RoomType")]
        public string RoomType { get; set; }

        [XmlAttribute("Capacity")]
        public int Capacity { get; set; }

        [XmlAttribute("Price")]
        public double Price { get; set; }
    }

    [XmlType("Person")]
    public class PersonXml
    {
        [XmlAttribute("FirstName")]
        public string FirstName { get; set; }

        [XmlAttribute("LastName")]
        public string LastName { get; set; }

        [XmlAttribute("Email")]
        public string Email { get; set; }

        [XmlAttribute("Phone")]
        public string Phone { get; set; }

        [XmlAttribute("Status")]
        public string Status { get; set; }

        [XmlAttribute("RegistrationDate")]
        public DateTime RegistrationDate { get; set; }

        [XmlAttribute("LastVisitDate")]
        public DateTime? LastVisitDate { get; set; }
    }

    [XmlType("Order")]
    public class OrderXml
    {
        [XmlAttribute("XmlId")]
        public string XmlId { get; set; }

        [XmlAttribute("RoomNumber")]
        public string RoomNumber { get; set; }

        [XmlAttribute("PricePerNight")]
        public double PricePerNight { get; set; }

        [XmlAttribute("Nights")]
        public int Nights { get; set; }

        [XmlAttribute("OrderDate")]
        public DateTime OrderDate { get; set; }

        [XmlAttribute("CheckinDate")]
        public DateTime CheckinDate { get; set; }

        [XmlAttribute("Status")]
        public string Status { get; set; }

        [XmlAttribute("Paid")]
        public bool Paid { get; set; }
    }

    [XmlType("OrderRole")]
    public class OrderRoleXml
    {
        [XmlAttribute("OrderXmlId")]
        public string OrderXmlId { get; set; }

        [XmlAttribute("PersonEmail")]
        public string PersonEmail { get; set; }

        [XmlAttribute("Role")]
        public string Role { get; set; }
    }

    [XmlType("Payment")]
    public class PaymentXml
    {
        [XmlAttribute("OrderXmlId")]
        public string OrderXmlId { get; set; }

        [XmlAttribute("Amount")]
        public double Amount { get; set; }

        [XmlAttribute("PaymentDate")]
        public DateTime PaymentDate { get; set; }

        [XmlAttribute("PaymentMethod")]
        public string PaymentMethod { get; set; }

        [XmlAttribute("Note")]
        public string Note { get; set; }
    }
}