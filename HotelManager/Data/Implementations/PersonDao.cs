using System.Data;
using HotelManager.Data.Interfaces;
using HotelManager.Data.Utility;
using HotelManager.Domain;
using Microsoft.Data.SqlClient;

namespace HotelManager.Data.Implementations;

public class PersonDao : IPersonDao
{
    private readonly SqlConnection _connection;

    public PersonDao()
    {
        _connection = SqlConnectionSingleton.Instance.Connection;
    }

    public Person GetById(int id)
    {
        Person person = null;
        const string sql = "SELECT * FROM Person WHERE id = @id";
        using (var cmd = new SqlCommand(sql, _connection))
        {
            cmd.Parameters.AddWithValue("@id", id);
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                    person = new Person
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        FirstName = reader["first_name"].ToString(),
                        LastName = reader["last_name"].ToString(),
                        Email = reader["email"].ToString(),
                        Phone = reader["phone"].ToString(),
                        Status = reader["status"].ToString(),
                        RegistrationDate = Convert.ToDateTime(reader["registration_date"]),
                        LastVisitDate = reader["last_visit_date"] != DBNull.Value
                            ? Convert.ToDateTime(reader["last_visit_date"])
                            : null
                    };
            }

            _connection.Close();
        }

        return person;
    }

    public IEnumerable<Person> GetAll()
    {
        var persons = new List<Person>();
        const string sql = "SELECT * FROM Person";
        using (var cmd = new SqlCommand(sql, _connection))
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var person = new Person
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        FirstName = reader["first_name"].ToString(),
                        LastName = reader["last_name"].ToString(),
                        Email = reader["email"].ToString(),
                        Phone = reader["phone"].ToString(),
                        Status = reader["status"].ToString(),
                        RegistrationDate = Convert.ToDateTime(reader["registration_date"]),
                        LastVisitDate = reader["last_visit_date"] != DBNull.Value
                            ? Convert.ToDateTime(reader["last_visit_date"])
                            : null
                    };
                    persons.Add(person);
                }
            }

            _connection.Close();
        }

        return persons;
    }

    public void Insert(Person person)
    {
        const string sql = @"INSERT INTO Person 
                    (first_name, last_name, email, phone, status, registration_date, last_visit_date) 
                    VALUES (@first_name, @last_name, @email, @phone, @status, @registration_date, @last_visit_date);
                    SELECT SCOPE_IDENTITY();";
        using (var cmd = new SqlCommand(sql, _connection))
        {
            cmd.Parameters.AddWithValue("@first_name", person.FirstName);
            cmd.Parameters.AddWithValue("@last_name", person.LastName);
            cmd.Parameters.AddWithValue("@email", person.Email);
            cmd.Parameters.AddWithValue("@phone", person.Phone ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@status", person.Status);
            cmd.Parameters.AddWithValue("@registration_date", person.RegistrationDate);
            cmd.Parameters.AddWithValue("@last_visit_date",
                person.LastVisitDate.HasValue ? person.LastVisitDate.Value : DBNull.Value);
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
            var result = cmd.ExecuteScalar();
            if (result != null) person.Id = Convert.ToInt32(result);
            _connection.Close();
        }
    }

    public void Update(Person person)
    {
        const string sql = @"UPDATE Person SET 
                               first_name = @first_name, 
                               last_name = @last_name, 
                               email = @email, 
                               phone = @phone, 
                               status = @status, 
                               registration_date = @registration_date, 
                               last_visit_date = @last_visit_date 
                               WHERE id = @id";
        using (var cmd = new SqlCommand(sql, _connection))
        {
            cmd.Parameters.AddWithValue("@first_name", person.FirstName);
            cmd.Parameters.AddWithValue("@last_name", person.LastName);
            cmd.Parameters.AddWithValue("@email", person.Email);
            cmd.Parameters.AddWithValue("@phone", person.Phone ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@status", person.Status);
            cmd.Parameters.AddWithValue("@registration_date", person.RegistrationDate);
            cmd.Parameters.AddWithValue("@last_visit_date",
                person.LastVisitDate.HasValue ? person.LastVisitDate.Value : DBNull.Value);
            cmd.Parameters.AddWithValue("@id", person.Id);
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
            cmd.ExecuteNonQuery();
            _connection.Close();
        }
    }

    public void Delete(int id)
    {
        const string sql = "DELETE FROM Person WHERE id = @id";
        using (var cmd = new SqlCommand(sql, _connection))
        {
            cmd.Parameters.AddWithValue("@id", id);
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
            cmd.ExecuteNonQuery();
            _connection.Close();
        }
    }

    public IEnumerable<Person> SearchByName(string name)
    {
        var persons = new List<Person>();
        const string sql = "SELECT * FROM Person WHERE first_name LIKE @name OR last_name LIKE @name";
        using (var cmd = new SqlCommand(sql, _connection))
        {
            cmd.Parameters.AddWithValue("@name", "%" + name + "%");
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var person = new Person
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        FirstName = reader["first_name"].ToString(),
                        LastName = reader["last_name"].ToString(),
                        Email = reader["email"].ToString(),
                        Phone = reader["phone"].ToString(),
                        Status = reader["status"].ToString(),
                        RegistrationDate = Convert.ToDateTime(reader["registration_date"]),
                        LastVisitDate = reader["last_visit_date"] != DBNull.Value
                            ? Convert.ToDateTime(reader["last_visit_date"])
                            : null
                    };
                    persons.Add(person);
                }
            }

            _connection.Close();
        }

        return persons;
    }

    public Person GetByEmail(string personEmail)
    {
        try
        {
            Person person = null;
            const string sql = "SELECT * FROM Person WHERE email = @Email";
            using (var cmd = new SqlCommand(sql, _connection))
            {
                cmd.Parameters.AddWithValue("@Email", personEmail);
                if (_connection.State != ConnectionState.Open)
                    _connection.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        person = new Person
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            FirstName = reader["first_name"].ToString(),
                            LastName = reader["last_name"].ToString(),
                            Email = reader["email"].ToString(),
                            Phone = reader["phone"].ToString(),
                            Status = reader["status"].ToString(),
                            RegistrationDate = Convert.ToDateTime(reader["registration_date"]),
                            LastVisitDate = reader["last_visit_date"] != DBNull.Value
                                ? Convert.ToDateTime(reader["last_visit_date"])
                                : null
                        };
                }
            }

            return person;
        }
        catch (Exception ex)
        {
            throw new Exception("Chyba při získávání osoby podle e-mailu: " + ex.Message, ex);
        }
        finally
        {
            if (_connection.State == ConnectionState.Open)
                _connection.Close();
        }
    }
}