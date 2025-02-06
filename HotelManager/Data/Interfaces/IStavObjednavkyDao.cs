using HotelManager.Domain;

namespace HotelManager.Data.Interfaces;

using System.Collections.Generic;

public interface IStavObjednavkyDao {
    void Add(StavObjednavky stavObjednavky);
    void Update(StavObjednavky stavObjednavky);
    void Delete(byte id);
    StavObjednavky GetById(byte id);
    List<StavObjednavky> GetAll();
}
