using HotelManager.Domain;

namespace HotelManager.Data.Interfaces
{
    public interface IRoomDao
    {
        Room GetById(int id);
        IEnumerable<Room> GetAll();
        void Insert(Room room);
        void Update(Room room);
        void Delete(int id);
    }
}