using HotelManager.Domain;

namespace HotelManager.Data.Interfaces;

public interface IOrderRoleDao
{
    OrderRole GetById(int id);
    IEnumerable<OrderRole> GetAll();
    void Insert(OrderRole orderRole);
    void Update(OrderRole orderRole);
    void Delete(int id);
    void DeleteByOrderId(int orderId);
    IEnumerable<OrderRole> GetByOrderId(int orderId);
}