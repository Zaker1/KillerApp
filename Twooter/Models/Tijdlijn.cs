using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twooter.Models
{
    public class Tijdlijn
    {
        public Profiel Profiel { get; set; }
        public List<Profiel> Vrienden { get; set; }
        public List<Twoot> Twoots { get; set; }
        public Tijdlijn(Profiel profiel)
        {
            this.Profiel = profiel;
        }
    }
}
