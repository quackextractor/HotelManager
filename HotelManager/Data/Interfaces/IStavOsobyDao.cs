using HotelManager.Domain;

namespace HotelManager.Data.Interfaces;

using System.Collections.Generic;

public interface IStavOsobyDao {
    void Add(StavOsoby stavOsoby);
    void Update(StavOsoby stavOsoby);
    void Delete(byte id);
    StavOsoby GetById(byte id);
    List<StavOsoby> GetAll();
}
