using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twooter.Models;

namespace Twooter.ViewModels
{
    public class GetProfiel
    {
        public Profiel profiel { get; set; }
        public GetProfiel(Profiel profiel)
        {
            this.profiel = profiel;
        }
    }
}
