using HotelManager.Domain;

namespace HotelManager.Data.Interfaces
{
    public interface IPaymentDao
    {
        Payment GetById(int id);
        IEnumerable<Payment> GetAll();
        void Insert(Payment payment);
        void Update(Payment payment);
        void Delete(int id);
    }
}