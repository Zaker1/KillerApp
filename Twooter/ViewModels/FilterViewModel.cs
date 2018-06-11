using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twooter.Models;

namespace Twooter.ViewModels
{
    public class FilterViewModel
    {
        public List<Filter> Filter { get; set; }
        public string FilterWoordAdd { get; set; }
        public string FilterWoordDelete { get; set; }
        public IEnumerable<int> FilterId { get; set; }
        public FilterViewModel(List<Filter> filters)
        {
            this.Filter = filters;
        }
        public FilterViewModel()
        {

        }
    }
}
