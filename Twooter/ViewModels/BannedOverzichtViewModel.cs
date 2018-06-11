using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twooter.Models;

namespace Twooter.ViewModels
{
    public class BannedOverzichtViewModel
    {
        public List<Profiel> BannedProfielen { get; set; }
        public BannedOverzichtViewModel()
        {

        }
        public BannedOverzichtViewModel(List<Profiel> profielen)
        {
            this.BannedProfielen = profielen;
        }
    }
}
