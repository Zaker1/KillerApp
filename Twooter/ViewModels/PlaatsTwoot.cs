using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twooter.ViewModels
{
    public class PlaatsTwoot
    {
        public string bericht { get; set; }
        public int profielID { get; set; }
        public PlaatsTwoot()
        {

        }
        public PlaatsTwoot(int id)
        {
            profielID = id;
        }

    }
}
