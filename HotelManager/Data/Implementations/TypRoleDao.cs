using HotelManager.Data.Helpers;
using HotelManager.Data.Interfaces;
using HotelManager.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HotelManager.Data.Implementations
{
    public class TypRoleDao : ITypRoleDao
    {
        private readonly DatabaseConnection _db;

        public TypRoleDao(DatabaseConnection db)
        {
            _db = db;
        }

        public void Add(TypRole typRole)
        {
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO TypRole (nazev) VALUES (@nazev)";
                    cmd.Parameters.AddWithValue("@nazev", typRole.Nazev);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(TypRole typRole)
        {
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "UPDATE TypRole SET nazev = @nazev WHERE id = @id";
                    cmd.Parameters.AddWithValue("@nazev", typRole.Nazev);
                    cmd.Parameters.AddWithValue("@id", typRole.Id);
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
                    cmd.CommandText = "DELETE FROM TypRole WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public TypRole GetById(byte id)
        {
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM TypRole WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new TypRole
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

        public List<TypRole> GetAll()
        {
            var list = new List<TypRole>();
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM TypRole";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new TypRole
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
