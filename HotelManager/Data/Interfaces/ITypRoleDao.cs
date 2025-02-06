using HotelManager.Domain;

namespace HotelManager.Data.Interfaces;


using System.Collections.Generic;

public interface ITypRoleDao {
    void Add(TypRole typRole);
    void Update(TypRole typRole);
    void Delete(byte id);
    TypRole GetById(byte id);
    List<TypRole> GetAll();
}