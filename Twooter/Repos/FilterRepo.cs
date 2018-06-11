using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Twooter.Interfaces;
using Twooter.Models;

namespace Twooter.Repos
{
    public class FilterRepo
    {
        private readonly IFilterContext _context;

        public FilterRepo(IFilterContext context)
        {
            _context = context;
        }
        public List<Filter> HaalFiltersOp()
        {
            return _context.Select();
        }
        public void MaakFilterAan(string filter)
        {
            _context.Insert(filter);
        }
        public void VerwijderFilter(string filter)
        {
            List<Filter> filters = _context.Select();

            Filter filtera = filters.FirstOrDefault(m => m.Woord == filter);

            if (filtera != null)
            {
                _context.Delete(filter);
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
