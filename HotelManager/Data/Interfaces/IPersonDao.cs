using HotelManager.Domain;

namespace HotelManager.Data.Interfaces;

public interface IPersonDao
{
    Person GetById(int id);
    IEnumerable<Person> GetAll();
    void Insert(Person person);
    void Update(Person person);
    void Delete(int id);
    IEnumerable<Person> SearchByName(string name);
    Person GetByEmail(string personEmail);
}