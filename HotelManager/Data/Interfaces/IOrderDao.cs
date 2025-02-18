using HotelManager.Domain;

namespace HotelManager.Data.Interfaces;

public interface IOrderDao
{
    Order GetById(int id);
    IEnumerable<Order> GetAll();
    void Insert(Order order);
    void Update(Order order);
    void Delete(int id);
    IEnumerable<Order> SearchByOrderNumber(string orderNumber);
    IEnumerable<Order> SearchByPersonName(string personName);
    IEnumerable<Order> SearchByDate(DateTime date);
}