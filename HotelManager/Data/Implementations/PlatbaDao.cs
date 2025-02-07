using HotelManager.Data.Helpers;
using HotelManager.Data.Interfaces;
using HotelManager.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HotelManager.Data.Implementations
{
    public class PlatbaDao : IPlatbaDao
    {
        private readonly DatabaseConnection _db;

        public PlatbaDao(DatabaseConnection db)
        {
            _db = db;
        }

        public void Add(Platba platba)
        {
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Platba (objednavka_id, castka, datum_platby, zpusob_platby_id, poznamka)
                        VALUES (@objednavka_id, @castka, @datum_platby, @zpusob_platby_id, @poznamka)";
                    
                    cmd.Parameters.AddWithValue("@objednavka_id", platba.ObjednavkaId);
                    cmd.Parameters.AddWithValue("@castka", platba.Castka);
                    cmd.Parameters.AddWithValue("@datum_platby", platba.DatumPlatby);
                    cmd.Parameters.AddWithValue("@zpusob_platby_id", platba.ZpusobPlatbyId);
                    cmd.Parameters.AddWithValue("@poznamka", platba.Poznamka);
                    
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Platba platba)
        {
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Platba 
                        SET objednavka_id = @objednavka_id, 
                            castka = @castka, 
                            datum_platby = @datum_platby, 
                            zpusob_platby_id = @zpusob_platby_id, 
                            poznamka = @poznamka 
                        WHERE id = @id";
                    
                    cmd.Parameters.AddWithValue("@objednavka_id", platba.ObjednavkaId);
                    cmd.Parameters.AddWithValue("@castka", platba.Castka);
                    cmd.Parameters.AddWithValue("@datum_platby", platba.DatumPlatby);
                    cmd.Parameters.AddWithValue("@zpusob_platby_id", platba.ZpusobPlatbyId);
                    cmd.Parameters.AddWithValue("@poznamka", platba.Poznamka);
                    cmd.Parameters.AddWithValue("@id", platba.Id);
                    
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
                    cmd.CommandText = "DELETE FROM Platba WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Platba GetById(int id)
        {
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Platba WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Platba
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                ObjednavkaId = Convert.ToInt32(reader["objednavka_id"]),
                                Castka = Convert.ToDecimal(reader["castka"]),
                                DatumPlatby = Convert.ToDateTime(reader["datum_platby"]),
                                ZpusobPlatbyId = Convert.ToByte(reader["zpusob_platby_id"]),
                                Poznamka = reader["poznamka"].ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }

        public List<Platba> GetAll()
        {
            var list = new List<Platba>();
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Platba";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Platba
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                ObjednavkaId = Convert.ToInt32(reader["objednavka_id"]),
                                Castka = Convert.ToDecimal(reader["castka"]),
                                DatumPlatby = Convert.ToDateTime(reader["datum_platby"]),
                                ZpusobPlatbyId = Convert.ToByte(reader["zpusob_platby_id"]),
                                Poznamka = reader["poznamka"].ToString()
                            });
                        }
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// Searches for payments by order.
        /// </summary>
        public List<Platba> SearchByObjednavkaId(int objednavkaId)
        {
            var list = new List<Platba>();
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Platba WHERE objednavka_id = @objednavka_id";
                    cmd.Parameters.AddWithValue("@objednavka_id", objednavkaId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Platba
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                ObjednavkaId = Convert.ToInt32(reader["objednavka_id"]),
                                Castka = Convert.ToDecimal(reader["castka"]),
                                DatumPlatby = Convert.ToDateTime(reader["datum_platby"]),
                                ZpusobPlatbyId = Convert.ToByte(reader["zpusob_platby_id"]),
                                Poznamka = reader["poznamka"].ToString()
                            });
                        }
                    }
                }
            }
            return list;
        }
    }
}
