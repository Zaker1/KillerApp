using System;
using System.Collections.Generic;
using Twooter.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Twooter.Interfaces
{
    public interface IFilterContext
    {
        List<Filter> Select();
        void Insert(string filter);
        void Delete(string filter);
    }
}
