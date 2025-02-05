namespace HotelManager.Domain;

public class Objednavka
{
    public int Id { get; set; }
    public decimal CenaZaNoc { get; set; }
    // 'CenaKZaplaceni' is a computed column in the database
    // keep it read-only
    public decimal CenaKZaplaceni { get; set; }
    public DateTime DatumVystaveni { get; set; }
    public DateTime DatumUbytovani { get; set; }
    public int PocetNoci { get; set; }
    public byte StatusId { get; set; }
    public bool Zaplaceno { get; set; }
}
