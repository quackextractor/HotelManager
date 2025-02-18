namespace HotelManager.Domain;

public class Order
{
    public int Id { get; set; }
    public double PricePerNight { get; set; }
    public int Nights { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime CheckinDate { get; set; }
    public string Status { get; set; }
    public bool Paid { get; set; }
    public double TotalPrice { get; set; } // Vypočteno v DB
    public int? RoomId { get; set; }

    // Navigation properties
    public List<Person> Persons { get; set; }
    public List<Payment> Payments { get; set; }
}