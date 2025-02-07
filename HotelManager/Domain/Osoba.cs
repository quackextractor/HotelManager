using System;
using System.Collections.Generic;

namespace HotelManager.Domain
{
    public class Osoba
    {
        public int Id { get; set; }
        public string Jmeno { get; set; }
        public string Prijmeni { get; set; }
        public string Email { get; set; }
        public DateTime? NaposledyNavstiveno { get; set; }
        public DateTime DatumRegistrace { get; set; }
        public byte StatusId { get; set; }

        // Navigation properties:
        public List<Telefon> Telefony { get; set; } = new List<Telefon>();
        public List<Objednavka> Objednavkas { get; set; } = new List<Objednavka>();
    }
}