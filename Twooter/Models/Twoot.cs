using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Twooter.Models
{
    public class Twoot
    {
        public Twoot()
        {

        }
        public int ID { get; set; }
        public string Bericht { get; set; }
        public int ProfielID { get; set; }
        public DateTime date { get; set; }
        public bool Favoriet { get; set; }
        public string naamPoster { get; set; }
        public string HtmlKlasse
        {
            get
            {
                if (Favoriet)
                {
                    return "Show";
                }
                else
                {
                    return "Hide";
                }
            }
        }
    }
}
