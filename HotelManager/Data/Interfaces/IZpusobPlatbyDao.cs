using HotelManager.Domain;

namespace HotelManager.Data.Interfaces;

using System.Collections.Generic;

public interface IZpusobPlatbyDao {
    void Add(ZpusobPlatby zpusobPlatby);
    void Update(ZpusobPlatby zpusobPlatby);
    void Delete(byte id);
    ZpusobPlatby GetById(byte id);
    List<ZpusobPlatby> GetAll();
}
