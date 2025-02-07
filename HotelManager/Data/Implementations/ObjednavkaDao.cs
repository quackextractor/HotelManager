using HotelManager.Data.Helpers;
using HotelManager.Data.Interfaces;
using HotelManager.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HotelManager.Data.Implementations
{
    public class ObjednavkaDao : IObjednavkaDao
    {
        private readonly DatabaseConnection _db;

        public ObjednavkaDao(DatabaseConnection db)
        {
            _db = db;
        }

        public void Add(Objednavka objednavka)
        {
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Objednavka (cena_za_noc, datum_vystaveni, datum_ubytovani, pocet_noci, status_id, zaplaceno)
                        VALUES (@cena_za_noc, @datum_vystaveni, @datum_ubytovani, @pocet_noci, @status_id, @zaplaceno)";
                    
                    cmd.Parameters.AddWithValue("@cena_za_noc", objednavka.CenaZaNoc);
                    cmd.Parameters.AddWithValue("@datum_vystaveni", objednavka.DatumVystaveni);
                    cmd.Parameters.AddWithValue("@datum_ubytovani", objednavka.DatumUbytovani);
                    cmd.Parameters.AddWithValue("@pocet_noci", objednavka.PocetNoci);
                    cmd.Parameters.AddWithValue("@status_id", objednavka.StatusId);
                    cmd.Parameters.AddWithValue("@zaplaceno", objednavka.Zaplaceno);
                    
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Objednavka objednavka)
        {
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE Objednavka 
                        SET cena_za_noc = @cena_za_noc, 
                            datum_vystaveni = @datum_vystaveni, 
                            datum_ubytovani = @datum_ubytovani, 
                            pocet_noci = @pocet_noci, 
                            status_id = @status_id, 
                            zaplaceno = @zaplaceno 
                        WHERE id = @id";
                    
                    cmd.Parameters.AddWithValue("@cena_za_noc", objednavka.CenaZaNoc);
                    cmd.Parameters.AddWithValue("@datum_vystaveni", objednavka.DatumVystaveni);
                    cmd.Parameters.AddWithValue("@datum_ubytovani", objednavka.DatumUbytovani);
                    cmd.Parameters.AddWithValue("@pocet_noci", objednavka.PocetNoci);
                    cmd.Parameters.AddWithValue("@status_id", objednavka.StatusId);
                    cmd.Parameters.AddWithValue("@zaplaceno", objednavka.Zaplaceno);
                    cmd.Parameters.AddWithValue("@id", objednavka.Id);
                    
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
                    cmd.CommandText = "DELETE FROM Objednavka WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Objednavka GetById(int id)
        {
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Objednavka WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Objednavka
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                CenaZaNoc = Convert.ToDecimal(reader["cena_za_noc"]),
                                CenaKZaplaceni = Convert.ToDecimal(reader["cena_k_zaplaceni"]),
                                DatumVystaveni = Convert.ToDateTime(reader["datum_vystaveni"]),
                                DatumUbytovani = Convert.ToDateTime(reader["datum_ubytovani"]),
                                PocetNoci = Convert.ToInt32(reader["pocet_noci"]),
                                StatusId = Convert.ToByte(reader["status_id"]),
                                Zaplaceno = Convert.ToBoolean(reader["zaplaceno"])
                            };
                        }
                    }
                }
            }
            return null;
        }

        public List<Objednavka> GetAll()
        {
            var list = new List<Objednavka>();
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Objednavka";
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Objednavka
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                CenaZaNoc = Convert.ToDecimal(reader["cena_za_noc"]),
                                CenaKZaplaceni = Convert.ToDecimal(reader["cena_k_zaplaceni"]),
                                DatumVystaveni = Convert.ToDateTime(reader["datum_vystaveni"]),
                                DatumUbytovani = Convert.ToDateTime(reader["datum_ubytovani"]),
                                PocetNoci = Convert.ToInt32(reader["pocet_noci"]),
                                StatusId = Convert.ToByte(reader["status_id"]),
                                Zaplaceno = Convert.ToBoolean(reader["zaplaceno"])
                            });
                        }
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// Searches for Objednavka records by datum_ubytovani.
        /// </summary>
        public List<Objednavka> SearchByDatumUbytovani(DateTime datum)
        {
            var list = new List<Objednavka>();
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Objednavka WHERE datum_ubytovani = @datum_ubytovani";
                    cmd.Parameters.AddWithValue("@datum_ubytovani", datum);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Objednavka
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                CenaZaNoc = Convert.ToDecimal(reader["cena_za_noc"]),
                                CenaKZaplaceni = Convert.ToDecimal(reader["cena_k_zaplaceni"]),
                                DatumVystaveni = Convert.ToDateTime(reader["datum_vystaveni"]),
                                DatumUbytovani = Convert.ToDateTime(reader["datum_ubytovani"]),
                                PocetNoci = Convert.ToInt32(reader["pocet_noci"]),
                                StatusId = Convert.ToByte(reader["status_id"]),
                                Zaplaceno = Convert.ToBoolean(reader["zaplaceno"])
                            });
                        }
                    }
                }
            }
            return list;
        }
    }
}
