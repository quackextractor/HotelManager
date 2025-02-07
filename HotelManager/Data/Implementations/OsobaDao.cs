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
                            return osoba;
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

        /// <summary>
        /// Searches for Osoba records by first or last name. 
        /// </summary>
        public List<Osoba> SearchByName(string searchTerm)
        {
            var result = new Dictionary<int, Osoba>();

            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT 
                            o.id, o.jmeno, o.prijmeni, o.email, o.naposledy_navstiveno, o.datum_registrace, o.status_id,
                            od.id AS objednavka_id, od.cena_za_noc, od.cena_k_zaplaceni, od.datum_vystaveni, 
                            od.datum_ubytovani, od.pocet_noci, od.status_id AS objednavka_status_id, od.zaplaceno
                        FROM Osoba o
                        LEFT JOIN RoleVObjednavce r ON o.id = r.osoba_id
                        LEFT JOIN Objednavka od ON r.objednavka_id = od.id
                        WHERE o.jmeno LIKE @search OR o.prijmeni LIKE @search";
                    
                    cmd.Parameters.AddWithValue("@search", "%" + searchTerm + "%");

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int osobaId = Convert.ToInt32(reader["id"]);
                            Osoba osoba;
                            if (!result.TryGetValue(osobaId, out osoba))
                            {
                                osoba = new Osoba
                                {
                                    Id = osobaId,
                                    Jmeno = reader["jmeno"].ToString(),
                                    Prijmeni = reader["prijmeni"].ToString(),
                                    Email = reader["email"].ToString(),
                                    NaposledyNavstiveno = reader["naposledy_navstiveno"] != DBNull.Value 
                                        ? (DateTime?)Convert.ToDateTime(reader["naposledy_navstiveno"]) 
                                        : null,
                                    DatumRegistrace = Convert.ToDateTime(reader["datum_registrace"]),
                                    StatusId = Convert.ToByte(reader["status_id"]),
                                    Objednavkas = new List<Objednavka>()
                                };
                                result.Add(osobaId, osoba);
                            }
                            
                            if (reader["objednavka_id"] != DBNull.Value)
                            {
                                var objednavka = new Objednavka
                                {
                                    Id = Convert.ToInt32(reader["objednavka_id"]),
                                    CenaZaNoc = Convert.ToDecimal(reader["cena_za_noc"]),
                                    CenaKZaplaceni = Convert.ToDecimal(reader["cena_k_zaplaceni"]),
                                    DatumVystaveni = Convert.ToDateTime(reader["datum_vystaveni"]),
                                    DatumUbytovani = Convert.ToDateTime(reader["datum_ubytovani"]),
                                    PocetNoci = Convert.ToInt32(reader["pocet_noci"]),
                                    StatusId = Convert.ToByte(reader["objednavka_status_id"]),
                                    Zaplaceno = Convert.ToBoolean(reader["zaplaceno"])
                                };
                                osoba.Objednavkas.Add(objednavka);
                            }
                        }
                    }
                }
            }
            return new List<Osoba>(result.Values);
        }
    }
}
