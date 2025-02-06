using HotelManager.Domain;

namespace HotelManager.Data.Interfaces;

using System.Collections.Generic;

public interface IPlatbaDao {
    void Add(Platba platba);
    void Update(Platba platba);
    void Delete(int id);
    Platba GetById(int id);
    List<Platba> GetAll();
}
