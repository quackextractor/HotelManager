using HotelManager.Data.Helpers;
using HotelManager.Data.Interfaces;
using HotelManager.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HotelManager.Data.Implementations
{
    public class ZpusobPlatbyDao : IZpusobPlatbyDao
    {
        private readonly DatabaseConnection _db;

        public ZpusobPlatbyDao(DatabaseConnection db)
        {
            _db = db;
        }

        public void Add(ZpusobPlatby zpusobPlatby)
        {
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO ZpusobPlatby (nazev) VALUES (@nazev)";
                    cmd.Parameters.AddWithValue("@nazev", zpusobPlatby.Nazev);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(ZpusobPlatby zpusobPlatby)
        {
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "UPDATE ZpusobPlatby SET nazev = @nazev WHERE id = @id";
                    cmd.Parameters.AddWithValue("@nazev", zpusobPlatby.Nazev);
                    cmd.Parameters.AddWithValue("@id", zpusobPlatby.Id);
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
                    cmd.CommandText = "DELETE FROM ZpusobPlatby WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public ZpusobPlatby GetById(byte id)
        {
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM ZpusobPlatby WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ZpusobPlatby
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

        public List<ZpusobPlatby> GetAll()
        {
            var list = new List<ZpusobPlatby>();
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM ZpusobPlatby";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new ZpusobPlatby
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
