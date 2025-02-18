using System.Data;
using HotelManager.Data.Interfaces;
using HotelManager.Data.Utility;
using HotelManager.Domain;
using Microsoft.Data.SqlClient;

namespace HotelManager.Data.Implementations;

public class PersonDao : IPersonDao
{
    private readonly SqlConnection connection;

    public PersonDao()
    {
        connection = SqlConnectionSingleton.Instance.Connection;
    }

    public Person GetById(int id)
    {
        Person person = null;
        var sql = "SELECT * FROM Person WHERE id = @id";
        using (var cmd = new SqlCommand(sql, connection))
        {
            cmd.Parameters.AddWithValue("@id", id);
            if (connection.State != ConnectionState.Open)
                connection.Open();
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

            connection.Close();
        }

        return person;
    }

    public IEnumerable<Person> GetAll()
    {
        List<Person> persons = new List<Person>();
        var sql = "SELECT * FROM Person";
        using (var cmd = new SqlCommand(sql, connection))
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
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

            connection.Close();
        }

        return persons;
    }

    public void Insert(Person person)
    {
        var sql = @"INSERT INTO Person 
                (first_name, last_name, email, phone, status, registration_date, last_visit_date) 
                VALUES (@first_name, @last_name, @email, @phone, @status, @registration_date, @last_visit_date);
                SELECT SCOPE_IDENTITY();";
        using (var cmd = new SqlCommand(sql, connection))
        {
            cmd.Parameters.AddWithValue("@first_name", person.FirstName);
            cmd.Parameters.AddWithValue("@last_name", person.LastName);
            cmd.Parameters.AddWithValue("@email", person.Email);
            cmd.Parameters.AddWithValue("@phone", person.Phone ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@status", person.Status);
            cmd.Parameters.AddWithValue("@registration_date", person.RegistrationDate);
            cmd.Parameters.AddWithValue("@last_visit_date", person.LastVisitDate.HasValue ? person.LastVisitDate.Value : (object)DBNull.Value);
            if (connection.State != ConnectionState.Open)
                connection.Open();
            object result = cmd.ExecuteScalar();
            if (result != null)
            {
                person.Id = Convert.ToInt32(result);
            }
            connection.Close();
        }
    }

    public void Update(Person person)
    {
        var sql = @"UPDATE Person SET 
                           first_name = @first_name, 
                           last_name = @last_name, 
                           email = @email, 
                           phone = @phone, 
                           status = @status, 
                           registration_date = @registration_date, 
                           last_visit_date = @last_visit_date 
                           WHERE id = @id";
        using (var cmd = new SqlCommand(sql, connection))
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
            if (connection.State != ConnectionState.Open)
                connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }

    public void Delete(int id)
    {
        var sql = "DELETE FROM Person WHERE id = @id";
        using (var cmd = new SqlCommand(sql, connection))
        {
            cmd.Parameters.AddWithValue("@id", id);
            if (connection.State != ConnectionState.Open)
                connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }

    public IEnumerable<Person> SearchByName(string name)
    {
        List<Person> persons = new List<Person>();
        var sql = "SELECT * FROM Person WHERE first_name LIKE @name OR last_name LIKE @name";
        using (var cmd = new SqlCommand(sql, connection))
        {
            cmd.Parameters.AddWithValue("@name", "%" + name + "%");
            if (connection.State != ConnectionState.Open)
                connection.Open();
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

            connection.Close();
        }

        return persons;
    }

    public Person GetByEmail(string personEmail)
    {
        try
        {
            Person person = null;
            var sql = "SELECT * FROM Person WHERE email = @Email";
            using (var cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.AddWithValue("@Email", personEmail);
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
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
            }
            return person;
        }
        catch (Exception ex)
        {
            throw new Exception("Chyba při získávání osoby podle e-mailu: " + ex.Message, ex);
        }
        finally
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
    }

}