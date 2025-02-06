using HotelManager.Domain;

namespace HotelManager.Data.Interfaces;

using System.Collections.Generic;

public interface IRoleVObjednavceDao {
    void Add(RoleVObjednavce roleVObjednavce);
    void Update(RoleVObjednavce roleVObjednavce);
    void Delete(int id);
    RoleVObjednavce GetById(int id);
    List<RoleVObjednavce> GetAll();
}
