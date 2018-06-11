using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twooter.Models;

namespace Twooter.ViewModels
{
    public class BanViewModel
    {
        public Profiel Profiel { get; set; }
        public int ProfielId { get; set; }
        public BanViewModel()
        {

        }
        public BanViewModel(Profiel profiel)
        {
            this.Profiel = profiel;
        }
    }
}
