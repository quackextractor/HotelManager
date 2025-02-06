using HotelManager.Domain;

namespace HotelManager.Data.Interfaces;

using System.Collections.Generic;

public interface IObjednavkaDao {
    void Add(Objednavka objednavka);
    void Update(Objednavka objednavka);
    void Delete(int id);
    Objednavka GetById(int id);
    List<Objednavka> GetAll();
}
