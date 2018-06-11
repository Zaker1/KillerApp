using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twooter.Models;

namespace Twooter.ViewModels
{
    public class ReportViewModel
    {
        public Profiel Reported { get; set; }
        public string Bericht { get; set; }
        public ReportViewModel()
        {

        }
        public ReportViewModel(Profiel profiel)
        {
            this.Reported = profiel;
        }
    }
}
