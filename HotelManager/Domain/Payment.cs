namespace HotelManager.Domain;

public class Payment
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public double Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string PaymentMethod { get; set; }
    public string Note { get; set; }
}