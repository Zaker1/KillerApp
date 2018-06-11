using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twooter.Models;
using Twooter.Interfaces;
using Twooter.Context;
using System.Text.RegularExpressions;

namespace Twooter.Repos
{
    public class TijdlijnRepo
    {
        FilterRepo fRepo = new FilterRepo(new FilterMSSQLContext());
        private readonly ITijdlijnContext _context;
        public TijdlijnRepo(ITijdlijnContext context)
        {
            _context = context;
        }
        public List<Twoot> TwootsOphalen(Tijdlijn tijdlijn)
        {
            List<Twoot> uncensoredTwoots = _context.SelectTwoot(tijdlijn);

            return TwootsCensoren(uncensoredTwoots);
        }
        public List<Profiel> VriendenOphalen(Tijdlijn tijdlijn)
        {
            return _context.SelectVriend(tijdlijn);
        }

        private List<Twoot> TwootsCensoren(List<Twoot> uncensoredTwoots)
        {
            List<Twoot> censoredTwoots = new List<Twoot>();
            List<Filter> filters = fRepo.HaalFiltersOp();

            foreach (Twoot item in uncensoredTwoots)
            {
                foreach (Filter filter in filters)
                {
                    if (item.Bericht.Contains(filter.Woord))
                    {
                        string input = item.Bericht;
                        string pattern = String.Format(@"\b{0}\b", filter.Woord);
                        string replace = "******";
                        Regex rgx = new Regex(pattern);
                        string result = rgx.Replace(input, replace);
                        item.Bericht = result;
                    }
                }
                censoredTwoots.Add(item);
            }
            return censoredTwoots;
        }
    }
}
