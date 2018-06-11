using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Twooter.Klassen
{
    public static class Exceptions
    {
        public static Exception FoutQuery(Exception e)
        {
            return new Exception("Query fout", e);
        }
    }
}
