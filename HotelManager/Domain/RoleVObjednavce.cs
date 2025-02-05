namespace HotelManager.Domain;

public class RoleVObjednavce
{
    public int Id { get; set; }
    public int OsobaId { get; set; }
    public int ObjednavkaId { get; set; }
    public byte TypId { get; set; }
}
