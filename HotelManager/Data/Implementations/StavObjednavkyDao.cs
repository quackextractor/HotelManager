using HotelManager.Data.Helpers;
using HotelManager.Data.Interfaces;
using HotelManager.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HotelManager.Data.Implementations
{
    public class StavObjednavkyDao : IStavObjednavkyDao
    {
        private readonly DatabaseConnection _db;

        public StavObjednavkyDao(DatabaseConnection db)
        {
            _db = db;
        }

        public void Add(StavObjednavky stavObjednavky)
        {
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO StavObjednavky (nazev) VALUES (@nazev)";
                    cmd.Parameters.AddWithValue("@nazev", stavObjednavky.Nazev);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(StavObjednavky stavObjednavky)
        {
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "UPDATE StavObjednavky SET nazev = @nazev WHERE id = @id";
                    cmd.Parameters.AddWithValue("@nazev", stavObjednavky.Nazev);
                    cmd.Parameters.AddWithValue("@id", stavObjednavky.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(byte id)
        {
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM StavObjednavky WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public StavObjednavky GetById(byte id)
        {
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM StavObjednavky WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new StavObjednavky
                            {
                                Id = Convert.ToByte(reader["id"]),
                                Nazev = reader["nazev"].ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }

        public List<StavObjednavky> GetAll()
        {
            var list = new List<StavObjednavky>();
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM StavObjednavky";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new StavObjednavky
                            {
                                Id = Convert.ToByte(reader["id"]),
                                Nazev = reader["nazev"].ToString()
                            });
                        }
                    }
                }
            }
            return list;
        }
    }
}
