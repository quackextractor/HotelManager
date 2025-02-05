using HotelManager.Data.Helpers;
using HotelManager.Data.Interfaces;
using HotelManager.Domain;

namespace HotelManager.Data.Implementations;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class OsobaDao : IOsobaDao {
    private readonly DatabaseConnection _db;
    
    public OsobaDao(DatabaseConnection db) {
        _db = db;
    }
    
    public void Add(Osoba osoba) {
        using (var connection = _db.CreateConnection()) {
            connection.Open();
            using (var cmd = connection.CreateCommand()) {
                cmd.CommandText = @"
                    INSERT INTO Osoba (Jmeno, Prijmeni, Email, NaposledyNavstiveno, DatumRegistrace, StatusId) 
                    VALUES (@Jmeno, @Prijmeni, @Email, @NaposledyNavstiveno, @DatumRegistrace, @StatusId)";
                cmd.Parameters.AddWithValue("@Jmeno", osoba.Jmeno);
                cmd.Parameters.AddWithValue("@Prijmeni", osoba.Prijmeni);
                cmd.Parameters.AddWithValue("@Email", osoba.Email);
                cmd.Parameters.AddWithValue("@NaposledyNavstiveno", osoba.NaposledyNavstiveno.HasValue ? (object)osoba.NaposledyNavstiveno.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@DatumRegistrace", osoba.DatumRegistrace);
                cmd.Parameters.AddWithValue("@StatusId", osoba.StatusId);
                cmd.ExecuteNonQuery();
            }
        }
    }
    
    public void Update(Osoba osoba) {
        using (var connection = _db.CreateConnection()) {
            connection.Open();
            using (var cmd = connection.CreateCommand()) {
                cmd.CommandText = @"
                    UPDATE Osoba 
                    SET Jmeno = @Jmeno, Prijmeni = @Prijmeni, Email = @Email, NaposledyNavstiveno = @NaposledyNavstiveno, DatumRegistrace = @DatumRegistrace, StatusId = @StatusId 
                    WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Jmeno", osoba.Jmeno);
                cmd.Parameters.AddWithValue("@Prijmeni", osoba.Prijmeni);
                cmd.Parameters.AddWithValue("@Email", osoba.Email);
                cmd.Parameters.AddWithValue("@NaposledyNavstiveno", osoba.NaposledyNavstiveno.HasValue ? (object)osoba.NaposledyNavstiveno.Value : DBNull.Value);
                cmd.Parameters.AddWithValue("@DatumRegistrace", osoba.DatumRegistrace);
                cmd.Parameters.AddWithValue("@StatusId", osoba.StatusId);
                cmd.Parameters.AddWithValue("@Id", osoba.Id);
                cmd.ExecuteNonQuery();
            }
        }
    }
    
    public void Delete(int id) {
        using (var connection = _db.CreateConnection()) {
            connection.Open();
            using (var cmd = connection.CreateCommand()) {
                cmd.CommandText = "DELETE FROM Osoba WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
    
    public Osoba GetById(int id) {
        using (var connection = _db.CreateConnection()) {
            connection.Open();
            using (var cmd = connection.CreateCommand()) {
                cmd.CommandText = "SELECT * FROM Osoba WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", id);
                using (var reader = cmd.ExecuteReader()) {
                    if (reader.Read()) {
                        return new Osoba {
                            Id = Convert.ToInt32(reader["Id"]),
                            Jmeno = reader["Jmeno"].ToString(),
                            Prijmeni = reader["Prijmeni"].ToString(),
                            Email = reader["Email"].ToString(),
                            NaposledyNavstiveno = reader["NaposledyNavstiveno"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["NaposledyNavstiveno"]) : null,
                            DatumRegistrace = Convert.ToDateTime(reader["DatumRegistrace"]),
                            StatusId = Convert.ToByte(reader["StatusId"])
                        };
                    }
                }
            }
        }
        return null;
    }
    
    public List<Osoba> GetAll() {
        var list = new List<Osoba>();
        using (var connection = _db.CreateConnection()) {
            connection.Open();
            using (var cmd = connection.CreateCommand()) {
                cmd.CommandText = "SELECT * FROM Osoba";
                using (var reader = cmd.ExecuteReader()) {
                    while (reader.Read()) {
                        var osoba = new Osoba {
                            Id = Convert.ToInt32(reader["Id"]),
                            Jmeno = reader["Jmeno"].ToString(),
                            Prijmeni = reader["Prijmeni"].ToString(),
                            Email = reader["Email"].ToString(),
                            NaposledyNavstiveno = reader["NaposledyNavstiveno"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(reader["NaposledyNavstiveno"]) : null,
                            DatumRegistrace = Convert.ToDateTime(reader["DatumRegistrace"]),
                            StatusId = Convert.ToByte(reader["StatusId"])
                        };
                        list.Add(osoba);
                    }
                }
            }
        }
        return list;
    }
}