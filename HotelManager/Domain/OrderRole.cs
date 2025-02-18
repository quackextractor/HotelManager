namespace HotelManager.Domain;

public class OrderRole
{
    public int Id { get; set; }
    public int PersonId { get; set; }
    public int OrderId { get; set; }
    public string Role { get; set; }
}