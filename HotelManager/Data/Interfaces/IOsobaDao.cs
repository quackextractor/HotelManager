using HotelManager.Domain;

namespace HotelManager.Data.Interfaces;

public interface IOsobaDao {
    void Add(Osoba osoba);
    void Update(Osoba osoba);
    void Delete(int id);
    Osoba GetById(int id);
    List<Osoba> GetAll();
}