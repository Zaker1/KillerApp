using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twooter.Models
{
    public class Profiel
    {
        public int ID { get; set; }
        public string Gebruikersnaam { get; set; }
        public string Beschrijving { get; set; }
        public byte[] Foto { get; set; }
        public Account Account { get; set; }
        public bool Volgend { get; set; }
        public List<Report> Reports { get; set; }
        public int AantalReports { get; set; }
    }
}
