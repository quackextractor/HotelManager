namespace HotelManager.Models;

public class Platba
{
    public int Id { get; set; }
    public int ObjednavkaId { get; set; }
    public decimal Castka { get; set; }
    public DateTime DatumPlatby { get; set; }
    public byte ZpusobPlatbyId { get; set; }
    public string Poznamka { get; set; }
}