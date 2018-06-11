using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twooter.Models;

namespace Twooter.ViewModels
{
    public class GetTijdlijn
    {
        public Tijdlijn Tijdlijn { get; set; }
        public GetTijdlijn(Tijdlijn tijdlijn)
        {
            this.Tijdlijn = tijdlijn;
        }

        public bool Volgend { get; set; }
        public GetTijdlijn(Tijdlijn tijdlijn, bool volgend)
        {
            this.Tijdlijn = tijdlijn;
            this.Volgend = volgend;
        }
    }
}
