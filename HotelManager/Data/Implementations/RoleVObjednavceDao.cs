using HotelManager.Data.Helpers;
using HotelManager.Data.Interfaces;
using HotelManager.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HotelManager.Data.Implementations
{
    public class RoleVObjednavceDao : IRoleVObjednavceDao
    {
        private readonly DatabaseConnection _db;

        public RoleVObjednavceDao(DatabaseConnection db)
        {
            _db = db;
        }

        public void Add(RoleVObjednavce roleVObjednavce)
        {
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO RoleVObjednavce (osoba_id, objednavka_id, typ_id) 
                        VALUES (@osoba_id, @objednavka_id, @typ_id)";
                    
                    cmd.Parameters.AddWithValue("@osoba_id", roleVObjednavce.OsobaId);
                    cmd.Parameters.AddWithValue("@objednavka_id", roleVObjednavce.ObjednavkaId);
                    cmd.Parameters.AddWithValue("@typ_id", roleVObjednavce.TypId);
                    
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(RoleVObjednavce roleVObjednavce)
        {
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE RoleVObjednavce 
                        SET osoba_id = @osoba_id, objednavka_id = @objednavka_id, typ_id = @typ_id 
                        WHERE id = @id";
                    
                    cmd.Parameters.AddWithValue("@osoba_id", roleVObjednavce.OsobaId);
                    cmd.Parameters.AddWithValue("@objednavka_id", roleVObjednavce.ObjednavkaId);
                    cmd.Parameters.AddWithValue("@typ_id", roleVObjednavce.TypId);
                    cmd.Parameters.AddWithValue("@id", roleVObjednavce.Id);
                    
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
                    cmd.CommandText = "DELETE FROM RoleVObjednavce WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public RoleVObjednavce GetById(int id)
        {
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM RoleVObjednavce WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new RoleVObjednavce
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                OsobaId = Convert.ToInt32(reader["osoba_id"]),
                                ObjednavkaId = Convert.ToInt32(reader["objednavka_id"]),
                                TypId = Convert.ToByte(reader["typ_id"])
                            };
                        }
                    }
                }
            }
            return null;
        }

        public List<RoleVObjednavce> GetAll()
        {
            var list = new List<RoleVObjednavce>();
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM RoleVObjednavce";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new RoleVObjednavce
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                OsobaId = Convert.ToInt32(reader["osoba_id"]),
                                ObjednavkaId = Convert.ToInt32(reader["objednavka_id"]),
                                TypId = Convert.ToByte(reader["typ_id"])
                            });
                        }
                    }
                }
            }
            return list;
        }
    }
}
