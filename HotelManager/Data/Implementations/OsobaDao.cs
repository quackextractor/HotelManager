using HotelManager.Data.Helpers;
using HotelManager.Data.Interfaces;
using HotelManager.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HotelManager.Data.Implementations
{
    public class OsobaDao : IOsobaDao
    {
        private readonly DatabaseConnection _db;

        public OsobaDao(DatabaseConnection db)
        {
            _db = db;
        }

        public void Add(Osoba osoba)
        {
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Osoba (jmeno, prijmeni, email, naposledy_navstiveno, datum_registrace, status_id) 
                        VALUES (@jmeno, @prijmeni, @email, @naposledy_navstiveno, @datum_registrace, @status_id)";
                    
                    cmd.Parameters.AddWithValue("@jmeno", osoba.Jmeno);
                    cmd.Parameters.AddWithValue("@prijmeni", osoba.Prijmeni);
                    cmd.Parameters.AddWithValue("@email", osoba.Email);
                    cmd.Parameters.AddWithValue("@naposledy_navstiveno", 
                        osoba.NaposledyNavstiveno.HasValue ? (object)osoba.NaposledyNavstiveno.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@datum_registrace", osoba.DatumRegistrace);
                    cmd.Parameters.AddWithValue("@status_id", osoba.StatusId);
                    
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Osoba osoba)
        {
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Osoba 
                        SET jmeno = @jmeno, 
                            prijmeni = @prijmeni, 
                            email = @email, 
                            naposledy_navstiveno = @naposledy_navstiveno, 
                            datum_registrace = @datum_registrace, 
                            status_id = @status_id 
                        WHERE id = @id";

                    cmd.Parameters.AddWithValue("@jmeno", osoba.Jmeno);
                    cmd.Parameters.AddWithValue("@prijmeni", osoba.Prijmeni);
                    cmd.Parameters.AddWithValue("@email", osoba.Email);
                    cmd.Parameters.AddWithValue("@naposledy_navstiveno", 
                        osoba.NaposledyNavstiveno.HasValue ? (object)osoba.NaposledyNavstiveno.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("@datum_registrace", osoba.DatumRegistrace);
                    cmd.Parameters.AddWithValue("@status_id", osoba.StatusId);
                    cmd.Parameters.AddWithValue("@id", osoba.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Osoba WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Osoba GetById(int id)
        {
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Osoba WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Osoba
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Jmeno = reader["jmeno"].ToString(),
                                Prijmeni = reader["prijmeni"].ToString(),
                                Email = reader["email"].ToString(),
                                NaposledyNavstiveno = reader["naposledy_navstiveno"] != DBNull.Value 
                                    ? (DateTime?)Convert.ToDateTime(reader["naposledy_navstiveno"]) 
                                    : null,
                                DatumRegistrace = Convert.ToDateTime(reader["datum_registrace"]),
                                StatusId = Convert.ToByte(reader["status_id"])
                            };
                        }
                    }
                }
            }
            return null;
        }

        public List<Osoba> GetAll()
        {
            var list = new List<Osoba>();
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Osoba";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var osoba = new Osoba
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Jmeno = reader["jmeno"].ToString(),
                                Prijmeni = reader["prijmeni"].ToString(),
                                Email = reader["email"].ToString(),
                                NaposledyNavstiveno = reader["naposledy_navstiveno"] != DBNull.Value 
                                    ? (DateTime?)Convert.ToDateTime(reader["naposledy_navstiveno"]) 
                                    : null,
                                DatumRegistrace = Convert.ToDateTime(reader["datum_registrace"]),
                                StatusId = Convert.ToByte(reader["status_id"])
                            };
                            list.Add(osoba);
                        }
                    }
                }
            }
            return list;
        }
    }
}
