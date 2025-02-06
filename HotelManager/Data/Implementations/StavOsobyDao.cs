using HotelManager.Data.Helpers;
using HotelManager.Data.Interfaces;
using HotelManager.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HotelManager.Data.Implementations
{
    public class StavOsobyDao : IStavOsobyDao
    {
        private readonly DatabaseConnection _db;

        public StavOsobyDao(DatabaseConnection db)
        {
            _db = db;
        }

        public void Add(StavOsoby stavOsoby)
        {
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO StavOsoby (nazev) VALUES (@nazev)";
                    cmd.Parameters.AddWithValue("@nazev", stavOsoby.Nazev);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(StavOsoby stavOsoby)
        {
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "UPDATE StavOsoby SET nazev = @nazev WHERE id = @id";
                    cmd.Parameters.AddWithValue("@nazev", stavOsoby.Nazev);
                    cmd.Parameters.AddWithValue("@id", stavOsoby.Id);
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
                    cmd.CommandText = "DELETE FROM StavOsoby WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public StavOsoby GetById(byte id)
        {
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM StavOsoby WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new StavOsoby
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

        public List<StavOsoby> GetAll()
        {
            var list = new List<StavOsoby>();
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM StavOsoby";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new StavOsoby
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
