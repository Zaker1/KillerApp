using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twooter.Models;

namespace Twooter.ViewModels
{
    public class ZoekViewModel
    {
        public List<Profiel> Profielen { get; set; }
        public string zoekTerm { get; set; }
        public ZoekViewModel(List<Profiel> profielen)
        {
            this.Profielen = profielen;
        }
        public ZoekViewModel()
        {

        }
    }
}
