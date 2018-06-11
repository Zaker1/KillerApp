using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twooter.Models;
using Twooter.Interfaces;
using Twooter.Context;

namespace Twooter.Repos
{
    public class TwootRepo
    {
        private readonly ITwootContext _context;
        public TwootRepo(ITwootContext context)
        {
            _context = context;
        }
        public void TwootRetwooten()
        {

        }
        public void TwootFavorieten(int twootID, int profielID)
        {
            _context.InsertFavorieteTwoot(twootID, profielID);
        }
        public void TwootPlaatsen(Twoot twoot)
        {
            if (String.IsNullOrEmpty(twoot.Bericht))
            {
                throw new ArgumentException("Bericht is null");
            }
            else if (twoot.Bericht.Length > 256)
            {
                string actualValue = twoot.Bericht.Substring(0, 256);
                throw new ArgumentOutOfRangeException("Bericht te lang");
            }
            else if (twoot.ProfielID == 0)
            {
                throw new ArgumentNullException("Geen profiel");
            }
            else
            {
                twoot.date = DateTime.UtcNow;
                _context.InsertTwoot(twoot);
            }
        }
        public void TwootUnfavorieten(int twootID, int profielID)
        {
            _context.DeleteFavorieteTwoot(twootID, profielID);
        }
    }
}
