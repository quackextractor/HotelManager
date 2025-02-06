using HotelManager.Domain;

namespace HotelManager.Data.Interfaces;

using System.Collections.Generic;

public interface ITelefonDao {
    void Add(Telefon telefon);
    void Update(Telefon telefon);
    void Delete(int id);
    Telefon GetById(int id);
    List<Telefon> GetAll();
}
