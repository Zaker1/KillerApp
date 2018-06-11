using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twooter.Models
{
    public class Report
    {
        public int Reporter_id { get; set; }
        public string ReporterNaam { get; set; }
        public int Reported_id { get; set; }
        public string Bericht { get; set; }
        public string Tijd { get; set; }
    }
}
