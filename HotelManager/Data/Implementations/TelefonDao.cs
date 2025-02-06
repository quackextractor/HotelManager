using HotelManager.Data.Helpers;
using HotelManager.Data.Interfaces;
using HotelManager.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HotelManager.Data.Implementations
{
    public class TelefonDao : ITelefonDao
    {
        private readonly DatabaseConnection _db;

        public TelefonDao(DatabaseConnection db)
        {
            _db = db;
        }

        public void Add(Telefon telefon)
        {
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO Telefon (osoba_id, cislo) VALUES (@osoba_id, @cislo)";
                    cmd.Parameters.AddWithValue("@osoba_id", telefon.OsobaId);
                    cmd.Parameters.AddWithValue("@cislo", telefon.Cislo);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Telefon telefon)
        {
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "UPDATE Telefon SET osoba_id = @osoba_id, cislo = @cislo WHERE id = @id";
                    cmd.Parameters.AddWithValue("@osoba_id", telefon.OsobaId);
                    cmd.Parameters.AddWithValue("@cislo", telefon.Cislo);
                    cmd.Parameters.AddWithValue("@id", telefon.Id);
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
                    cmd.CommandText = "DELETE FROM Telefon WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Telefon GetById(int id)
        {
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Telefon WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Telefon
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                OsobaId = Convert.ToInt32(reader["osoba_id"]),
                                Cislo = reader["cislo"].ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }

        public List<Telefon> GetAll()
        {
            var list = new List<Telefon>();
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Telefon";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Telefon
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                OsobaId = Convert.ToInt32(reader["osoba_id"]),
                                Cislo = reader["cislo"].ToString()
                            });
                        }
                    }
                }
            }
            return list;
        }
    }
}
